using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	
	public class StarArmorHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stargazing Hero's Hood");
			// Tooltip.SetDefault("Spatial garb of ages past");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Red; // The rarity of the item
			Item.vanity = true; // The amount of defense the item will give when equipped
		}

		
		
		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<EnigmaticDust>(), 1)
				.AddIngredient(ItemType<PrismaticCore>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}