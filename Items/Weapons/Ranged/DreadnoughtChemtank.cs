using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Chemtank;
using StarsAbove.Projectiles.Chemtank;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class DreadnoughtChemtank : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			/* Tooltip.SetDefault("Attacks with this weapon quickly fire close-ranged shotgun blasts" +
				"\nHolding this weapon surrounds you with six [c/5ADC54:Purge Markers]" +
				"\nAttacking with this weapon in the direction of a [c/5ADC54:Purge Marker] will activate it, unleashing [c/28A223:Echoing Flames]" +
				"\n[c/28A223:Echoing Flames] deals 5% of the foe's Max HP (Max 250) and additionally inflicts Cursed Inferno for 3 seconds" +
				"\nAfter activation, each [c/5ADC54:Purge Marker] has a seperate 8 second cooldown" +
				"\nDouble tap any cardinal direction to activate [c/5CB068:Disdain], performing a quick dash in the specified direction (1 second cooldown)" +
				"\nGain 20 defense for a very short time upon activation of [c/5CB068:Disdain]" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 20;           //The damage of your weapon
			Item.DamageType = DamageClass.Ranged;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 14;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
											//Item.reuseDelay = 20;
			Item.UseSound = SoundID.Item11;
			Item.shoot = ProjectileType<ChemtankRound>();
			Item.shootSpeed = 25f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}

		int followUpTimer;
		float rotation = 0f;
        public override bool CanUseItem(Player player)
		{
			

			return true;
		}

		public override bool? UseItem(Player player)
		{

			rotation = MathHelper.ToDegrees((player.Center - Main.MouseWorld ).ToRotation());
			if (rotation < 0)
			{
				rotation += 360;
			}
			
			
			if (rotation >= 330 || rotation < 30)//30 under and 30 above 0.
			{
				//First Chemtank Marker

				player.AddBuff(BuffType<Chemtank1>(), 4);
			}
			if (rotation >= 30 && rotation < 90)//30 under and 30 above 60.
			{
				player.AddBuff(BuffType<Chemtank2>(), 4);

			}
			if (rotation >= 90 && rotation < 150)
			{
				player.AddBuff(BuffType<Chemtank3>(), 4);

			}
			if (rotation >= 150 && rotation < 210)
			{
				player.AddBuff(BuffType<Chemtank4>(), 4);

			}
			if (rotation >= 210 && rotation < 270)
			{
				player.AddBuff(BuffType<Chemtank5>(), 4);

			}
			if (rotation >= 270 && rotation < 330)
			{
				player.AddBuff(BuffType<Chemtank6>(), 4);

			}




			return true;
		}

		public override void HoldItem(Player player)
		{
			
			player.AddBuff(BuffType<Buffs.Chemtank.ChemtankBuff>(), 2);
			player.GetModPlayer<WeaponPlayer>().ChemtankHeld = true;
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chemtank.ChemtankMarker1>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Chemtank.ChemtankMarker1>(), 10, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chemtank.ChemtankMarker2>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Chemtank.ChemtankMarker2>(), 10, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chemtank.ChemtankMarker3>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Chemtank.ChemtankMarker3>(), 10, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chemtank.ChemtankMarker4>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Chemtank.ChemtankMarker4>(), 10, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chemtank.ChemtankMarker5>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Chemtank.ChemtankMarker5>(), 10, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chemtank.ChemtankMarker6>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Chemtank.ChemtankMarker6>(), 10, 4, player.whoAmI, 0f);


			}
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{


			int numberProjectiles = 12 + Main.rand.Next(2); //random shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(45)); // 30 degree spread.
																														// If you want to randomize the speed to stagger the projectiles
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}

			for (int d = 0; d < 21; d++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(47));
				float scale = 2f - (Main.rand.NextFloat() * .9f);
				perturbedSpeed = perturbedSpeed * scale;
				int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
				Main.dust[dustIndex].noGravity = true;

			}
			for (int d = 0; d < 16; d++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(47));
				float scale = 2f - (Main.rand.NextFloat() * .9f);
				perturbedSpeed = perturbedSpeed * scale;
				int dustIndex = Dust.NewDust(position, 0, 0, 31, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
			}
			return false;



			
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Emerald, 1)
				.AddIngredient(ItemID.SoulofNight, 5)
				.AddIngredient(ItemID.CursedFlame, 30)
				.AddIngredient(ItemType<EssenceOfChemtech>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.Emerald, 1)
				.AddIngredient(ItemID.SoulofNight, 5)
				.AddIngredient(ItemID.Ichor, 30)
				.AddIngredient(ItemType<EssenceOfChemtech>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

	public class ChemtankDashPlayer : ModPlayer
	{
		// These indicate what direction is what in the timer arrays used
		public const int DashDown = 0;
		public const int DashUp = 1;
		public const int DashRight = 2;
		public const int DashLeft = 3;

		public const int DashCooldown = 50; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
		public const int DashDuration = 35; // Duration of the dash afterimage effect in frames

		// The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
		public const float DashVelocity = 10f;

		// The direction the player has double tapped.  Defaults to -1 for no dash double tap
		public int DashDir = -1;

		// The fields related to the dash accessory
		public bool DashAccessoryEquipped;
		public int DashDelay = 0; // frames remaining till we can dash again
		public int DashTimer = 0; // frames remaining in the dash

		public override void ResetEffects()
		{
			// Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
			DashAccessoryEquipped = Player.GetModPlayer<WeaponPlayer>().ChemtankHeld;

			// ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
			// When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
			// If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
			
			if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
			{
				DashDir = DashDown;
			}
			else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15)
			{
				DashDir = DashUp;
			}
			else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
			{
				DashDir = DashRight;
			}
			else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
			{
				DashDir = DashLeft;
			}
			else
			{
				DashDir = -1;
			}
		}

		// This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
		// If they double tapped this frame, they'll move fast this frame
		public override void PreUpdateMovement()
		{
			// if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
			if (CanUseDash() && DashDir != -1 && DashDelay == 0)
			{
				
				Vector2 newVelocity = Player.velocity;

				switch (DashDir)
				{
					// Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
					case DashUp when Player.velocity.Y > -DashVelocity:
					case DashDown when Player.velocity.Y < DashVelocity:
						{
							// Y-velocity is set here
							// If the direction requested was DashUp, then we adjust the velocity to make the dash appear "faster" due to gravity being immediately in effect
							// This adjustment is roughly 1.3x the intended dash velocity
							float dashDirection = DashDir == DashDown ? 1 : -1.3f;
							newVelocity.Y = dashDirection * DashVelocity;
							break;
						}
					case DashLeft when Player.velocity.X > -DashVelocity:
					case DashRight when Player.velocity.X < DashVelocity:
						{
							// X-velocity is set here
							float dashDirection = DashDir == DashRight ? 1 : -1;
							newVelocity.X = dashDirection * DashVelocity;
							break;
						}
					default:
						return; // not moving fast enough, so don't start our dash
				}

				// start our dash
				DashDelay = DashCooldown;
				DashTimer = DashDuration;
				Player.velocity = newVelocity;

				// Here you'd be able to set an effect that happens when the dash first activates
				// Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
				//Player.AddBuff(BuffType<ChemtankDashCooldown>(), 50);
				Player.AddBuff(BuffType<ChemtankDefenseBuff>(), 20);

			}

			if (DashDelay > 0)
				DashDelay--;

			if (DashTimer > 0)
			{ // dash is active
			  // This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
			  // Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
			  // Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
				Player.eocDash = DashTimer;
				Player.armorEffectDrawShadowEOCShield = true;

				// count down frames remaining
				DashTimer--;
			}
		}

		private bool CanUseDash()
		{
			return DashAccessoryEquipped
				&& Player.dashType == 0 // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
				&& !Player.setSolar // player isn't wearing solar armor
				&& !Player.mount.Active
				&& !Player.HasBuff(BuffType<ChemtankDashCooldown>());
		}
	}

}
