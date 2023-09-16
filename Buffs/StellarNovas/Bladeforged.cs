using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class Bladeforged : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        float modifier;
        public override void Update(Player player, ref int buffIndex)
        {
            if(player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
                modifier = MathHelper.Lerp(0.02f, 0.15f, (player.statLife + 1) / 500);
                player.GetDamage(DamageClass.Generic) += modifier;
                player.GetCritChance(DamageClass.Generic) += modifier;
                player.GetArmorPenetration(DamageClass.Generic) += modifier;
                player.GetKnockback(DamageClass.Generic) += modifier;
                player.statDefense += (int)MathHelper.Lerp(0, 8, (player.statLife + 1) / 500);
            }
            else if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                modifier = MathHelper.Lerp(2, 10, (player.statLife + 1) / 500);
                //if boss active, modifier is set at 10.
                //player.GetDamage(DamageClass.Generic) += modifier;
                player.GetCritChance(DamageClass.Generic) += modifier;
                player.GetArmorPenetration(DamageClass.Generic) += modifier;
                player.GetKnockback(DamageClass.Generic) += modifier;
            }
        }
    }
}
