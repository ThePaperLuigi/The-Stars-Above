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
using StarsAbove.Buffs;
using Microsoft.Xna.Framework;

namespace StarsAbove.NPCs.OffworldNPCs.DreamingCity
{
    public class ParacausalEnigma : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Headpiercer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PigronCorruption];
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
			NPC.CloneDefaults(NPCID.PigronCorruption);
			
			NPC.damage = 50;
			NPC.defense = 0;
			NPC.lifeMax = 2000;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.DemonEye;
			AnimationType = NPCID.PigronCorruption;
			AIType = NPCID.PigronCorruption;

			NPC.noTileCollide = true;
			
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

			NPC.ai[3]++;
			if(NPC.life < NPC.lifeMax/2)
			{
                NPC.ai[3]+= 2;
				NPC.noTileCollide = true;
            }
            if (NPC.ai[3] > 240)
			{
                Vector2 position = NPC.Center;

                float launchSpeed = 45f;
                Vector2 direction = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center);
                NPC.velocity = direction * launchSpeed;
				NPC.ai[3] = 0;
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
