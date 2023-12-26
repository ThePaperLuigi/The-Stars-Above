using StarsAbove.Systems;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SupremeAuthority
{
    public class DarkAura : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Aura");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int stacks = Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs;
            tip = LangHelper.GetTextValue("BuffDescription.DarkAuraBuff", stacks);

             
        }
        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
