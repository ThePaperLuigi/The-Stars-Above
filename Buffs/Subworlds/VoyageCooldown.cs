using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Subworlds
{
    public class VoyageCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anomaly Voyage Cooldown");
            Description.SetDefault("You can not visit another Anomaly until this debuff expires" +
                "");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.persistentBuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
           
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
