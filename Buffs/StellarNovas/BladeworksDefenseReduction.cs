using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class BladeWorksDefenseReduction : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        float modifier;
        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
    public class BladeWorksDRNPC : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<BladeWorksDefenseReduction>()))
            {
                modifiers.FinalDamage *= 1.1f;
            }
        }

    }

}
