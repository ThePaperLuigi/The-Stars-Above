using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Materials;
using StarsAbove.Systems;

namespace StarsAbove.Items.Accessories
{
    public class CrystallizedAbsence : StargazerRelic
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Crystallized Absence");
			/* Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +
				"\nMax HP is reduced by 300 and defense is reduced by 20, but damage is increased by 100%" +
				"\nOnly active when unmodified Max HP is 400 or higher" +
				"\n[c/ADEEFF:Only one Stargazer Relic can be equipped at a time]" +
				"\n'From a world cursed to fade away'"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			//player.GetModPlayer<StarsAbovePlayer>().CrystallizedAbsence = true;
			if(player.statLifeMax >= 400)
            {
				player.statLifeMax2 -= 300;
				player.statDefense -= 20;
				player.GetDamage(DamageClass.Generic) += 0.5f;
            }
			//player.GetDamage(DamageClass.Generic) += 0.12f;
			//player.respawnTimer += 600;

		}

		

		public override void AddRecipes() {
			CreateRecipe(1)
				.AddIngredient(ItemType<InertShard>(), 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
