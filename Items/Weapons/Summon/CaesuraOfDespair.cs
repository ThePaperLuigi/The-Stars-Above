using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class CaesuraOfDespair : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cæsura of Despair");
			/* Tooltip.SetDefault("" +
				"Holding this weapon will transform you into a [c/E03166:Fae Nephilim]" +
				"\nAttacks will pierce enemies and deal minion damage" +
				"\nFlight time is infinite as a [c/E03166:Fae Nephilim] " +
				"\nAdditionally, three [c/D36F6F:Nephilim Shards] and the [c/D36F9C:Irys] will surround you" +
				"\nThe [c/D36F6F:Nephilim Shards] will independently target and attack nearby foes" +
				"\nThe [c/D36F9C:Irys] will inflict [c/D7367F:Irys Gaze] on one nearby enemy, ignoring player targetting" +
				"\nFoes affected by [c/D7367F:Irys Gaze] take 50 extra damage from all sources and allows for minions to crit at a 15% chance" +
				"\n'Hope has descended'" +
				$""); */  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 58;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 40;            //Weapon's texture's width
			Item.mana = 4;
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 1;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Cyan;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item125;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<IrysBolt>();
			Item.shootSpeed = 14f;
		}
		 
		public override bool AltFunctionUse(Player player)
		{
			return false;
		}

		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			player.AddBuff(BuffType<Buffs.IrysBuff>(), 2);
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Irys>()] < 1)
			{

				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Irys>(), 0, 4, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = Item.damage;

			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.IrysCrystalMain>()] < 1)
			{
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.IrysCrystalMain>(), 0, 4, player.whoAmI, 0f);

				Main.projectile[index].originalDamage = Item.damage;

			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.IrysCrystal1>()] < 1)
			{

				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.IrysCrystal1>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;
			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.IrysCrystal2>()] < 1)
			{
				
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.IrysCrystal2>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);

				Main.projectile[index].originalDamage = Item.damage;

			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.IrysCrystal3>()] < 1)
			{
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.IrysCrystal3>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;

			}



			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vector8 = new Vector2(player.Center.X -2, player.Center.Y - 7);
			 
			float rotation = (float)Math.Atan2(player.Center.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), player.Center.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));

			for (int d = 0; d < 25; d++)
			{
				float Speed2 = Main.rand.NextFloat(3, 33);  //projectile speed
				Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
				int dustIndex = Dust.NewDust(vector8, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
			}
			for (int d = 0; d < 45; d++)
			{
				float Speed3 = Main.rand.NextFloat(3, 17);  //projectile speed
				Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
				int dustIndex = Dust.NewDust(vector8, 0, 0, 90, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
				Main.dust[dustIndex].noGravity = true;
			}
			int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			Main.projectile[index].originalDamage = Item.damage;
			return false;
            
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.AngelWings, 1)
				.AddIngredient(ItemID.DemonWings, 1)
				.AddIngredient(ItemID.SoulofLight, 7)
				.AddIngredient(ItemID.SoulofNight, 7)
				.AddIngredient(ItemType<EssenceOfIRyS>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
