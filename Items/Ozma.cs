using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Ozma;
using StarsAbove.Projectiles.Ozma;

namespace StarsAbove.Items
{
    public class Ozma : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ozma Ascendant");
			Tooltip.SetDefault("" +
				"Holding this weapon conjures the [c/6A6A6A:Ascendant Array] to follow you" +
                "\nAttacks with this weapon will execute a unique five-hit combo attack utilizing the [c/6A6A6A:Ascendant Array]" +
				"\nRight click to consume 60 Mana, cycling through [c/CF2D2D:Ruby Execution], [c/E2BC3D:Topaz Execution], and [c/33AAE6:Sapphire Execution] attacks in order" +
				"\n[c/CF2D2D:Ruby Execution]" +
                "\nUpon activation, attack foes around your cursor for fast, powerful damage" +
				"\nGain Wrath for 2 seconds upon activation of [c/CF2D2D:Ruby Execution]" +
				"\n[c/E2BC3D:Topaz Execution]" +
                "\nAfter a short delay upon activation, creates piercing attacks at your cursor's location" +
				"\n[c/E2BC3D:Topaz Execution] attacks stun foes for a very short duration" +
				"\n[c/33AAE6:Sapphire Execution]" +
                "\nUpon activation, create a virus field at your cursor, which deals damage to all foes in its vicinity over time" +
				"\n[c/33AAE6:Sapphire Execution] turns 10% of damage dealt to health and Mana (Max 5 per tick)" +
				"\nCritical strikes with this weapon grant [c/AD0000:Annihilation State] for 3 seconds" +
				"\n[c/AD0000:Annihilation State] grants 30% increased damage and 10% increased critical strike chance" +
				"\nWithin [c/AD0000:Annihilation State], right click to unleash [c/B98080:Finale Descends] (2 minute cooldown)" +
				"\nFoes caught between [c/B98080:Finale Descends] will take powerful damage over time until the attack resolves" +
				"\nAdditionally, [c/B98080:Finale Descends] inflicts Stun for 1 second per hit" +
				"\n'I am.. a monster..'" +
				$"");  //The (English) text shown below your weapon's name
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}

		public override void SetDefaults()
		{
			Item.damage = 139;
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 130;            //Weapon's texture's width
			Item.height = 130;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.crit = 0;
			//Item.reuseDelay = 20;

			Item.shoot = ProjectileType<OzmaAttack1>();
			Item.shootSpeed = 3f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}
		int currentSwing;
		int currentExecution;

		 
		
		int comboTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.HasBuff(BuffType<ComboCooldown>()))
            {
				return false;
            }
			
			//Whoever is reading this... Ouch.
			float launchSpeed = 110f;
			float launchSpeed2 = 25f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity3 = direction * launchSpeed;
			Vector2 projectile = direction * 20f;
			Vector2 projectileFast = direction * 80f;

			Vector2 muzzleOffset = Vector2.Normalize(projectile) * 50f;

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			Vector2 skyDirection1 = Vector2.Normalize(Main.MouseWorld - new Vector2(player.Center.X - 100, player.Center.Y - 400));
			Vector2 skyDirection2 = Vector2.Normalize(Main.MouseWorld - new Vector2(player.Center.X, player.Center.Y - 400));
			Vector2 skyDirection3 = Vector2.Normalize(Main.MouseWorld - new Vector2(player.Center.X + 100, player.Center.Y - 400));
			Vector2 meteorVelocity1 = skyDirection1 * 64f;
			Vector2 meteorVelocity2 = skyDirection2 * 68f;
			Vector2 meteorVelocity3 = skyDirection3 * 60f;

			Vector2 directionTowardsMouse = Vector2.Normalize(player.GetModPlayer<StarsAbovePlayer>().playerMousePos - player.Center);
			Vector2 velocityTowardsMouse = directionTowardsMouse * launchSpeed;

			if (player.altFunctionUse == 2)
			{
				if(player.HasBuff(BuffType<AnnihilationState>()) && !player.HasBuff(BuffType<FinaleDescendsCooldown>()))
                {
					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X,
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y,
									0,
									0,
									ProjectileType<OzmaFinaleDescends>(),
									Item.damage,
									2f,
									player.whoAmI);
					player.AddBuff(BuffType<FinaleDescendsCooldown>(), 7200);
                }
				else
                {
					if (player.statMana >= 60)
					{
						//If in Annihilation State and has no cooldown... activate Finale Descends.

						player.statMana -= 60;
						player.manaRegenDelay = 240;
						if (currentExecution == 0)//Ruby
                        {
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X,
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y,
									0,
									0,
									ProjectileType<OzmaOrbAttack>(),
									(Item.damage / 5),
									2f,
									player.whoAmI);
							for (int l = 0; l < 6; l++)
							{
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X + Main.rand.Next(-270, 270),
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y + Main.rand.Next(-270, 270),
									0,
									0,
									ProjectileType<OzmaOrbAttack>(),
									(Item.damage / 5),
									2f,
									player.whoAmI);
							}
							player.AddBuff(BuffID.Wrath, 120);
							currentExecution++;
							return true;
						}
						if (currentExecution == 1)//Topaz
						{
							for (int l = 0; l < 3; l++)
							{
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X + Main.rand.Next(-170, 170),
									player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y + Main.rand.Next(-170, 170),
									velocityTowardsMouse.X,
									velocityTowardsMouse.Y,
									ProjectileType<OzmaSpikePortal>(),
									(Item.damage / 3),
									2f,
									player.whoAmI);
							}
							currentExecution++;
							return true;
						}
						if (currentExecution == 2)//Sapphire
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 0, 0, ProjectileType<OzmaDrainAttack>(), player.GetWeaponDamage(Item)/5, 3, player.whoAmI, 0f);

							currentExecution = 0;
							return true;
						}


					}
					else
					{
						return false;
					}
				}
				

			}
			else
			{


				if (currentSwing == 0)
				{
					
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, projectile.X, projectile.Y, ProjectileType<OzmaAttack1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);

					player.AddBuff(BuffType<OzmaAttack>(), 30);
					comboTimer = 60;
					currentSwing++;
					return true;
				}
				if (currentSwing == 1)
				{
					
					SoundEngine.PlaySound(SoundID.Item1, player.position);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X - 100, player.MountedCenter.Y - 400, meteorVelocity1.X, meteorVelocity1.Y, ProjectileType<OzmaAttack2>(), player.GetWeaponDamage(Item), 5, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y - 400, meteorVelocity2.X, meteorVelocity2.Y, ProjectileType<OzmaAttack2>(), player.GetWeaponDamage(Item), 5, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + 100, player.MountedCenter.Y - 400, meteorVelocity3.X, meteorVelocity3.Y, ProjectileType<OzmaAttack2>(), player.GetWeaponDamage(Item), 5, player.whoAmI);
					player.AddBuff(BuffType<OzmaAttack>(), 30);
					comboTimer = 60;
					currentSwing++;
					return true;
				}
				if (currentSwing == 2)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, projectileFast.X, projectileFast.Y, ProjectileType<OzmaAttack3>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);
					player.AddBuff(BuffType<OzmaAttack>(), 30);
					comboTimer = 60;
					currentSwing++;
					return true;
				}
				if (currentSwing == 3)
				{
					for (int d = 0; d < 12; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

					}
					SoundEngine.PlaySound(SoundID.Item1, player.position);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<OzmaAttack4>(), player.GetWeaponDamage(Item), 5, player.whoAmI);

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<OzmaAttack4>(), player.GetWeaponDamage(Item), 5, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<OzmaAttack4>(), player.GetWeaponDamage(Item), 5, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<OzmaAttack4>(), player.GetWeaponDamage(Item), 5, player.whoAmI);
					player.AddBuff(BuffType<OzmaAttack>(), 110);
					player.AddBuff(BuffType<ComboCooldown>(), 80);
					comboTimer = 120;
					currentSwing++;
					return true;
				}
				if (currentSwing >= 4)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 0, 0, ProjectileType<OzmaAttack5>(), 0, 3, player.whoAmI, 0f);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X+50, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y+50, 0, 0, ProjectileType<OzmaAttack5Slash>(), (player.GetWeaponDamage(Item)), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);
					player.AddBuff(BuffType<OzmaAttack>(), 40);

					player.AddBuff(BuffType<ComboCooldown>(), 120);
					currentSwing = 0;
					return true;
				}
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
			
			player.GetModPlayer<WeaponPlayer>().OzmaHeld = true;
			player.AddBuff(BuffType<OzmaBuff>(),10);
			if(currentSwing != 0)
            {
				comboTimer--;
			}
			if(comboTimer <= 0)
            {
				currentSwing = 0;
            }
			if (player.ownedProjectileCounts[ProjectileType<OzmaBack3>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<OzmaBack3>(), 0, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<OzmaBack4>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<OzmaBack4>(), 0, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<OzmaBack2>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<OzmaBack2>(), 0, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<OzmaBack1>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<OzmaBack1>(), 0, 4, player.whoAmI, 0f);


			}
			




		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Nanites, 400)
				.AddIngredient(ItemID.TissueSample, 20)
				.AddIngredient(ItemID.SpectreBar, 4)
				.AddIngredient(ItemType<EssenceOfTheAscendant>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.Nanites, 400)
				.AddIngredient(ItemID.ShadowScale, 20)
				.AddIngredient(ItemID.SpectreBar, 4)
				.AddIngredient(ItemType<EssenceOfTheAscendant>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
