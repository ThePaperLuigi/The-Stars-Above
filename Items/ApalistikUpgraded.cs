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
	public class ApalistikUpgraded : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Seaborn Apalistik");
			Tooltip.SetDefault("18 summon tag damage" +
                "\nStriking foes with the weapon inflicts [c/4A91FF:Riptide] for 4 seconds" +
				"\nWhen a minion strikes an enemy with [c/4A91FF:Riptide], they take 30% extra damage, removing [c/4A91FF:Riptide]" +
				"\nAdditionally, with this weapon held, minions have a 10% chance to apply [c/4776A7:Ocean's Culling] for 5 seconds upon striking foes" +
				"\nWhen you strike a foe with [c/4776A7:Ocean's Culling] with this weapon, the foe will detonate, taking damage and spewing damaging bubbles around the area" +
				"\nBubbles count as Minion damage and have a 30% chance to crit" +
				"\nIf bubbles hit a foe with [c/4A91FF:Riptide], this chance is increased to 100%" +
				"\nRight click to imbue yourself with [c/00FFC1:Seaborn Wrath] for 8 seconds" +
				"\nUnder the effects of [c/00FFC1:Seaborn Wrath], attacks with this weapon will fire bubbles in a spread pattern" +
				"\nAdditionally, minions will deal double damage" +
				"\nAfter use, [c/00FFC1:Seaborn Wrath] has a 30 second cooldown" +
				"\nDamage scales greatly with world progression" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Red;
			Item.DamageType = DamageClass.SummonMeleeSpeed;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<ApalistikUpgradedProjectile>();
		}

		

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void UpdateInventory(Player player)
        {
			if (NPC.downedSlimeKing)
			{
				Item.damage = 40;
			}
			if (NPC.downedBoss1)
			{
				Item.damage = 42;
			}
			if (NPC.downedBoss2)
			{
				Item.damage = 45;
			}
			if (NPC.downedQueenBee)
			{
				Item.damage = 48;
			}
			if (NPC.downedBoss3)
			{
				Item.damage = 54;
			}
			if (Main.hardMode)
			{
				Item.damage = 69;
			}
			if (NPC.downedMechBossAny)
			{
				Item.damage = 70;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				Item.damage = 85;
			}
			if (NPC.downedPlantBoss)
			{
				Item.damage = 98;
			}
			if (NPC.downedGolemBoss)
			{
				Item.damage = 105;
			}
			if (NPC.downedFishron)
			{
				Item.damage = 111;
			}
			if (NPC.downedAncientCultist)
			{
				Item.damage = 125;
			}
			if (NPC.downedMoonlord)
			{
				Item.damage = 165;
			}
			base.UpdateInventory(player);
        }

        public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<Buffs.SeabornCooldown>()) && !player.HasBuff(BuffType<Buffs.SeabornWrath>()))
				{
					for (int d = 0; d < 10; d++)
					{
						Dust.NewDust(player.Center, 0, 0, 15, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 28; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustType<Dusts.bubble>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-15, 15), 0, default(Color), 1.5f);
					}
					player.AddBuff(BuffType<Buffs.SeabornWrath>(), 480);
					return true;
				}
				else
                {
					return false;
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
				return false;

			}
			else
            {
				if (player.HasBuff(BuffType<Buffs.SeabornWrath>()))
				{
					int numberProjectiles = 8 + Main.rand.Next(2); //random shots
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25)); // 30 degree spread.
																														// If you want to randomize the speed to stagger the projectiles
						float scale = 2f - (Main.rand.NextFloat() * .3f);
						perturbedSpeed = perturbedSpeed * scale;
						
						int index1 = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<Bubble>(), damage / 10, knockback, player.whoAmI);
						Main.projectile[index1].originalDamage = Item.damage;
					}
				}
			}

			int index = Projectile.NewProjectile(source, position, velocity, type, Item.damage, knockback, player.whoAmI, 0f);
			Main.projectile[index].originalDamage = Item.damage;

			return false;

		}
		public override void HoldItem(Player player)
		{
			
			
			base.HoldItem(player);
		}
		public virtual void PreUpdate()
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			

		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddIngredient(ItemID.Coral, 15)
				.AddIngredient(ItemID.SharkFin, 5)
				.AddIngredient(ItemType<Apalistik>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
