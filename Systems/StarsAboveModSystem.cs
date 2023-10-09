
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using ReLogic.Content;
using SubworldLibrary;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarsAbove.Systems
{


    public class StarsAboveModSystem : ModSystem
    {
        //Thank you to tomat for code!
        private ILHook uiModItemOnInitializeHook;
        private sealed class UIAnimatedImageAlwaysHovering : UIElement
        {
            private readonly Asset<Texture2D> texture;
            private readonly int countY;
            private int tickCounter;
            private int frameCounter;

            public int FrameCount { get; set; } = 1;

            public int TicksPerFrame { get; set; } = 5;

            private int DrawHeight => (int)Height.Pixels;

            private int DrawWidth => (int)Width.Pixels;

            public UIAnimatedImageAlwaysHovering(Asset<Texture2D> texture, int width, int height, int countY)
            {
                this.texture = texture;
                this.countY = countY;
                Width.Pixels = width;
                Height.Pixels = height;
            }

            private Rectangle FrameToRect(int frame)
            {
                var horizIndex = frame / countY;
                var vertIndex = frame % countY;
                return new Rectangle(horizIndex * DrawWidth, vertIndex * DrawHeight, DrawWidth, DrawHeight);
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                if (++tickCounter < TicksPerFrame)
                    return;

                tickCounter = 0;
                if (++frameCounter < FrameCount)
                    return;

                frameCounter = 0;
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                var dims = GetDimensions();
                var frame = frameCounter % FrameCount;
                spriteBatch.Draw(texture.Value, dims.ToRectangle(), FrameToRect(frame), Color.White);
            }
        }
        public override void Load()
        {
            base.Load();
            //To be used with v1.6
            var uiModItemType = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem")!;
            var onInitializeMethod = uiModItemType.GetMethod("OnInitialize", BindingFlags.Public | BindingFlags.Instance)!;
            uiModItemOnInitializeHook = new ILHook(onInitializeMethod, AnimateModIcon);
            
            //IL_UIText.DrawSelf += CreateVector2Scale;
        }

        public override void Unload()
        {
            base.Unload();

            uiModItemOnInitializeHook?.Dispose();
        }

        private static void AnimateModIcon(ILContext il)
        {
            var c = new ILCursor(il);

            var uiModItemType = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem")!;
            var modIconField = uiModItemType.GetField("_modIcon", BindingFlags.NonPublic | BindingFlags.Instance)!;
            var modNameProperty = uiModItemType.GetProperty("ModName", BindingFlags.Public | BindingFlags.Instance)!;
            c.GotoNext(MoveType.Before, x => x.MatchStfld(modIconField));
            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Callvirt, modNameProperty.GetMethod!);
            c.EmitDelegate((UIImage modIconImage, string modName) =>
            {
                if (modName == "StarsAbove")
                {
                    return new UIAnimatedImageAlwaysHovering(ModContent.Request<Texture2D>("StarsAbove/Menu/AnimatedIcon"), 80, 80, 31)
                    {
                        Left = { Percent = 0f },
                        Top = { Percent = 0f },
                        Width = { Pixels = 80 },
                        Height = { Pixels = 80 },
                        TicksPerFrame = 4,
                        FrameCount = 32,
                    };
                }

                return (UIElement)modIconImage;
            });
        }
        public override void OnWorldLoad()
        {

        }
        public override void PreUpdatePlayers()
        {


            base.PreUpdatePlayers();
        }

        public override void PreUpdateInvasions()
        {

            if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior)
            {
                Main.eclipse = false;
            }
        }

        public override void ResetNearbyTileEffects()
        {
            StarsAbovePlayer modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            modPlayer.lightMonolith = false;


        }

        public static Vector2 GetPlayerArmPosition(Projectile proj)
        {
            Player player = Main.player[proj.owner];
            Vector2 value = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                value.X = (float)player.bodyFrame.Width - value.X;
            }
            if (player.gravDir != 1f)
            {
                value.Y = (float)player.bodyFrame.Height - value.Y;
            }
            value -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            return player.RotatedRelativePoint(player.MountedCenter - new Vector2(20f, 42f) / 2f + value + Vector2.UnitY * player.gfxOffY);
        }

    }
}
