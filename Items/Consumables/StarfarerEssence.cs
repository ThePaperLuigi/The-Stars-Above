using Microsoft.Xna.Framework;
using StarsAbove.Biomes;
using StarsAbove.NPCs.Starfarers;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Consumables
{
    public class StarfarerEssence : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Starfarer Essence");
			/* Tooltip.SetDefault("A mystical gift from your Starfarer" +
				"\nDoesn't do anything." +
                "\n" +
				$""); */

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            //Temporary access anywhere
            return !NPC.AnyNPCs(ModContent.NPCType<StarfarerBoss>()) && SubworldSystem.Current == null;// && player.InModBiome<NeonVeilBiome>();
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                // If the player using the item is the client
                // (explicitely excluded serverside here)


                int type1 = ModContent.NPCType<StarfarerBoss>();
                int type2 = ModContent.NPCType<StarfarerBossWallsNPC>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type1);
                    NPC.NewNPC(null, (int)player.Center.X, (int)player.Center.Y, type2);

                }
                else
                {
                    // If the player is in multiplayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type1);
                    


                }
            }

            //NPC.NewNPC(null, (int)player.Center.X,(int)player.Center.Y-900, NPCType<NPCs.Arbitration.ArbitrationBoss>());
            //Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

    
         public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		
	}
}