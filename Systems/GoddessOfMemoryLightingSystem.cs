using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Light;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryLightingSystem : ModSystem
    {
        public override void Load()
        {
            if (!Main.dedServ)
            {
                On_TileLightScanner.GetTileLight += ApplyMinimumLight;
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                On_TileLightScanner.GetTileLight -= ApplyMinimumLight;
            }
        }

        private void ApplyMinimumLight(On_TileLightScanner.orig_GetTileLight orig, TileLightScanner self, int x, int y, out Vector3 outputColor)
        {
            orig(self, x, y, out outputColor);
            if (GoddessOfMemoryArenaSystem.IsActive)
            {
                outputColor = Vector3.Max(outputColor, Vector3.One * 0.01f);
            }
        }
    }
}
