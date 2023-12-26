using Microsoft.Xna.Framework;
using StarsAbove.Items.Prisms;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace StarsAbove.Items.Pets
{
    public class StarstruckAxe : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Starstruck Axe");
			/* Tooltip.SetDefault("Summons a diamond in the rough" +
				"\n'It's still stellar'"); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ProjectileType<Projectiles.Pets.SuiseiPet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.buffType = BuffType<Buffs.Pets.SuiseiPetBuff>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<Materials.StellarRemnant>(), 10)
				.AddCustomShimmerResult(ItemType<Materials.StellarRemnant>(), 3)
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