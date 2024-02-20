using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Tiles.CyberWorld
{
    public class DeepAsphalt : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = false;
			Main.tileLighted[Type] = false;
            AddMapEntry(new Color(18, 23, 23));
        }

		

		

		
	}
}