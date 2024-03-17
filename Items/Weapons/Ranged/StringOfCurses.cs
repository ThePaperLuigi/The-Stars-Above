using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Ranged.ElCapitansHardware;
using System;
using StarsAbove.Utilities;
using StarsAbove.Projectiles.Ranged.DevotedHavoc;
using StarsAbove.Projectiles.Ranged.StringOfCurses;
using StarsAbove.Buffs.Ranged.StringOfCurses;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class StringOfCurses : ModItem
	{
		public override void SetStaticDefaults() {
			

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useAnimation = 12;
            Item.useTime = 4;
			Item.crit = 16;
            Item.reuseDelay = 10;
            Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
			Item.shoot = ProjectileType<StringOfCursesShot>();
			Item.shootSpeed = 30f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
		}
		public override bool AltFunctionUse(Player player)
		{
			return false;
		}

		public override void HoldItem(Player player)
		{
           if(player.HasBuff(BuffType<Cursewrought>()) && player.GetModPlayer<StarsAbovePlayer>().novaGauge >= player.GetModPlayer<StarsAbovePlayer>().trueNovaGaugeMax)
			{
				player.AddBuff(BuffID.Ironskin, 2);
				player.AddBuff(BuffID.Swiftness, 2);
				player.AddBuff(BuffID.Heartreach, 2);
			}
            base.HoldItem(player);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float launchSpeed = 120f;
			float launchSpeed2 = 20f;
			float launchSpeed3 = 10f;

			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 Gun = direction * launchSpeed2;
			Vector2 throwableVelocity = direction * launchSpeed3;

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 80f;
            position = new Vector2(position.X, position.Y);
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, Gun.X, Gun.Y, ProjectileType<StringOfCursesGun>(), 0, knockback, player.whoAmI);

            //Normal shooting
            SoundEngine.PlaySound(SoundID.Item11, player.Center);

            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);


            return false;
		}

		
		public override void AddRecipes()
		{
			
		}
	}

}
