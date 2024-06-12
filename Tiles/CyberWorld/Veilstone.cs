using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Tiles.CyberWorld
{
    public class Veilstone : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = false;
			Main.tileLighted[Type] = false;
            AddMapEntry(new Color(151, 141, 138));
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }





    }
}