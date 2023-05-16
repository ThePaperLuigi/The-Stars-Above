using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class AsphodeneBlessing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/5DC5DE:Astral Resonance]");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }
        public static bool disableAspectPenalty;

        public override void Update(Player player, ref int buffIndex)
        {
            //Natural passive unlocks
            player.statManaMax2 += 20;
            if(player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2|| player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
            {
                if(!disableAspectPenalty)
                {
                    //player.GetDamage(DamageClass.Generic) *= 0.9f;
                }
                
            }


            
            
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            Vector2 Center = new Vector2(drawParams.SourceRectangle.Width, drawParams.SourceRectangle.Height) / 2;
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(
                drawParams.Texture,
                new Vector2(drawParams.Position.X + 16, drawParams.Position.Y + 16),
                null,
                Color.White,
                MathHelper.ToRadians(modPlayer.GlobalRotation),
                Center,
                1f,
                SpriteEffects.None,
                1f);
            


            return false;
        }*/
    }
}
