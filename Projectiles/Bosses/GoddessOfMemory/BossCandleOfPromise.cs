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
    public class BossCandleOfPromise : ModProjectile
    {
        public const int CastDuration = 120;
        public const int AimDuration = 6;
        public const int SweepDuration = 80;
        private const int SpawnDuration = 30;
        private const int FlameInterval = 2;
        private const float SweepSpeed = 1.5f;

        public int BossIndex => (int)Projectile.ai[0];
        private int Side => Projectile.ai[1] < 0f ? -1 : 1;

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/CandleOfPromises";

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1.4f;
            Projectile.alpha = 255;
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

        public void StartSweep()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || Projectile.ai[2] != 0f)
            {
                return;
            }

            Projectile.ai[2] = 1f;
            Projectile.netUpdate = true;
        }

        public override void AI()
        {
            Projectile.timeLeft = 2;
            if (BossIndex < 0 || BossIndex >= Main.maxNPCs || !Main.npc[BossIndex].active ||
                Main.npc[BossIndex].ModNPC is not GoddessOfMemoryBoss ||
                Main.npc[BossIndex].ai[0] == (float)ActionState.Dying)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            NPC boss = Main.npc[BossIndex];
            if (Projectile.ai[2] == 0f && boss.ai[0] != (float)ActionState.Casting)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            Projectile.Center = boss.Center + new Vector2(Side * 40f, -40f);
            Projectile.velocity = Vector2.Zero;

            if (Projectile.ai[2] == 0f)
            {
                float progress = MathHelper.Clamp(boss.localAI[3] / SpawnDuration, 0f, 1f);
                float easedProgress = 1f - (1f - progress) * (1f - progress);
                Projectile.scale = MathHelper.Lerp(1.4f, 1f, easedProgress);
                Projectile.Opacity = easedProgress;
                Projectile.rotation = MathHelper.Lerp(Side * MathHelper.PiOver2, 0f, easedProgress);
                return;
            }

            Projectile.scale = 1f;
            Projectile.Opacity = 1f;
            float upwardRotation = -Side * MathHelper.ToRadians(87f);
            if (Projectile.ai[2] <= AimDuration)
            {
                float progress = Projectile.ai[2] / AimDuration;
                float easedProgress = 1f - (1f - progress) * (1f - progress);
                Projectile.rotation = MathHelper.Lerp(0f, upwardRotation, easedProgress);
                Projectile.ai[2]++;
                return;
            }

            float sweepAge = (Projectile.ai[2] - AimDuration - 1f) * SweepSpeed;
            float sweepProgress = MathHelper.Clamp(sweepAge / SweepDuration, 0f, 1f);
            Projectile.rotation = upwardRotation + Side * MathHelper.ToRadians(170f) * sweepProgress;
            if (sweepAge == 0f || (int)(sweepAge / FlameInterval) > (int)((sweepAge - SweepSpeed) / FlameInterval))
            {
                Vector2 direction = new Vector2(Side, 0f).RotatedBy(Projectile.rotation);
                Vector2 muzzlePosition = Projectile.Center + new Vector2(Side * 34f, 5f).RotatedBy(Projectile.rotation);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), muzzlePosition, direction * 14f,
                        ModContent.ProjectileType<BossCandleOfPromiseFlame>(), Projectile.damage, 0f, Main.myPlayer, 0f, 0f, BossIndex + 1);
                }

                if (!Main.dedServ && sweepAge % 12f == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item34, Projectile.Center);
                }
            }

            if (sweepAge >= SweepDuration)
            {
                Projectile.Kill();
                return;
            }

            Projectile.ai[2]++;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects effects = Side > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White * Projectile.Opacity,
                Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, effects, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.dedServ)
            {
                return;
            }

            Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.1f, 0.2f));
            for (int i = 0; i < 48; i++)
            {
                Vector2 offset = Main.rand.NextVector2Circular(34f, 12f).RotatedBy(Projectile.rotation);
                Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2f, 7f);
                Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemRuby, velocity, 0, Color.White, Main.rand.NextFloat(1.4f, 2.2f));
                dust.noGravity = true;
                dust.noLight = false;
            }
        }
    }
}
