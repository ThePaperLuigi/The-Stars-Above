using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;
using System;
using StarsAbove.Buffs.StellarSpoils.EmberFlask;
using StarsAbove.Mounts.Forklift;

namespace StarsAbove.Items.Tools
{

    public class AsphodeneForkliftKey : ModItem
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
			Item.ResearchUnlockCount = 1;
			Item.mountType = ModContent.MountType<AsphodeneForklift>();
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
			
		}
	}
}