
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.SparkblossomBeacon;

namespace StarsAbove.Items
{
    public class SparkblossomBeacon : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sparkblossom's Beacon");
			Tooltip.SetDefault("Summons [c/49EAFA:Fleeting Sparks] to assail foes with homing electricity" +
				"\n[c/49EAFA:Fleeting Sparks] will latch onto foes and ignore terrain when targetting" +
				"\nGain 15% increased Armor Penetration for summon weapons when this weapon is held" +
				"\nEvery time a [c/49EAFA:Fleeting Spark] attacks, it has a 10% chance to explode" +
				"\nWhen a [c/49EAFA:Fleeting Spark] explodes, it deals 2x critical damage to nearby foes and returns" +
                "\nAdditionally, explosions have a 50% chance to generate a Mana Star" +
				"\nEach attack increases the strength of the [c/49EAFA:Fleeting Spark] by 2% (Maximum 10% increased damage, resets upon exploding)" +
				"\n'Keep a good distance - there!'"
				+ $"");

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 0;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item44;

			Item.shoot = ProjectileType<FleetingSparkMinion>();
			Item.buffType = BuffType<Buffs.SparkblossomBeacon.FleetingSparkBuff>(); //The buff added to player after used the item
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

        public override void HoldItem(Player player)
        {
			player.GetArmorPenetration(DamageClass.Summon) += 0.15f;
        }
        public override bool CanUseItem(Player player)
        {
			
			return base.CanUseItem(player);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(4, -22);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			
			
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);

			return false;



		}
		public override void AddRecipes()
		{

			CreateRecipe(1)
				.AddIngredient(ItemID.Wire, 30)
				.AddIngredient(ItemID.IronBar, 6)
				.AddIngredient(ItemID.LargeDiamond, 1)
				.AddIngredient(ItemID.Lever, 2)
				.AddIngredient(ItemID.MechanicalLens, 1)
				.AddIngredient(ItemType<EssenceOfStaticShock>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.Wire, 30)
				.AddIngredient(ItemID.LeadBar, 6)
				.AddIngredient(ItemID.LargeDiamond, 1)
				.AddIngredient(ItemID.Lever, 2)
				.AddIngredient(ItemID.MechanicalLens, 1)
				.AddIngredient(ItemType<EssenceOfStaticShock>())
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
