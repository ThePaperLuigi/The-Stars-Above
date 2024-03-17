using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.TheOnlyThingIKnowForReal
{
    public class ImpactRecoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Impact Recoil");
            // Description.SetDefault("Guntrigger Execution has halved damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetDamage(DamageClass.Generic) += 0.5f;
            //player.statDefense -= 30;
        }
    }
}
