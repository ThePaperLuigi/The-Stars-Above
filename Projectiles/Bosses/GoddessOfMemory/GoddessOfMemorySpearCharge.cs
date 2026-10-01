using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.GoddessOfMemory;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class GoddessOfMemorySpearCharge : GoddessOfMemorySpriteProjectile
    {
        public const int ChargeDuration = 36;
        public const int SpriteEndTime = 30;
        public const float ChargeDistance = 484f;

        private bool swingSoundPlayed;

        protected override int FrameCount => 1;

        public override void AI()
        {
            NPC boss = GetBoss();
            if (boss == null || boss.ai[0] == (float)ActionState.Dying || Main.netMode != NetmodeID.MultiplayerClient && (boss.ai[0] != (float)ActionState.PersistentCast || boss.localAI[3] >= SpriteEndTime))
            {
                Projectile.Kill();
                return;
            }

            if (!Main.dedServ && !swingSoundPlayed)
            {
                SoundEngine.PlaySound(SoundID.Item1 with { MaxInstances = 0 }, boss.Center);
                swingSoundPlayed = true;
            }

            Projectile.Center = boss.Center;
            Projectile.velocity = Vector2.Zero;
            Projectile.spriteDirection = Projectile.ai[1].ToRotationVector2().X > 0f ? 1 : -1;
            Projectile.rotation = MathHelper.WrapAngle(Projectile.ai[1] + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi));
            float maxRotation = MathHelper.ToRadians(15f);
            Projectile.rotation = MathHelper.Clamp(Projectile.rotation, -maxRotation, maxRotation);
            Projectile.timeLeft = 2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            NPC boss = GetBoss();
            if (boss == null)
            {
                return false;
            }

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle frame = texture.Frame(1, FrameCount, 0, Projectile.frame);
            Vector2 drawPosition = boss.Center - Main.screenPosition;
            SpriteEffects effects = Projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.EntitySpriteDraw(texture, drawPosition, frame, Projectile.GetAlpha(lightColor), Projectile.rotation, frame.Size() * 0.5f, boss.scale, effects, 0f);
            return false;
        }

        private NPC GetBoss()
        {
            int bossIndex = (int)Projectile.ai[0];
            if (bossIndex < 0 || bossIndex >= Main.maxNPCs)
            {
                return null;
            }

            NPC boss = Main.npc[bossIndex];
            return boss.active && boss.type == ModContent.NPCType<GoddessOfMemoryBoss>() ? boss : null;
        }
    }
}
