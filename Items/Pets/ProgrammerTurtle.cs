using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using StarsAbove.Systems;

namespace StarsAbove.Items.Pets
{
    public class ProgrammerTurtle : ModItem
	{
        public override void SetStaticDefaults()
        {

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ProjectileType<Projectiles.Pets.ProgrammerTurtlePet>();
            Item.width = 16;
            Item.height = 30;
            Item.UseSound = SoundID.Item2;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.buffType = BuffType<Buffs.Pets.NeuroPetBuff>();
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
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}