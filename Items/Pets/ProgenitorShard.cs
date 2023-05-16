using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Items.Materials;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace StarsAbove.Items.Pets
{
    public class ProgenitorShard : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Progenitor's Shard");
			/* Tooltip.SetDefault("Summons a discount Warrior of Light" +
                "\n'I will transcend you!'"); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ProjectileType<Projectiles.Pets.WarriorPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.buffType = BuffType<Buffs.WarriorPetBuff>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
										.AddIngredient(ItemType<PrismaticCore>(), 3)
				.AddIngredient(ItemType<DullTotemOfLight>(), 1)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}