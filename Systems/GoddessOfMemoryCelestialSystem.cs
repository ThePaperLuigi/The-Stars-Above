using StarsAbove.NPCs.GoddessOfMemory;
using StarsAbove.SceneEffects.CustomSkies;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryCelestialSystem : ModSystem
    {
        private const string SkyKey = "StarsAbove:GoddessOfMemoryCelestial";
        private bool isActive;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                SkyManager.Instance[SkyKey] = new GoddessOfMemoryCelestialSky();
            }
        }

        public override void PostUpdateEverything()
        {
            if (Main.dedServ)
            {
                return;
            }

            bool shouldBeActive = !Main.gameMenu && NPC.AnyNPCs(ModContent.NPCType<GoddessOfMemoryBoss>());
            if (shouldBeActive == isActive)
            {
                return;
            }

            isActive = shouldBeActive;
            if (isActive)
            {
                SkyManager.Instance.Activate(SkyKey);
            }
            else
            {
                SkyManager.Instance.Deactivate(SkyKey);
            }
        }

        private void ResetSky()
        {
            isActive = false;
            if (!Main.dedServ)
            {
                SkyManager.Instance[SkyKey]?.Reset();
            }
        }

        public override void OnWorldLoad()
        {
            ResetSky();
        }

        public override void OnWorldUnload()
        {
            ResetSky();
        }

        public override void Unload()
        {
            ResetSky();
            if (!Main.dedServ && SkyManager.Instance[SkyKey] is GoddessOfMemoryCelestialSky sky)
            {
                sky.Unload();
            }
        }
    }
}
