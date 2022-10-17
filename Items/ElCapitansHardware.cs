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
    public class ElCapitansHardware : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("El Capitan's Hardware");
			Tooltip.SetDefault("WORK IN PROGRESS WEAPON" +
                "\nAttacks with this weapon pierce foes and bounce off walls up to 4 times" +
				"\nStriking foes charges the [??? Gauge] (Critical strikes grant more charge)" +
                "\nOnce the [??? Gauge] is above 50%, right-click to consume the [??? Gauge], swapping weapons and firing a [Gyro Disk]" +
                "\nThe [Gyro Disk] targets and attacks up to 3 nearby foes, and can be moved by right-clicking" +
                "\nAfter 10 seconds, the [Gyro Disk] disappears and the [??? Gauge] can gain energy again" +
                "\nIf the [??? Gauge] is higher than 50% on activation, the [Gyro Disk] gains bonus damage proportional to the extra amount consumed" +
				"\n'GAME RIGHTS?!'" +
				$"");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 33;
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

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int d = 0; d < 27; d++)//Visual effects
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(55));
				float scale = 3f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale;
				int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;

			}
			return true;
		}

		
		public override void AddRecipes()
		{
			
		}
	}
}
