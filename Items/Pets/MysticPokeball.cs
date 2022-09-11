using Microsoft.Xna.Framework;
using StarsAbove.Items.Prisms;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; using Microsoft.Xna.Framework;

namespace StarsAbove.Items.Pets
{
	public class MysticPokeball : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mystic Monster Ball");
			Tooltip.SetDefault("Summons the twin Mystic Hounds" +
				"\nOccupies both normal pet and light pet slots" +
				"\nCan't be equipped normally");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ProjectileType<Projectiles.Pets.UmbreonPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.buffType = BuffType<Buffs.UmbreonPetBuff>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
										.AddIngredient(ItemType<PrismaticCore>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(BuffType<Buffs.UmbreonPetBuff>(), 3600, true);
				player.AddBuff(BuffType<Buffs.EspeonPetBuff>(), 3600, true);
			}
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(player.HasBuff(BuffType<Buffs.EspeonPetBuff>()))
            {
				player.DelBuff(BuffType<Buffs.UmbreonPetBuff>());
				player.DelBuff(BuffType<Buffs.EspeonPetBuff>());
			}
			
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("UmbreonPet").Type, 0, 0, player.whoAmI, 0f);
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, Mod.Find<ModProjectile>("EspeonPet").Type, 0, 0, player.whoAmI, 0f);


			return false;
        }
    }
}