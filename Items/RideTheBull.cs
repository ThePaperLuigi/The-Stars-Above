using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class RideTheBull : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("Sprays a barrage of powerful bullets" +
				"\nKilling foes will refill a portion of mana and grant Wrath" +
				"\n'Never stop shooting'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 26;
			Item.DamageType = DamageClass.Ranged;
			Item.mana = 2;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Lime;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<HuckleberryRound>();
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		public override void HoldItem(Player player)
		{

			
			base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
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

			if (player.statMana > 5)
			{

				SoundEngine.PlaySound(StarsAboveAudio.SFX_HuckleberryShoot, player.Center);

			}
			else
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_HuckleberryReload, player.Center);
			}

			return true;
		}

		
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 33)
				.AddIngredient(ItemID.SoulofMight, 12)
				.AddIngredient(ItemType<EssenceOfTheBull>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
