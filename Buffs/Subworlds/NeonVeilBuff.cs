using Terraria.ID;

namespace StarsAbove.Buffs.Subworlds
{
    public class NeonVeilBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // Do not show the remaining buff time
            Main.buffNoTimeDisplay[Type] = true;
            
            // Prevents the player from clearing this buff using right click
            Main.debuff[Type] = true;
            
            // Make sure the Nurse NPC cannot remove this (de)buff
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // When the player is holding an item that can mine tiles
            if (player.HeldItem.pick > 0)
            {
                // Decreases swing speed by 60%
                player.GetAttackSpeed(DamageClass.Generic) -= 0.6f;
            }

            // Decreases mining speed by 60% (higher value means slower)
            player.pickSpeed += 0.6f;
        }
    }
}