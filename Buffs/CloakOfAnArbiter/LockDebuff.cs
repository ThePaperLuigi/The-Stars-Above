using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CloakOfAnArbiter
{
    public class LockDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blue Orb");
            // Description.SetDefault("Preparing 'Lightbreak Strike'");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.damage = (int)(npc.defDamage * 0.1);

            base.Update(npc, ref buffIndex);
        }
    }
}
