using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;

namespace StarsAbove.Items
{
    public class EveryMomentMatters : ModItem
	{
		public override void SetStaticDefaults() {
			//Unobtainable

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 129;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 71;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Pink;
			Item.autoReuse = false;
			Item.shootSpeed = 86f;
			Item.crit = 10;
			Item.reuseDelay = 40;
			Item.shoot = 10;
			Item.useAmmo = AmmoID.Bullet;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		int shotCount = 0;
		int shotHitCount = 0;
		int missingHP = 0;
		int eightShotCount = 0;

		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			shotCount++;
			eightShotCount++;

			if (shotCount == 4)
			{
				if (eightShotCount == 8)
				{
					
					eightShotCount = 0;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_TeleportFinisher, player.Center);
					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 271, 0f, 0f, 150, default(Color), 1.5f);
					}
				}
				else
				{
					Item.damage = 130 + shotCount * 10;
					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 244, 0f, 0f, 150, default(Color), 1.5f);
					}
				}
				shotCount = 0;
				Item.shootSpeed = 486f;
				Item.crit = 100;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_DeathInFourActsFinish, player.Center);
				player.AddBuff(BuffID.Swiftness, 340);
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.reuseDelay = 40;
				shotHitCount = 0;


			}
			else
			{
				if (shotCount == 3)
				{
					
					Item.damage = 130 + shotCount * 10;
					if (eightShotCount == 7)
					{
						Item.damage = 260;
					}
					Item.useTime = 70;
					Item.useAnimation = 70;
					Item.reuseDelay = 70;
					Item.shootSpeed = 86f;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_DeathInFourActsShoot, player.Center);
				}
				else
				{
					Item.damage = 130 + shotCount * 10;
					Item.crit = 10;
					Item.useTime = 20;
					Item.useAnimation = 20;
					Item.reuseDelay = 40;
					Item.shootSpeed = 86f;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_DeathInFourActsShoot, player.Center);
				}
			}

			return true;
		}

		public override void HoldItem(Player player)
		{
			if (shotCount == 4)
			{

			}
			Vector2 position = player.position;
			if (shotCount == 3)
			{
				if (eightShotCount == 7)
				{

					Dust.NewDust(position, 20, 20, 269, 0f, 0f, 150, default(Color), 1.5f);
				}
				else
				{
					Dust.NewDust(position, 20, 20, 206, 0f, 0f, 150, default(Color), 1.5f);
					

				}
			}
			else
			{
				

			}
		}
		public virtual void OnHitNPCWithProj(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
			
			if (shotCount == 3)
			{
				
			}

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
		}


		public override void AddRecipes()
		{
			
		}
	}
}
