using Microsoft.Xna.Framework;
using StarsAbove.Items.Materials;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Prisms
{
	public class PrismOfTheCosmicPhoenix : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Prism of the Cosmic Phoenix");
			Tooltip.SetDefault("[c/FF4F9F:Tier 3 Stellar Prism]" +
				"\nAffix to a Stellar Nova to gain the following abilities:" +
				"\n[c/C05CC8:Stellar Drive]" +
				"\nInstantly gain 100 Mana when casting the Stellar Nova" +
				"\nGain all Mana instead if health is below 100" +
				"\n[c/6D3CB0:Going Supercritical]" +
				"\nInstantly charge the Stellar Nova to near-full when HP drops below 50" +
				"\n2 minute cooldown" +
				"\n[c/796B8C:On Wings of Faith]" +
				"\nLose the ability to fly below 40 Mana" +//0
				"\n[c/C77878:Tier 3 Prisms can only be slotted in the first slot of the Stellar Nova Menu]" +
				"\n'I'm not about to stop here!'" + //-3
				"");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(7, 8));
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
			CreateRecipe(1)
				.AddIngredient(ItemType<TotemOfLightEmpowered>())
				.AddIngredient(ItemType<PrismaticCore>(), 20)
				.AddIngredient(ItemType<PaintedPrism>())
				.AddIngredient(ItemType<SpatialPrism>())
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}