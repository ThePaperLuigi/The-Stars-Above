using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class AstarteDriverEnemyCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astarte Driver Enemy Cooldown");
            Description.SetDefault("Enemy only debuff.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
