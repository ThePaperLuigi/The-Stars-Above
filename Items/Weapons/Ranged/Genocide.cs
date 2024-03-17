using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using System;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Ranged.Genocide;
using StarsAbove.Buffs.Ranged.Genocide;
using StarsAbove.Systems;
using StarsAbove.Buffs.Melee.Unforgotten;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class Genocide : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Genocide");

			/* Tooltip.SetDefault("Attacks fire powerful explosive grenades with high knockback" +
				"\nStriking an enemy with an attack grants 25% increased movement and critical strike chance for 4 seconds" +
				"\nWill automatically take time to reload after 6 shots" +
				"\nThe final shot before reloading has a larger explosion radius and deals 2x base damage" +
				"\nThe weapon will additionally fire an artillery shell at the target (3 second cooldown)" +
				"\nUpon arriving at the target destination or hitting a tile the artillery shell will explode, dealing 3x base damage" +
				"\nRight click to fire a [c/4BF0DE:Plasma Grenade] (8 second cooldown)" +
				"\nThe [c/4BF0DE:Plasma Grenade] will automatically bounce to the nearest foe (Up to 3 times)" +
				"\nEnemies struck by the [c/4BF0DE:Plasma Grenade] are inflicted with Mortal Wounds for 12 seconds, increasing damage taken from this weapon by 50%" +
				"\nThe [c/4BF0DE:Plasma Grenade] can not bounce to a foe with Mortal Wounds" +
				"\n'Keep your mouth shut and eyes open!'" +
				$""); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 130;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 128;
			Item.height = 50;
			Item.useAnimation = 22;
			Item.useTime = 22;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 10;
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<GenocideRound>();
			Item.shootSpeed = 20f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item11;
		}
		//int reloadTimer;
		int reloadDuration;
		int becomeWhite = 255;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			//reloadTimer--;
			reloadDuration--;

			if(player.GetModPlayer<WeaponPlayer>().genocideBullets == 1)
            {
				Item.useAnimation = 120;
				Item.useTime = 120;

			}
			else
            {
				Item.useAnimation = 22;
				Item.useTime = 22;
			}
			/*if (player.HasBuff(BuffType<GenocideReloadBuff>()))
			{
				item.noUseGraphic = true;
			}
			else
			{
				item.noUseGraphic = false;

			}*/

			if(reloadDuration < 110 && reloadDuration > 60)
            {
				becomeWhite -= 20;


			}
			else
			{
				becomeWhite += 20;

				if (becomeWhite > 255)
				{
					becomeWhite = 255;
				}
			}


		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<PlasmaGrenadeCooldownBuff>()))
				{
					//player.GetModPlayer<WeaponPlayer>().genocideBullets = 2;
					return true;
					
				}
				return false;

			}
			
			if(reloadDuration <= 0)
            {
					return true;
			}
			return false;
			


			
		}

		

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
		}

		

			
		public override bool? UseItem(Player player)
		{
			

			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 85f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse == 2)
			{
				/*
				Vector2 target = Main.MouseWorld;
				float radius = Vector2.Distance(player.Center, Main.MouseWorld) / 2;
				Vector2 midpoint = Main.MouseWorld + (player.Center - Main.MouseWorld) / 2;
				int proj = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<GenocidePlasmaGrenade>(), damage, knockback, player.whoAmI, 3);
				GenocidePlasmaGrenade mp = Main.projectile[proj].ModProjectile as GenocidePlasmaGrenade;
				mp.MidPoint = midpoint;  //'midpoint' is the calculated midpoint
				mp.Radius = radius;      //'radius' is just half the distance to the mouse
				if (player.direction == 1)
				{
					mp.RotateClockwise = true;  //'clockwise' is self-explanatory

				}
				else
				{
					mp.RotateClockwise = false;  //'clockwise' is self-explanatory

				}
				mp.InitialDirection = player.DirectionFrom(midpoint);  //used to make the rotation look good
				mp.Target = target;
				*/
				player.AddBuff(BuffType<PlasmaGrenadeCooldownBuff>(), 480);

				NPC closest = null;
				float closestDistance = 9999999;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					float distance = Vector2.Distance(npc.Center, player.Center);


					if (npc.active && npc.Distance(player.position) < closestDistance && !npc.HasBuff(BuffType<MortalWounds>()))
					{
						closest = npc;
						closestDistance = npc.Distance(player.position);
					}




				}

				if (closest.CanBeChasedBy() && closestDistance < 1200f)//If the enemy is a reasonable distance away and is hostile (projectile.ai[0] is the amount of bounces left (Should start at 2, counting initial bounce)
				{

					float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse
					closest.AddBuff(BuffType<Stun>(), 30);
					float launchSpeed = 7f;
					Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
					Vector2 direction = Vector2.Normalize(closest.Center - player.Center);
					Vector2 velocity1 = direction * launchSpeed;

					Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity1.X, velocity1.Y, ProjectileType<Projectiles.Ranged.Genocide.GenocidePlasmaGrenade>(), damage, 0f, player.whoAmI, 2);

					




				}
				return false;


			}
			if (player.GetModPlayer<WeaponPlayer>().genocideBullets > 0)
            {
				if(player.GetModPlayer<WeaponPlayer>().genocideBullets == 1)
                {
					Projectile.NewProjectile(source,position.X, position.Y, velocity.X, velocity.Y,type, damage*2, knockback, player.whoAmI, 1);

				}
				else
                {
					Projectile.NewProjectile(source,position.X, position.Y, velocity.X, velocity.Y,type, damage, knockback, player.whoAmI);

				}
				Vector2 target = Main.MouseWorld;
				float radius = Vector2.Distance(player.Center, Main.MouseWorld) / 2;
				Vector2 midpoint = Main.MouseWorld + (player.Center - Main.MouseWorld) / 2;
                if (!player.HasBuff(BuffType<GenocideReloadBuff>()))
                {
					player.AddBuff(BuffType<GenocideReloadBuff>(), 180);

					int proj = Projectile.NewProjectile(source,position.X, position.Y, velocity.X, velocity.Y,ProjectileType<GenocideArtillery>(), damage, knockback, player.whoAmI);
					GenocideArtillery mp = Main.projectile[proj].ModProjectile as GenocideArtillery;
					mp.MidPoint = midpoint;  //'midpoint' is the calculated midpoint
					mp.Radius = radius;      //'radius' is just half the distance to the mouse
					if(player.direction == 1)
                    {
						mp.RotateClockwise = true;  //'clockwise' is self-explanatory

					}
					else
                    {
						mp.RotateClockwise = false;  //'clockwise' is self-explanatory

					}
					mp.InitialDirection = player.DirectionFrom(midpoint);  //used to make the rotation look good
					mp.Target = target;
				}

				player.GetModPlayer<WeaponPlayer>().genocideBullets--;


				for (int d = 0; d < 10; d++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(12));
					float scale = 2f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 8; d++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(12));
					float scale = 2f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 223, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 8; d++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(12));
					float scale = 2f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}


				//If the gun ran out of bullets on this shot, trigger the reload animation.
				if (player.GetModPlayer<WeaponPlayer>().genocideBullets <= 0)
                {
					reloadDuration = 120;
					player.GetModPlayer<WeaponPlayer>().genocideBullets = 6;
					//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, velocity.X/100, velocity.Y/100, ProjectileType<GenocideReload>(), 0, 0, player.whoAmI);
					//player.AddBuff(BuffType<GenocideReloadBuff>(), 60);
				}
				
			}
			else
            {
				
				
				return false;
                
			}
			
			return false;
		}

		// How can I get a "Clockwork Assault Rifle" effect?
		// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
		/*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override void OnConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

		// How can I shoot 2 different projectiles at the same time?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

		// How can I choose between several projectiles randomly?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/

		public override Color? GetAlpha(Color lightColor)
		{


			return new Color(255, becomeWhite, becomeWhite);
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.ShroomiteBar, 8)
				.AddIngredient(ItemID.RocketIV, 4)
				.AddIngredient(ItemID.Dynamite, 4)
				.AddIngredient(ItemID.ChlorophyteBar, 16)
				.AddIngredient(ItemType<EssenceOfExplosions>())
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}
