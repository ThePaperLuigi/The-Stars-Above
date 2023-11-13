using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.NPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class PaintedAttendantA : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Living Brush");
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = false // Hides this NPC from the bestiary
			};
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}
		public override void SetDefaults() {
			NPC.width = 28;
			NPC.height = 122;
			NPC.damage = 60;
			NPC.defense = 30;
			NPC.lifeMax = 500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.timeLeft = 900;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 23;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return 0f;
		}
		public override void AI()
		{
			//NPC.spriteDirection = NPC.direction;
			if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.Penthesilea.PenthesileaBoss>()))
			{
				NPC.HitEffect();
				NPC.life = 0;
				NPC.active = false;
				NPC.netUpdate = true;
			}
			base.AI();
		}
		public override void HitEffect(NPC.HitInfo hit) {
			for (int i = 0; i < 10; i++) {
				int dustType = 271;
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}
