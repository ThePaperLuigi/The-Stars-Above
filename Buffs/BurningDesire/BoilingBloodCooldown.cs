using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.BurningDesire
{
    public class BoilingBloodCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extinguished");
            Description.SetDefault("Unable to activate Boiling Blood");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        int lifeDrainTimer;
        public override void Update(Player player, ref int buffIndex)
        {
            
               
        }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            
              

            base.ModifyBuffTip(ref tip, ref rare);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {


            


            return true;
        }

        
    }
}
