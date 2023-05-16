using StarsAbove.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.ManiacalJustice
{
    public class LVBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("LV");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int LVAmount = Main.LocalPlayer.GetModPlayer<WeaponPlayer>().LVStacks;

            if (LVAmount == 100)
            {

            }
            else if (LVAmount >= 50)
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.3f;

            }
            else if (LVAmount >= 20)
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.2f;

            }
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int LVAmount = Main.LocalPlayer.GetModPlayer<WeaponPlayer>().LVStacks;
            tip = LangHelper.GetTextValue("BuffDescription.LVBuff", LVAmount);
            if(LVAmount == 100)
            {
                tip += LangHelper.GetTextValue("BuffDescription.LVBuff100", LVAmount);
            }
            else if(LVAmount >= 50)
            {
                tip += LangHelper.GetTextValue("BuffDescription.LVBuff50", LVAmount);

            }
            else if(LVAmount >= 20)
            {
                tip += LangHelper.GetTextValue("BuffDescription.LVBuff20", LVAmount);

            }

            base.ModifyBuffText(ref tip, ref rare);
        }
    }
}
