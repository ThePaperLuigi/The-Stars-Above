using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.NPCs.NeonVeil
{
    public class LogicBombEnemy : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Starcell");
			Main.npcFrameCount[NPC.type] = 1;
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
			NPC.width = 26;
			NPC.height = 26;
			NPC.damage = 30;
			NPC.defense = 0;
			NPC.lifeMax = 60;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.timeLeft = 640;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 14;
			NPC.noTileCollide = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.NeonVeilBiome>().Type };

        }
        int nframe;
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return 0f;
		}
		public override void AI()
		{
            int dustType = DustID.LifeDrain;
            int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
            Dust dust = Main.dust[dustIndex];
            dust.noGravity = true;
            dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
            dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
            dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            Lighting.AddLight(NPC.Center, TorchID.Orange);
			NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90);
			base.AI();
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
