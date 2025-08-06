using StarsAbove.Tiles;
using StarsAbove.Tiles.CyberWorld;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace StarsAbove.Items.Placeable.CyberWorld
{
    public class HologramPlinthBigTree : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Foreign Vending Machine");
			// Tooltip.SetDefault("");
			
        }

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = ModContent.TileType<HologramPlinthBigTreeTile>();

		}

		public override void AddRecipes()
		{
            CreateRecipe(1)
                .AddIngredient(ItemType<DeepAsphalt>(), 25)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}