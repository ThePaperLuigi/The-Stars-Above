using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Projectiles.VermillionDaemon;
using StarsAbove.Buffs.VermillionDaemon;
using StarsAbove.Buffs;
using System;

namespace StarsAbove.Items
{
    public class VermillionDaemon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vermilion Daemon");
			Tooltip.SetDefault("Holding this weapon prevents natural Mana Regeneration" +
                "\nAttacks with this weapon will execute a three-hit combo attack" +
				"\nStriking foes with the final swing of the combo attack will grant 15 Mana and inflict [c/B81E1E:Crimson Mark] for 12 seconds" +
				"\nRight click to activate [c/F13791:Warp Strike] with 30 Mana, throwing the weapon towards your cursor while disabling normal attacks" +
				"\nStriking a tile with [c/F13791:Warp Strike] will teleport you to the tile" +
				"\nStriking a foe with [c/F13791:Warp Strike] will teleport you to their position and execute an attack, dealing 2x base damage" +
				"\nAdditionally, this grants 1 second of Invincibility" +
				"\nStriking a foe with [c/B81E1E:Crimson Mark] with the [c/F13791:Warp Strike] will grant one stack of [c/F48080:Spectral Arsenal], up to 3" +
				"\nAdditionally, this removes the [c/B81E1E:Crimson Mark]" +
				"\nEach stack of [c/F48080:Spectral Arsenal] will cause a follow up blade to attack nearby foes for a short duration when executing normal attacks" +
				"\nWith 3 stacks of [c/F48080:Spectral Arsenal], right click will instead activate [c/C80000:Retribution], costing no Mana" +
				"\n[c/C80000:Retribution] consumes all [c/F48080:Spectral Arsenal] stacks to summon a barrage of spectral blades towards your cursor for 3 seconds" +
                "\n'I'm afraid you're out of luck'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 125;
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 130;            //Weapon's texture's width
			Item.height = 130;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.crit = 14;
			//Item.reuseDelay = 20;

			Item.shoot = ProjectileType<VermillionBlades>();
			Item.shootSpeed = 3f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}
		int currentSwing;
		int slashDuration;
		 
		int bladeRotationTimer;
		int spectralRetributionTimer;
		int comboTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.HasBuff(BuffType<ComboCooldown>()) || player.HasBuff(BuffType<Retribution>()))
            {
				return false;
            }
			//if (player.HasBuff(BuffType<PrepDarkmoon>()))
			//{
			//	return false;
			//}

			float launchSpeed = 110f;
			float launchSpeed2 = 25f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity3 = direction * launchSpeed;
			Vector2 projectile = direction * launchSpeed2;

			Vector2 muzzleOffset = Vector2.Normalize(projectile) * 50f;

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();

			if (player.altFunctionUse == 2)
			{
				if(modPlayer.SpectralArsenal >= 3)
                {
					player.AddBuff(BuffType<Retribution>(), 180);
					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
                }
				else
                {
					if (player.statMana >= 30)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, projectile.X, projectile.Y, ProjectileType<WarpStrike>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						player.statMana -= 30;
						player.manaRegenDelay = 240;

						return true;
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
					
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionSlash1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);
                    if(player.HasBuff(BuffType<SpectralArsenal1>()))//Tier 1
                    {
						
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item)/3, 3, player.whoAmI, 0f);
					}
					if (player.HasBuff(BuffType<SpectralArsenal2>()))//Tier2
					{

						for (int i = 0; i < 2; i++)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

						}
					}
					if (player.HasBuff(BuffType<SpectralArsenal3>()))//Tier 3
					{
						for (int i = 0; i < 3; i++)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

						}
					}
					comboTimer = 60;
					currentSwing++;
					return true;
				}
				if (currentSwing == 1)
				{
					
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionSlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);

					if (player.HasBuff(BuffType<SpectralArsenal1>()))//Tier 1
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);
					}
					if (player.HasBuff(BuffType<SpectralArsenal2>()))//Tier2
					{

						for (int i = 0; i < 2; i++)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

						}
					}
					if (player.HasBuff(BuffType<SpectralArsenal3>()))//Tier 3
					{
						for(int i = 0; i < 3; i++)
                        {
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

						}
					}
					comboTimer = 60;
					currentSwing++;
					return true;
				}
				if(currentSwing > 1)
                {

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionSlash3>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(SoundID.Item1, player.position);

					if (player.HasBuff(BuffType<SpectralArsenal1>()))//Tier 1
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);
					}
					if (player.HasBuff(BuffType<SpectralArsenal2>()))//Tier2
					{

						for (int i = 0; i < 2; i++)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

						}
					}
					if (player.HasBuff(BuffType<SpectralArsenal3>()))//Tier 3
					{
						for (int i = 0; i < 3; i++)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<VermillionBlades>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

						}
					}
					player.AddBuff(BuffType<ComboCooldown>(), 50);
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
			player.manaRegen = 0;
			player.manaRegenDelay = 20;
			player.GetModPlayer<WeaponPlayer>().VermillionDaemonHeld = true;
			bladeRotationTimer++;
			if(currentSwing != 0)
            {
				comboTimer--;
			}
			
			if(comboTimer <= 0)
            {
				currentSwing = 0;
            }
			float launchSpeed2 = 25f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 projectile = direction * launchSpeed2;
			if (player.HasBuff(BuffType<Retribution>()))
			{
				spectralRetributionTimer++;
				if(spectralRetributionTimer >= 3)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X + Main.rand.Next(-60, 60), player.Center.Y + Main.rand.Next(-60, 60), projectile.X, projectile.Y, ProjectileType<RetributionAttacks>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
					spectralRetributionTimer = 0;
				}


				
					Vector2 vector = new Vector2(
						Main.rand.Next(-48, 48) * (0.003f * 40 - 10),
						Main.rand.Next(-48, 48) * (0.003f * 40 - 10));
					Dust d = Main.dust[Dust.NewDust(
						player.Center + vector, 1, 1,
						223, 0, 0, 255,
						new Color(0.8f, 0.4f, 1f), 0.4f)];
					d.velocity = -vector / 16;
					d.velocity -= player.velocity / 8;
					d.noLight = true;
					d.noGravity = true;

				
			}

			if (player.HasBuff(BuffType<SpectralArsenal1>()))
            {
				if (player.ownedProjectileCounts[ProjectileType<SpectralArsenalProjectile1>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<SpectralArsenalProjectile1>(), 0, 4, player.whoAmI, 0f);


				}
				if (bladeRotationTimer > 6)
				{
					player.GetModPlayer<WeaponPlayer>().DaemonGlobalRotation++;
				}
			}
			if (player.HasBuff(BuffType<SpectralArsenal2>()))
			{
				if (player.ownedProjectileCounts[ProjectileType<SpectralArsenalProjectile2>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<SpectralArsenalProjectile2>(), 0, 4, player.whoAmI, 0f);


				}
				if (bladeRotationTimer > 4)
				{
					player.GetModPlayer<WeaponPlayer>().DaemonGlobalRotation++;
				}
			}
			if (player.HasBuff(BuffType<SpectralArsenal3>()))
			{
				if (player.ownedProjectileCounts[ProjectileType<SpectralArsenalProjectile3>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<SpectralArsenalProjectile3>(), 0, 4, player.whoAmI, 0f);


				}
				if (bladeRotationTimer > 2)
				{
					player.GetModPlayer<WeaponPlayer>().DaemonGlobalRotation++;
				}
			}
			if (player.HasBuff(BuffType<Retribution>()))
			{
				
				player.GetModPlayer<WeaponPlayer>().DaemonGlobalRotation+=7;
				
			}

			if (player.GetModPlayer<WeaponPlayer>().DaemonGlobalRotation > 360)
            {
				player.GetModPlayer<WeaponPlayer>().DaemonGlobalRotation = 0;

			}

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, player.Center);


				if (
					npc.active
					&& npc.Distance(player.Center) < 1000f
					&& npc.CanBeChasedBy()
					&& npc.HasBuff(BuffType<CrimsonMark>())
					)
				{
					for (int i1 = 0; i1 < 12; i1++)
					{//Circle
						Vector2 offset = new Vector2();
						double angle = Main.rand.NextDouble() * 2d * Math.PI;
						offset.X += (float)(Math.Sin(angle) * 80);
						offset.Y += (float)(Math.Cos(angle) * 80);

						Dust d = Dust.NewDustPerfect(npc.Center + offset, 219, Vector2.Zero, 200, default(Color), 0.3f);
						d.fadeIn = 0.1f;
						d.noGravity = true;
					}
				}




			}

			base.HoldItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.SkyFracture, 1)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.Ruby, 12)
				.AddIngredient(ItemID.SoulofNight, 15)
				.AddIngredient(ItemID.SpectreBar, 12)
				.AddIngredient(ItemType<EssenceOfAdagium>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
