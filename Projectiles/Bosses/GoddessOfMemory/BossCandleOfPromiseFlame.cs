using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.GoddessOfMemory;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossCandleOfPromiseFlame : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Flames);
            AIType = ProjectileID.Flames;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (Projectile.wet && !Projectile.lavaWet)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.ai[2] <= 0f || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int bossIndex = (int)Projectile.ai[2] - 1;
            if (bossIndex < 0 || bossIndex >= Main.maxNPCs)
            {
                Projectile.Kill();
                return;
            }

            NPC boss = Main.npc[bossIndex];
            if (!boss.active || boss.ModNPC is not GoddessOfMemoryBoss || boss.ai[0] == (float)ActionState.Dying)
            {
                Projectile.Kill();
            }
        }

        public override bool? CanDamage()
        {
            return Projectile.localAI[0] < 54f ? null : false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int radius = (int)Utils.Remap(Projectile.localAI[0], 0f, 72f, 10f, 40f);
            hitbox.Inflate(radius, radius);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return projHitbox.Intersects(targetHitbox) && Collision.CanHit(Projectile.Center, 0, 0, targetHitbox.Center.ToVector2(), 0, 0);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity * 0.95f;
            Projectile.position -= Projectile.velocity;
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            float age = Projectile.localAI[0];
            float progress = Utils.Remap(age, 0f, 72f, 0f, 1f);
            if (progress >= 1f)
            {
                return false;
            }

            Color flameColor = new Color(255, 80, 20, 200);
            Color coreColor = new Color(255, 255, 20, 70);
            Color emberColor = Color.Lerp(new Color(255, 80, 20, 100), coreColor, 0.25f);
            Color smokeColor = new Color(80, 80, 80, 100);
            Color color;
            if (progress < 0.1f)
            {
                color = Color.Lerp(Color.Transparent, flameColor, Utils.GetLerpValue(0f, 0.1f, progress, true));
            }
            else if (progress < 0.2f)
            {
                color = Color.Lerp(flameColor, coreColor, Utils.GetLerpValue(0.1f, 0.2f, progress, true));
            }
            else if (progress < 0.35f)
            {
                color = coreColor;
            }
            else if (progress < 0.7f)
            {
                color = Color.Lerp(coreColor, emberColor, Utils.GetLerpValue(0.35f, 0.7f, progress, true));
            }
            else if (progress < 0.85f)
            {
                color = Color.Lerp(emberColor, smokeColor, Utils.GetLerpValue(0.7f, 0.85f, progress, true));
            }
            else
            {
                color = Color.Lerp(smokeColor, Color.Transparent, Utils.GetLerpValue(0.85f, 1f, progress, true));
            }

            float step = age > 50f ? 0.175f : 0.2f;
            float opacity = Utils.Remap(age, 60f, 72f, 1f, 0f);
            float trailLength = Math.Min(age, 20f);
            float scale = Utils.Remap(progress, 0.2f, 0.5f, 0.25f, 1f);
            Rectangle frame = texture.Frame(1, 7, 0, 3);
            Vector2 origin = frame.Size() / 2f;

            for (int layer = 0; layer < 2; layer++)
            {
                for (float trail = 1f; trail >= 0f; trail -= step)
                {
                    float strength = (1f - trail) * Utils.Remap(progress, 0f, 0.2f, 0f, 1f);
                    Vector2 position = Projectile.Center - Main.screenPosition - Projectile.velocity * trailLength * trail;
                    Color innerColor = color * strength;
                    Color outerColor = innerColor;
                    outerColor.G /= 2;
                    outerColor.B /= 2;
                    outerColor.A = (byte)Math.Min(innerColor.A + 80f * strength, 255f);
                    float rotationSpeed = (trail + 1f) / step;
                    float rotation = Projectile.rotation + trail * MathHelper.PiOver2 + Main.GlobalTimeWrappedHourly * rotationSpeed * 2f;
                    float reverseRotation = Projectile.rotation - trail * MathHelper.PiOver2 - Main.GlobalTimeWrappedHourly * rotationSpeed * 2f;

                    if (layer == 0)
                    {
                        Main.EntitySpriteDraw(texture, position - Projectile.velocity * trailLength * step * 0.5f, frame, outerColor * opacity * 0.25f, rotation + MathHelper.PiOver4, origin, scale, SpriteEffects.None);
                        Main.EntitySpriteDraw(texture, position, frame, outerColor * opacity, reverseRotation, origin, scale, SpriteEffects.None);
                    }
                    else
                    {
                        Main.EntitySpriteDraw(texture, position - Projectile.velocity * trailLength * step * 0.2f, frame, innerColor * opacity * 0.25f, rotation + MathHelper.PiOver2, origin, scale * 0.75f, SpriteEffects.None);
                        Main.EntitySpriteDraw(texture, position, frame, innerColor * opacity, reverseRotation + MathHelper.PiOver2, origin, scale * 0.75f, SpriteEffects.None);
                    }
                }
            }

            return false;
        }
    }
}
