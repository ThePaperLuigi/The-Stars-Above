using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.DreamersInkwell;
using StarsAbove.Projectiles.IrminsulDream;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Magic
{
    public class DreamersInkwell : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.AnimatesAsSoul[Item.type] = false;
		}

		public override void SetDefaults()
		{
			Item.damage = 110;          
			Item.DamageType = DamageClass.Magic;          
			Item.width = 40;            
			Item.height = 40;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.autoReuse = true;
			Item.channel = false;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;       
			Item.value = Item.buyPrice(gold: 1);          
			Item.rare = ItemRarityID.Lime;           
			Item.value = Item.buyPrice(gold: 1);          
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<InkwellEarthInk>();
			Item.shootSpeed = 0f;
		}
		 
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				SoundEngine.PlaySound(SoundID.Item150, null);
				for (int d = 0; d < 14; d++)
				{
					Dust.NewDust(player.Center, 0, 0, DustID.PurificationPowder, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
				}
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile other = Main.projectile[i];

					if (other.active && other.owner == player.whoAmI &&
						(other.type == ProjectileType<InkwellAirInk>() 
						|| other.type == ProjectileType<InkwellEarthInk>()
						|| other.type == ProjectileType<InkwellFireInk>()
						|| other.type == ProjectileType<InkwellWaterInk>()))
					{
						other.Kill();
					}
				}
			}
			else if(player.GetModPlayer<WeaponPlayer>().InkwellMana <= 0)
			{
				return false;
            }
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<WeaponPlayer>().InkwellHeld = true;
			if (player.ownedProjectileCounts[ProjectileType<InkwellHeld>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<InkwellHeld>(), 0, 0, player.whoAmI, 0f);


			}
		}
        public override bool? UseItem(Player player)
        {
			if(player.altFunctionUse == 2)
            {

            }
			else if (player.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0 && player.statMana > 0 && player.whoAmI == Main.myPlayer)
			{
				SoundEngine.PlaySound(SoundID.Item9, null);

				if (player.GetModPlayer<WeaponPlayer>().InkwellInk == 0)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<InkwellEarthInk>(), player.GetWeaponDamage(Item)/2, 0, player.whoAmI, 0f);

				}
				if (player.GetModPlayer<WeaponPlayer>().InkwellInk == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<InkwellAirInk>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

				}
				if (player.GetModPlayer<WeaponPlayer>().InkwellInk == 2)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<InkwellFireInk>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

				}
				if (player.GetModPlayer<WeaponPlayer>().InkwellInk == 3)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<InkwellWaterInk>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

				}
			}
			
			return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			//Earth Ink
			


			//player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.PurpleMucos)
				.AddIngredient(ItemID.BlackInk)
				.AddIngredient(ItemID.VialofVenom)
				.AddIngredient(ItemID.SoulBottleNight)
				.AddIngredient(ItemID.EmptyDropper)
				.AddIngredient(ItemType<EssenceOfDreams>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
