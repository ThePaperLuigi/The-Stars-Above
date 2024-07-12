using StarsAbove.Items.Prisms;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Systems;
using StarsAbove.Systems.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Memories
{
    public class GarridineGadget : WeaponMemory
	{
        public override void SetDefaults()
        {
            Item.GetGlobalItem<ItemMemorySystem>().isMemory = true;

            Item.width = 30;
            Item.height = 30;
            Item.accessory = false;
            Item.value = Item.sellPrice(0);
            Item.rare = ModContent.GetInstance<WeaponMemoryRarity>().Type; // Custom Rarity
        }

        public override void AddRecipes() {
			
		}
	}
}
