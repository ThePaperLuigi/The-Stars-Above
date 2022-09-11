using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
	public class StellarRarity : ModRarity
	{
		public override Color RarityColor => new Color(Main.masterColor + 0.5f, -Main.masterColor, 0.8f);
		

		public override int GetPrefixedRarity(int offset, float valueMult)
		{

			
			return Type; // no 'higher' tier to go to, so return the type of this rarity.
		}
	}
}
