using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;

namespace StarsAbove.Items
{
    public class VenerationOfButterflies : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires bursts of fluttering energy at your foe while increasing the [c/F59EEF:Butterfly Gauge]" +
				"\nProjectiles inflict Confusion and deal bonus damage to Confused foes" +
				"\nOnce the [c/F59EEF:Butterfly Gauge] is at its maximum, use right click to enter [c/9B88D7:Butterfly Trance] for 5 seconds" +
				"\nDuring [c/9B88D7:Butterfly Trance], the Mana cost of Veneration of Butterflies drops to zero and you can shoot twice as fast" +
				"\nAdditionally, gain 50% damage reduction and slow falling" +
				$"");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 45;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.width = 36;
			Item.height = 38;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 12;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3;
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item26;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<ButterflyProjectile>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{

			
			if (player.altFunctionUse == 2)
			{

				if (player.GetModPlayer<WeaponPlayer>().ButterflyResourceCurrent == 100)
				{
					if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.ButterflyTrance>()))
					{
						player.GetModPlayer<WeaponPlayer>().ButterflyResourceCurrent = 0;
						player.AddBuff(BuffType<Buffs.ButterflyTrance>(), 300);

						for (int d = 0; d < 10; d++)
						{
							Dust.NewDust(player.position, player.width, player.height, 21, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
						}
						for (int d = 0; d < 5; d++)
						{
							Dust.NewDust(player.position, player.width, player.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
						}



					}
					

				}






			}
			else
			{

			}

			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.ButterflyTrance>()))
			{
				Item.mana = 0;
				Item.useTime = 10;
				Item.useAnimation = 10;
			}
			else
			{
				Item.mana = 10;
				Item.useTime = 20;
				Item.useAnimation = 20;
			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.ButterflyTrance>()))
			{
				player.GetModPlayer<WeaponPlayer>().ButterflyResourceCurrent += 5;


			}
			
			if (player.GetModPlayer<WeaponPlayer>().ButterflyResourceCurrent > 100)
			{
				player.GetModPlayer<WeaponPlayer>().ButterflyResourceCurrent = 100;
			}
			
			
			
			return true; // return false because we don't want tmodloader to shoot projectile
		}

		// What if I wanted an inaccurate gun? (Chain Gun)
		// Inaccurate Gun style: Single Projectile, Random spread 
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;
			return true;
		}*/

		// What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
		// Even Arc style: Multiple Projectile, Even Spread 
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}*/


		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?

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
			  .AddIngredient(ItemID.HallowedBar, 7)
			  .AddIngredient(ItemID.SoulofLight, 14)
			  .AddIngredient(ItemType<EssenceOfButterflies>())
			  .AddTile(TileID.Anvils)
			  .Register();


		}
	}
}
