using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class StellarSickness : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Sickness");
            Description.SetDefault("Attack and defenses are halved as you adjust to your newfound strength");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.witheredArmor = true;
            player.witheredWeapon = true;
        }
    }
}
