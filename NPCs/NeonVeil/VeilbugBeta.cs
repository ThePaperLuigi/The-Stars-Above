using StarsAbove.Items.Materials;
using SubworldLibrary;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace StarsAbove.NPCs.NeonVeil
{
    public class VeilbugBeta : ModNPC
	{
		public override void SetStaticDefaults() {
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Ghost];
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(), // Plain black background
				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}

		public override void SetDefaults() {
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 10;
			NPC.defense = 0;
			NPC.lifeMax = 200;
			//NPC.HitSound = SoundID.NPCHit1;
			//NPC.DeathSound = SoundID.NPCDeath1;
			NPC.timeLeft = 240;
            NPC.value = Item.buyPrice(0, 0, 1, 45);
            NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 14;
			AIType = NPCID.Ghost;
			AnimationType = NPCID.Ghost;
			NPC.noTileCollide = true;

            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.NeonVeilBiome>().Type };
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NeonTelemetry>(), 5, 1, 2));

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			
			return 0f;
			
                   
			
		}
		public override void AI()
		{
			
			base.AI();
		}
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 10; i++)
            {
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
