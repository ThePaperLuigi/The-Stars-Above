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
using System;
using Terraria.Audio;
using StarsAbove.Projectiles.SoulReaver;
using StarsAbove.Buffs.SoulReaver;
using StarsAbove.Projectiles;
using StarsAbove.Projectiles.Manifestation;
using StarsAbove.Buffs.Manifestation;

namespace StarsAbove.Items
{
    public class Manifestation : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Manifestation");
			Tooltip.SetDefault("" +
				"[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes at 70% efficiency, but inherits 150% from critical strike chance modifiers]" +
                "\nAttacks sweep in a colossal arc; all critical strike damage is increased by 20%" +
				"\nTaking or dealing damage grants [c/D53B3B:Emotional Turbulence] (Critical strikes grant double [c/D53B3B:Emotional Turbulence] below 200 HP)" +
				"\nDefeating foes increases [c/D53B3B:Emotional Turbulence] by 5% (doubled below 200 HP)" +
				"\nOnce [c/D53B3B:Emotional Turbulence] is full, automatically gain the buff [c/AE0000:E.G.O. Manifestation] while becoming Invincible for 2 seconds" +
				"\n[c/AE0000:E.G.O. Manifestation] grants 30% increased damage, 40% increased attack speed and 90% increased movement speed, but reduces defense by 30" +
				"\nIf Manifestation of E.G.O. is active, unique [c/F75252:Greater Split] attacks can be preformed" +
				"\nExecute [c/F75252:Great Split: Vertical] with right click or [c/F75252:Great Split: Horizontal] with the Weapon Action Key (Both attacks share a 1 minute cooldown)" +
				"\n[c/F75252:Great Split: Vertical] will teleport you to your cursor and grant Invincibility for 1 second and deal 5x guaranteed critical damage after a short delay" +
				"\n[c/F75252:Great Split: Horizontal] will deal guaranteed critical damage to all foes in a colossal radius around you after a short delay" +
				"\nBoth [c/F75252:Greater Split] attacks will execute any non-boss enemy below 30% HP and additionally refill [c/D53B3B:Emotional Turbulence] to full on use" +
                "\nIf [c/AE0000:E.G.O. Manifestation] is active, [c/D53B3B:Emotional Turbulence] will passively decrease over time (Decreases rapidly out of combat)" +
				"\nThe longer [c/AE0000:E.G.O. Manifestation] lasts, the faster [c/D53B3B:Emotional Turbulence] will drain" +
                "\nIf [c/D53B3B:Emotional Turbulence] is below 10% for 3 seconds, [c/AE0000:E.G.O. Manifestation] will end and you will be inflicted with Vulnerability for 10 seconds" +
				"\n'Let's start this for real- I'll crush you all...'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 211;           //The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.PsychomentDamageClass>();
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 25f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.crit = 20;
		}
		int currentSwing;
		int slashDuration;

		int soulHarvestRange = 700;
		bool altSwing;
		bool hasTarget = false;
		Vector2 enemyTarget = Vector2.Zero;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
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
			//debug

			if (player.altFunctionUse != 2)
			{
				//player.AddBuff(BuffType<EGOManifestedBuff>(), 1480);
			}


				return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<ManifestationPlayer>().manifestationHeld = true;
			if (player.ownedProjectileCounts[ProjectileType<ManifestationHeld>()] < 1)
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ManifestationHeld>(), 0, 0, player.whoAmI, 0f);
			}
			if (player.ownedProjectileCounts[ProjectileType<ManifestationHeadVFX>()] < 1 && player.HasBuff(BuffType<EGOManifestedBuff>()))
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ManifestationHeadVFX>(), 0, 0, player.whoAmI, 0f);
			}
			if (player.ownedProjectileCounts[ProjectileType<ManifestationHeadVFX>()] < 1 && player.HasBuff(BuffType<EGOManifestedBuff>()))
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ManifestationHeadVFX2>(), 0, 0, player.whoAmI, 0f);
			}
			base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			if (player.altFunctionUse != 2)
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_skofnungSwing, player.Center);

				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ManifestationSwordSlash2>(), 0, 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<ManifestationSlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ManifestationSwordSlash1>(), 0, 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<ManifestationSlash1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ManifestationSwordSlash1>(), 0, 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<ManifestationSlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ManifestationSwordSlash2>(), 0, 3, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<ManifestationSlash1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						altSwing = true;
					}
				}
			}
			else
            {
				player.GetModPlayer<ManifestationPlayer>().greaterSplitTimer = 25;
				player.GetModPlayer<ManifestationPlayer>().greaterSplitAlpha = 1;

			}
			return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
