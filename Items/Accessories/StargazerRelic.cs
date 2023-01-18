using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{

	public abstract class StargazerRelic : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}
		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (slot < 10) // This allows the accessory to equip in vanity slots with no reservations
			{
				// Here we use named ValueTuples and retrieve the index of the item, since this is what we need here
				int index = FindDifferentEquippedExclusiveAccessory().index;
				if (index != -1)
				{
					return slot == index;
				}
			}
			return base.CanEquipAccessory(player, slot, modded);
		}

		// We make our own method for compacting the code because we will need to check equipped accessories often
		// This method returns a named ValueTuple, indicated by the (Type name1, Type name2, ...) as the return type
		// This allows us to return more than one value from a method
		protected (int index, Item accessory) FindDifferentEquippedExclusiveAccessory()
		{
			int maxAccessoryIndex = 5 + Main.LocalPlayer.extraAccessorySlots;
			for (int i = 3; i < 3 + maxAccessoryIndex; i++)
			{
				Item otherAccessory = Main.LocalPlayer.armor[i];
				// IsAir makes sure we don't check for "empty" slots
				// IsTheSameAs() compares two items and returns true if their types match
				// "is ExclusiveAccessory" is a way of performing pattern matching
				// Here, inheritance helps us determine if the given item is indeed one of our ExclusiveAccessory ones
				if (!otherAccessory.IsAir &&
					Item.type != otherAccessory.type &&
					otherAccessory.ModItem is StargazerRelic)
				{
					// If we find an item that matches these criteria, return both the index and the item itself
					// The second argument is just for convenience, technically we don't need it since we can get the item from just i
					return (i, otherAccessory);
				}
			}
			// If no item is found, we return default values for index and item, always check one of them with this default when you call this method!
			return (-1, null);
		}
	}
}