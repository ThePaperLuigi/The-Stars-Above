using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Gundbits;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.Gundbit;
using StarsAbove.Projectiles.RedMage;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Magic
{
    public class GundbitStaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 55;
			//The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 52;            //Weapon's texture's width
			Item.height = 52;           //Weapon's texture's height
			Item.useTime = 45;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 45;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			//item.knockback = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 35;
			Item.mana = 7;
		}
		int stabTimer;
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
		public override void HoldItem(Player player)
		{
			float launchSpeed = 12f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
			player.AddBuff(BuffType<AerialDefensiveArray>(), 10);
			
			if(player.ownedProjectileCounts[ProjectileType<Gundbits>()] < 1 && player.whoAmI == Main.myPlayer)
            {
				//Spawn 11 bit staves each with a unique iD
				for(int i = 0; i < 11; i++)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Gundbits>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f,Main.rand.Next(0,360),i);

				}
			}
			if (Main.myPlayer == player.whoAmI)
			{
				if (StarsAbove.weaponActionKey.JustPressed && !player.HasBuff(BuffType<GundbitShieldCooldown>()) && !player.HasBuff(BuffType<GundbitBeamAttack>()))
				{
					player.AddBuff(BuffType<GundbitShieldBuff>(), 60 * 3);
					player.AddBuff(BuffType<GundbitShieldCooldown>(), 60 * 10);
					SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, player.Center);

					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<GundbitShield>(), 0, 0, player.whoAmI);
				}

			}
			//player.GetModPlayer<WeaponPlayer>().blackMana--;
			//player.GetModPlayer<WeaponPlayer>().whiteMana--;
			base.HoldItem(player);
		}
        public override bool? UseItem(Player player)
        {
			
			
			return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
		
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<GundbitLaserCooldown>()) && !player.HasBuff(BuffType<GundbitShieldBuff>()))
			{
				player.AddBuff(BuffType<GundbitBeamAttack>(), 3 * 60);
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<GundbitGunCharged>(), damage, 0, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, player.Center);

				//Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<GundbitUnchargedLaser>(), damage, 0, player.whoAmI, 0f);

			}
			else
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<GundbitGunUncharged>(), 0, 0, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item125, player.Center);

				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<GundbitUnchargedLaser>(), damage, 0, player.whoAmI, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
