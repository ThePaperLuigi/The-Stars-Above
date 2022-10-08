using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items
{
    public class VisionOfEuthymia : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Use this item to summon the [c/C341E9:Eye of Euthymia] for 40 seconds" +
				"\nThis item can be re-used to replenish the duration of [c/C341E9:Eye of Euthymia]" +
				"\nWhen striking foes with projectiles, the [c/C341E9:Eye of Euthymia] will follow-up, dealing 1/5th of damage dealt again, capping at 500" +
                "\nThis follow-up attack has a 2 second cooldown" +
				"\nWhen Mana is consumed while the [c/C341E9:Eye of Euthymia] is present, it is directly added into the [c/8A71D6:Eternity Gauge], with a cap of 1000" +
				"\nThe [c/8A71D6:Eternity Gauge] will naturally increase to 500 over time" +
				"\nThe higher the [c/8A71D6:Eternity Gauge], the lower the cooldown of [c/C341E9:Eye of Euthymia]'s follow-up attack" +
				"\nThe [c/8A71D6:Eternity Gauge] will deplete upon taking damage proportional to half of the damage taken" +
				"\nThe [c/8A71D6:Eternity Gauge] will reset when the Eye of Euthymia disappears" +
				"\n'Fulfill your promise; an everlasting Eternity'" +
				$"");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 40;
			Item.width = 36;
			Item.height = 38;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			//item.UseSound = SoundID.Item26;
			Item.autoReuse = false;
			//item.shoot = ProjectileType<ButterflyProjectile>();
			Item.shootSpeed = 15f;
			
		}
		int upkeepTimer;

		public override bool CanUseItem(Player player)
		{
			



			


			return true;
		}
        public override bool? UseItem(Player player)
        {
			player.AddBuff(BuffType<Buffs.EyeOfEuthymiaBuff>(), 2400);
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.EyeOfEuthymia>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.EyeOfEuthymia>(), 66, 4, player.whoAmI, 0f);


			}
			return base.UseItem(player);
        }

        public override void HoldItem(Player player)
		{

			


			base.HoldItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			
			
			
			return false; // return false because we don't want tmodloader to shoot projectile
		}

		
		public override void AddRecipes()
		{
			CreateRecipe(1)
										.AddIngredient(ItemType<EssenceOfEuthymia>())
				.AddIngredient(ItemType<PrismaticCore>(), 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
