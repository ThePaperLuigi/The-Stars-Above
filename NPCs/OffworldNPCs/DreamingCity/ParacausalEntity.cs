using StarsAbove.Items.Materials;
using StarsAbove.Items.Memories;
using SubworldLibrary;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Subworlds.ThirdRegion;
using StarsAbove.Utilities;
using Terraria.DataStructures;
using System.Numerics;
using StarsAbove.Buffs.Boss;

namespace StarsAbove.NPCs.OffworldNPCs.DreamingCity
{
    public class ParacausalEntity : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Headpiercer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.NebulaBrain];
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true // Hides this NPC from the bestiary
			};
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			

			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}
		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.NebulaBrain);
			
			NPC.damage = 40;
			NPC.defense = 0;
			NPC.lifeMax = 3000;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.NebulaFloater;
			AnimationType = NPCID.NebulaBrain;
			AIType = NPCID.NebulaBrain;

			NPC.noTileCollide = false;
			
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandedTenebrium>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StellarRemnant>(), 10, 1, 2));

        }
        public override void AI()
		{
			Lighting.AddLight(NPC.Center, TorchID.White);
			NPC.velocity *= 0.90f;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                if (p.active && p.Distance(NPC.Center) < 500 && !p.dead)
                {
					p.AddBuff(ModContent.BuffType<Vulnerable>(), 10);
                }
            }
            int dustType = DustID.GemDiamond;
            int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
            Dust dust = Main.dust[dustIndex];
            dust.noGravity = true;
            dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
            dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
            dust.scale *= 0.5f + Main.rand.Next(-30, 31) * 0.01f;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo) {

			
			return 0f;
		}
        public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			Player p = Main.player[projectile.owner];

			if (p.velocity.X == 0)
			{
				return true;
			}
			else
			{
				return false;
			}

            return base.CanBeHitByProjectile(projectile);
        }
        public override void HitEffect(NPC.HitInfo hit) {
			for (int i = 0; i < 10; i++) {
				int dustType = DustID.GemDiamond;
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}
