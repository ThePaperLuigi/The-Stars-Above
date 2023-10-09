using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Projectiles.Summon.Wavedancer;
using StarsAbove.Buffs.Wavedancer;

namespace StarsAbove.Items.Weapons.Summon
{
    public class Wavedancer : ModItem
	{
		public override void SetStaticDefaults()
		{
			

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 22;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 14;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;
			//Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<WavedancerSummon>();
			Item.shootSpeed = 7f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.channel = true;
		}

		int followUpTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)
		{
			player.AddBuff(BuffType<Buffs.Wavedancer.WavedancerBuff>(), 10);
			if (player.ownedProjectileCounts[ProjectileType<WavedancerSummon>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<WavedancerSummon>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);

			}
			if (player.altFunctionUse == 2)
            {
				
			}
			
			return true;
		}

		public override void HoldItem(Player player)
		{
			if(!player.channel)
            {
				player.GetModPlayer<Systems.WeaponPlayer>().wavedancerTarget = player.Center;
            }
			else
            {
				if (Main.myPlayer == player.whoAmI)
				{
					if (StarsAbove.weaponActionKey.JustPressed && !player.HasBuff(BuffType<WavedancerCooldown>()))
					{
						Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<WavedancerSwordSpin>(), player.GetWeaponDamage(Item), 0, player.whoAmI);
						player.AddBuff(BuffType<WavedancerCooldown>(), 240);
					}

				}
			}
			
			base.HoldItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
