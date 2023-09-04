using Microsoft.Xna.Framework;
using StarsAbove.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Prisms
{
    public class PrismOfTheRuinedKing : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Prism of the Ruined King");
			/* Tooltip.SetDefault("[c/FF4F9F:Tier 3 Stellar Prism]" +
				"\nAffix to a Stellar Nova to gain the following abilities:" +
				"\n[c/6ECBA5:Harrowed Path]" +
				"\nUpon cast of the Stellar Nova, gain 'Sovereign's Dominion'" +
				"\nDuring Sovereign's Dominion, gain 30% Movement Speed and 10% Attack Damage" +
				"\nSovereign's Dominion lasts for 15 seconds" + //0
				"\n[c/527060:Blade of the Ruined King]" +
				"\nCritical hits on foes below 50% HP will inflict Ruination, dealing damage over time" +
				"\nRuination lasts for 30 seconds and can be applied without use of the Stellar Nova" +
				"\nDuring Sovereign's Dominion, critical hits on foes inflicted with Ruination will also heal you for a portion of damage" +//+3
				"\n[c/9AC89F:The Mist Recedes]" +
				"\nLose Stellar Nova Energy upon getting hit" +//0
				"\n[c/C77878:Tier 3 Prisms can only be slotted in the first slot of the Stellar Nova Menu]" +
				"\n'You are not the hero of this tale! You are not anything!'" + //-3
				""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ItemID.Sets.ItemNoGravity[Item.type] = true;
			
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Expert;
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
			
			
		}
	}
}