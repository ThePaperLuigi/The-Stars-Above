using StarsAbove.Projectiles.Summon.Wavedancer;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Wavedancer
{
    public class WavedancerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            if (player.ownedProjectileCounts[ProjectileType<WavedancerSummon>()] > 0)
            {
                modPlayer.WavedancerMinion = true;
            }
            if (!modPlayer.WavedancerMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;

            }

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
