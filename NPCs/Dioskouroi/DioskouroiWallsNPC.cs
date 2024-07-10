using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.NPCs.Dioskouroi
{
    public class DioskouroiWallsNPC : ModNPC
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault(" ");
			
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true // Hides this NPC from the bestiary
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);
		}
		public override void SetDefaults() {
			NPC.width = 1;
			NPC.height = 1;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 10;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.timeLeft = 600; 
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.value = 0f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.dontTakeDamage = true;
		}

		private int portalFrame
		{
			get => (int)NPC.localAI[0];
			set => NPC.localAI[0] = value;
		}
		public static readonly int arenaWidth = (int)(1.2f * 880);
		public static readonly int arenaHeight = (int)(1.2f * 720);
		public override void AI()
		{
			NPC.ai[1]++;
			NPC.dontCountMe = true;
			NPC.dontTakeDamage = true;
			NPC.timeLeft = 10;
			if (!NPC.AnyNPCs(ModContent.NPCType<PolluxBoss>()) && !NPC.AnyNPCs(ModContent.NPCType<CastorBoss>()))
			{

				NPC.active = false;
				NPC.netUpdate = true;
			}
			else
			{
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.type == ModContent.NPCType<CastorBoss>())
                    {
						NPC.position.X = npc.position.X + 500;

                    }
                }
            }
			if (!Main.dedServ)
			{
				portalFrame++;
				portalFrame %= 6 * Main.projFrames[ProjectileID.PortalGunGate];
			}
			
			NPC.velocity = Vector2.Zero;
			base.AI();
		}
		
		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			int portalWidth = 48;
			int portalDepth = 18;
			Color color = new Color(255, 255, 255);
			int centerX = (int)NPC.Center.X;
			int centerY = (int)NPC.Center.Y;
			Main.instance.LoadProjectile(ProjectileID.PortalGunGate);
			for (int x = centerX - arenaWidth / 2; x < centerX + arenaWidth / 2; x += portalWidth)
			{
				int frameNum = (portalFrame / 6 + x / portalWidth) % Main.projFrames[ProjectileID.PortalGunGate];
				Rectangle frame = new Rectangle(0, frameNum * (portalWidth + 2), portalDepth, portalWidth);
				Vector2 drawPos = new Vector2(x + portalWidth / 2, centerY - arenaHeight / 2) - Main.screenPosition;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)-Math.PI / 2f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
				drawPos.Y += arenaHeight;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)Math.PI / 2f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
			}
			for (int y = centerY - arenaHeight / 2; y < centerY + arenaHeight / 2; y += portalWidth)
			{
				int frameNum = (portalFrame / 6 + y / portalWidth) % Main.projFrames[ProjectileID.PortalGunGate];
				Rectangle frame = new Rectangle(0, frameNum * (portalWidth + 2), portalDepth, portalWidth);
				Vector2 drawPos = new Vector2(centerX - arenaWidth / 2, y + portalWidth / 2) - Main.screenPosition;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)Math.PI, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
				drawPos.X += arenaWidth;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, 0f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
			}
		}

		public override void HitEffect(NPC.HitInfo hit) {
			
		}
	}
}
