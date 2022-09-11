
using Microsoft.Xna.Framework;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Tiles.CyberWorld
{
	public class ChainlinkFenceWall : ModWall
	{
		public override void SetStaticDefaults() {
			Main.wallHouse[Type] = true;
			//drop = ItemType<Items.Placeable.CyberWorld.ChainlinkFence>();
			AddMapEntry(new Color(150, 150, 150));
		}

		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.4f;
			g = 0.4f;
			b = 0.4f;
		}
	}
}