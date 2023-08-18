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

namespace StarsAbove.Items.Weapons.Melee
{
    public class Umbra : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Umbra");
			/* Tooltip.SetDefault("" +
				"Attacks with this weapon will sweep in an arc and unleash fast, piercing [c/A75BD9:Timeless Blades]" +
                "\nDealing damage with the weapon swing will grant [c/AF32CF:Timeless Potential] for 4 seconds" +
                "\n[c/AF32CF:Timeless Potential] causes non-critical strikes to have a 30% chance to become critical and additionally grants a 10% chance to dodge attacks when active" +
                "\nAdditionally, if you would die due to taking lethal damage, [c/AF32CF:Timeless Potential] will be consumed, resurrecting you with 50 HP and 2 seconds of Invincibility" +
                "\nIf [c/AF32CF:Timeless Potential] is used to resurrect you, [c/AF32CF:Timeless Potential] will have a 2 minute cooldown until it can be obtained again" +
				"\n[c/A75BD9:Timeless Blades] fill the [c/4A91FF:Timeless Gauge] by 2% upon striking a foe (Halved when [c/4A91FF:Timeless Gauge] is above 50%)" +
				"\n[c/A75BD9:Timeless Blades] will pierce foes up to 3 times and have no damage falloff" +
				"\nRight click to consume half of the [c/4A91FF:Timeless Gauge] to unleash an enhanced [c/A75BD9:Timeless Blade] that does not fill the [c/4A91FF:Timeless Gauge]" +
				"\nEnhanced [c/A75BD9:Timeless Blades] are faster, can pierce up to 5 times, and deal 30% increased damage" +
                "\nCritical strikes with enhanced [c/A75BD9:Timeless Blades] will additionally conjure additional [c/A75BD9:Timeless Blades] to strike the foe" +
                "\nThe amount of [c/A75BD9:Timeless Blades] summoned depend on the remaining pierces of the enhanced [c/A75BD9:Timeless Blade]" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 119;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
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
			

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();

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

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
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

			CreateRecipe(1)
				.AddIngredient(ItemType<EssenceOfTheTimeless>(), 1)
				.AddIngredient(ItemID.Cardinal, 1)
				.AddIngredient(ItemID.HallowedBar, 15)
				.AddIngredient(ItemID.SoulofFlight, 30)
				.AddIngredient(ItemID.LargeAmethyst, 1)
				.AddIngredient(ItemID.PlatinumWatch, 1)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
