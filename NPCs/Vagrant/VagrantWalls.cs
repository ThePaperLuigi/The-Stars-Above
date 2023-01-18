using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.NPCs.Vagrant
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class VagrantWalls : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault(" ");
			
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
			NPC.aiStyle = 0;
			NPC.noGravity = true;
			NPC.dontTakeDamage = true;
		}

		private int portalFrame
		{
			get => (int)NPC.localAI[0];
			set => NPC.localAI[0] = value;
		}
		public static readonly int arenaWidth = (int)(1.2f * 600);
		public static readonly int arenaHeight = (int)(1.2f * 600);
		public override void AI()
		{
			NPC.ai[1]++;
			NPC.dontCountMe = true;
			NPC.dontTakeDamage = true;
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.active && npc.type == NPCType<VagrantBoss>())
				{

					
				}
				else
                {
					//NPC.active = false;
					
				}
			}
			if (!Main.dedServ)
			{
				portalFrame++;
				portalFrame %= 6 * Main.projFrames[ProjectileID.PortalGunGate];
			}
			if (NPC.ai[1] >= 600)
			{
				NPC.netUpdate = true;
				NPC.active = false;
			}
			NPC.velocity = Vector2.Zero;
			base.AI();
		}
		
		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			int portalWidth = 48;
			int portalDepth = 18;
			Color color = new Color(122, 255, 132);
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

		public override void HitEffect(int hitDirection, double damage) {
			
		}
	}
}
