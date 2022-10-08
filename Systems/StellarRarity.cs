using Microsoft.Xna.Framework;
using Terraria;
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
