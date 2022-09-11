using Microsoft.Xna.Framework;
using StarsAbove.NPCs;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class RyukenStun : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ryuken Stun");
            Description.SetDefault("Ow.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity = Vector2.Zero;
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().RyukenStun = true;
        }
    }
}
