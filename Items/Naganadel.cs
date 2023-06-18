using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class Naganadel : ModItem //Naganadel
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Use right click to expend 25 mana, summoning up to 5 Aspected Blades" +
				"\nBlades deal minor contact damage while idle" +
				"\nAttack with blades present to launch them towards the cursor" +
				"\nUse right click once all blades are summoned to launch all blades at once, dealing random effects" +
				"\nEach blade has unique effects:" +
				"\nThe [c/54AFDF:Stardust Aspect] will restore 20 mana on hit, but deals half damage" +
				"\nThe [c/54DF80:Vortex Aspect] travels faster and farther, deals 6x damage on a critical hit and grants Swiftness" +
				"\nThe [c/DF8454:Solar Aspect] burns foes on hit and deals double damage to flaming enemies, but travels slower and has less range" +
				"\nThe [c/C654DF:Nebula Aspect] summons a barrage of falling stars from the sky on hit" +
				"\n[c/BDD5B5:Naganadel] costs 50 mana to summon instead, but will apply most other effects with a lunar bonus effect" +
				"\nIf the destination is closer than the maximum range, the blades will deal residual damage as they fade away" +
				"\nBlades will vanish when swapping weapons" +
				//"\n[c/FFA957:This blade deals summoning damage]" +
				"\n'I have no regrets. This is the only path'" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 215;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.reuseDelay = 20;

			Item.shoot = ProjectileType<NaganadelProjectile1>();
			Item.shootSpeed = 3f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = false;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		bool preWeaponSummoned;
		bool weaponThrow;
		int weaponCooldown = 0;
		int bladeNumber = 0;
		bool weaponThrowAll;
		int weaponThrowAllValue;
		int damageType = 0;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		
		public override bool CanUseItem(Player player)
		{

			




			if (player.altFunctionUse == 2)
			{
				
				if (weaponCooldown > 0)
				{
					return false;
				}
				if (bladeNumber <= 4)
				{
					if (bladeNumber == 4)
					{
						if (player.statMana >= 50)
						{
							player.statMana -= 50;
							player.manaRegenDelay = 240;
							preWeaponSummoned = true;
						}
						else
						{
							return false;
						}
					}
					if (bladeNumber < 4)
					{
						if (player.statMana >= 25)
						{
							player.statMana -= 25;
							player.manaRegenDelay = 240;
							preWeaponSummoned = true;
						}
						else
						{
							return false;
						}
					}

					
				}
				else
				{
					weaponThrowAll = true;
				}
				
			}
			else
			{
				if (player.ownedProjectileCounts[Item.shoot] < 1)
				{
					/*damageType++;
					if (damageType > 5)
					{
						damageType = 1;
					}
					if (damageType == 1)
					{
					item.melee = false;
					item.magic = false;
					item.ranged = false;
					item.summon = true;
					}
					if (damageType == 2)
					{
					item.melee = false;
					item.magic = false;
					item.ranged = true;
					item.summon = false;
					}
					if (damageType == 3)
					{
					item.melee = true;
					item.magic = false;
					item.ranged = false;
					item.summon = false;
					}
					if (damageType == 4)
					{
					item.melee = false;
					item.magic = true;
					item.ranged = false;
					item.summon = false;
					}
					if (damageType == 5)
					{
					item.melee = true;
					item.magic = false;
					item.ranged = false;
					item.summon = false;
					}
					for (int d = 0; d < 3; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 172, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}*/
					return false;
				}
				else
				{
					weaponThrow = true;
				}
			}





			return true;
			
		}

		public override bool? UseItem(Player player)
		{
			




			return true;
		}

		public override void HoldItem(Player player)
		{
			weaponCooldown--;
			
			base.HoldItem(player);
		}
        public override void UpdateInventory(Player player)
        {
			if (player.HeldItem.ModItem is Naganadel)
			{

			}
			else
			{
				bladeNumber = 0;
			}

			base.UpdateInventory(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			if (preWeaponSummoned)
			{
				
				if (bladeNumber == 0)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<NaganadelProjectile1>(), damage / 10, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon1Summoned = true;

				}
				if (bladeNumber == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<NaganadelProjectile2>(), damage / 10, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon2Summoned = true;
					
				}
				if (bladeNumber == 2)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<NaganadelProjectile3>(), damage / 10, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon3Summoned = true;
					
				}
				if (bladeNumber == 3)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<NaganadelProjectile4>(), damage / 10, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon4Summoned = true;
					
				}
				if (bladeNumber == 4)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<NaganadelProjectile5>(), damage / 10, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon5Summoned = true;
					
				}
				bladeNumber++;
				weaponCooldown = 0;
				preWeaponSummoned = false;
				

			}
			if (weaponThrow)
			{
				if (bladeNumber == 1)
				{
					
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon1Summoned = false;

				}
				if (bladeNumber == 2)
				{
					
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon2Summoned = false;
				}
				if (bladeNumber == 3)
				{
					
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon3Summoned = false;
				}
				if (bladeNumber == 4)
				{
					
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon4Summoned = false;
				}
				if (bladeNumber == 5)
				{
					
					player.GetModPlayer<WeaponPlayer>().naganadelWeapon5Summoned = false;
				}
				bladeNumber--;
				
				weaponThrow = false;
				weaponCooldown = 0;
			}
			if (weaponThrowAll)
			{
				

					player.GetModPlayer<WeaponPlayer>().naganadelWeapon1Summoned = false;

				

					player.GetModPlayer<WeaponPlayer>().naganadelWeapon2Summoned = false;
				

					player.GetModPlayer<WeaponPlayer>().naganadelWeapon3Summoned = false;
				

					player.GetModPlayer<WeaponPlayer>().naganadelWeapon4Summoned = false;
				

					player.GetModPlayer<WeaponPlayer>().naganadelWeapon5Summoned = false;

					
				
				bladeNumber = 0;
				weaponThrowAll = false;
				weaponCooldown = 60;
				
			}


			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.LunarBar, 20)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddIngredient(ItemType<EssenceOfLunarDominion>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

