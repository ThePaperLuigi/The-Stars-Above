using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.VermillionDaemon;
using StarsAbove.Buffs;
using System;
using StarsAbove.Projectiles.SakuraVengeance;
using StarsAbove.Buffs.SakuraVengeance;

namespace StarsAbove.Items.Weapons.Melee
{
    public class SakuraVengeance : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sakura's Vengeance");
			/* Tooltip.SetDefault("Attacks with this weapon swing in a wide arc" +
				"\nEvery 30 seconds, the weapon cycles through 4 [c/FF71D8:Elemental Arts]" +
				"\n[c/55B168:Earthsplitter] increases defense by 10 and grants 25% damage resistance" +
                "\nAttacks stun enemies for 1 second (Critical strikes stun enemies for 4 seconds and grant Invincibility briefly)" +
				"\n[c/B0BEB3:Heavenly Volley] increases attack and movement speed by 20% " +
				"\nAttacks are accompanied by falling stars that inflict Ichor for 8 seconds" +
				"\n[c/8AD8E6:Cooling Haze] increases Health and Mana Regeneration and inflicts Frostburn for 2 seconds on hit" +
                "\nNearby foes are inflicted with Frostburn until they leave the radius" +
				"\n[c/B9384E:Volcanic Wrath] increases attack and critical strike chance by 20% and inflicts On Fire for 2 seconds on hit" +
                "\nDefeating foes will cause them to explode into a shower of flames, inflicting Cursed Flame for 8 seconds" +
				"\nHolding this weapon will cause the [c/CE8EBD:Vengeance Gauge] to appear" +
				"\nDefeating foes with this weapon will fill the [c/CE8EBD:Vengeance Gauge]" +
				"\nRight click to consume the [c/CE8EBD:Vengeance Gauge] and gain the buff [c/DF2AAE:Elemental Chaos] for 14 seconds" +
				"\n[c/DF2AAE:Elemental Chaos] grants all [c/FF71D8:Elemental Arts] buffs and the weapon will inflict all [c/FF71D8:Elemental Arts] debuffs on foes" +
				"\n[c/DF2AAE:Elemental Chaos] does not grant the unique abilities of each [c/FF71D8:Elemental Art]" +
				"\n'Remember those who have passed, and they will forever live on'" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 220;
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 130;            //Weapon's texture's width
			Item.height = 130;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			//Item.reuseDelay = 20;

			Item.shoot = ProjectileType<SakuraSlashEarth1>();
			Item.shootSpeed = 3f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}
		int currentSwing;
		 
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
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			Vector2 skyDirection = Vector2.Normalize(Main.MouseWorld - new Vector2(player.Center.X, player.Center.Y - 600));
			Vector2 meteorVelocity = skyDirection * 25f;
			if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<ElementalChaos>()))
			{
				if(modPlayer.VengeanceGauge >= 100)
                {
					player.AddBuff(BuffType<ElementalChaos>(), 840);
					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
					modPlayer.VengeanceGauge = 0;
					for (int d = 0; d < 18; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustID.Fireworks, Main.rand.NextFloat(-45, 45), Main.rand.NextFloat(-45, 45), 50, default(Color), 1f);

					}
					for (int d = 0; d < 18; d++)
					{
						Dust.NewDust(player.Center, 0, 0, 20, Main.rand.NextFloat(-45, 45), Main.rand.NextFloat(-45, 45), 50, default(Color), 1f);

					}
					for (int d = 0; d < 18; d++)
					{
						Dust.NewDust(player.Center, 0, 0, 220, Main.rand.NextFloat(-45, 45), Main.rand.NextFloat(-45, 45), 50, default(Color), 1f);

					}
					for (int d = 0; d < 18; d++)
					{
						Dust.NewDust(player.Center, 0, 0, 92, Main.rand.NextFloat(-45, 45), Main.rand.NextFloat(-45, 45), 50, default(Color), 1f);

					}
				}
				else
                {
					return false;
                }
				
				//modPlayer.VengeanceElementTimer += 1800;//Debug
			}
			else
			{


				
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
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			Vector2 skyDirection = Vector2.Normalize(Main.MouseWorld - new Vector2(player.Center.X, player.Center.Y - 600));
			Vector2 meteorVelocity = skyDirection * 25f;
			if (currentSwing == 0)
			{
				if (modPlayer.VengeanceElement == 0)
				{
					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashEarth2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashEarth1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}

				}
				if (modPlayer.VengeanceElement == 1)
				{
					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashHeaven2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, meteorVelocity.X, meteorVelocity.Y, ProjectileType<HeavenlyStar>(), player.GetWeaponDamage(Item), 5, player.whoAmI);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashHeaven1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, meteorVelocity.X, meteorVelocity.Y, ProjectileType<HeavenlyStar>(), player.GetWeaponDamage(Item), 5, player.whoAmI);


					}
				}
				if (modPlayer.VengeanceElement == 2)
				{

					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashCooling2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashCooling1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
				}
				if (modPlayer.VengeanceElement == 3)
				{
					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashVolcano2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashVolcano1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
				}

				comboTimer = 60;
				currentSwing++;
				return true;
			}
			if (currentSwing > 0)
			{
				if (modPlayer.VengeanceElement == 0)
				{
					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashEarth1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashEarth2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}

				}
				if (modPlayer.VengeanceElement == 1)
				{
					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashHeaven1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, meteorVelocity.X, meteorVelocity.Y, ProjectileType<HeavenlyStar>(), player.GetWeaponDamage(Item), 5, player.whoAmI);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashHeaven2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, meteorVelocity.X, meteorVelocity.Y, ProjectileType<HeavenlyStar>(), player.GetWeaponDamage(Item), 5, player.whoAmI);


					}
				}
				if (modPlayer.VengeanceElement == 2)
				{

					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashCooling1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashCooling2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
				}
				if (modPlayer.VengeanceElement == 3)
				{
					if (player.direction == 1)
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashVolcano1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
					else
					{

						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SakuraSlashVolcano2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);


					}
				}

				currentSwing = 0;
				return true;
			}

			return true;



			return base.UseItem(player);
		}
		public override void HoldItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();

			modPlayer.SakuraVengeanceHeld = true;

			if (currentSwing != 0)
            {
				comboTimer--;
			}
			if(comboTimer <= 0)
            {
				currentSwing = 0;
            }
			
			if(modPlayer.VengeanceElement == 0)
            {
				player.AddBuff(BuffType<SakuraEarthBuff>(), 2);
            }
			if (modPlayer.VengeanceElement == 1)
			{
				player.AddBuff(BuffType<SakuraHeavenBuff>(), 2);

			}
			if (modPlayer.VengeanceElement == 2)
			{
				player.AddBuff(BuffType<SakuraCoolingBuff>(), 2);
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					float distance = Vector2.Distance(npc.Center, player.Center);


					if (npc.active && npc.Distance(player.Center) < 280f && npc.CanBeChasedBy())
					{
						npc.AddBuff(BuffID.Frostburn, 2);
						if(player.HasBuff(BuffType<ElementalChaos>()))
                        {
							npc.AddBuff(BuffID.OnFire, 2);
                        }
					}


				}

				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 280);
					offset.Y += (float)(Math.Cos(angle) * 280);

					Dust d = Dust.NewDustPerfect(player.Center + offset, 20, Vector2.Zero, 200, default(Color), 0.7f);
					d.fadeIn = 0.1f;
					d.noGravity = true;
				}
			}
			if (modPlayer.VengeanceElement == 3)
			{
				player.AddBuff(BuffType<SakuraVolcanoBuff>(), 2);

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
				.AddIngredient(ItemID.VanityTreeSakuraSeed, 1)
				.AddIngredient(ItemID.ChlorophyteBar, 15)
				.AddIngredient(ItemID.SoulofLight, 9)
				.AddIngredient(ItemType<EssenceOfSakura>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
