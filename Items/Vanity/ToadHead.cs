using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
	
	public class ToadHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = false;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.LightRed; // The rarity of the item
			Item.vanity = true; // The amount of defense the item will give when equipped
			Item.ResearchUnlockCount = 0;

		}



		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			
		}

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemType<Materials.NeonTelemetry>(), 100)
                .AddIngredient(ItemType<Materials.StellarRemnant>(), 3)
                .AddCustomShimmerResult(ItemType<Materials.NeonTelemetry>(), 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}