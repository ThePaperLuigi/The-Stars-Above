using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BurningDesire
{
    public class PowerStrikeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Power Strike");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += (player.GetModPlayer<WeaponPlayer>().powerStrikeStacks * 0.05f);
            player.statDefense += player.GetModPlayer<WeaponPlayer>().powerStrikeStacks * 5;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int powerStrikeStacks = Main.LocalPlayer.GetModPlayer<WeaponPlayer>().powerStrikeStacks;
            tip = LangHelper.GetTextValue("BuffDescription.PowerStrikeBuff", powerStrikeStacks, powerStrikeStacks * 5,
                powerStrikeStacks * 5);

        }
    }
}
