using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Tiles
{
    public class InvisibleWallTile : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			
		}

		

		

		
	}
}