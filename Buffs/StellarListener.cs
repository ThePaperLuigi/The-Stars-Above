using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    
    public class StellarListener : ModBuff
    {
        public const float buffRadius = 600; // 100ft, same as shared accessory info

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Listener");
            Description.SetDefault("The nearby performance invigorates you, granting an increase to damage, mana regeneration, and health regeneration");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // campfire/band of regen
            player.lifeRegen += 1;

            // regen band
            player.manaRegenDelayBonus++;
            player.manaRegenBonus += 25;
            player.GetDamage(DamageClass.Generic) *= 1.05f;

        }
    }
}
