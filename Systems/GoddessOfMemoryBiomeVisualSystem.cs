using Microsoft.Xna.Framework;
using StarsAbove.SceneEffects;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryBiomeVisualSystem : ModSystem
    {
        public override void Load()
        {
            if (!Main.dedServ)
            {
                On_Main.DoDraw += DrawBiomeVisuals;
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                On_Main.DoDraw -= DrawBiomeVisuals;
            }
        }

        private void DrawBiomeVisuals(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            if (Main.gameMenu || !GoddessOfMemorySceneEffect.HasBiomeBuff(Main.LocalPlayer))
            {
                orig(self, gameTime);
                return;
            }

            bool dayTime = Main.dayTime;
            double time = Main.time;
            int moonPhase = Main.moonPhase;
            float cloudAlpha = Main.cloudAlpha;
            int numClouds = Main.numClouds;
            int numCloudsTemp = Main.numCloudsTemp;
            float cloudBGAlpha = Main.cloudBGAlpha;

            try
            {
                bool seaOfStars = GoddessOfMemorySceneEffect.HasSeaOfStarsBuff(Main.LocalPlayer);
                Main.dayTime = !seaOfStars;
                Main.time = seaOfStars ? 18000 : 12000;
                Main.moonPhase = 4;
                Main.cloudAlpha = 0f;
                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                orig(self, gameTime);
            }
            finally
            {
                Main.dayTime = dayTime;
                Main.time = time;
                Main.moonPhase = moonPhase;
                Main.cloudAlpha = cloudAlpha;
                Main.numClouds = numClouds;
                Main.numCloudsTemp = numCloudsTemp;
                Main.cloudBGAlpha = cloudBGAlpha;
            }
        }
    }
}
