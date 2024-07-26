using StarsAbove.Projectiles.Magic.CloakOfAnArbiter;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.CloakOfAnArbiter
{
    public class FairyTagDamage : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
            // Other mods may check it for different purposes.
            BuffID.Sets.IsATagBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<FairyDebuffNPC>().marked = true;
        }
    }

    public class FairyDebuffNPC : GlobalNPC
    {
        // This is required to store information on entities that isn't shared between them.
        public override bool InstancePerEntity => true;

        public bool marked;

        public override void ResetEffects(NPC npc)
        {
            marked = false;
        }

        // TODO: Inconsistent with vanilla, increasing damage AFTER it is randomised, not before. Change to a different hook in the future.
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            // Only player attacks should benefit from this buff, hence the NPC and trap checks.
            if (marked && !projectile.npcProj && !projectile.trap)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X + Main.rand.Next(-10, 10), npc.Center.Y + Main.rand.Next(-10, 10), 0, 0, ModContent.ProjectileType<FairyAttackEffect>(), 0, 0, projectile.owner, 0f, 0f, 1f);

                }
                modifiers.FlatBonusDamage += 20;
            }
        }
    }
}

