using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace StarsAbove.Buffs
{
    public class UmbralRapture : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Umbral Rapture");
            Description.SetDefault("Gain HP and Invincibility when on Eridani's tiles");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var tilePos = player.Bottom.ToTileCoordinates16();
            Tile tile = Framing.GetTileSafely(tilePos.X, tilePos.Y);
            if (tile.TileType != TileID.AmethystGemspark)
            {
                
            }
            else
            {
                player.statLife++;
                player.immune = true;
                player.immuneTime = 240;
            }

        }

    }
}
