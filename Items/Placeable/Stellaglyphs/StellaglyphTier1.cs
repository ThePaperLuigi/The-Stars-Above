
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SubworldLibrary;
using StarsAbove.Buffs;
using StarsAbove.Projectiles.Otherworld;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Subworlds;

namespace StarsAbove.Items.Placeable.Stellaglyphs
{

    public class StellaglyphTier1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stellaglyph (Tier 1)");
			

			Tooltip.SetDefault("" +
				"A mystical dias formed by the Celestriad Root" +
				"\nAllows for the traversal of the cosmos through use of [c/F1AFFF:Celestial Cartography] in the [c/EC356F:Starfarer Menu] once placed" +
				"\nMust stay in proximity to the Stellaglyph for travel" +
                "\nCan be upgraded to Tier 2 through crafting" +
				"\nCan be enhanced by placing [c/FF1B8D:Stellar Foci] nearby to grant buffs during Cosmic Voyages" +
				"\nAble to sustain 5 [c/FF1B8D:Stellar Foci]");
			//
			//
			//"\n[c/D32C2C:Modded chests from mods added after world generation may cease to open once entering a subworld]" +
			//"\n[c/D32C2C:Mods which allow global auto-use may cause issues upon usage]" +
			//"\n[c/D32C2C:Mods which 'cull' projectiles (anti-lag mods) will cause issues]");
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			//Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Stellaglyph.StellaglyphTier1>(), 0);
			Item.createTile = ModContent.TileType<Tiles.Stellaglyph.StellaglyphTier1>();
			Item.useTurn = true;
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 1;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.noUseGraphic = false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 1)
				.AddIngredient(ItemID.Topaz, 1)
				.AddIngredient(ItemID.Amber, 1)
				.AddIngredient(ItemID.Amethyst, 1)
				.AddIngredient(ItemID.Sapphire, 1)
				.AddIngredient(ItemID.Emerald, 1)
				.AddTile(TileType<Tiles.CelestriadRoot>())
				.Register();
		}
	}
}