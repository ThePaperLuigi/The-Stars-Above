using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;
using System;
using StarsAbove.Buffs.StellarSpoils.EmberFlask;
using StarsAbove.Mounts.Forklift;
using StarsAbove.Items.Consumables;

namespace StarsAbove.Items.Tools
{

    public class ForkliftKey : ModItem
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 1;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item1;
			Item.consumable = false;
			Item.ResearchUnlockCount = 0;
		}

        public override void UpdateInventory(Player player)
        {
			if(player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
			{
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemType<AsphodeneForkliftKey>());

                Item.TurnToAir();
			}
			else if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemType<EridaniForkliftKey>());


                Item.TurnToAir();
            }

            base.UpdateInventory(player);
        }
        public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override bool CanUseItem(Player player) {
            return true;
        }

		public override bool? UseItem(Player player) {

			
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<Materials.StellarRemnant>(), 40)
				.AddCustomShimmerResult(ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}