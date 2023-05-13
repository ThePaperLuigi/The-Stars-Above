
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;


namespace StarsAbove
{


    public class StarsAboveModSystem : ModSystem
	{
        
        public override void OnWorldLoad()
        {
			
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
