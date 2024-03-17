using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.EternalStar
{
    public class ImmemorialSupernova : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Immemorial Supernova");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {




            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }
    }
}
