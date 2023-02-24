using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Projectiles.KissOfDeath;
using StarsAbove.Utilities;

namespace StarsAbove.Items
{
    public class KissOfDeath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Kiss of Death");
			Tooltip.SetDefault("Right click to cycle between [c/F54F0C:Skyfish], [c/CF3C32:Thunderbird], and [c/CF1826:Behemoth Typhoon] with a short cooldown" +
				"\n[c/F54F0C:Skyfish] unleashes a powerful minigun, rapidly firing piercing bullets that deal 20% increased damage to foes above 50% HP" +
                "\nEach bullet will consume 2 mana (This mana cost can not be negated by any means)" +
				"\nBullets will apply [c/F3CE36:Security Level] for 12 seconds, marking the target" +
				"\n[c/CF3C32:Thunderbird] allows for the deployment of homing bombs that ignore terrain, exploding and dealing 20% increased damage to foes below 50% HP" +
				"\nExplosions will apply [c/F3CE36:Security Level] for 12 seconds, marking the target" +
				"\n[c/CF1826:Behemoth Typhoon] swings a massive coffin, dealing 3x damage when released" +
				"\nAttacks on foes with [c/F3CE36:Security Level] using [c/CF1826:Behemoth Typhoon]'s released attack cleanses the debuff, charging the [c/EB936A:Overdrive Gauge]" +
				"\nHolding the Weapon Action Key will charge a powerful attack, consuming the [c/EB936A:Overdrive Gauge]" +
				"\nReleasing the Weapon Action Key executes a powerful close-ranged strike, increasing in power with the amount of [c/EB936A:Overdrive Gauge] consumed" +
				"\nIf over half of the [c/EB936A:Overdrive Gauge] is consumed, the attack is guaranteed to be a critical strike" +
                "\n'It's not a big deal'" +
				$"");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 66;          
			Item.DamageType = DamageClass.Ranged;          
			Item.width = 108;          
			Item.height = 118;         
			Item.useTime = 30;          
			Item.useAnimation = 30;        
			Item.useStyle = 1;         
			Item.knockBack = 5;        
			Item.value = Item.buyPrice(gold: 1);          
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.scale = 2f;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TruesilverSlash>();
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);          
		}
		int mode;//0 = sword | 1 = scythe | 2 = shotgun
		int swapCooldown;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2 )//Did the player right click?
			{
				

				if (mode == 0 && swapCooldown <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item23, player.position);
					swapCooldown = 20;
					mode = 1;
					if (player.whoAmI == Main.myPlayer)
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 113, 62, 240), LangHelper.GetTextValue($"CombatText.KissOfDeath.Thunderbird"), false, false);
					}
				}
				if (mode == 1 && swapCooldown <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item23, player.position);
					swapCooldown = 20;
					mode = 2;
					if (player.whoAmI == Main.myPlayer)
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 113, 62, 240), LangHelper.GetTextValue($"CombatText.KissOfDeath.BehemothTyphoon"), false, false);

					}
				}
				if (mode == 2 && swapCooldown <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item23, player.position);
					swapCooldown = 20;
					mode = 0;
					if (player.whoAmI == Main.myPlayer)
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 113, 62, 240), LangHelper.GetTextValue($"CombatText.KissOfDeath.Skyfish"), false, false);
					}
				}
			}
			return base.CanUseItem(player);
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
        public override bool? UseItem(Player player)
        {
			



				return base.UseItem(player);
        }
		public bool chargingUppercut;
		public float overdriveModifier;
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<WeaponPlayer>().KissOfDeathHeld = true;
			swapCooldown--;
			if(mode == 0)
            {
				Item.useStyle = ItemUseStyleID.Swing;
				Item.useStyle = ItemUseStyleID.HiddenAnimation;

				Item.shootSpeed = 18f;
				Item.useTime = 5;
				Item.useAnimation = 5;
				Item.autoReuse = true;
				Item.channel = false;
			}
			if (mode == 1)
			{
				Item.useStyle = ItemUseStyleID.Swing;

				Item.shootSpeed = 10f;
				Item.useTime = 30;
				Item.useAnimation = 30;

				Item.autoReuse = true;
			}
			if (mode == 2)
			{
				Item.useStyle = 5;
				Item.shootSpeed = 9f;
				Item.useTime = 30;
				Item.useAnimation = 30;
				Item.noMelee = true;
				Item.noUseGraphic = true;
				Item.autoReuse = true;
			}

			

			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.Current && player.itemTime <= 0)
			{
				if (player.GetModPlayer<WeaponPlayer>().overdriveGauge > 0)
				{
					player.SetDummyItemTime(10);
					player.GetModPlayer<WeaponPlayer>().overdriveGauge -= 10;
					overdriveModifier += 10;
					overdriveModifier = MathHelper.Clamp(overdriveModifier, 0, 100);
					chargingUppercut = true;
					for(int i = 0; i < 5; i++)
					{
						// Charging dust
						Vector2 vector = new Vector2(
							Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
							Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
						Dust d = Main.dust[Dust.NewDust(
							player.Center + vector, 1, 1,
							DustID.Firework_Yellow, 0, 0, 255,
							new Color(1f, 1f, 1f), 1.5f)];
						d.velocity = -vector / 16;
						d.velocity -= player.velocity / 8;
						d.noLight = true;
						d.noGravity = true;
					}
				}
			}
			if (player.whoAmI == Main.myPlayer && chargingUppercut && (!StarsAbove.weaponActionKey.Current || player.GetModPlayer<WeaponPlayer>().overdriveGauge <= 0))
			{
				Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center);
				Vector2 Velocity = direction * (8);
				player.velocity = Velocity;
				chargingUppercut = false;

				if(overdriveModifier > 50)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, Velocity/2, ProjectileType<KissOfDeathMelee>(), (int)(player.GetWeaponDamage(Item) + overdriveModifier), 0, player.whoAmI, 0,1f);

				}
				else
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, Velocity/2, ProjectileType<KissOfDeathMelee>(), (int)(player.GetWeaponDamage(Item) + overdriveModifier), 0, player.whoAmI);

				}
				overdriveModifier = 0;

			}

			base.HoldItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				//Don't use the weapon if the player is swapping forms.
			}
			else
            {
				
				if (mode == 0)
				{
					if(player.statMana >= 2)
                    {
						player.statMana-=2;
						player.manaRegenDelay = 240;
						Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 120f;
						if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
						{
							position += muzzleOffset;
						}
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<KissOfDeathMinigun>(), 0, knockback, player.whoAmI);
						int numberProjectiles = 1; //random shots
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(2)); // 30 degree spread.

						float scale = 1f - (Main.rand.NextFloat() * .3f);
						perturbedSpeed = perturbedSpeed * scale;
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<KissOfDeathBullet>(), damage, knockback, player.whoAmI);
						
						for (int d = 0; d < 21; d++)
						{
							Vector2 perturbedSpeedA = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(47));
							float scaleA = 2f - (Main.rand.NextFloat() * .9f);
							perturbedSpeedA = perturbedSpeedA * scaleA;
							int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeedA.X, perturbedSpeedA.Y, 150, default(Color), 2f);
							Main.dust[dustIndex].noGravity = true;

						}
						for (int d = 0; d < 16; d++)
						{
							Vector2 perturbedSpeedB = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(47));
							float scaleB = 2f - (Main.rand.NextFloat() * .9f);
							perturbedSpeedB = perturbedSpeedB * scaleB;
							int dustIndex = Dust.NewDust(position, 0, 0, 31, perturbedSpeedB.X, perturbedSpeedB.Y, 150, default(Color), 1f);
							Main.dust[dustIndex].noGravity = true;
						}
						SoundEngine.PlaySound(SoundID.Item11, player.position);
					}
					
				}
				if (mode == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<KissOfDeathBomb>(), damage, knockback, player.whoAmI);

					SoundEngine.PlaySound(SoundID.Item1, player.position);
				}
				if (mode == 2)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<KissOfDeathCoffin>(), damage, knockback, player.whoAmI);

					SoundEngine.PlaySound(SoundID.Item1, player.position);
					
				}
			}
			
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<EssenceOfMisery>())
				.AddIngredient(ItemID.Diamond, 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
