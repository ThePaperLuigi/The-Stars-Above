using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.ArachnidNeedlepoint
{
    public class WebOfNeedles : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Web of Needles");
            Description.SetDefault("Summon damage is increased by 30%");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.GetDamage(DamageClass.Summon) += 0.3f;

            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }
    }
}
