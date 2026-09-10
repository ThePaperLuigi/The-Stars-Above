using Microsoft.Xna.Framework;
using StarsAbove.Buffs.GoddessOfMemory;
using Terraria;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryWeatherSystem : ModSystem
    {
        private const string BlizzardKey = "StarsAbove:GoddessOfMemoryBlizzard";

        public override void Load()
        {
            if (Main.dedServ)
            {
                return;
            }

            Filters.Scene[BlizzardKey] = new Filter(new BlizzardShaderData("FilterBlizzardForeground")
                .UseColor(1f, 1f, 1f)
                .UseSecondaryColor(0.7f, 0.7f, 1f)
                .UseImage("Images/Misc/noise")
                .UseIntensity(0.4f)
                .UseImageScale(new Vector2(3f, 0.75f)), EffectPriority.High);
            Filters.Scene[BlizzardKey].Load();

            Overlays.Scene[BlizzardKey] = new SimpleOverlay("Images/Misc/noise", new BlizzardShaderData("FilterBlizzardBackground")
                .UseColor(1f, 1f, 1f)
                .UseSecondaryColor(0.7f, 0.7f, 1f)
                .UseImage("Images/Misc/noise")
                .UseIntensity(0.4f)
                .UseImageScale(new Vector2(3f, 0.75f)), EffectPriority.VeryHigh, RenderLayers.Landscape);
        }

        public override void PostUpdatePlayers()
        {
            if (Main.dedServ)
            {
                return;
            }

            Player player = Main.LocalPlayer;
            bool isActive = player.active && !player.dead && player.HasBuff<GoddessOfMemoryPhase1Buff>();
            player.ManageSpecialBiomeVisuals(BlizzardKey, isActive);
        }

        public override void OnWorldUnload()
        {
            if (Main.dedServ)
            {
                return;
            }

            Filters.Scene[BlizzardKey].Opacity = 0f;
            Overlays.Scene[BlizzardKey].Opacity = 0f;
            Main.LocalPlayer.ManageSpecialBiomeVisuals(BlizzardKey, false);
        }
    }
}
