using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Prisms
{
    public class VerdantPrism : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Verdant Prism");
			/* Tooltip.SetDefault("[c/C2BDFF:Tier 1 Stellar Prism]" +
				"\nAffix to a Stellar Nova to gain the following stats:" +
				"\n[c/B6B6B6:0% Damage]" + //0
				"\n[c/83FF4D:+21% Crit Chance]" +//+3
				"\n[c/B6B6B6:0% Crit Damage]" +//0
				"\n[c/FF4D4D:+15 Energy Cost]" + //-3
				""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 1;
		}

		public override bool OnPickup(Player player)
		{
			bool pickupText = false;

			for (int i = 0; i < 50; i++)
			{
				if (player.inventory[i].type == ItemID.None && pickupText == false)
				{
					Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
					CombatText.NewText(textPos, new Color(255, 198, 125, 105), "Stellar Prism acquired!", false, false);
					pickupText = true;
				}
				else
				{

				}
			}
			return true;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 7)
				.DisableDecraft()
				.Register();


		}
	}
}