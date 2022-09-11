using Terraria;using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
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
	
	
	public class CelestialPrincessGenesis : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Princess' Genesis");
			Tooltip.SetDefault("" +
                "One of the storied garbs from legends past" +
                "\nEquip to your Starfarer to gain the following:" +
				"\n[c/FF30AD:Cosmos of the Beginning]" +
                "\nThe Stellar Nova is locked to Edin Genesis Quasar" +
                "\nAttacking foes during Astarte Driver will cause a follow-up attack to occur, dealing 1/3rd original damage again" +
				"\n[c/30D6FF:Galactic Spirit Origin]" +	
                "\nDefeating foes refreshes the duration of Astarte Driver" +
                "\nTaking damage charges the Stellar Nova Gauge by 3" +
				"\n[c/3068FF:Luminant Starling]" +
                "\nIncoming damage is reduced by 10%" +
				"\n[c/A55396:Primeval Blasphemy]" +
				"\nDeal 70% less damage when Astarte Driver is not active" +
				"\n'Know that you are dimensions below me...'" +
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
				.AddIngredient(ItemType<CelestialPrincessGenesisPrecursor>(), 1)
				.AddIngredient(ItemType<TwilightNeedle>(), 1)
				.AddIngredient(ItemType<BoltOfTrueStarsilk>(), 3)
				.AddIngredient(ItemID.LunarBar, 32)
				.AddIngredient(ItemID.FragmentNebula, 18)
				.AddIngredient(ItemID.FragmentStardust, 18)
				.AddIngredient(ItemID.FragmentSolar, 18)
				.AddIngredient(ItemID.FragmentVortex, 18)
				.AddIngredient(ItemID.Ectoplasm, 15)
				.AddIngredient(ItemID.LastPrism, 1)
				.AddIngredient(ItemID.FallenStar, 20)
				.AddIngredient(ItemType<PrismaticCore>(), 20)
				.AddTile(TileID.Anvils)
				.Register();
			
		}
	}
}