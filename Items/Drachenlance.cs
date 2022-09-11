using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using System.Security.Policy;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
	public class Drachenlance : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Use right click to perform a Jump into the air (12 second cooldown) and become imbued with the [c/6090EC:Blood of the Dragon] for 5 seconds" +
				"\nYou will also be granted Featherfall for 1 second" +
				"\n[c/6090EC:Blood of the Dragon] will apply the following effects:" +
				"\nDuring [c/6090EC:Blood of the Dragon], the next attack will launch you towards your cursor after Featherfall ends" +
				"\nDuring [c/6090EC:Blood of the Dragon], Damage is increased by 40%" +
				"\nDuring [c/6090EC:Blood of the Dragon], Movement speed is increased" +
				"\nDuring [c/6090EC:Blood of the Dragon], you will take no fall damage" +
				"\nAfter two instances of [c/6090EC:Blood of the Dragon], the next Jump will instead grant [c/EC6060:Life of the Dragon] for 8 seconds" +
				"\n[c/EC6060:Life of the Dragon] will apply the following effects:" +
				"\nDuring [c/EC6060:Life of the Dragon], the next attack will fire a barrage of draconic fireballs that lower enemy defense" +
				"\nDuring [c/EC6060:Life of the Dragon], damage and critical chance is increased by 60%" +
				"\nDuring [c/EC6060:Life of the Dragon], every attack after the first will launch you towards your target, dealing high amounts of bonus damage" +
				"\nDuring [c/EC6060:Life of the Dragon], you will gain 1 second of Invincibility after each strike" +
				"\nDuring [c/EC6060:Life of the Dragon], you will take no fall damage" +
				"\nJump counters will be reset when swapping off this weapon" +
				"\n'Born amidst the timeless conflict between men and dragons, forged that they might better pierce the scaled hides of their mortal foes'" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 88;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Lime;
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<DrachenlanceProjectile>();
			Item.shootSpeed = 6f;
		}

		int cooldown = 0;
		int jumps = 0;
		int jumpReady = 0;
		int specialAttackReady = 0;
		int dragonBlood = 0;
		int dragonLife = 0;
		int dragonLife2 = 0;
		bool weaponHeld;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        
        public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.JumpCooldown>()))

				{

					if (jumps < 2)
					{
						jumps++;
						dragonBlood = 1;
						jumpReady = 1;

						player.AddBuff(BuffType<Buffs.BloodOfTheDragon>(), 300);
						player.AddBuff(BuffID.Featherfall, 60);
						player.AddBuff(BuffType<Buffs.JumpCooldown>(), 720);
						specialAttackReady = 60;
						
						
						//jump sound effect
						
					}
					else
					{
						jumps = 0;
						jumpReady = 1;
						dragonLife = 1;
						
						player.AddBuff(BuffType<Buffs.LifeOfTheDragon>(), 480);
						player.AddBuff(BuffType<Buffs.JumpCooldown>(), 720);
						//put a playsound dragon roar here
						player.AddBuff(BuffID.Featherfall, 60);
						
						specialAttackReady = 20;
					}
				}

			}

			else
			{
				
			}

			return player.ownedProjectileCounts[Item.shoot] < 1;//Important (only 1 spear)
		}

		public virtual void PostUpdateRunSpeeds()
		{

		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			

			if (player.altFunctionUse == 2)
			{
				if (jumpReady == 1)
				{
					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 113, 0f, 0f, 150, default(Color), 1.5f);
					}
					//Vector2 airJump = Vector2.Normalize(new Vector2(velocity.X, velocity.Y + 800 * 2));
					Vector2 jump = new Vector2(player.velocity.X, player.velocity.Y - 12);
					player.velocity = jump;
					jumpReady = 0;
					return false;
				}
			}
			else
			{
				if (specialAttackReady <= 1)
				{
					if (dragonBlood == 1)
					{

						Vector2 jumpAttack = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
						player.velocity = jumpAttack;
						dragonBlood = 0;

					}
					else
					{
						if (dragonLife2 == 1)
						{
							Vector2 jumpAttack = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 15f;
							player.velocity = jumpAttack;
							player.AddBuff(BuffType<Buffs.Invincibility>(), 60);
						}

						if (dragonLife == 1)
						{
							for (int i = 0; i < 3; i++)
							{
								type = 711;
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X* 2 + Main.rand.Next(0,7), velocity.Y* 2 + Main.rand.Next(0,7), type, damage / 3, knockback, player.whoAmI, 0f);

							}
							int numberProjectiles = 4 + Main.rand.Next(5);
							for (int i = 0; i < numberProjectiles; i++)
							{
								Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(40));
								type = 711;

								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X* 2 + Main.rand.Next(0,7), perturbedSpeed.Y* 2 + Main.rand.Next(0,7), type, damage / 2, knockback, player.whoAmI);
							}
							Vector2 shotKnockback = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 8f * -1f;
							player.velocity = shotKnockback;
							//Fireball burst here
							dragonLife = 0;
							dragonLife2 = 1;
							specialAttackReady = 0;
						}
					}

				}
			}

			

			return true;

		}
		public override void HoldItem(Player player)
		{
			cooldown--;
			specialAttackReady--;

			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LifeOfTheDragon>()))
			{
				Item.crit = 60;
				
			}
			else
			{
				
				Item.crit = 0;
				dragonLife2 = 0;
			}
			base.HoldItem(player);
		}
		public override void UpdateInventory(Player player)
		{
			if(player.HeldItem.ModItem is Drachenlance)
            {

            }
			else
            {
				jumps = 0;
            }
			base.UpdateInventory(player);

		}

		public virtual void PreUpdate()
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.BloodOfTheDragon>()))
			{
				for (int d = 0; d < 30; d++)
				{
					Dust.NewDust(target.position, target.width, target.height, 21, 0f, 0f, 150, default(Color), 1.5f);
				}

			}
			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LifeOfTheDragon>()))
			{
				player.statLife += 50;
				for (int d = 0; d < 30; d++)
				{
					Dust.NewDust(target.position, target.width, target.height, 258, 0f, 0f, 150, default(Color), 1.5f);
				}
			}

		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 33)
				.AddIngredient(ItemID.SoulofSight, 12)
				.AddIngredient(ItemType<EssenceOfTheDragonslayer>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
