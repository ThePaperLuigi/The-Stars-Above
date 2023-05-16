using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class Apalistik : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Apalistik");
			/* Tooltip.SetDefault("5 summon tag damage" +
                "\nStriking foes with the weapon inflicts [c/4A91FF:Riptide] for 4 seconds" +
				"\nWhen a minion strikes an enemy with [c/4A91FF:Riptide], they take 30% extra damage, removing [c/4A91FF:Riptide]" +
				"\nAdditionally, with this weapon held, minions have a 10% chance to apply [c/4776A7:Ocean's Culling] for 5 seconds upon striking foes" +
				"\nWhen you strike a foe with [c/4776A7:Ocean's Culling] with this weapon, the foe will detonate, taking damage and spewing damaging bubbles around the area" +
				"\nBubbles count as Minion damage and have a 30% chance to crit" +
				"\nIf bubbles hit a foe with [c/4A91FF:Riptide], this chance is increased to 100%" +
				"\nDamage scales with world progression" +
				$""); */  //The (English) text shown below your weapon's name
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 6;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.shootSpeed = 3.0f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Lime;
			Item.DamageType = DamageClass.SummonMeleeSpeed;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<ApalistikProjectile>();
		}

		

		public override bool AltFunctionUse(Player player)
		{
			return false;
		}
        public override void UpdateInventory(Player player)
        {
			if (NPC.downedSlimeKing)
			{
				Item.damage = 8;
			}
			if (NPC.downedBoss1)
			{
				Item.damage = 12;
			}
			if (NPC.downedBoss2)
			{
				Item.damage = 14;
			}
			if (NPC.downedQueenBee)
			{
				Item.damage = 19;
			}
			if (NPC.downedBoss3)
			{
				Item.damage = 22;
			}
			if (Main.hardMode)
			{
				Item.damage = 26;
			}
			if (NPC.downedMechBossAny)
			{
				Item.damage = 35;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				Item.damage = 44;
			}
			if (NPC.downedPlantBoss)
			{
				Item.damage = 68;
			}
			if (NPC.downedGolemBoss)
			{
				Item.damage = 80;
			}
			if (NPC.downedFishron)
			{
				Item.damage = 96;
			}
			if (NPC.downedAncientCultist)
			{
				Item.damage = 110;
			}
			if (NPC.downedMoonlord)
			{
				Item.damage = 125;
			}
			base.UpdateInventory(player);
        }

        public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				

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
				
			}
			else
			{
				
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

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			

		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Coral, 15)
				.AddIngredient(ItemType<EssenceOfTheOcean>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
