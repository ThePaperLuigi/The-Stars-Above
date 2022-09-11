using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    
    public class DreamlikeCharisma : ModBuff
    {
        //public const float buffRadius = 600; // 100ft, same as shared accessory info

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dreamlike Charisma");
            Description.SetDefault("Damage is increased, and increased further at max Mana");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            if(player.statMana >= 100)
            {
                player.GetDamage(DamageClass.Generic) *= 1.32f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) *= 1.16f;
            }
            //

        }
    }
}
