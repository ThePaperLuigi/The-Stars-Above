using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Kifrosse
{
    public class AmaterasuWinterCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amaterasu's Winter Cooldown");
            Description.SetDefault("Amaterasu's Winter will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
