using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class BearerOfLight : ModBuff
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
                player.lifeRegen += 5;
            }
            else if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                player.GetDamage(DamageClass.Generic) += 0.08f;
            }
        }
    }
}
