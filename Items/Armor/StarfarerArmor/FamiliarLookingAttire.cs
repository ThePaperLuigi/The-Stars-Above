using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Prisms;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using Terraria.UI.Chat;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria.GameContent;

namespace StarsAbove.Items.Armor.StarfarerArmor
{


    public class FamiliarLookingAttire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Familiar Looking Attire");
			Tooltip.SetDefault("" +
                "One of the storied garbs from legends past" +
                "\nEquip to your Starfarer to gain the following:" +
				"\n[c/C0C0C0:No effect]" +
				"\nVanity Item" +
				//"\n[c/E572A1:Land of Miracles]" +
				//"\nStarfarer Voyages on 'Radiant Planets' will always succeed, are 50% faster, and gain 80% increased Riches" +
                "");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.sellPrice(platinum: 5); // How many coins the item is worth
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
			
		}

		public override void UpdateArmorSet(Player player)
		{
			
		}
		private Vector2 boxSize; // stores the size of our tooltip box
		private const int paddingForBox = 10;

		public override bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
		{
			// You can offset the entire tooltip by changing x and y
			// You can actually have the entire tooltip draw somewhere else, x and y is where the tooltip starts drawing

			// Draw a magic box for this tooltip
			// From all tooltips we select their texts
			var texts = lines.Select(z => z.Text);
			// Calculate our width for the box, which will be the width of the longest text, plus some padding. This code takes into account Snippets and character widths.
			int widthForBox = texts.Max(t => (int)ChatManager.GetStringSize(FontAssets.MouseText.Value, t, Vector2.One).X) + paddingForBox * 2;
			// Calculate our height for the box, which will be the sum of the text heights, plus some padding
			int heightForBox = (int)texts.ToList().Sum(z => FontAssets.MouseText.Value.MeasureString(z).Y) + paddingForBox * 2;
			// Set our boxSize to our calculated size, now we can use this elsewhere too
			boxSize = new Vector2(widthForBox, heightForBox);
			x -= paddingForBox;
			
			

			return true;
		}
		
		public override void AddRecipes()
		{//Outfits should be expensive chase goals, with a complicated recipe.

			CreateRecipe(1)
				.AddIngredient(ItemID.FamiliarPants, 1)
				.AddIngredient(ItemID.FamiliarShirt, 1)
				.AddIngredient(ItemType<PrismaticCore>(), 20)
				.AddTile(TileID.Anvils)
				.Register();
			
		}
	}
}