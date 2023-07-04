using Microsoft.Xna.Framework;
using StarsAbove.Items.Prisms;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace StarsAbove.Items.Pets
{
    public class HolographicToken : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Holographic Token");
			/* Tooltip.SetDefault("Summons the Detective Dog and Bubbled Shark" +
				"\nOccupies both normal pet and light pet slots" +
				"\nEquipping the item only summons the Bubbled Shark" +
                ""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ProjectileType<Projectiles.Pets.BloopPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.buffType = BuffType<Buffs.BloopPetBuff>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<Materials.StellarRemnant>(), 10)
				.DisableDecraft()
				.AddTile(TileID.Anvils)
				.Register();
		}
		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(BuffType<Buffs.BloopPetBuff>(), 3600, true);
				player.AddBuff(BuffType<Buffs.BubbaPetBuff>(), 3600, true);
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.HasBuff(BuffType<Buffs.BubbaPetBuff>()))
			{
				player.DelBuff(BuffType<Buffs.BloopPetBuff>());
				player.DelBuff(BuffType<Buffs.BubbaPetBuff>());
			}
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("BloopPet").Type, 0, 0, player.whoAmI, 0f);
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("BubbaPet").Type, 0, 0, player.whoAmI, 0f);


			return false;
        }
    }
}