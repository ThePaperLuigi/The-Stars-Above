
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

    public class StellaglyphTier3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stellaglyph (Tier 3)");
			

			Tooltip.SetDefault("" +
				"A mystical dias formed by the Celestriad Root" +
				"\nAllows for the traversal of the cosmos through use of [c/F1AFFF:Celestial Cartography] in the [c/EC356F:Starfarer Menu] once placed" +
				"\nMust stay in proximity to the Stellaglyph for travel" +
				"\nCan be upgraded by placing [c/FF1B8D:Stellar Foci] nearby to grant buffs during Cosmic Voyages");
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
			Item.createTile = ModContent.TileType<Tiles.Stellaglyph.StellaglyphTier3>();
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.LightRed;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.noUseGraphic = false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.SoulofLight, 15)
				.AddIngredient(ItemID.SoulofNight, 15)
				//Some more subworld specific materials. Perhaps the shards from the Bleached world, etc.
				.AddIngredient(ItemType<Prisms.PrismaticCore>(), 20)
				.AddIngredient(ItemType<StellaglyphTier2>())
				.AddTile(TileType<Tiles.CelestriadRoot>())
				.Register();
		}
	}
}