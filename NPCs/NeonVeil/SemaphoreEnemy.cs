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
using StarsAbove.Buffs.Boss;
using System;
using StarsAbove.Projectiles.Enemy.NeonVeil;
using StarsAbove.Systems;
using Terraria.Audio;

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
			NPC.lifeMax = 200;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.Vulture;
			AnimationType = NPCID.Raven;
			AIType = NPCID.Raven;

			NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.NeonVeilBiome>().Type };

        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandedTenebrium>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NeonTelemetry>(), 5, 1, 2));

        }
        public override void AI()
        {
            NPC.noTileCollide = true;

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
                            for (int i3 = 0; i3 < 50; i3++)
                            {
                                SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, NPC.Center);

                                Vector2 position = Vector2.Lerp(new Vector2(p.Center.X, p.Center.Y - 700), NPC.Center, (float)i3 / 50);
                                Dust d = Dust.NewDustPerfect(position, DustID.LifeDrain, null, 240, default, 2f);
                                d.fadeIn = 0.3f;
                                d.noLight = true;
                                d.noGravity = true;
                            }
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), p.Center.X, p.Center.Y - 700, 0, 3, ModContent.ProjectileType<SemaphoreBolt>(), NPC.damage, 0f, Main.myPlayer);
                        }
                    }
                }
				NPC.ai[0] = 0;
            }
			Lighting.AddLight(NPC.Center, TorchID.Orange);
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
