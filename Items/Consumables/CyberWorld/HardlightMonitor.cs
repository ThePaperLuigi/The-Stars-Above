using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Consumables.CyberWorld
{
    public class HardlightMonitor : ModItem
	{
		public override void SetStaticDefaults() {
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 15;

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.LightRed;
			Item.maxStack = 1;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.UseSound = SoundID.MenuOpen;
            Item.consumable = false;
        }

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}
        public override bool? UseItem(Player player)
        {
			if(player.itemAnimation == player.itemAnimationMax)
			{
                Main.NewText(LangHelper.GetTextValue("Common.HardlightMonitor", player.GetModPlayer<StarsAbovePlayer>().hardlight));

            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                  .AddIngredient(ItemID.ManaCrystal, 1)
                  .AddIngredient(ModContent.ItemType<NeonTelemetry>())
                  .AddTile(TileID.Anvils)
                  .Register();
        }
    }
}