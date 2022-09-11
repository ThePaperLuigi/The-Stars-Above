using IL.Terraria.GameContent.Achievements;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace StarsAbove.NPCs
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class SpectreOfLight : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Spectre Of Light");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(), // Plain black background
				new FlavorTextBestiaryInfoElement("An otherworldy hero, summoned forth by the First Starbearer to assail this land.")
			});
		}

		public override void SetDefaults() {
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 60;
			NPC.defense = 0;
			NPC.lifeMax = 10;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.timeLeft = 240;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 8;
			AIType = NPCID.RuneWizard;
			AnimationType = NPCID.Zombie;

			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior)
            {
				return SpawnCondition.OverworldDaySlime.Chance;
			}
			else
            {
				return 0f;
			}
                   
			
		}
		public override void AI()
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.WarriorOfLight>()))
			{
				if(!(NPC.downedMoonlord && !DownedBossSystem.downedWarrior))
                {
					NPC.HitEffect();
					NPC.life = 0;
					NPC.active = false;
					NPC.netUpdate = true;
				}
				
			}
			base.AI();
		}
		public override void HitEffect(int hitDirection, double damage) {
			for (int i = 0; i < 10; i++) {
				int dustType = 271;
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}
