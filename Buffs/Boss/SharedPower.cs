using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class SharedPower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binding Light");
            // Description.SetDefault("Entrapped by coils of light");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) -= 0.1f;
            player.statDefense -= 10;
            player.statLifeMax2 -= 100;
          
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            

            base.Update(npc, ref buffIndex);
        }
        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
