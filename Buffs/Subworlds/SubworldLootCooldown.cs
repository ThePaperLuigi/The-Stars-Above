using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Subworlds
{
    public class SubworldLootCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subworld Loot Cooldown");
            Description.SetDefault("Subworld Loot can not be claimed until this buff expires.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.persistentBuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
