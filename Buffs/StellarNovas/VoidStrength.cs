using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{

    public class VoidStrength : ModBuff
    {
        //public const float buffRadius = 600; // 100ft, same as shared accessory info

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Strength Of The Void");
            // Description.SetDefault("Damage is increased drastically");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {


            player.GetDamage(DamageClass.Generic) += 3f;

            //

        }
    }
}
