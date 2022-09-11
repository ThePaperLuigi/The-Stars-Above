
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using StarsAbove.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using StarsAbove.NPCs.OffworldNPCs;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;

namespace StarsAbove.NPCs
{
	public class VagrantDrops : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return DownedBossSystem.downedVagrant;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops after the Vagrant of Space and Time is vanquished";
		}
	}

	public class MoonLordDrops : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return NPC.downedMoonlord;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops after the Moon Lord is vanquished";
		}
	}
}
