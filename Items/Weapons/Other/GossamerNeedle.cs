using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.GossamerNeedle;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using StarsAbove.Projectiles.Other.GossamerNeedle;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Other
{
    public class GossamerNeedle : ModItem
	{
		public override void SetStaticDefaults()
		{
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 52;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 72;            //Weapon's texture's width
			Item.height = 72;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Orange;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			
			
			Item.shoot = ProjectileType<GossamerNeedleStab>();
			Item.shootSpeed = 4f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}

		int cooldown = 0;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if(player.HasBuff(BuffType<GossamerDashCooldown>()))
                {
					return false;
                }

			}

			return base.CanUseItem(player);
		}
		public override void HoldItem(Player player)
		{
			if (Main.myPlayer == player.whoAmI)
			{
				if (StarsAbove.weaponActionKey.JustPressed && !player.HasBuff(BuffType<GossamerAOECooldown>()))
				{
					player.Heal(15);
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<GossamerAOEAttack>(), player.GetWeaponDamage(Item), 0, player.whoAmI);
					player.AddBuff(BuffType<GossamerAOECooldown>(), 600);
				}

			}
			base.HoldItem(player);
		}
		
		


		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				float launchSpeed = 16f;
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
				Vector2 arrowVelocity = direction * launchSpeed;

				float rotation = (float)Math.Atan2(player.Center.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), player.Center.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));
				SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, player.Center);

				float dustAmount = 36f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
					int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = player.Center + spinningpoint5;
					Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
				}
				player.velocity += new Vector2(arrowVelocity.X, arrowVelocity.Y);
				
				player.AddBuff(BuffType<GossamerDashCooldown>(), 240);
			}
			else
			{
				
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Stinger, 5)
				.AddIngredient(ItemID.Cobweb, 10)
				.AddIngredient(ItemID.WhiteString, 1)
				.AddIngredient(ItemType<EssenceOfTheHallownest>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
