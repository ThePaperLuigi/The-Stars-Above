using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Youmu
{
    public class DeathsDance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death's Dance");
            Description.SetDefault("A graze with death has granted you strength");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.3f;
            
        }
    }
}
