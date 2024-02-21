using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarsAbove.Tiles.CyberWorld
{
	public class CubicMountedSignageTile : ModTile
	{
		public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;

            Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 111), CreateMapEntryName());
            DustType = 7;
		}
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 247 / 255f;
            g = 250 / 255f;
            b = 203 / 255f;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
		}
	}
}