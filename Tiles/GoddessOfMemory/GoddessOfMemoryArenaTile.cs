using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarsAbove.Tiles.GoddessOfMemory
{
    public abstract class GoddessOfMemoryArenaTile : ModTile
    {
        public override string Texture => "StarsAbove/Tiles/BossLoot/Empty";

        public abstract string MaskTexture { get; }
        public abstract string BackgroundTexture { get; }
        public abstract string BorderTexture { get; }
        public virtual float BorderScale => 0.95f;
        public virtual float RotationSpeed => 0f;
        public virtual Color MarkerColor => new Color(200, 150, 255);

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileWaterDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorTop = AnchorData.Empty;
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.AnchorLeft = AnchorData.Empty;
            TileObjectData.newTile.AnchorRight = AnchorData.Empty;
            TileObjectData.newTile.AnchorWall = false;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.WaterDeath = false;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.addTile(Type);

            AddMapEntry(MarkerColor, CreateMapEntryName());
        }

        public override bool CanDrop(int i, int j)
        {
            return false;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            return false;
        }
    }
}
