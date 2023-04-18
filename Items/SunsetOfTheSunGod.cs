using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;
using StarsAbove.Items.Pets;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.SunsetOfTheSunGod;
using System;
using StarsAbove.Buffs.SunsetOfTheSunGod;

namespace StarsAbove.Items
{
    public class SunsetOfTheSunGod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sunset of the Sun God");
			Tooltip.SetDefault("" +
				"[c/F592BF:This weapon is unaffected by Aspected Damage Type penalty]" +
				"\nRight click to unleash [c/FFC500:Vasavi Shakti], leaping into the air, gaining Featherfall and Invincibility for 2 seconds (1 minute cooldown)" +
				"\n[c/FFC500:Vasavi Shakti] channels a powerful beam of energy towards your cursor for 2 seconds" +
				"\n[c/FFC500:Vasavi Shakti] will conclude with a colossal explosion, dealing 2x guaranteed critical damage and inflicting Daybroken for 8 seconds" +
				"\nThe current [c/F592BF:Aspected Damage Type] influences the weapon's attacks (Weapon can not be used without [c/F592BF:Aspected Damage Type])" +
				"\n[c/FFAB4D:Melee]: Rapidly stab in a close-ranged flurry; Charged attacks swing in a wide arc, burning foes for 8 seconds" +
				"\n[c/EF4DFF:Magic]: Lightning spears converge on your cursor after a delay, causing lightning to arc from foes struck" +
				"\n[c/4DFF5B:Ranged]: Attacks launch the spear at high velocity, dealing 2x damage and piercing infinitely without damage falloff" +
				"\n[c/00CDFF:Summon]: Attacks extend a burning whip, causing summons to inflict Daybroken for 2 seconds on hit" +
				"\n'With this single strike, I shall inflict extinction!'" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 220;
			Item.DamageType = DamageClass.Melee;
			Item.useStyle = ItemUseStyleID.HiddenAnimation;
			Item.width = 46;
			Item.height = 216;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

			Item.noUseGraphic = true;
			Item.noMelee = true;

			Item.shoot = ProjectileType<KarnaSpearAttack>();
			Item.shootSpeed = 20;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect != 2
				&& player.GetModPlayer<StarsAbovePlayer>().MagicAspect != 2
				&& player.GetModPlayer<StarsAbovePlayer>().RangedAspect != 2
				&& player.GetModPlayer<StarsAbovePlayer>().SummonAspect != 2)
			{

				return false;
			}
			
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<KarnaLaserBuff>()) && !player.HasBuff(BuffType<KarnaCooldown>()))
				{
					float launchSpeed = 8f;
					Vector2 mousePosition = Main.MouseWorld;
					Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
					Vector2 arrowVelocity = direction * launchSpeed;
					player.velocity = new Vector2(player.velocity.X, player.velocity.Y - 10);
					player.AddBuff(BuffType<Invincibility>(), 120);
					player.AddBuff(BuffID.Featherfall, 120);
					player.AddBuff(BuffType<KarnaCooldown>(), 60 * 60);
					player.AddBuff(BuffType<KarnaLaserBuff>(), 120);
					player.GetModPlayer<WeaponPlayer>().KarnaTarget = Main.MouseWorld;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);


					int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, Vector2.Zero, ProjectileType<KarnaLaser>(), player.GetWeaponDamage(Item), 0, player.whoAmI);
					Main.projectile[index].originalDamage = Item.damage;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, arrowVelocity, ProjectileType<KarnaLaserSpear>(), 0, 0, player.whoAmI);

					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

					return false;
				}
				else
				{
					return false;
				}
			}
			
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public int stabAmount;
		public int stabTimer;
		public override void HoldItem(Player player)
		{
			float launchSpeed = 8f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
			stabTimer--;
			if (stabAmount > 0)
			{
				
				if (stabTimer <= 0)
				{
					stabAmount--;

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<KarnaSpearAttack>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);
					stabTimer = 10;
				}
			}
			
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
				Item.channel = true;
				Item.useTime = 30;
				Item.useAnimation = 30;
			}
			else
            {
				Item.channel = false;

			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
				Item.useTime = 60;
				Item.useAnimation = 60;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{
				Item.useTime = 30;
				Item.useAnimation = 30;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{
				Item.useStyle = ItemUseStyleID.Swing;

				Item.useTime = 30;
				Item.useAnimation = 30;
			}
			else
            {
				Item.useStyle = ItemUseStyleID.HiddenAnimation;

			}

			if (Main.myPlayer == player.whoAmI)
			{
				
				
				if (player.altFunctionUse != 2 && player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
					if (player.channel)
					{
						Item.useTime = 10;
						Item.useAnimation = 10;
						player.GetModPlayer<WeaponPlayer>().bowChargeActive = true;
						player.GetModPlayer<WeaponPlayer>().bowCharge += 4;
						if (player.GetModPlayer<WeaponPlayer>().bowCharge == 1)
						{
							//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
						}
						if (player.GetModPlayer<WeaponPlayer>().bowCharge == 98)
						{
							for (int d = 0; d < 32; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}



						}
						
						if (player.GetModPlayer<WeaponPlayer>().bowCharge < 100)
						{
							for (int i = 0; i < 30; i++)
							{//Circle
								Vector2 offset = new Vector2();
								double angle = Main.rand.NextDouble() * 2d * Math.PI;
								offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));
								offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));

								Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.LifeDrain, player.velocity, 200, default(Color), 0.5f);
								d2.fadeIn = 0.1f;
								d2.noGravity = true;
							}
							//Charge dust
							Vector2 vector = new Vector2(
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
							Dust d = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.LifeDrain, 0, 0, 255,
								new Color(0.8f, 0.4f, 1f), 0.8f)];
							d.velocity = -vector / 12;
							d.velocity -= player.velocity / 8;
							d.noLight = true;
							d.noGravity = true;

						}
						
					}
					else
					{
						Item.useTime = 25;
						Item.useAnimation = 25;

						if (player.GetModPlayer<WeaponPlayer>().bowCharge >= 98)//If the weapon is fully charged...
						{

							player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
							player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
							//Reset the charge gauge.
							SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, player.position);

							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<KarnaChargedMelee>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<KarnaChargedMeleeVFX>(), 0, 0, player.whoAmI, 0f);

						}
						else
						{
							if (player.GetModPlayer<WeaponPlayer>().bowCharge > 0 && player.GetModPlayer<WeaponPlayer>().bowCharge <= 30)
							{//Uncharged attack (lower than the threshold.)
							 //SoundEngine.PlaySound(SoundID.Item11, player.position);

								player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
								player.GetModPlayer<WeaponPlayer>().bowCharge = 0;

								stabAmount = 3;
								//stabTimer = 0;
								return;
							}
							else
							{
								player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
								player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
							}
						}
					}
				}
			}
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			//player.GetModPlayer<WeaponPlayer>().radiance++;
			
		}
		bool altFire;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
				return false;
				
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{

				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<KarnaWhipAttack>(), damage, 0, player.whoAmI, 0f);
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<KarnaWhip>(), 0, 0, player.whoAmI, 0f);


			}
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<KarnaSpearThrow>(), damage*2, 0, player.whoAmI, 0f);

				
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
				SoundEngine.PlaySound(SoundID.Item117, player.position);

				if (altFire)
                {
					for (int i = 0; i < 5; i++)
					{
						float offsetAmount = i * 72;
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<KarnaLightningSpearAlt>(), damage, 0, player.whoAmI, 0, offsetAmount);

					}
					altFire = false;
				}
				else
                {
					for (int i = 0; i < 5; i++)
					{
						float offsetAmount = i * 72;
						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<KarnaLightningSpear>(), damage, 0, player.whoAmI, 0, offsetAmount);

					}
					altFire = true;

				}


			}
			return false;
		}

		public override void AddRecipes()
		{
			/*
			CreateRecipe(1)
				.AddIngredient(ItemID.ChlorophyteBar, 8)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.Ectoplasm, 10)
				.AddIngredient(ItemID.SoulofLight, 12)
				.AddIngredient(ItemID.LunarBar, 12)
				.AddIngredient(ItemType<AegisCrystal>())
				.AddIngredient(ItemType<EssenceOfLuminance>())
				.AddTile(TileID.Anvils)
				.Register();*/
		}
	}
}
