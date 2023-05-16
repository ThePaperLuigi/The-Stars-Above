using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Prisms
{
    public class BurnishedPrism : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Burnished Prism");
			/* Tooltip.SetDefault("[c/FF5934:Tier 2 Stellar Prism]" +
				"\nAffix to a Stellar Nova to gain the following ability:" +
				"\n[c/9C3B3B:Might of the King]" +
				"\nThe Stellar Nova deals 40% increased damage to non-boss enemies, but can no longer deal critical strikes to bosses" +
				"\n" + //0
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

		
	}
}