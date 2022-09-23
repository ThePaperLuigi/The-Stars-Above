using Microsoft.Xna.Framework;
using StarsAbove.Utilities;
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
            int powerStrikeStacks = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().powerStrikeStacks;
            tip = LangHelper.GetTextValue("BuffDescription.PowerStrikeBuff", powerStrikeStacks, powerStrikeStacks * 5,
                powerStrikeStacks * 5);

            base.ModifyBuffTip(ref tip, ref rare);
        }
    }
}
