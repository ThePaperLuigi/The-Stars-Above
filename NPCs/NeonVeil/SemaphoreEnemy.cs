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
using System;
using StarsAbove.Projectiles.Enemy.NeonVeil;

namespace StarsAbove.NPCs.NeonVeil
{
    public class SemaphoreEnemy : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Headpiercer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Raven];
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = false // Hides this NPC from the bestiary
			};
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}
        
        public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.Raven);
			
			NPC.damage = 20;
			NPC.defense = 0;
			NPC.lifeMax = 400;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.Vulture;
			AnimationType = NPCID.Vulture;
			AIType = NPCID.Vulture;

			NPC.noTileCollide = false;
			
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandedTenebrium>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NeonTelemetry>(), 12, 1, 2));

        }
        public override void AI()
		{
			NPC.ai[0]++;
			if (NPC.ai[0] >= 240)
			{
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.Distance(NPC.Center) < 500 && !p.dead)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), p.Center.X, p.Center.Y - 200, 0, 0, ModContent.ProjectileType<SemaphoreBolt>(), NPC.damage, 0f, Main.myPlayer);
                        }
                    }
                }
            }
			Lighting.AddLight(NPC.Center, TorchID.White);
			NPC.velocity *= 0.90f;
			/*
            */
            int dustType = DustID.LifeDrain;
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
        
        public override void HitEffect(NPC.HitInfo hit) {
			for (int i = 0; i < 10; i++) {
				int dustType = DustID.LifeDrain;
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}
