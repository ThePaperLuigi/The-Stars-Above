using Microsoft.Xna.Framework;
using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ID;

using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using StarsAbove.Systems;

namespace StarsAbove.Subworlds.ThirdRegion
{
    public class FaintArchives : Subworld
    {
        public override int Width => 3600;
        public override int Height => 800;

        //public override ModWorld modWorld => ModContent.GetInstance < your modworld here>();


        public override List<GenPass> Tasks => new() { new PassLegacy("Subworld", SubworldGeneration) };
        private void SubworldGeneration(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Loading"; //Sets the text above the worldgen progress bar

            Main.spawnTileX = 1986;
            Main.spawnTileY = 354;


            Main.worldSurface = Main.maxTilesY / 2 + 420;
            Main.rockLayer = Main.maxTilesY / 2 + 600;

            int tileAdjustment = 360;
            int tileAdjustmentX = 300 + 459;


            StructureHelper.Generator.GenerateStructure("Structures/FaintArchives/FaintArchives", new Terraria.DataStructures.Point16(Main.maxTilesX / 2 - tileAdjustmentX, Main.maxTilesY / 2 - tileAdjustment), StarsAbove.Instance);

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {


                    progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); //Controls the progress bar, should only be set between 0f and 1f
                                                                                                       //Main.tile[i, j].active(true);
                                                                                                       //Main.tile[i, j].type = TileID.Air;
                }
                if (i == Main.maxTilesX / 2)
                {

                }
            }
        }

        private const string assetPath = "StarsAbove/Subworlds/LoadingScreens";

        public override void DrawMenu(GameTime gameTime)
        {
            Texture2D MenuBG = (Texture2D)Request<Texture2D>($"{assetPath}/DefaultLS");//Background
            Vector2 zero = Vector2.Zero;
            float width = Main.screenWidth / (float)MenuBG.Width;
            float height = Main.screenHeight / (float)MenuBG.Height;

            if (width != height)
            {
                if (height > width)
                {
                    width = height;
                    zero.X -= (MenuBG.Width * width - Main.screenWidth) * 0.5f;
                }
                else
                {
                    zero.Y -= (MenuBG.Height * width - Main.screenHeight) * 0.5f;
                }
            }

            Main.spriteBatch.Draw(MenuBG, zero, null, Color.White, 0f, Vector2.Zero, width, 0, 0f);
            DrawStarfarerAnimation();

            base.DrawMenu(gameTime);
        }
        int animationTime;
        int animationTime2;
        int animationFrame;
        int animationFrame2;
        private void DrawStarfarerAnimation()
        {
            Texture2D AsphoRunAnimation = (Texture2D)Request<Texture2D>($"StarsAbove/UI/CelestialCartography/RunAnimation/ARun" + animationFrame + "0");
            Texture2D EriRunAnimation = (Texture2D)Request<Texture2D>($"StarsAbove/UI/CelestialCartography/RunAnimation/ERun" + animationFrame2 + "0");
            animationTime++;
            animationTime2++;
            if (animationTime > 9)
            {
                animationTime = 0;
                animationFrame++;
                if (animationFrame > 7)
                {
                    animationFrame = 0;
                }
            }
            if (animationTime2 > 10)
            {
                animationTime2 = 0;
                animationFrame2++;
                if (animationFrame2 > 7)
                {
                    animationFrame2 = 0;
                }
            }
            //Shadow
            Main.spriteBatch.Draw(
                AsphoRunAnimation, //The texture being drawn.
                new Vector2(Main.screenWidth / 2 + 44, Main.screenHeight / 2 - 66), //The position of the texture.
                new Rectangle(0, 0, AsphoRunAnimation.Width, AsphoRunAnimation.Height),
                Color.Black * 0.2f, //The color of the texture.
                0, // The rotation of the texture.
                AsphoRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
                1f, //The scale of the texture.
                SpriteEffects.None,
                0f);
            Main.spriteBatch.Draw(
                EriRunAnimation, //The texture being drawn.
                new Vector2(Main.screenWidth / 2 - 36, Main.screenHeight / 2 - 66), //The position of the texture.
                new Rectangle(0, 0, EriRunAnimation.Width, EriRunAnimation.Height),
                Color.Black * 0.2f, //The color of the texture.
                0, // The rotation of the texture.
                EriRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
                1f, //The scale of the texture.
                SpriteEffects.None,
                0f);
            Main.spriteBatch.Draw(
                AsphoRunAnimation, //The texture being drawn.
                new Vector2(Main.screenWidth / 2 + 40, Main.screenHeight / 2 - 70), //The position of the texture.
                new Rectangle(0, 0, AsphoRunAnimation.Width, AsphoRunAnimation.Height),
                Color.White, //The color of the texture.
                0, // The rotation of the texture.
                AsphoRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
                1f, //The scale of the texture.
                SpriteEffects.None,
                0f);
            Main.spriteBatch.Draw(
                EriRunAnimation, //The texture being drawn.
                new Vector2(Main.screenWidth / 2 - 40, Main.screenHeight / 2 - 70), //The position of the texture.
                new Rectangle(0, 0, EriRunAnimation.Width, EriRunAnimation.Height),
                Color.White, //The color of the texture.
                0, // The rotation of the texture.
                EriRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
                1f, //The scale of the texture.
                SpriteEffects.None,
                0f);


        }

        public override bool GetLight(Tile tile, int x, int y, ref FastRandom rand, ref Vector3 color)
        {
            color = new Vector3(0.00f, 0.00f, 0.00f);
            return base.GetLight(tile, x, y, ref rand, ref color);
        }
        public override void OnLoad()
        {


        }
        public override void OnEnter()
        {
            //Clear the background.
            Main.numClouds = 0;
            Main.numCloudsTemp = 0;
            Main.cloudBGAlpha = 0f;

            Main.cloudAlpha = 0f;
            Main.resetClouds = true;
            Main.moonPhase = 4;
            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "FaintArchives";
            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().loadingScreenOpacity = 1f;

        }
        public override void Load()
        {
            //SubworldSystem.noReturn = true;



        }
    }
}
