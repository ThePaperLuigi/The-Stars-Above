using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.Kifrosse
{
    public class AmaterasuGrace : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amaterasu's Grace");
            // Description.SetDefault("Mystic energy grants effective damage against foes inflicted with frostburn");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}