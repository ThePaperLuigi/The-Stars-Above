using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Materials;
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


    public class FaerieVoyagerAttire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Attire of the Faerie Voyager");
			Tooltip.SetDefault("One of the storied garbs from legends past" +
                "\nEquip to your Starfarer to gain the following:" +
				"\n[c/E69DE9:The Promised Star]" +
				"\nGain 1 Stellar Nova Energy upon defeating foes" +
				"\nWhen your Stellar Nova Gauge is below 50%, gain 1 additional Stellar Nova Energy" +
				"\n[c/73E096:A Midsummer Night's Dream]" +
				"\nGain an extra Stellar Array Energy point" +
				//"\n[c/00EE57:Dreamer's Reticence] doubles all outgoing damage, calculated multiplicatively" +
				"\n[c/A185AD:Fleeting Memories]" +
                "\nTaking damage inflicts Vulnerable for 4 seconds, halving defense" +
				"\n[c/A176F1:Long Sought Paradise]" +
				"\nAfter defeating a boss, gain [c/E1A8F3:Lucent Bliss] for 2 minutes" +
				"\n[c/E1A8F3:Lucent Bliss] drastically increases maximum Luck and grants 200% base Luck" +
				//"\n[c/E572A1:Land of Miracles]" +
				//"\nStarfarer Voyages on 'Radiant Planets' will always succeed, are 50% faster, and gain 80% increased Riches" +
                "\n'Perfect for escapades in dreamland'");

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
				.AddIngredient(ItemType<FaerieVoyagerAttirePrecursor>(), 1)
				.AddIngredient(ItemType<TwilightNeedle>(), 1)
				.AddIngredient(ItemType<BoltOfStarsilk>(), 5)
				.AddIngredient(ItemID.PrincessFish, 1)
				.AddIngredient(ItemID.PinkPearl, 1)
				.AddIngredient(ItemID.PinkGel, 30)
				.AddIngredient(ItemID.ButterflyDust, 2)
				.AddIngredient(ItemID.PinkThread, 20)
				.AddIngredient(ItemID.Amethyst, 25)
				.AddIngredient(ItemID.Sapphire, 25)
				.AddIngredient(ItemID.SoulofLight, 50)
				.AddIngredient(ItemType<PrismaticCore>(), 30)
				.AddTile(TileID.Anvils)
				.Register();
			
		}
	}
}