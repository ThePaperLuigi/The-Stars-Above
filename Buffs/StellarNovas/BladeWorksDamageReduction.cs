using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class BladeWorksDamageReduction : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        float modifier;
        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
    public class BladeWorksNPC : GlobalNPC
    {

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if(npc.HasBuff(ModContent.BuffType<BladeWorksDamageReduction>()))
            {
                modifiers.FinalDamage *= 0.9f;
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }
        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }
    }

}
