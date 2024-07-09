using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items.Armor.NeopursuantArmor
{
    [AutoloadEquip(EquipType.Head)]
	
	public class NeopursuantHeadbooster : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.sellPrice(silver: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.LightRed; // The rarity of the item
			Item.vanity = false; // The amount of defense the item will give when equipped
			Item.ResearchUnlockCount = 1;
            Item.defense = 14;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.manaCost *= 0.9f;
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
		{
			
		}

        public override void AddRecipes()
        {
            CreateRecipe(1)
                  .AddIngredient(ItemID.CobaltBar, 8)
                  .AddIngredient(ModContent.ItemType<NeonTelemetry>(), 6)
                  .AddTile(TileID.Anvils)
                  .Register();
            CreateRecipe(1)
                 .AddIngredient(ItemID.PalladiumBar, 8)
                 .AddIngredient(ModContent.ItemType<NeonTelemetry>(), 6)
                 .AddTile(TileID.Anvils)
                 .Register();
        }
    }
}