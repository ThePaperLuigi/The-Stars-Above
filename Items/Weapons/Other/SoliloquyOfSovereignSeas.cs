using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Buffs.SoliloquyOfSovereignSeas;
using StarsAbove.Projectiles.Other.SoliloquyOfSovereignSeas;

namespace StarsAbove.Items.Weapons.Other
{
    public class SoliloquyOfSovereignSeas : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes at 70% efficiency, but inherits 150% from critical strike chance modifiers]" +
                "\nBullets will fire from your cursor" +
				"\nLine of sight is not required, but bullets will inherit direction" +
				"\nCritical rate and damage will increase with the amount of bullets fired, but the seventh shot will have recoil damage and damage allies" +
				"\nIf the recoil damage would kill you, the gun will misfire" +
				"\n'This magical bullet can truly hit anyone- just like you say'" +
				$""); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.DamageType = ModContent.GetInstance<Systems.ArkheDamageClass>();
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);
			Item.noUseGraphic = true;
		}
		int curse = 0;

		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

        public override Vector2? HoldoutOffset()
        {
			return new Vector2(-20, 0);
        }
		bool altSwing;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			
			player.AddBuff(BuffType<SoliloquyMinionBuff>(), 2);
            if (player.ownedProjectileCounts[ProjectileType<SoliloquyJellySummon>()] <= 0)
			{
				player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquyJellySummon>(), damage, knockback);

				player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquyCrabSummon>(), damage, knockback);
				player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquySeahorseSummon>(), damage, knockback);
			}
			if (altSwing)
			{
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<SoliloquySword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
				altSwing = false;
			}
			else
			{
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<SoliloquySword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
				altSwing = true;
			}

			return false;	
		}
		public override void HoldItem(Player player)
		{
			
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Handgun, 1)
				.AddIngredient(ItemType<EssenceOfTheFreeshooter>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

}
