using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.Kifrosse
{
    public class AmaterasuWinter : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amaterasu's Winter");
            // Description.SetDefault("Mystic energy empowers the Blizzard Foxfires");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}