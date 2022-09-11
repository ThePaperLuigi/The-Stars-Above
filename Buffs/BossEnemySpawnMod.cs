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
    public class BossEnemySpawnMod : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overwhelming Presence");
            Description.SetDefault("The powerful foe before you prevents natural enemy spawns");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }
        
        public override void Update(Player player, ref int buffIndex)
        {
           

            
            
        }

    }
}
