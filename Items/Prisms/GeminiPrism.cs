using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Prisms
{
    public class GeminiPrism : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gemini Prism");
			Tooltip.SetDefault("[c/FF5934:Tier 2 Stellar Prism]" +
				"\nAffix to a Stellar Nova to gain the following ability:" +
				"\n[c/40F1FF:Twin][c/FF4040:cast]" +
				"\nUpon use of the Stellar Nova, gain 50% of the Stellar Nova Gauge back after 5 seconds (2 minute cooldown)"+
				"\n" + //0
				"");
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

		
	}
}