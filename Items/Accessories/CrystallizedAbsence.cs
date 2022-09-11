using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Materials;

namespace StarsAbove.Items.Accessories
{
	public class CrystallizedAbsence : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Crystallized Absence");

			Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +//In the future, only one Dungeon accessory can be worn at a time. (There's only one for now.)
				"\nMax HP is reduced by 300 and defense is reduced by 20, but damage is doubled" +
				"\nOnly active when unmodified Max HP is 400 or higher" +
				"\n[c/ADEEFF:Only one Stargazer Relic can be equipped at a time]" +
				"\n'From a world cursed to fade away'");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Cyan;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			//player.GetModPlayer<StarsAbovePlayer>().CrystallizedAbsence = true;
			if(player.statLifeMax >= 400)
            {
				player.statLifeMax2 -= 300;
				player.statDefense -= 20;
				player.GetDamage(DamageClass.Generic) += 1f;
            }
			//player.GetDamage(DamageClass.Generic) += 0.12f;
			//player.respawnTimer += 600;

		}

		

		public override void AddRecipes() {
			CreateRecipe(1)
				.AddIngredient(ItemType<InertShard>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
