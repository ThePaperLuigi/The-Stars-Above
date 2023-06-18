using StarsAbove.Items.Materials;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.NPCs.OffworldNPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class AmethystHeadpiercer : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Headpiercer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.VortexRifleman];
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
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
			NPC.CloneDefaults(NPCID.VortexRifleman);
			
			NPC.damage = 10;
			NPC.defense = 0;
			NPC.lifeMax = 100;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.Fighter;
			AnimationType = NPCID.VortexRifleman;
			AIType = NPCID.VortexRifleman;

			NPC.noTileCollide = false;
			
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandedTenebrium>(), 8, 1, 1));


		}
		public override void AI()
		{
			Lighting.AddLight(NPC.Center, TorchID.Purple);
			NPC.velocity *= 0.90f;

		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return 0f;
		}
		
		public override void HitEffect(NPC.HitInfo hit) {
			for (int i = 0; i < 10; i++) {
				int dustType = DustID.RedMoss;
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}
