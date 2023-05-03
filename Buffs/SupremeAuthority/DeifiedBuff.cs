using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SupremeAuthority
{
    public class DeifiedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deified");
            Description.SetDefault("" +
                "Sacrifices has halved Max HP, but incoming damage is halved and most debuffs are resisted" +
                "\nCan not be removed by right-click");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 /= 2;

            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Bleeding] = true;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
