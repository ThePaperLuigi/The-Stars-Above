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
    [AutoloadEquip(EquipType.Head)]
	
	public class OceanHuedHunterHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stargazing Hero's Hood");
			// Tooltip.SetDefault("Spatial garb of ages past");
			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.sellPrice(gold: 1); // How many coins the item is worth
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

		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			
		}

		
	}
}