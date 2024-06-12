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

namespace StarsAbove.NPCs.NeonVeil
{
    public class LogicVirusEnemy : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Headpiercer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GigaZapper];
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
			NPC.CloneDefaults(NPCID.GigaZapper);
			
			NPC.damage = 20;
			NPC.defense = 5;
			NPC.lifeMax = 400;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.Fighter;
			AnimationType = NPCID.GigaZapper;
			AIType = NPCID.GigaZapper;

			NPC.noTileCollide = false;
			
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandedTenebrium>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NeonTelemetry>(), 12, 1, 2));

        }
        public override void OnKill()
        {

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int dustAmount = 40;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(NPC.velocity.ToRotation());
                    int dust = Dust.NewDust(NPC.Center, 0, 0, DustID.LifeDrain);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = NPC.Center + spinningpoint5;
                    Main.dust[dust].velocity = NPC.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                }
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<LogicBombEnemy>(), 0, 0, 0, 0, 0, NPC.target);
            }
            base.OnKill();
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
