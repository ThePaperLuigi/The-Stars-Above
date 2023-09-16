using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Systems
{

    public class StarsAboveGlobalBuff : GlobalBuff
    {
        public static bool DisableManaSicknessChange;

        public override void Update(int type, Player player, ref int buffIndex)
        {
            // If the player gets the Chilled debuff while he already has more than 5 other buffs/debuffs, limit the max duration to 3 seconds
            if (type == BuffID.ManaSickness && !DisableManaSicknessChange)
            {
                //Replace mana damage reduction will a reduction in all damage.
                player.GetDamage(DamageClass.Magic) *= 1f + player.manaSickReduction;

                player.GetDamage(DamageClass.Generic) *= 1f - player.manaSickReduction;
            }
        }

        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;


            if (type == BuffID.ManaSickness && !DisableManaSicknessChange)
            {
                tip = LangHelper.GetTextValue($"Buffs.ManaSicknessAlter.Description", (int)(player.manaSickReduction * 100f) + 1);
            }

        }

    }
}