using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class CrimsonOutbreak : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("Fires a highly accurate three round burst that inflicts [c/C70039:Nanite Plague]" +
				"\nEnemies with [c/C70039:Nanite Plague] will take damage over time and will take more damage from this gun" +
				"\n[c/C70039:Nanite Plague] stacks with every shot, and will increase damage taken by bullets further (Max 20 stacks)" +
				"\nCrits on enemies afflicted with [c/C70039:Nanite Plague] will also deal triple damage" +
				"\n'~directive = KILL while enemies = PRESENT: execute(directive)~'" +
				$""); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 56;
			Item.DamageType = DamageClass.Ranged;
			Item.mana = 14;
			Item.width = 40;
			Item.height = 20;
			Item.useAnimation = 12;
			Item.useTime = 4;
			Item.reuseDelay = 14;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<OutbreakRound>();
			Item.shootSpeed = 20f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}


		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		public override void HoldItem(Player player)
		{
			

			

		}

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
		}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?

			
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
			if (!(player.itemAnimation < Item.useAnimation - 2))
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_outbreakShoot, player.Center);
			}

			return true;
		}

		// How can I get a "Clockwork Assault Rifle" effect?
		// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
		/*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override void OnConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

		// How can I shoot 2 different projectiles at the same time?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

		// How can I choose between several projectiles randomly?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.SpectreBar, 5)
				.AddIngredient(ItemID.Nanites, 20)
				.AddIngredient(ItemType<EssenceOfTheSwarm>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
