using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.NPCs.WarriorOfLight
{
    public class GardenOfAvalonNPC : ModNPC
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
			NPC.width = 292;
			NPC.height = 292;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 21000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.timeLeft = 600; 
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.Opacity = 255f;
			NPC.value = 0f;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
		}

		private int portalFrame
		{
			get => (int)NPC.localAI[0];
			set => NPC.localAI[0] = value;
		}

		public static readonly int arenaWidth = (int)(1.2f * 1320);
		public static readonly int arenaHeight = (int)(1.2f * 800);
		public override void AI()
		{
			NPC.ai[1]++;
			NPC.timeLeft = 10;
			if (!NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBoss>()) && !NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBossFinalPhase>()))
			{

				NPC.active = false;
				NPC.netUpdate = true;
			}
			
			
			NPC.velocity = Vector2.Zero;
			base.AI();
		}
		
			
		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			Vector2 drawOrigin = new Vector2(NPC.width * 0.5f, NPC.height * 0.5f);
			int r1 = (int)color1.R;
			//drawOrigin.Y += 34f;
			//drawOrigin.Y += 8f;
			--drawOrigin.X;
			Vector2 position1 = NPC.Bottom - Main.screenPosition;
			Texture2D texture2D2 = (Texture2D)Request<Texture2D>("StarsAbove/NPCs/WarriorOfLight/GardenOfAvalonNPC");
			float num11 = (float)((double)Main.GlobalTimeWrappedHourly % 8.0 / 8.0);
			float num12 = num11;
			if ((double)num12 > 0.5)
				num12 = 1f - num11;
			if ((double)num12 < 0.0)
				num12 = 0.0f;
			float num13 = (float)(((double)num11 + 0.5) % 1.0);
			float num14 = num13;
			if ((double)num14 > 0.5)
				num14 = 1f - num13;
			if ((double)num14 < 0.0)
				num14 = 0.0f;
			Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
			drawOrigin = r2.Size() / 2f;
			Vector2 position3 = position1 + new Vector2(0.0f, 0f);
			Microsoft.Xna.Framework.Color color3 = new Color(255, 246, 177, 100);
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation, drawOrigin, 1f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			float num15 = 1f + num11 * 0.15f;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, NPC.rotation, drawOrigin, 1f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			float num16 = 1f + num13 * 0.15f;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, NPC.rotation, drawOrigin, 1f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

			/*int portalWidth = 48;
			int portalDepth = 18;
			Color color = new Color(255, 246, 177);
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
			}*/
		}

		public override void HitEffect(NPC.HitInfo hit) {
			
		}
	}
}
