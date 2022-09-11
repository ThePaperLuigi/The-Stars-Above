using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace StarsAbove.Buffs
{
    public class Determination : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowbringer");
            Description.SetDefault("Dream now of a dark tomorrow... Damage and defenses have been drastically increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (Main.expertMode)
                {
                    if ((bool)calamityMod.Call("GetDifficultyActive", "revengeance"))
                    {
                        if ((bool)calamityMod.Call("GetDifficultyActive", "malice") || (bool)calamityMod.Call("GetDifficultyActive", "death"))
                        {
                            player.GetDamage(DamageClass.Generic) *= 1.25f;
                            player.statDefense += 5;
                        }
                        else
                        {
                            player.GetDamage(DamageClass.Generic) *= 1.5f;
                            player.statDefense += 12;
                        }
                    }
                    else
                    {
                        player.GetDamage(DamageClass.Generic) *= 1.5f;
                        player.statDefense += 25;
                    }
                    
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) *= 2f;
                    player.statDefense += 50;
                }
            }
            else
            {
                if (Main.expertMode)
                {
                    player.GetDamage(DamageClass.Generic) *= 1.5f;
                    player.statDefense += 25;
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) *= 2f;
                    player.statDefense += 50;
                }
            }
            



        }

    }
}
