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


    public class AegisOfHopesLegacy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aegis of Hope's Legacy");
			Tooltip.SetDefault("" +
                "One of the storied garbs from legends past" +
                "\nEquip to your Starfarer to gain the following:" +
				"\n[c/FFCA5A:Shining Journey]" +
				"\nAttacking and taking damage will grant stacks of Hope's Brilliance (Max 100 stacks)" +
                "\nEvery 10 stacks of Hope's Brilliance increases damage by 2%" +
                "\nStriking a foe with the Stellar Nova will consume all stacks of Hope's Brilliance" +
				"\n[c/87E3DE:Passage of Arms]" +
                "\nEnemies are more likely to target you" +
                "\nTaking damage grants Ironskin, Regeneration, and Endurance for 8 seconds" +
                "\nAdditionally, gain the buff Nascent Aria for 3 seconds, increasing maximum health by 500" +
				"\n[c/F94135:Proof of a Hero]" +
                "\nAttacks will deal 40% less damage to foes not targetting you" +
				"\n[c/8BB9F8:The Echo of Hope]" +
				"\nTier 3 Stellar Array Abilities gain bonus effects" +
                "\nBeyond Infinity: Damage is increased by a further 30%" +
                "\nKey of Chronology: Cleanse Potion Sickness and gain Heartreach for 30 seconds upon activation" +
                "\nBetween the Boundary: Gain 40 defense during Flow, and lose 20 defense during Ebb" +
                "\nUnbridled Radiance: Stellar Nova Energy can not drop below 10" +
				"\n'Stand tall- my friend / May all of the dark deep inside you / Find Light again' " +
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
				.AddIngredient(ItemType<AegisOfHopesLegacyPrecursor>(), 1)
				.AddIngredient(ItemType<TwilightNeedle>(), 1)
				.AddIngredient(ItemType<BoltOfTrueStarsilk>(), 3)
				.AddIngredient(ItemID.WoodBreastplate, 1)
				.AddIngredient(ItemID.FossilShirt, 1)
				.AddIngredient(ItemID.GladiatorBreastplate, 1)
				.AddIngredient(ItemID.AncientCobaltBreastplate, 1)
				.AddIngredient(ItemID.MoltenBreastplate, 1)
				.AddIngredient(ItemID.FrostBreastplate, 1)
				.AddIngredient(ItemID.HallowedPlateMail, 1)
				.AddIngredient(ItemID.ChlorophytePlateMail, 1)
				.AddIngredient(ItemID.ShroomiteBreastplate, 1)
				.AddIngredient(ItemID.SolarFlareBreastplate, 1)
				.AddIngredient(ItemType<DullTotemOfLight>(), 1)
				.AddIngredient(ItemType<PrismaticCore>(), 20)
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}