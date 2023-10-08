using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Memories
{

	public abstract class WeaponMemory : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}
		

	}
}