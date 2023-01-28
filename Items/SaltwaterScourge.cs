using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.BloodBlade;
using StarsAbove.Projectiles.BloodBlade;
using StarsAbove.Projectiles.Umbra;

namespace StarsAbove.Items
{
    public class SaltwaterScourge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saltwater Scourge");
			Tooltip.SetDefault("" +
				"Holding this weapon will periodically cleanse debuffs" +//Ankh charm
                "\nAttacks with this weapon alternate between a sword slash and a short-ranged cannonball" +
                "\nBoth attacks burn foes for 2 seconds on hit and additionally grant Swiftness for 4 seconds" +
				"\nRight click to consume 50 mana to place a [Powder Keg] on your cursor within range" +
                "\nThis mana cost can not be negated by any means" +
                "\nYou can have up to 2 [Powder Kegs] active at the same time, and kegs disappear after 20 seconds or when not holding this weapon" +
				"\nAfter a short delay, the [Powder Keg] becomes active, and attacking it with this weapon will cause it to explode" +
                "\nThis explosion deals 3x damage and an additional 10% of the foe's current HP as true damage (Max 250) while additionally granting Swiftness for 8 seconds" +
				"\nAdditionally, nearby [Powder Kegs] will also explode" +
                "\nPress the Weapon Action Key to unleash [Cannonfire Deluge], causing a rain of cannonballs to descend on your cursor, dealing 1/2 damage each" +
                "\nThese cannonballs stun foes for 1 second on hit, and deal 50% bonus damage to stunned foes" +
				"\n'Neither the flames nor the depths could claim me...'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 55;           //The damage of your weapon
			Item.DamageType = DamageClass.Ranged;         //Is your weapon a melee weapon?
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 22;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 22;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Cyan;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 55f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		
		bool altSwing;
		bool hasTarget = false;
		Vector2 enemyTarget = Vector2.Zero;

		int debuffCleanseTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			
			float launchSpeed = 110f;
			float launchSpeed2 = 10f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * 26f;
			

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if (player.altFunctionUse == 2)
			{
				if(modPlayer.UmbraGauge >= 50)
                {
					modPlayer.UmbraGauge -= 50;

                }
				else
                {
					return false;
                }
			}
			else
            {

				return true;
            }
			
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

			
		}
        public override bool? UseItem(Player player)
        {


			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			//player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (player.Center - Main.MouseWorld).ToRotation() + MathHelper.PiOver2);
			//Testing
			player.GetModPlayer<StarsAbovePlayer>().SaltwaterScourgeHeld = true;

			debuffCleanseTimer++;
			if(debuffCleanseTimer >= 1200)
            {
				if(player.HasBuff(BuffID.Slow))
                {
					player.ClearBuff(BuffID.Slow);
                }
            }

			base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 60f;
			position = new Vector2(position.X, position.Y + 7);
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse != 2)
			{
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<UmbraSwordShoot>(), damage, knockback, player.whoAmI, 0f);

				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<UmbraSlash2>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<UmbraSlash1>(), damage, knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<UmbraSlash1>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<UmbraSlash2>(), damage, knockback, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}
			else
            {
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<UmbraSlashSlow>(), damage, knockback, player.whoAmI, 0f);

				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<EnhancedUmbraSwordShoot>(), (int)(damage * 1.3), knockback, player.whoAmI, 0f);

			}

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<EssenceOfTheTimeless>(), 1)
				.AddIngredient(ItemID.Cardinal, 1)
				.AddIngredient(ItemID.HallowedBar, 15)
				.AddIngredient(ItemID.SoulofFlight, 30)
				.AddIngredient(ItemID.LargeAmethyst, 1)
				.AddIngredient(ItemID.GoldWatch, 1)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
