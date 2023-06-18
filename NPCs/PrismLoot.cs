using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace StarsAbove.NPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class PrismLoot : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Prismatic Core");
			Main.npcFrameCount[NPC.type] = 1;
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				
				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}

		public override void SetDefaults() {
			NPC.width = 20;
			NPC.height = 20;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 300;
			NPC.HitSound = SoundID.Coins;
			NPC.DeathSound = SoundID.Shatter;
			NPC.Opacity = 255f;
			NPC.timeLeft = 500;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 114;
			
			

			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
		}
		
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			foreach (var npcLootItems in Main.ItemDropsDB.GetRulesForNPCID(this.Type, false))
			{
				npcLoot.Remove(npcLootItems);
			}
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.PrismaticCore>(), 1, 1, 1));


		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			
			//return 0.000001f;
			return SpawnCondition.OverworldDaySlime.Chance * 0.0001f;



		}
		public override void AI()
		{
			Lighting.AddLight(NPC.Center, TorchID.Rainbow);
			base.AI();
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;

			Rectangle frame;

			frame = texture.Frame();
			

			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(NPC.width / 2 - frameOrigin.X, NPC.height - frame.Height);
			Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = NPC.timeLeft / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), 0, frameOrigin, 1, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), 0, frameOrigin, 1, SpriteEffects.None, 0);
			}
			return true;
        }
    }
}
