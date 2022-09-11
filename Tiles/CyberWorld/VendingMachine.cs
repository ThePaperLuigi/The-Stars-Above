using System;
using Microsoft.Xna.Framework;
using Terraria;using Terraria.ID;
using Terraria.ID;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace StarsAbove.Tiles.CyberWorld
{
	public class VendingMachine : ModTile
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.CoordinateHeights = new[]{16, 16, 16, 18};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Vending Machine");
            AddMapEntry(new Color(200, 200, 200), name);
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[]{ TileID.Books };
		}

		
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX < 66)
			{
				r = 0.9f;
				g = 0.9f;
				b = 0.9f;
			}
		}
		
		public override bool RightClick(int i, int j)
		{
			for (int k = 0; k < 255; k++)
			{
			Player player = Main.player[k];
			if (player.active)
				{
					switch (Main.rand.Next(2))
					{
						case 0:
							player.QuickSpawnItem(null, Mod.Find<ModItem>("Onigiri").Type);
							counter++;
							break;
						case 1:
							player.QuickSpawnItem(null, Mod.Find<ModItem>("JojaCola").Type);
							counter++;
							break;
						
					}
				}
			}
			return true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i,j),i * 16, j * 16, 16, 32, Mod.Find<ModItem>("VendingMachine").Type);
		}
		
		public override void MouseOver(int i, int j)
		{
			int whoAmI = 0;
			Player player = Main.player[whoAmI];
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Mod.Find<ModItem>("VendingMachine").Type;
		}
	}
}