using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class AmmoRecycle : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyclic Hunter");
            Description.SetDefault("Move much faster and gain 10% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;

            
            
        }
    }
}
