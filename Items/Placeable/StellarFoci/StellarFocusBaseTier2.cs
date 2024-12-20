
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
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items.Placeable.StellarFoci
{

    public class StellarFocusBaseTier2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stellar Focus Base (Tier 2)");
			

			/* Tooltip.SetDefault("" +
				"Utilized to craft [c/FF1B8D:Stellar Foci]" +
				""); */
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
			
			Item.useTurn = true;
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ModContent.GetInstance<Systems.StellarRarity>().Type; // Custom Rarity
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 1;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.noUseGraphic = false;
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HellstoneBar, 15)
				.AddIngredient(ModContent.ItemType<PrismaticCore>(), 5)
				.AddIngredient(ItemID.GoldCoin, 20)
				.AddIngredient(ModContent.ItemType<StellarFocusBaseTier1>(), 1)
                .DisableDecraft()
                .AddTile(TileType<Tiles.CelestriadRoot>())
				.Register();
		}
	}
}