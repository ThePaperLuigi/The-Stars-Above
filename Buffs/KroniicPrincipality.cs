using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class KroniicPrincipality : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kroniic Principality");
            Description.SetDefault("You wield the twin blades of Time");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetDamage(DamageClass.Generic) += 2;
           // player.moveSpeed += 2f;
        }
    }
}
