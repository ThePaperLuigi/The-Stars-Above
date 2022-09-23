using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CarianDarkMoon
{
    public class PrepDarkmoon : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonlit Greatblade");
            Description.SetDefault("Otherworldly lunar energy grants you strength");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.velocity = Vector2.Zero;
        }
    }
}
