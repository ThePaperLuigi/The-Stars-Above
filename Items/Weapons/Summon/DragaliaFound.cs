using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.DragaliaFound;
using StarsAbove.Items.Essences;
using StarsAbove.Mounts.DragaliaFound;
using StarsAbove.Projectiles.DragaliaFound;
using StarsAbove.Projectiles.Generics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class DragaliaFound : ModItem
	{
		public override void SetStaticDefaults()
		{
			

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 70;           //The damage of your weapon
			Item.DamageType = DamageClass.SummonMeleeSpeed;          //Is your weapon a melee weapon?
			Item.width = 38;            //Weapon's texture's width
			Item.height = 132;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<Projectiles.PhantomInTheMirrorProjectile>();
			Item.shootSpeed = 3f;
			Item.value = Item.buyPrice(gold: 1);
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.HasBuff(BuffType<ComboCooldown>()))
			{
				return false;
			}

			return base.CanUseItem(player);
		}


		internal static void Teleport(Player player)
		{
			
		}
		public override void HoldItem(Player player)
		{
			attackComboCooldown--;
			if(attackComboCooldown <= 0)
            {
				attackType = 0;
            }
			if(attackType == 3)
            {
				//The stab is faster
				player.GetAttackSpeed(DamageClass.Generic) += 0.4f;
            }
			base.HoldItem(player);
		}

		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}
		int attackType;
		int attackComboCooldown;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			attackComboCooldown = 30;
			if(player.altFunctionUse == 2)
				//temp
            {
				player.GetModPlayer<StarsAbovePlayer>().WhiteFade = 20;
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragonArm>(), 0, 0, player.whoAmI);
				player.mount.SetMount(MountType<DragonshiftMount>(), player);
				player.AddBuff(BuffType<DragonshiftActiveBuff>(), 240);
			}
			else
            {
				if(player.HasBuff(BuffType<DragonshiftActiveBuff>()))
                {
					switch (attackType)
					{
						case 0: //Swing downwards
							Projectile.NewProjectile(source, player.Center, velocity*2, ProjectileType<DragaliaFoundDragonAttack>(), damage*2, knockback*3, player.whoAmI, -1, player.itemTimeMax, 1.5f);
							attackType++;
							return false;
						case 1: //Swing upwards
							Projectile.NewProjectile(source, player.Center, velocity*2, ProjectileType<DragaliaFoundDragonAttack>(), damage*2, knockback*3, player.whoAmI, 1, player.itemTimeMax, 1.5f);
							attackType = 0;
							return false;
					}
				}
				else
                {
					switch (attackType)
					{
						case 0: //Swing downwards
							Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
							attackType++;
							return false;
						case 1: //Swing upwards
							Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
							attackType++;
							return false;
						case 2: //Swing down again but faster + prep stab
							Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSwordRecoil>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
							attackType++;
							return false;
						case 3: //Stab					
							Projectile.NewProjectile(source, player.Center, Vector2.Normalize(velocity), ProjectileType<DragaliaFoundStab>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
							player.velocity += velocity * 3f;
							float num10 = 16f;
							for (int num11 = 0; (float)num11 < num10; num11++)
							{
								Vector2 spinningpoint5 = Vector2.UnitX * 0f;
								spinningpoint5 += -Vector2.UnitY.RotatedBy((float)num11 * ((float)Math.PI * 2f / num10)) * new Vector2(1f, 4f);
								spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
								int num13 = Dust.NewDust(player.Center, 0, 0, DustID.TintableDustLighted);
								Main.dust[num13].color = Color.SkyBlue;
								Main.dust[num13].scale = 2f;
								Main.dust[num13].noGravity = true;
								Main.dust[num13].position = player.Center + spinningpoint5;
								Main.dust[num13].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
							}
							player.AddBuff(BuffType<Invincibility>(), 30);
							attackType++;
							return false;
						case 4: //Spin
							Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSwordSpin>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
							attackType = 0;
							return false;
					}
				}
				
				if (attackType > 4)
				{
					attackType = 0;
				}
			}
			
			/**/
			return false;
		}
		public override void AddRecipes()
		{
			
		}
	}
}
