using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.BurningDesire
{
    public class BoilingBloodCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Extinguished");
            // Description.SetDefault("Unable to activate Boiling Blood");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        int lifeDrainTimer;
        public override void Update(Player player, ref int buffIndex)
        {


        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {



        }
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {





            return true;
        }


    }
}
