using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Melee.RebellionBloodArthur;

namespace StarsAbove.Items.Weapons.Melee
{
    public class RebellionBloodArthur : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 119;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 96;            //Weapon's texture's width
			Item.height = 96;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.shoot = 10;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				

			}
			
			return base.CanUseItem(player);
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<WeaponPlayer>().RebellionHeld = true;
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			//player.GetModPlayer<WeaponPlayer>().radiance++;
			
		}
		public int attackType;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			switch (attackType)
			{
				case 0: //Swing downwards
					Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<RebellionSword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
					attackType++;
					return false;
				case 1: //Swing upwards
					Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<RebellionSword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
					if(player.GetModPlayer<WeaponPlayer>().rebellionGauge>=15)
                    {
						attackType++;

					}
					else
                    {
						attackType = 0;

					}
					return false;
				case 2: //Spin
					Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<RebellionSwordSpin>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
					attackType = 0;
					return false;
			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.LifeFruit, 3)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.SoulofLight, 12)
				.AddIngredient(ItemID.EyeoftheGolem, 1)
				.AddIngredient(ItemType<EssenceOfRadiance>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
