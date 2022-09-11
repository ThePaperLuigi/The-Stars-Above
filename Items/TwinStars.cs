using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles;
using System;
using Terraria;using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
	public class TwinStars : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twin Stars of Albiero");
			Tooltip.SetDefault("" +
				"Holding this weapon will conjure twin stars to orbit you, granting 100 extra Max Mana" +
				"\nAttacking with this weapon fires powerful beams of piercing starlight" +
				"\nThis weapon drains mana at an incredibly high rate that can not be reduced by any means" +
				"\nCritical strikes grant [c/47D9E7:Binary Magnitude] for a very short time" +
				"\nDuring this time, beams leave devastating napalm in their wake" +
				"\nAbove 250 Mana, this weapon deals 50% more damage" +
				"\n1/8th of Max Mana is added directly to damage" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 35;          
			Item.DamageType = DamageClass.Magic;          
			Item.width = 40;            
			Item.mana = 5;
			Item.height = 40;        
			Item.useTime = 20;         
			Item.useAnimation = 20;       
			Item.useStyle = 5;          
			Item.knockBack = 1;       
			Item.value = Item.buyPrice(gold: 1);          
			Item.rare = ItemRarityID.Cyan;           
			Item.UseSound = SoundID.Item125;      
			//item.autoReuse = false;         
			Item.channel = true;
			Item.value = Item.buyPrice(gold: 1);          
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<Projectiles.TwinStars.TwinStarLaser1>();
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

			player.AddBuff(BuffType<Buffs.TwinStarsBuff>(), 2);
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.TwinStars.TwinStar1>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.TwinStars.TwinStar1>(), 0, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.TwinStars.TwinStar2>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.TwinStars.TwinStar2>(), 0, 4, player.whoAmI, 0f);


			}



			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<Projectiles.TwinStars.TwinStarLaser2>(), damage, 0f, player.whoAmI);
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.GetModPlayer<StarsAbovePlayer>().starPosition1, Vector2.Zero, ProjectileType<Projectiles.TwinStars.TwinStarShine1>(), 0, 0f, player.whoAmI);
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.GetModPlayer<StarsAbovePlayer>().starPosition2, Vector2.Zero, ProjectileType<Projectiles.TwinStars.TwinStarShine2>(), 0, 0f, player.whoAmI);


			player.manaRegenDelay = 240;
			player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

			return true;
            
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.FallenStar, 20)
				.AddIngredient(ItemID.MeteoriteBar, 30)
				.AddIngredient(ItemType<PrismaticCore>(), 6)
				.AddIngredient(ItemType<EssenceOfTwinStars>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
