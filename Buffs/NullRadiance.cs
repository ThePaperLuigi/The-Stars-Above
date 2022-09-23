using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class NullRadiance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Null Radiance");
            Description.SetDefault("Your light has gone dim, reducing outgoing damage by half");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.witheredWeapon = true;
           
        }
    }
}
