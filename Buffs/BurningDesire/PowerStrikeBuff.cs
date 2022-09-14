using Microsoft.Xna.Framework;
using Terraria;using Terraria.ID;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.BurningDesire
{
    public class PowerStrikeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Strike");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += (player.GetModPlayer<StarsAbovePlayer>().powerStrikeStacks * 0.05f);
            player.statDefense += player.GetModPlayer<StarsAbovePlayer>().powerStrikeStacks * 5;
        }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = $"{Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().powerStrikeStacks}/5 stacks" +
                $"\nDamage increased by {Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().powerStrikeStacks * 5}%" +
                $"\nDefense increased by {Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().powerStrikeStacks * 5}" +
                $"\n'Do you hear the roar of this chainsaw?'";

            base.ModifyBuffTip(ref tip, ref rare);
        }
    }
}
