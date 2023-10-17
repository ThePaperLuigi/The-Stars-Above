using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using StarsAbove.Systems.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Memories
{

	public abstract class WeaponMemory : ModItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
		{
			Item.GetGlobalItem<ItemMemorySystem>().isMemory = true;

			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}
		

	}
}