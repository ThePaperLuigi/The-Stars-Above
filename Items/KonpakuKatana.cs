using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using System;
using StarsAbove.Projectiles.Youmu;
using StarsAbove.Buffs.Youmu;

namespace StarsAbove.Items
{
    public class KonpakuKatana : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("" +
                "Holding this weapon conjures a Phantom Spirit to attack foes" +
                "\nAttacks fire quick long-ranged bullets" +
				"\nProximity to hostile projectiles or enemies with contact damage grants the buff Death's Dance for 2 seconds" +
                "\nDeath's Dance grants 30% increased damage" +
				"\nRight click to activate [c/A6F3C4:Phantom Slash], spinning the weapon around you for 3x base damage (8 second cooldown)" +
				"\nIf Death's Dance is currently active, [c/A6F3C4:Phantom Slash] will do 6x base damage instead and grant Swiftness for 4 seconds" +
				"\n'If there is something I can't make clear, I try cutting it!'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 12;
			Item.DamageType = DamageClass.Summon;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Green;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item11;
			Item.shoot = ProjectileType<Projectiles.Youmu.YoumuRound>();
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			if (player.ownedProjectileCounts[ProjectileType<YoumuSpirit>()] < 1)
			{
				player.AddBuff(BuffType<PhantomMinion>(), 999);
				
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<YoumuSpirit>(), Item.damage, 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;

			}


			Projectile closest = null;
			float closestDistance = 9999999;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile projectile = Main.projectile[i];
				float distance = Vector2.Distance(projectile.Center, player.Center);


				if (projectile.hostile && projectile.Distance(player.Center) < closestDistance && projectile.damage > 0)
				{
					closest = projectile;
					closestDistance = projectile.Distance(player.Center);
				}




			}
			NPC closestnpc = null;
			float closestnpcDistance = 9999999;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, player.Center);


				if (npc.active && npc.Distance(player.Center) < closestnpcDistance && npc.CanBeChasedBy() && npc.damage > 0)
				{
					closestnpc = npc;
					closestnpcDistance = npc.Distance(player.Center);
				}




			}
			if (closestDistance < 88f || closestnpcDistance < 88f)
            {
				player.AddBuff(BuffType<DeathsDance>(), 120);

				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 80);
					offset.Y += (float)(Math.Cos(angle) * 80);

					Dust d = Dust.NewDustPerfect(player.Center + offset, 91, Vector2.Zero, 200, default(Color), 0.7f);
					d.fadeIn = 0.1f;
					d.noGravity = true;
				}

			}
			else
            {
				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 85);
					offset.Y += (float)(Math.Cos(angle) * 85);

					Dust d = Dust.NewDustPerfect(player.Center + offset, 20, Vector2.Zero, 200, default(Color), 0.4f);
					d.fadeIn = 0.1f;
					d.noGravity = true;
				}
			}
			

			base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			
			if(player.altFunctionUse == 2 && player.HasBuff(BuffType<YoumuCooldown>()))
            {
				return false;
            }
			return base.CanUseItem(player);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 5);
		}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse == 2)
			{
				player.AddBuff(BuffType<YoumuCooldown>(), 480);
				type = ProjectileType<Projectiles.Youmu.YoumuSpin>();
				damage *= 3;
				if(player.HasBuff(BuffType<DeathsDance>()))
                {
					damage *= 2;
					player.AddBuff(BuffID.Swiftness, 240);
                }
			}
			else
			{
				
			}

			int index = Projectile.NewProjectile(source, position, velocity, type, Item.damage, 4, player.whoAmI, 0f);


			Main.projectile[index].originalDamage = Item.damage;
			return false;
		}

		
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.SilverBar, 17)
				.AddIngredient(ItemType<EssenceOfTheGardener>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.TungstenBar, 17)
				.AddIngredient(ItemType<EssenceOfTheGardener>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
