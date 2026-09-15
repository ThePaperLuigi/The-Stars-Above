using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryMapSystem : ModSystem
    {
        private static bool MapsDisabled => !Main.dedServ && !Main.gameMenu && NPC.AnyNPCs(ModContent.NPCType<GoddessOfMemoryBoss>());

        public override void Load()
        {
            if (!Main.dedServ)
            {
                On_Main.DoDraw += DrawDuringFight;
                On_Main.DrawMap += DrawMap;
                On_Main.DrawInventory += DrawInventory;
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                On_Main.DoDraw -= DrawDuringFight;
                On_Main.DrawMap -= DrawMap;
                On_Main.DrawInventory -= DrawInventory;
            }
        }

        public override void PostUpdateInput()
        {
            if (!MapsDisabled)
            {
                return;
            }

            Main.mapFullscreen = false;
            BlockMapControls(PlayerInput.Triggers.Current);
            BlockMapControls(PlayerInput.Triggers.JustPressed);
            BlockMapControls(PlayerInput.Triggers.JustReleased);
        }

        private static void BlockMapControls(TriggersSet triggers)
        {
            triggers.MapFull = false;
            triggers.MapStyle = false;
            triggers.MapZoomIn = false;
            triggers.MapZoomOut = false;
            triggers.MapAlphaUp = false;
            triggers.MapAlphaDown = false;
        }

        private void DrawDuringFight(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            if (MapsDisabled)
            {
                Main.mapFullscreen = false;
            }

            orig(self, gameTime);
        }

        private void DrawMap(On_Main.orig_DrawMap orig, Main self, GameTime gameTime)
        {
            if (!MapsDisabled)
            {
                orig(self, gameTime);
            }
        }

        private void DrawInventory(On_Main.orig_DrawInventory orig, Main self)
        {
            if (!MapsDisabled)
            {
                orig(self);
                return;
            }

            bool mapEnabled = Main.mapEnabled;
            try
            {
                Main.mapEnabled = false;
                orig(self);
            }
            finally
            {
                Main.mapEnabled = mapEnabled;
            }
        }
    }
}
