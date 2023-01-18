using StarsAbove.Items.Materials;
using SubworldLibrary;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace StarsAbove.NPCs.OffworldNPCs.Caelum
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Soulless : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Soulless");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Ghost];
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
				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}

		public override void SetDefaults() {
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 60;
			NPC.defense = 0;
			NPC.lifeMax = 250;
			//NPC.HitSound = SoundID.NPCHit1;
			//NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.timeLeft = 240;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 14;
			AIType = NPCID.Ghost;
			AnimationType = NPCID.Ghost;
			NPC.noTileCollide = true;

			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InertShard>(), 5, 1, 1));


		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			
			return 0f;
			
                   
			
		}
		public override void AI()
		{
			
			base.AI();
		}
		public override void HitEffect(int hitDirection, double damage) {
			for (int i = 0; i < 10; i++) {
				int dustType = DustID.Ghost;
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}
