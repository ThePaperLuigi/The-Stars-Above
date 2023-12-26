using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;

namespace StarsAbove.Items.Vanity.OceanHuedHunter

{
    [AutoloadEquip(EquipType.Legs)]
	public class OceanHuedHunterLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stargazing Hero's Greaves");
			// Tooltip.SetDefault("Spatial garb of ages past");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
			Item.value = 1;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ModContent.ItemType<Materials.StellarRemnant>(), 20)
				.AddCustomShimmerResult(ModContent.ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(Terraria.ID.TileID.Anvils)
				.Register();
		}
	}
}