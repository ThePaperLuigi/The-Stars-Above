using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class Determination : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shadowbringer");
            // Description.SetDefault("Dream now of a dark tomorrow... Damage and defenses have been drastically increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 2f;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (Main.expertMode)
                {
                    if ((bool)calamityMod.Call("GetDifficultyActive", "revengeance"))
                    {
                        if ((bool)calamityMod.Call("GetDifficultyActive", "malice") || (bool)calamityMod.Call("GetDifficultyActive", "death"))
                        {

                            player.statDefense += 5;
                        }
                        else
                        {

                            player.statDefense += 12;
                        }
                    }
                    else
                    {

                        player.statDefense += 25;
                    }

                }
                else
                {

                    player.statDefense += 50;
                }
            }
            else
            {
                if (Main.expertMode)
                {

                    player.statDefense += 25;
                }
                else
                {

                    player.statDefense += 50;
                }
            }




        }

    }
}
