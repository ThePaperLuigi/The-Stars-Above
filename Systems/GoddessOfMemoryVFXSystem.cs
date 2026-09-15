using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using StarsAbove.Projectiles.Bosses.GoddessOfMemory;
using StarsAbove.Tiles.GoddessOfMemory;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryVFXSystem : ModSystem
    {
        private const int ArenaSearchRadius = 160;
        private readonly List<Point> arenaTiles = new List<Point>();
        private GoddessOfMemoryScreenShaderData shader;
        private Asset<Texture2D> emptyTexture;
        private bool active;
        private Point lastSearchPosition;
        private int arenaSearchTimer;
        private int lastArena;
        private Action<Main, bool> drawWaters;
        private bool waterDrawPatchReady;
        private bool foregroundWaterDrawn;

        public override void Load()
        {
            if (Main.dedServ)
            {
                return;
            }

            shader = new GoddessOfMemoryScreenShaderData(ModContent.Request<Effect>("StarsAbove/Effects/MiscEffectReverse", AssetRequestMode.ImmediateLoad), "ReversePass");
            emptyTexture = ModContent.Request<Texture2D>("StarsAbove/Projectiles/Empty", AssetRequestMode.ImmediateLoad);
            drawWaters = typeof(Main).GetMethod("DrawWaters", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new[] { typeof(bool) }, null)?.CreateDelegate<Action<Main, bool>>();
            if (drawWaters != null)
            {
                IL_Main.DoDraw += MoveForegroundWater;
            }
            else
            {
                Mod.Logger.Warn("Goddess of Memory foreground water ordering was not enabled: Main.DrawWaters(bool) was not found.");
            }

            On_Main.DoDraw += DrawFrame;
            On_Main.DoDraw_Tiles_Solid += DrawBackground;
        }

        private void MoveForegroundWater(ILContext il)
        {
            waterDrawPatchReady = false;
            ILCursor cursor = new ILCursor(il);
            if (!cursor.TryGotoNext(MoveType.After,
                instruction => instruction.MatchLdarg0(),
                instruction => instruction.MatchLdcI4(0),
                instruction => instruction.MatchCall<Main>("DrawWaters")))
            {
                Mod.Logger.Warn("Goddess of Memory foreground water ordering was not enabled: the direct foreground water draw was not found in Main.DoDraw.");
                return;
            }

            Instruction directWaterDraw = cursor.Prev;
            if (!cursor.TryGotoNext(MoveType.After,
                instruction => instruction.MatchLdsfld<Main>(nameof(Main.spriteBatch)),
                instruction => instruction.MatchLdsfld<Main>(nameof(Main.waterTarget)),
                instruction => instruction.MatchLdsfld<Main>(nameof(Main.sceneWaterPos)),
                instruction => instruction.MatchLdsfld<Main>(nameof(Main.screenPosition)),
                instruction => instruction.MatchCall<Vector2>("op_Subtraction"),
                instruction => instruction.MatchCall<Color>("get_White"),
                instruction => instruction.MatchCallvirt<SpriteBatch>(nameof(SpriteBatch.Draw))))
            {
                Mod.Logger.Warn("Goddess of Memory foreground water ordering was not enabled: the cached foreground water draw was not found in Main.DoDraw.");
                return;
            }

            Instruction cachedWaterDraw = cursor.Prev;
            cursor.Goto(cachedWaterDraw);
            cursor.Remove();
            cursor.EmitDelegate<Action<SpriteBatch, Texture2D, Vector2, Color>>(DrawLateForegroundWaterTarget);
            cursor.Goto(directWaterDraw);
            cursor.Remove();
            cursor.EmitDelegate<Action<Main, bool>>(DrawLateForegroundWater);
            waterDrawPatchReady = true;
        }

        private void DrawLateForegroundWater(Main self, bool isBackground)
        {
            if (!foregroundWaterDrawn)
            {
                drawWaters(self, isBackground);
            }
        }

        private void DrawLateForegroundWaterTarget(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color)
        {
            if (!foregroundWaterDrawn)
            {
                spriteBatch.Draw(texture, position, color);
            }
        }

        private void DrawFrame(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            foregroundWaterDrawn = false;
            try
            {
                orig(self, gameTime);
            }
            finally
            {
                foregroundWaterDrawn = false;
            }
        }

        private void DrawForegroundWater(Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            try
            {
                if (Main.drawToScreen)
                {
                    drawWaters(self, false);
                }
                else
                {
                    Main.spriteBatch.Draw(Main.waterTarget, Main.sceneWaterPos - Main.screenPosition, Color.White);
                }
            }
            finally
            {
                Main.spriteBatch.End();
            }

            foregroundWaterDrawn = true;
        }

        private void DrawBackground(On_Main.orig_DoDraw_Tiles_Solid orig, Main self)
        {
            orig(self);

            if (active && !Main.gameMenu)
            {
                if (waterDrawPatchReady && !foregroundWaterDrawn)
                {
                    DrawForegroundWater(self);
                }

                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Matrix.Identity);

                try
                {
                    Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
                    shader.Apply();
                    Main.spriteBatch.Draw(emptyTexture.Value, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
                }
                finally
                {
                    Main.spriteBatch.End();
                }
            }
        }

        public override void PostUpdateProjectiles()
        {
            if (Main.dedServ)
            {
                return;
            }

            Projectile selectedProjectile = null;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (!projectile.active || projectile.ModProjectile is not GoddessOfMemoryArenaPhaseFinal)
                {
                    continue;
                }

                float distance = Vector2.DistanceSquared(Main.LocalPlayer.Center, projectile.Center);
                if (distance < closestDistance)
                {
                    selectedProjectile = projectile;
                    closestDistance = distance;
                }
            }

            if (selectedProjectile != null)
            {
                shader.UseProjectile(selectedProjectile);
            }
            else if (TryGetArenaTile(out Point tilePosition, out GoddessOfMemoryArenaTile arenaTile))
            {
                shader.UseTile(tilePosition, arenaTile);
            }
            else
            {
                active = false;
                shader.UseOpacity(0f);
                return;
            }

            active = true;
            shader.UseGlobalOpacity(1f);
        }

        private bool TryGetArenaTile(out Point position, out GoddessOfMemoryArenaTile arenaTile)
        {
            bool inArena = GoddessOfMemoryArenaSystem.IsActive;
            int arena = inArena ? GoddessOfMemoryArenaSystem.ActiveArena + (GoddessOfMemoryArenaSystem.ActiveIsPhase ? 6 : 0) : 0;
            Vector2 anchor = inArena ? GoddessOfMemoryArenaSystem.ActiveCenter : Main.LocalPlayer.Center;
            Point searchPosition = anchor.ToTileCoordinates();
            int markerType = inArena && GoddessOfMemoryArenaSystem.ActiveIsPhase
                ? GoddessOfMemoryArenaSystem.ActiveArena switch
                {
                    1 => ModContent.TileType<GoddessOfMemoryArenaPhase1Tile>(),
                    2 => ModContent.TileType<GoddessOfMemoryArenaPhase2Tile>(),
                    3 => ModContent.TileType<GoddessOfMemoryArenaPhase3Tile>(),
                    4 => ModContent.TileType<GoddessOfMemoryArenaPhase4Tile>(),
                    _ => ModContent.TileType<GoddessOfMemoryArenaIntermissionTile>()
                }
                : ModContent.TileType<GoddessOfMemoryArenaIntermissionTile>();

            if (--arenaSearchTimer <= 0 || arena != lastArena
                || Math.Abs(searchPosition.X - lastSearchPosition.X) > 8 || Math.Abs(searchPosition.Y - lastSearchPosition.Y) > 8)
            {
                arenaTiles.Clear();
                int horizontalRadius = inArena ? GoddessOfMemoryArenaSystem.ActiveIsPhase ? 149 : 49 : ArenaSearchRadius;
                int verticalRadius = inArena ? 49 : ArenaSearchRadius;
                int left = Math.Max(1, searchPosition.X - horizontalRadius);
                int right = Math.Min(Main.maxTilesX - 2, searchPosition.X + horizontalRadius);
                int top = Math.Max(1, searchPosition.Y - verticalRadius);
                int bottom = Math.Min(Main.maxTilesY - 2, searchPosition.Y + verticalRadius);

                for (int i = left; i <= right; i++)
                {
                    for (int j = top; j <= bottom; j++)
                    {
                        Tile tile = Main.tile[i, j];
                        if (tile.HasTile && (!inArena || tile.TileType == markerType)
                            && TileLoader.GetTile(tile.TileType) is GoddessOfMemoryArenaTile)
                        {
                            arenaTiles.Add(new Point(i, j));
                        }
                    }
                }

                lastSearchPosition = searchPosition;
                lastArena = arena;
                arenaSearchTimer = 30;
            }

            position = default;
            arenaTile = null;
            float closestDistance = ArenaSearchRadius * 16f * ArenaSearchRadius * 16f;
            for (int i = arenaTiles.Count - 1; i >= 0; i--)
            {
                Point tilePosition = arenaTiles[i];
                Tile tile = Main.tile[tilePosition.X, tilePosition.Y];
                if (!tile.HasTile || TileLoader.GetTile(tile.TileType) is not GoddessOfMemoryArenaTile candidate)
                {
                    arenaTiles.RemoveAt(i);
                    continue;
                }

                float distance = Vector2.DistanceSquared(anchor, tilePosition.ToWorldCoordinates());
                if (distance < closestDistance)
                {
                    position = tilePosition;
                    arenaTile = candidate;
                    closestDistance = distance;
                }
            }

            if (inArena && arenaTile == null)
            {
                position = searchPosition;
                arenaTile = TileLoader.GetTile(markerType) as GoddessOfMemoryArenaTile;
            }

            return arenaTile != null;
        }

        public override void OnWorldLoad()
        {
            arenaTiles.Clear();
            lastSearchPosition = default;
            arenaSearchTimer = 0;
            lastArena = 0;
            active = false;
            foregroundWaterDrawn = false;
        }

        public override void OnWorldUnload()
        {
            arenaTiles.Clear();
            lastSearchPosition = default;
            arenaSearchTimer = 0;
            lastArena = 0;
            active = false;
            foregroundWaterDrawn = false;
            if (Main.dedServ)
            {
                return;
            }

            shader.UseOpacity(0f);
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                IL_Main.DoDraw -= MoveForegroundWater;
                On_Main.DoDraw -= DrawFrame;
                On_Main.DoDraw_Tiles_Solid -= DrawBackground;
            }

            arenaTiles.Clear();
            shader = null;
            emptyTexture = null;
            active = false;
            drawWaters = null;
            waterDrawPatchReady = false;
            foregroundWaterDrawn = false;
        }
    }

    public class GoddessOfMemoryScreenShaderData : ScreenShaderData
    {
        private Asset<Texture2D> maskTexture;
        private Point? tileAnchor;
        private Vector2 position;
        private Vector2 scrollVelocity;
        private float rotation;
        private float scale;
        private float borderScale;
        private int frame;
        private int frameCount;
        private bool flipHorizontally;

        public GoddessOfMemoryScreenShaderData(Asset<Effect> shader, string passName) : base(shader, passName)
        {
        }

        public void UseProjectile(Projectile projectile)
        {
            tileAnchor = null;
            GoddessOfMemoryBackgroundIntermission backgroundProjectile = (GoddessOfMemoryBackgroundIntermission)projectile.ModProjectile;
            maskTexture = ModContent.Request<Texture2D>(projectile.ModProjectile.Texture, AssetRequestMode.ImmediateLoad);
            position = projectile.Center + new Vector2(0f, projectile.gfxOffY);
            scrollVelocity = backgroundProjectile.BackgroundScrollVelocity;
            rotation = projectile.rotation;
            scale = projectile.scale;
            borderScale = backgroundProjectile.BorderScale;
            frame = projectile.frame;
            frameCount = Main.projFrames[projectile.type];
            flipHorizontally = projectile.spriteDirection == -1;
            UseImage(ModContent.Request<Texture2D>(backgroundProjectile.BackgroundTexture, AssetRequestMode.ImmediateLoad), 0, SamplerState.LinearWrap);
            UseImage(maskTexture, 1, SamplerState.LinearClamp);
            UseImage(ModContent.Request<Texture2D>(backgroundProjectile.BorderTexture, AssetRequestMode.ImmediateLoad), 2, SamplerState.LinearClamp);
            UseOpacity(projectile.Opacity);
        }

        public void UseTile(Point tilePosition, GoddessOfMemoryArenaTile arenaTile)
        {
            maskTexture = ModContent.Request<Texture2D>(arenaTile.MaskTexture, AssetRequestMode.ImmediateLoad);
            position = tilePosition.ToWorldCoordinates();
            if (tileAnchor != tilePosition)
            {
                tileAnchor = tilePosition;
                UnifiedRandom random = new UnifiedRandom(unchecked(tilePosition.X * 397 ^ tilePosition.Y));
                scrollVelocity = Vector2.UnitX.RotatedBy(random.NextFloat(MathHelper.TwoPi)) * random.NextFloat(0.01f, 0.1f);
            }
            rotation = Main.GlobalTimeWrappedHourly * 60f * arenaTile.RotationSpeed;
            scale = 1f;
            borderScale = arenaTile.BorderScale;
            frame = 0;
            frameCount = 1;
            flipHorizontally = false;
            UseImage(ModContent.Request<Texture2D>(arenaTile.BackgroundTexture, AssetRequestMode.ImmediateLoad), 0, SamplerState.LinearWrap);
            UseImage(maskTexture, 1, SamplerState.LinearClamp);
            UseImage(ModContent.Request<Texture2D>(arenaTile.BorderTexture, AssetRequestMode.ImmediateLoad), 2, SamplerState.LinearClamp);
            UseOpacity(1f);
        }

        public override void Apply()
        {
            if (maskTexture != null)
            {
                Texture2D texture = maskTexture.Value;
                int frameHeight = texture.Height / frameCount;
                float maskScale = Math.Max(Math.Abs(scale), 0.0001f);
                Matrix screenToMask = Matrix.Invert(Main.GameViewMatrix.TransformationMatrix);
                screenToMask *= Matrix.CreateTranslation(Main.screenPosition.X - position.X, Main.screenPosition.Y - position.Y, 0f)
                    * Matrix.CreateRotationZ(-rotation)
                    * Matrix.CreateScale((flipHorizontally ? -1f : 1f) / (texture.Width * maskScale), 1f / (frameHeight * maskScale), 1f)
                    * Matrix.CreateTranslation(0.5f, 0.5f, 0f);

                Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
                Shader.Parameters["uViewportSize"].SetValue(new Vector2(viewport.Width, viewport.Height));
                Shader.Parameters["uMaskTransformX"].SetValue(new Vector3(screenToMask.M11, screenToMask.M21, screenToMask.M41));
                Shader.Parameters["uMaskTransformY"].SetValue(new Vector3(screenToMask.M12, screenToMask.M22, screenToMask.M42));
                Shader.Parameters["uMaskFrame"].SetValue(new Vector4(0f, (float)(frame * frameHeight) / texture.Height, 1f, (float)frameHeight / texture.Height));
                Shader.Parameters["uMaskEnabled"].SetValue(scale == 0f ? 0f : 1f);
                Shader.Parameters["uScrollSpeed"].SetValue(scrollVelocity);
                Shader.Parameters["uBorderColor"].SetValue(new Color(255, 248, 201, 100).ToVector4());
                Shader.Parameters["uBorderScale"].SetValue(borderScale);
                Shader.Parameters["uBorderDirection"].SetValue(new Vector2(flipHorizontally ? 1f : -1f, 1f));
            }

            base.Apply();
        }
    }
}
