using StarsAbove.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Armor.NeopursuantArmor

{
    [AutoloadEquip(EquipType.Legs)]
	public class NeopursuantLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sky Striker Armor");
			// Tooltip.SetDefault("You shouldn't be able to read this!");
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.LightRed;
            Item.vanity = false;
            Item.ResearchUnlockCount = 1;
            Item.defense = 12;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.2f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                  .AddIngredient(ItemID.CobaltBar, 12)
                  .AddIngredient(ModContent.ItemType<NeonTelemetry>(), 8)
                  .AddTile(TileID.Anvils)
                  .Register();
            CreateRecipe(1)
                 .AddIngredient(ItemID.PalladiumBar, 12)
                 .AddIngredient(ModContent.ItemType<NeonTelemetry>(), 8)
                 .AddTile(TileID.Anvils)
                 .Register();
        }
    }
}