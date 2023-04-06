using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.KazimierzSeraphim;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class KazimierzSeraphim : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Attacks with this weapons swing in a wide arc" +
				"\nHolding this weapon will conjure a [c/FF8B00:Brilliant Spark] to orbit you, dealing contact damage" +
				"\nStriking foes with this weapon grants stacks of [c/F1AF42:Radiance] (Up to 10)" +
				"\nCritical strikes grant two stacks of [c/F1AF42:Radiance]" +
				"\nWhen the [c/FF8B00:Brilliant Spark] strikes an enemy, it will consume all stacks of [c/F1AF42:Radiance]" +
				"\nEach stack of [c/F1AF42:Radiance] will contribute 5 extra damage" +
				"\nAt 5 or more stacks, the attack will always crit" +
				"\n'The light holds resolute here!'" +
				$"");  //The (English) text shown below your weapon's name

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 26;           //The damage of your weapon
			Item.DamageType = DamageClass.SummonMeleeSpeed;          //Is your weapon a melee weapon?
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Yellow;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.shoot = ProjectileType<KazimierzSlash1>();
		}
		bool altSwing;
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
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when swing the sword
				
					Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 269);
				
				
				
			}
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<WeaponPlayer>().seraphimHeld = 10;
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.KazimierzSeraphimProjectile>()] < 1)
			{
				
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.KazimierzSeraphimProjectile>(), 39, 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;

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
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse != 2)
			{
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazimierzSlash2>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX2>(), 0, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazimierzSlash1>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX1>(), 0, knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazimierzSlash1>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX1>(), 0, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazimierzSlash2>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX2>(), 0, knockback, player.whoAmI, 0f);


						altSwing = true;
					}
				}
			}

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.EnchantedSword, 1)
				.AddIngredient(ItemID.Starfury, 1)
				.AddIngredient(ItemType<EssenceOfThePegasus>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
