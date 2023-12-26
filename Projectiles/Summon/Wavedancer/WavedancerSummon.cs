using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Skofnung;
using StarsAbove.Buffs.Wavedancer;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.Wavedancer
{
    public class WavedancerSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            // DisplayName.SetDefault("Skofnung");
        }

        public override void SetDefaults()
        {
            // This method right here is the backbone of what we're doing here; by using this method, we copy all of
            // the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
            // so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
            // if you're going to copy the stats of a projectile, use CloneDefaults().

            Projectile.CloneDefaults(ProjectileID.EmpressBlade);

            // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
            // the projectile to essentially behave the same way as the vanilla projectile.
            AIType = ProjectileID.EmpressBlade;

            // After CloneDefaults has been called, we can now modify the stats to our wishes, or keep them as they are.
            // For the sake of example, lets make our projectile penetrate enemies a few more times than the vanilla projectile.
            // This can be done by modifying projectile.penetrate
            Projectile.width = 146;
            Projectile.height = 146;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 999;
            Projectile.hide = false;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 240;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallBlueTrail).Draw(Projectile);

            return true;
        }
        
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Lighting.AddLight(Projectile.Center, TorchID.Ice);

            if (player.ownedProjectileCounts[ProjectileType<WavedancerSwordSpin>()] > 0)
            {
                Projectile.alpha = 250;
                Projectile.friendly = false;
            }
            else
            {
                Projectile.friendly = true;
                Projectile.alpha -= 90;

            }
            player.empressBlade = false;
            if (!player.HasBuff(BuffType<WavedancerBuff>()) || !player.active || player.dead)
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 10;
            if (player.channel)
            {
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center -
                               new Vector2(Projectile.Center.X + (player.velocity.X * 0.05f), Projectile.Center.Y + (player.velocity.Y * 0.05f))
                               ).ToRotation() + MathHelper.PiOver2);
                //Go towards the mouse
                Projectile.Center = player.GetModPlayer<WeaponPlayer>().wavedancerTarget;
                Projectile.rotation = Vector2.Normalize(Main.MouseWorld - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);
                SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
                player.GetModPlayer<WeaponPlayer>().wavedancerPosition = Projectile.Center;
                Projectile.localNPCHitCooldown = 10;

                return false;
            }
            else
            {
                Projectile.localNPCHitCooldown = 20;

            }



            return base.PreAI();
        }
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 60f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 60f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if ((closest && inRange || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            //Projectile.friendly = foundTarget;
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            DrawOffsetX = -17;



            base.AI();
        }

        // While there are several different ways to change how our projectile could behave differently, lets make it so
        // when our projectile finally dies, it will explode into 4 regular Meowmere projectiles.
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            player.AddBuff(BuffID.Swiftness, 240);
            if (hit.Crit)
            {
                target.AddBuff(BuffType<Riptide>(), 120);

                //

            }

            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Keybrand,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];

            if(player.channel)
            {
                if (Main.rand.Next(0, 101) <= player.maxMinions * 2)
                {
                    modifiers.SetCrit();

                }
                modifiers.SourceDamage += MathHelper.Lerp(-1, 1.5f, Math.Clamp(Vector2.Distance(Main.MouseWorld, Projectile.Center) / 400, 0f,1f));
            }
            

        }
        public override void OnKill(int timeLeft)
        {


        }

    }
}
