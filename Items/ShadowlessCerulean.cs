using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using System;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Items
{
	public class ShadowlessCerulean : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("" +
				"Attacks with this weapon will sweep in a wide arc and charge the [c/3CC7FF:Cerulean Flame Gauge]" +
				"\nCritical hits charge the [c/3CC7FF:Cerulean Flame Gauge] much faster" +
				"\nOnce the [c/3CC7FF:Cerulean Flame Gauge] is at maximum, right click to unleash [c/007BEE:Shadowless Ignition]" +
				"\n[c/007BEE:Shadowless Ignition] will teleport you to your cursor, rendering you invincible and immobile" +
				"\nDuring this time, you will unleash a barrage of powerful slashes at your location" +
				"\nFoes caught within [c/007BEE:Shadowless Ignition] are binded and inflicted with Frostburn for 20 seconds" +
				"\nThe final strike of [c/007BEE:Shadowless Ignition] will deal bonus critical damage to one foe" +
				"\nOnce Shadowless Ignition ends, you will be launched in the opposite direction of your cursor and gain the buff [c/EE007E:Wrathful Cerulean Flame] for 8 seconds" +
				"\nAttacks during [c/EE007E:Wrathful Cerulean Flame] will become drastically empowered, ensuring critical hits and gaining 50 armor penetration" +
				"\nOnce [c/EE007E:Wrathful Cerulean Flame] ends, you will be inflicted with [c/9E7F90:Burnout], lowering defensive and offensive stats by 50% for 20 seconds" +
				"\nYou can not charge the [c/3CC7FF:Cerulean Flame Gauge] while under the effects of [c/9E7F90:Burnout]" +
				"\n'You will understand my choice one day... Forgive me'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 275;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = 10;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			float launchSpeed = 36f;
			float launchSpeed2 = 102f;
			float launchSpeed3 = 120f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 arrowVelocity2 = direction * launchSpeed2;
			Vector2 arrowVelocity3 = direction * launchSpeed3;

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			if (player.altFunctionUse == 2)
			{
				if (modPlayer.ceruleanFlameGauge >= 100)
                {

					float Speed = 28f;  //projectile speed

					Vector2 vector8 = player.Center;
					float rotation = (float)Math.Atan2(vector8.Y - (Main.MouseWorld.Y), vector8.X - (Main.MouseWorld.X));

					for (int d = 0; d < 25; d++)
					{
						float Speed2 = Main.rand.NextFloat(10, 28);  //projectile speed
						Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
						int dustIndex = Dust.NewDust(vector8, 0, 0, 221, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;
					}
					for (int d = 0; d < 45; d++)
					{
						float Speed3 = Main.rand.NextFloat(8, 34);  //projectile speed
						Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
						int dustIndex = Dust.NewDust(vector8, 0, 0, 88, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
						Main.dust[dustIndex].noGravity = true;
					}

					modPlayer.ceruleanFlameGauge = 0;
					player.AddBuff(BuffType<Buffs.Ignited>(), 60);
					player.AddBuff(BuffType<Buffs.Invincibility>(), 60);
					Vector2 teleportPosition = new Vector2(player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y - 5);
					player.Teleport(teleportPosition, 1, 0);
					NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)player.whoAmI, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 1, 0, 0);
					slashDuration = 62;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_AmiyaSlash, player.Center);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<AmiyaSlash>(), player.GetWeaponDamage(Item) * 4, 0, player.whoAmI, 0f);
					return true;
				}
				else
                {
					return false;
                }
				
			}
			else
            {
				

				
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
			float launchSpeed = 36f;
			float launchSpeed2 = 102f;
			float launchSpeed3 = 120f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 arrowVelocity2 = direction * launchSpeed2;
			Vector2 arrowVelocity3 = direction * launchSpeed3;

			if (currentSwing == 0)
			{
				if (player.HasBuff(BuffType<WrathfulCeruleanFlame>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<AmiyaSwingE1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, player.Center);
				}
				else
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<AmiyaSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);
				}

				currentSwing++;
			}
			else
			{
				if (player.HasBuff(BuffType<WrathfulCeruleanFlame>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<AmiyaSwingE2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, player.Center);

				}
				else
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<AmiyaSwing2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);
				}
				currentSwing = 0;
			}
			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			slashDuration--;
			if (slashDuration == 1)
            {
				player.AddBuff(BuffType<Buffs.WrathfulCeruleanFlame>(), 480);
				Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
				Vector2 leap = Vector2.Normalize(mousePosition2) * -11f;
				player.velocity = new Vector2(leap.X,leap.Y - 8);
			}

			if(player.HasBuff(BuffType<WrathfulCeruleanFlame>()))
            {
				player.GetArmorPenetration(DamageClass.Generic) = 50;
			}
			
            base.HoldItem(player);
        }

        // Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
        // See Source code for Star Wrath projectile to see how it passes through tiles.
        /*	The following changes to SetDefaults */

        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float behindTarget = target.Y + 40;
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 101) * player.direction), 0f);
				position.Y -= (0 * i);
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(velocity.X, velocity.Y).Length();
				velocity.X = heading.X;
				velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 200, velocity.X, velocity.Y,type, damage * 2, knockback, player.whoAmI, 0f);
			}
			return false;*/
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.Ruby, 1)
				.AddIngredient(ItemID.Sapphire, 1)
				.AddIngredient(ItemID.LunarBar, 20)
				.AddIngredient(ItemType<EssenceOfTheChimera>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
