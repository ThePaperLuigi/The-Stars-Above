using StarsAbove.Buffs;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Accessories
{
    public class Luciferium : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Lucifer's Bargain");

			/* Tooltip.SetDefault("" +
				"Defense is reduced by 30" +
                "\nUpon killing an enemy, gain the buff 'Sated Anguish' for 15 seconds" +
                "\nDuring this time, gain 20% increased damage, powerful life regeneration, and defense is restored" +
				"\n'Nanomachines of the highest caliber, or so they say'"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<WeaponPlayer>().luciferium = true;
			if(player.HasBuff(BuffType<SatedAnguish>()))
            {
				player.GetDamage(DamageClass.Generic) += 0.20f;
				player.lifeRegen += 2;
			}
			else
            {
				player.statDefense -= 30;
            }
			
			//player.respawnTimer += 600;

		}
		

		

		/*public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LifeCrystal, 2);
			recipe.AddIngredient(ItemID.ManaCrystal, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}
