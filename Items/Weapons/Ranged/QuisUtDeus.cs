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
using StarsAbove.Projectiles.Ranged.CrimsonOutbreak;
using StarsAbove.Projectiles.Ranged.QuisUtDeus;
using StarsAbove.Projectiles.Ranged.IzanagisEdge;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class QuisUtDeus : ModItem
	{
		public override void SetStaticDefaults() {
			
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 77;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useAnimation = 5;
			Item.useTime = 5;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Red;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<QuisUtDeusRound>();
			Item.shootSpeed = 20f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
		}


		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		public override void HoldItem(Player player)
		{
			

			

		}

			
		public override bool? UseItem(Player player)
		{
			

			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
            position += new Vector2(position.X, position.Y - 9);

            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, 0, 0, ProjectileType<QuisUtDeusGun>(), 0, knockback, player.whoAmI);
            SoundEngine.PlaySound(SoundID.Item11, player.Center);


            return true;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
