using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.TagDamage
{
    public class KingTagDamage : ModBuff
    {
		public override void SetStaticDefaults()
		{
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			// Other mods may check it for different purposes.
			BuffID.Sets.IsATagBuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<KingDebuffNPC>().marked = true;
		}
	}

	public class KingDebuffNPC : GlobalNPC
	{
		// This is required to store information on entities that isn't shared between them.
		public override bool InstancePerEntity => true;

		public bool marked;

		public override void ResetEffects(NPC npc)
		{
			marked = false;
		}
		int randomBuff;
		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			// Only player attacks should benefit from this buff, hence the NPC and trap checks.
			if (marked && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
			{
				modifiers.FlatBonusDamage += 22;

				randomBuff = Main.rand.Next(0, 12);
				if (randomBuff == 0)
				{
					npc.AddBuff(BuffID.Confused, 120);
				}
				if (randomBuff == 1)
				{
					npc.AddBuff(BuffID.CursedInferno, 120);
				}
				if (randomBuff == 2)
				{
					npc.AddBuff(BuffID.Ichor, 120);
				}
				if (randomBuff == 3)
				{
					npc.AddBuff(BuffID.BetsysCurse, 120);
				}
				if (randomBuff == 4)
				{
					npc.AddBuff(BuffID.Midas, 120);
				}
				if (randomBuff == 5)
				{
					npc.AddBuff(BuffID.Poisoned, 120);
				}
				if (randomBuff == 6)
				{
					npc.AddBuff(BuffID.Venom, 120);
				}
				if (randomBuff == 7)
				{
					npc.AddBuff(BuffID.OnFire, 120);
				}
				if (randomBuff == 8)
				{
					npc.AddBuff(BuffID.Frostburn, 120);
				}
				if (randomBuff == 9)
				{
					npc.AddBuff(BuffID.ShadowFlame, 120);
				}
				if (randomBuff == 10)
				{
					npc.AddBuff(BuffID.Oiled, 120);
				}
				if (randomBuff == 11)
				{
					npc.AddBuff(BuffID.Bleeding, 120);
				}
			}
		}
	}
}

