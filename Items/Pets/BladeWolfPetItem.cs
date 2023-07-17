using Microsoft.Xna.Framework;
using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Pets;
using StarsAbove.Systems;

namespace StarsAbove.Items.Pets
{
    public class BladeWolfPetItem : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Prototype Interface");
			/* Tooltip.SetDefault("Summons the Blade Wolf" +
				"\n'Alert. Alert rescinded.'"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ProjectileType<Projectiles.Pets.BladeWolfPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.buffType = BuffType<BladeWolfPetBuff>();
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<Materials.StellarRemnant>(), 10)
				.AddCustomShimmerResult(ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600);
			}
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("BladeWolfPet").Type, 0, 0, player.whoAmI, 0f);


			return false;
        }
    }
}