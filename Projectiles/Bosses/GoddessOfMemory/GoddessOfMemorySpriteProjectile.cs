using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.GoddessOfMemory;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public abstract class GoddessOfMemorySpriteProjectile : ModProjectile
    {
        protected abstract int FrameCount { get; }
        protected virtual float AnimationSpeed => 1f;
        protected virtual int AnimationDuration => (int)Math.Ceiling(FrameCount * GoddessOfMemoryBoss.IdleFrameDuration / AnimationSpeed);

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = FrameCount;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.damage = 0;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.netImportant = true;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void AI()
        {
            int bossIndex = NPC.FindFirstNPC(ModContent.NPCType<GoddessOfMemoryBoss>());
            if (bossIndex < 0)
            {
                Projectile.Kill();
                return;
            }

            NPC boss = Main.npc[bossIndex];
            if (boss.ai[0] == (float)ActionState.Dying || ShouldEndAnimation(boss))
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = boss.Center;
            Projectile.velocity = Vector2.Zero;
            Projectile.direction = boss.direction;
            Projectile.spriteDirection = -boss.direction;

            float elapsed = Projectile.ai[1];
            Projectile.frame = (int)(elapsed * AnimationSpeed / GoddessOfMemoryBoss.IdleFrameDuration) % FrameCount;
            UpdateOpacity(elapsed);
            Projectile.ai[1]++;
            Projectile.timeLeft = 2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            int bossIndex = NPC.FindFirstNPC(ModContent.NPCType<GoddessOfMemoryBoss>());
            if (bossIndex < 0)
            {
                return false;
            }

            NPC boss = Main.npc[bossIndex];
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle frame = texture.Frame(1, FrameCount, 0, Projectile.frame);
            Vector2 drawPosition = boss.Center - Main.screenPosition;
            SpriteEffects effects = boss.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.EntitySpriteDraw(texture, drawPosition, frame, Projectile.GetAlpha(lightColor), boss.rotation, frame.Size() * 0.5f, boss.scale, effects, 0f);
            return false;
        }

        protected virtual bool ShouldEndAnimation(NPC boss)
        {
            return Projectile.ai[1] >= AnimationDuration;
        }

        protected virtual void UpdateOpacity(float elapsed)
        {
            Projectile.alpha = 0;
        }
    }
}
