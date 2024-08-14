using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.NPCs
{
    public class DummyEnemy : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Dummy Enemy");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new()
			{
				Hide = true // Hides this NPC from the bestiary
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);
		}
		public override void SetDefaults() {
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 500000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.timeLeft = 240;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 0;
			
			AnimationType = NPCID.Zombie;
		}

		
		public override void AI()
		{
			NPC.life = NPC.lifeMax;
			NPC.velocity = Vector2.Zero;
			base.AI();
		}
		public override void HitEffect(NPC.HitInfo hit) {
			
		}
	}
}
