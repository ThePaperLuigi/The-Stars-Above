using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CarianDarkMoon
{
    public class MoonlitGreatblade : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonlit Greatblade");
            Description.SetDefault("Otherworldly lunar energy grants you strength, but prevents mana regen");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaRegenDelay = 10;
            //player.manaRegen = 0;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
