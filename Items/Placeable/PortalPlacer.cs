
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

namespace StarsAbove.Items.Placeable
{

    public class PortalPlacer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PortalPlacer");
			

			Tooltip.SetDefault("" +
				"Debug only." +
				"" +
				"" +
				"");
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
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Portal>(), 0);

			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.noUseGraphic = false;
		}
		
		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return true;
			
			return false;
		}
		public override void HoldItem(Player player)
		{
			return;
			

			base.HoldItem(player);
		}
		public override bool? UseItem(Player player)
		{
			return true;
			
			return false;
		}

		public override bool CanPickup(Player player)
		{
			

			return true;
		}

		public override void AddRecipes()
		{
			
		}
	}
}