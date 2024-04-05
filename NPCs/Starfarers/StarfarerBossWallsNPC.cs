using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.Boss;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.NPCs.Starfarers
{
    public class StarfarerBossWallsNPC : ModNPC
	{
        public static readonly int arenaWidth = (int)(1.2f * 1320);
        public static readonly int arenaHeight = (int)(1.2f * 800);
        public override void SetStaticDefaults() {
			// DisplayName.SetDefault(" ");
			
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers()
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
		public override void AI()
		{
			NPC.ai[1]++;
			NPC.dontCountMe = true;
			NPC.dontTakeDamage = true;
			NPC.timeLeft = 10;
			if (!NPC.AnyNPCs(ModContent.NPCType<StarfarerBoss>()))
			{

				NPC.active = false;
				NPC.netUpdate = true;
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
            Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
            Vector2 drawOrigin = new Vector2(NPC.width * 0.5f, NPC.height * 0.5f);
            int r1 = (int)color1.R;
            //drawOrigin.Y += 34f;
            //drawOrigin.Y += 8f;
            --drawOrigin.X;
            Vector2 position1 = NPC.Bottom - Main.screenPosition;
            Texture2D texture2D2 = (Texture2D)Request<Texture2D>("StarsAbove/Effects/WarriorOfLightWallsEffect");
            float num11 = (float)((double)Main.GlobalTimeWrappedHourly % 4.0 / 4.0);
            if (Main.LocalPlayer.HasBuff(BuffType<AthanoricCurse>()))
            {
                num11 = (float)((double)Main.GlobalTimeWrappedHourly % 0.5 / 0.5);
            }
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
            Microsoft.Xna.Framework.Color color3 = new Color(Main.DiscoR, 55, Main.DiscoB, 100);
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation, drawOrigin, 1f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num15 = 1f + num11 * 0.15f;
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, NPC.rotation, drawOrigin, 1f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num16 = 1f + num13 * 0.15f;
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, NPC.rotation, drawOrigin, 1f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

        }

        public override void HitEffect(NPC.HitInfo hit) {
			
		}
	}
}
