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
using StarsAbove.Projectiles.SaltwaterScourge;
using Terraria.Audio;
using StarsAbove.Buffs.SaltwaterScourge;
using System;

namespace StarsAbove.Items
{
    public class SaltwaterScourge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saltwater Scourge");
			Tooltip.SetDefault("" +
				"Holding this weapon will periodically cleanse certain movement-related debuffs and guarantee enemies drop 15 silver on kill" +
                "\nAttacks with this weapon alternate between a sword slash and firing short-ranged piercing cannonballs" +
                "\nSword attacks burn foes for 2 seconds on hit, deal 50% increased damage, and additionally grant Swiftness for 4 seconds" +
				"\nRight click to consume 50 mana to place a [c/BB3B27:Powder Keg] on your cursor within range" +
                "\nThis mana cost can not be negated by any means" +
				"\nYou can have up to 3 [c/BB3B27:Powder Kegs] active at the same time, and kegs disappear after 20 seconds or when not holding this weapon" +
				"\nAfter a short delay, the [c/BB3B27:Powder Keg] becomes active, and attacking it with this weapon will cause it to explode" +
                "\nThis explosion deals 4x damage while additionally granting Swiftness for 8 seconds" +
				"\nAdditionally, nearby [c/BB3B27:Powder Kegs] will also explode, dealing 50% more damage than if exploded directly" +
				"\nPress the Weapon Action Key to gain 3 stacks of [c/E18121:Cannonfire Deluge] for 12 seconds (1 minute cooldown)" +
				"\nWith stacks of [c/E18121:Cannonfire Deluge], pressing the Weapon Action Key will consume a stack, causing a rain of explosive cannonballs to descend on your cursor" +
                "\nAdditionally, gain Swiftness for 12 seconds upon activation" +
				"\nThe final [c/E18121:Cannonfire Deluge] will end with a larger cannonball that deals 3x damage, stunning foes for 2 seconds" +
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
			Item.rare = ItemRarityID.Yellow;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 9f;
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
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if (player.altFunctionUse == 2)
			{
				if(player.statMana >= 50 && player.ownedProjectileCounts[ProjectileType<PowderKeg>()] < 2)
                {
					player.statMana -= 50;
					player.manaRegenDelay = 240;
					
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
			if(debuffCleanseTimer >= 240)
            {
				if(player.HasBuff(BuffID.Slow))
                {
					player.ClearBuff(BuffID.Slow);
                }
				if (player.HasBuff(BuffID.Chilled))
				{
					player.ClearBuff(BuffID.Chilled);
				}
				if (player.HasBuff(BuffID.Confused))
				{
					player.ClearBuff(BuffID.Confused);
				}
				if (player.HasBuff(BuffID.Webbed))
				{
					player.ClearBuff(BuffID.Webbed);
				}
				if (player.HasBuff(BuffID.Stoned))
				{
					player.ClearBuff(BuffID.Stoned);
				}
			}

			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
			{
				if(player.HasBuff(BuffType<CannonfireDeluge3>()))
                {
					SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, new Vector2(player.Center.X, player.Center.Y - 600));
					FireCannonballs(player);
					
					

					player.ClearBuff(BuffType<CannonfireDeluge3>());
					player.AddBuff(BuffType<CannonfireDeluge2>(), 720);
                }
				else if (player.HasBuff(BuffType<CannonfireDeluge2>()))
				{
					SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, new Vector2(player.Center.X, player.Center.Y - 600));
					FireCannonballs(player);
					player.ClearBuff(BuffType<CannonfireDeluge2>());
					player.AddBuff(BuffType<CannonfireDeluge1>(), 720);
				}
				else if (player.HasBuff(BuffType<CannonfireDeluge1>()))
				{
					SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, new Vector2(player.Center.X, player.Center.Y - 600));
					FireCannonballs(player);
					FireBigCannonball(player);
					player.ClearBuff(BuffType<CannonfireDeluge1>());
				}
				else if (player.HasBuff(BuffType<CannonfireDelugeCooldown>()))
				{
					
				}
				else
                {
					//SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtLoad, player.Center);
					player.AddBuff(BuffType<CannonfireDelugeCooldown>(), 3600);

					player.AddBuff(BuffType<CannonfireDeluge3>(), 720);
					player.AddBuff(BuffID.Swiftness, 720);

					for (int g = 0; g < 4; g++)
					{
						int goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
						goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					}
					for (int d = 0; d < 14; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.8f);
						Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.6f);

					}
				}
				


			}

			base.HoldItem(player);
        }

		private void FireCannonballs(Player player)
        {
			for (int i = 0; i < 12; i++)
			{
				//int type = ProjectileType<SaltwaterCannonball>();
				int type = ProjectileID.CannonballFriendly;
				Vector2 position = new Vector2(Main.MouseWorld.X - 150 + i * 25, player.Center.Y - 600 + Main.rand.Next(-150, 0));

				float Speed = 28f;  //projectile speed

				Vector2 vector8 = position;
				float launchSpeed = 36f;
				Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
				Vector2 direction = Vector2.Normalize(mousePosition - position);
				Vector2 velocity = new Vector2(0, 10) * launchSpeed;
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, type, player.GetWeaponDamage(Item), 0f, player.whoAmI);


				Main.projectile[index].originalDamage = Item.damage;
				float rotation = (float)Math.Atan2(vector8.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), vector8.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));

				for (int d = 0; d < 25; d++)
				{
					float Speed2 = Main.rand.NextFloat(10, 18);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
					int dustIndex = Dust.NewDust(vector8, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 45; d++)
				{
					float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
					int dustIndex = Dust.NewDust(vector8, 0, 0, DustID.Flare, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
			}
		}
		private void FireBigCannonball(Player player)
		{
			int type = ProjectileType<SaltwaterCannonballBig>();
			//int type = ProjectileID.CannonballFriendly;
			Vector2 position = new Vector2(Main.MouseWorld.X, player.Center.Y - 600);

			float Speed = 28f;  //projectile speed

			Vector2 vector8 = position;
			float launchSpeed = 9f;
			Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
			Vector2 direction = Vector2.Normalize(mousePosition - position);
			Vector2 velocity = new Vector2(0, 1) * launchSpeed;
			int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, type, player.GetWeaponDamage(Item)*3, 0f, player.whoAmI);


			Main.projectile[index].originalDamage = Item.damage;
			float rotation = (float)Math.Atan2(vector8.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), vector8.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));

			for (int d = 0; d < 25; d++)
			{
				float Speed2 = Main.rand.NextFloat(10, 18);  //projectile speed
				Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
				int dustIndex = Dust.NewDust(vector8, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
			}
			for (int d = 0; d < 45; d++)
			{
				float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
				Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
				int dustIndex = Dust.NewDust(vector8, 0, 0, DustID.Flare, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
				Main.dust[dustIndex].noGravity = true;
			}
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 35f;
			position = new Vector2(position.X, position.Y + 7);
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse != 2)
			{

				if (player.direction == 1)
				{
					if (altSwing)
					{
						//Shoot gun
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<SaltwaterGun>(), 0, knockback, player.whoAmI, 0f);
						//Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.CannonballFriendly, damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<SaltwaterCannonball>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						//Swing sword
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<SaltwaterSlash1>(), (int)(damage*1.5f), knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						//Shoot gun
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<SaltwaterGun>(), 0, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<SaltwaterCannonball>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						//Swing sword
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<SaltwaterSlash2>(), (int)(damage * 1.5f), knockback, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}
			else
            {
				//Barrel place
				Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<PowderKeg>(), (int)(damage), knockback, player.whoAmI, 0f);

			}

			return false;
		}

		public override void AddRecipes()
		{
			
			CreateRecipe(1)
				.AddIngredient(ItemType<EssenceOfPiracy>(), 1)
				.AddIngredient(ItemID.Cutlass, 1)
				.AddIngredient(ItemID.CoinGun, 1)
				.AddIngredient(ItemID.Barrel, 2)
				.AddIngredient(ItemID.Dynamite, 10)
				.AddTile(TileID.Anvils)
				.Register();
			
		}
	}
}
