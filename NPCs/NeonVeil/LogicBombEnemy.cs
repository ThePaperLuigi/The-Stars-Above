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
using System.Numerics;
using StarsAbove.Buffs.Boss;
using System;
using StarsAbove.Projectiles.Enemy.NeonVeil;

namespace StarsAbove.NPCs.NeonVeil
{
    public class LogicBombEnemy : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Headpiercer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.MartianDrone];
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
			NPC.CloneDefaults(NPCID.MartianDrone);

			NPC.width = 12;
			NPC.damage = 50;
			NPC.defense = 0;
			NPC.lifeMax = 300;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			//NPC.Opacity = 255f;
			//NPC.timeLeft = 640;
			NPC.value = Item.buyPrice(0, 0, 5, 45);

			NPC.knockBackResist = 0.5f;
			
			NPC.aiStyle = NPCAIStyleID.Corite;
			AnimationType = NPCID.MartianDrone;
			AIType = NPCID.MartianDrone;

			NPC.noTileCollide = false;
			
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandedTenebrium>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NeonTelemetry>(), 12, 1, 2));

        }
        public override void OnKill()
        {

            
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
