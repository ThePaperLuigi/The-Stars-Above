using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class AstarteDriverPrep : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astarte Driver Prep");
            Description.SetDefault("The stars are granting you strength.. in just a second");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.armorEffectDrawShadow = true;
             
            player.velocity = new Vector2(0, -2);
            player.immune = true;
            player.immuneTime = 20;

        }
    }
}
