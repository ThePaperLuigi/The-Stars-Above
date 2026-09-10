using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StarsAbove.Projectiles.Bosses.GoddessOfMemory;
using StarsAbove.Tiles.GoddessOfMemory;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryVFXSystem : ModSystem
    {
        private const string FilterKey = "StarsAbove:GoddessOfMemory";
        private const int ArenaSearchRadius = 160;
        private readonly List<Point> arenaTiles = new List<Point>();
        private GoddessOfMemoryScreenShaderData shader;
        private Point lastSearchPosition;
        private int arenaSearchTimer;

        public override void Load()
        {
            if (Main.dedServ)
            {
                return;
            }

            shader = new GoddessOfMemoryScreenShaderData(ModContent.Request<Effect>("StarsAbove/Effects/MiscEffectReverse", AssetRequestMode.ImmediateLoad), "ReversePass");
            Filters.Scene[FilterKey] = new Filter(shader, EffectPriority.VeryHigh);
            Filters.Scene[FilterKey].Load();
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

            Vector2 position;
            if (selectedProjectile != null)
            {
                shader.UseProjectile(selectedProjectile);
                position = selectedProjectile.Center;
            }
            else if (TryGetArenaTile(out Point tilePosition, out GoddessOfMemoryArenaTile arenaTile))
            {
                shader.UseTile(tilePosition, arenaTile);
                position = tilePosition.ToWorldCoordinates();
            }
            else
            {
                shader.UseOpacity(0f);
                Filters.Scene[FilterKey].Opacity = 0f;
                if (Filters.Scene[FilterKey].IsActive())
                {
                    Filters.Scene.Deactivate(FilterKey);
                }

                return;
            }

            if (!Filters.Scene[FilterKey].IsActive())
            {
                Filters.Scene.Activate(FilterKey, position);
            }

            Filters.Scene[FilterKey].Opacity = 1f;
            shader.UseGlobalOpacity(1f);
        }

        private bool TryGetArenaTile(out Point position, out GoddessOfMemoryArenaTile arenaTile)
        {
            Point playerPosition = Main.LocalPlayer.Center.ToTileCoordinates();
            if (--arenaSearchTimer <= 0 || Math.Abs(playerPosition.X - lastSearchPosition.X) > 8 || Math.Abs(playerPosition.Y - lastSearchPosition.Y) > 8)
            {
                arenaTiles.Clear();
                int left = Math.Max(1, playerPosition.X - ArenaSearchRadius);
                int right = Math.Min(Main.maxTilesX - 2, playerPosition.X + ArenaSearchRadius);
                int top = Math.Max(1, playerPosition.Y - ArenaSearchRadius);
                int bottom = Math.Min(Main.maxTilesY - 2, playerPosition.Y + ArenaSearchRadius);

                for (int i = left; i <= right; i++)
                {
                    for (int j = top; j <= bottom; j++)
                    {
                        Tile tile = Main.tile[i, j];
                        if (tile.HasTile && TileLoader.GetTile(tile.TileType) is GoddessOfMemoryArenaTile)
                        {
                            arenaTiles.Add(new Point(i, j));
                        }
                    }
                }

                lastSearchPosition = playerPosition;
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

                float distance = Vector2.DistanceSquared(Main.LocalPlayer.Center, tilePosition.ToWorldCoordinates());
                if (distance < closestDistance)
                {
                    position = tilePosition;
                    arenaTile = candidate;
                    closestDistance = distance;
                }
            }

            return arenaTile != null;
        }

        public override void OnWorldLoad()
        {
            arenaTiles.Clear();
            lastSearchPosition = default;
            arenaSearchTimer = 0;
        }

        public override void OnWorldUnload()
        {
            arenaTiles.Clear();
            lastSearchPosition = default;
            arenaSearchTimer = 0;
            if (Main.dedServ)
            {
                return;
            }

            shader.UseOpacity(0f);
            Filters.Scene[FilterKey].Opacity = 0f;
            if (Filters.Scene[FilterKey].IsActive())
            {
                Filters.Scene.Deactivate(FilterKey);
            }
        }

        public override void Unload()
        {
            arenaTiles.Clear();
            shader = null;
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
                if (Main.LocalPlayer.gravDir == -1f)
                {
                    screenToMask = Main.GameViewMatrix.EffectMatrix * screenToMask;
                }
                screenToMask *= Matrix.CreateTranslation(Main.screenPosition.X - position.X, Main.screenPosition.Y - position.Y, 0f)
                    * Matrix.CreateRotationZ(-rotation)
                    * Matrix.CreateScale((flipHorizontally ? -1f : 1f) / (texture.Width * maskScale), 1f / (frameHeight * maskScale), 1f)
                    * Matrix.CreateTranslation(0.5f, 0.5f, 0f);

                Shader.Parameters["uViewportSize"].SetValue(new Vector2(Main.screenTarget.Width, Main.screenTarget.Height));
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
