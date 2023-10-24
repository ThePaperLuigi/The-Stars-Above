using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class WeaponMemoryRarity : ModRarity
	{
		public override Color RarityColor => new Color(Main.masterColor + 0.4f, Main.masterColor + 0.5f, 0.7f);
		

		public override int GetPrefixedRarity(int offset, float valueMult)
		{

			
			return Type; // no 'higher' tier to go to, so return the type of this rarity.
		}
	}
}
