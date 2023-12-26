
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Boss;
using StarsAbove.NPCs.Arbitration;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Arbitration
{
    public class ArbitrationTower : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           

        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;


        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
                {
                    NPC other = Main.npc[i];

                    if (other.active && other.type == ModContent.NPCType<ArbitrationBoss>())
                    {
                        Projectile.ai[2] = other.target;
                        
                        Projectile.localAI[0]++;
                        return;
                    }
                }
               
            }
            float rotationsPerSecond = 0.2f;
            if ((Main.player[(int)Projectile.ai[2]].dead && !Main.player[(int)Projectile.ai[2]].active) || !Main.player[(int)Projectile.ai[2]].HasBuff(ModContent.BuffType<TerrorTether>()))
            {
                Projectile.Kill();
            }
            if (Projectile.ai[1] == 1)
            {
                rotationsPerSecond = -0.2f;

            }
            else
            {

            }
            bool rotateClockwise = false;
            //The rotation is set here
            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
            Projectile.ai[0]++;

            //The boss will spawn 2 towers. One of them will connect to the boss's target, and one of them will be empty. The targetted player must move to the other tower
            // and then the tether will tether to that tower.

            //Targetted player will be denoted by a debuff
            if (Projectile.ai[1] == 0)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.HasBuff(ModContent.BuffType<TerrorTether>()) && !p.dead)
                    {
                        for (int ix = 0; ix < 60; ix++)
                        {
                            Vector2 position = Vector2.Lerp(p.Center, Projectile.Center, (float)ix / 60);
                            Dust d = Dust.NewDustPerfect(position, DustID.LifeDrain, null, 240, default, 1f);
                            d.fadeIn = 0.3f;
                            d.noGravity = true;

                        }
                    }
                }
                //This is the source projectile, and starts the tether
                
                

            }
            if (Projectile.ai[1] == 1)
            {
                //This is the destination projectile, and checks to see if nearby player has the buff, and then 
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.Distance(Projectile.Center) < 100 && !p.dead)
                    {
                        p.ClearBuff(ModContent.BuffType<TerrorTether>());
                    }
                }
            }
            if(Projectile.timeLeft <= 10)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.HasBuff(ModContent.BuffType<TerrorTether>()) && !p.dead)
                    {
                        for (int g = 0; g < 20; g++)
                        {
                            Dust dust4 = Dust.NewDustDirect(p.position,p.height, p.width, DustID.LifeDrain,
                                Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 269, Scale: 1.2f);
                            dust4.velocity += Projectile.velocity * 0.3f;
                            dust4.velocity *= 0.2f;
                        }
                        p.Hurt(PlayerDeathReason.ByCustomReason(LangHelper.GetTextValue($"DeathReason.Arbitration", Main.player[(int)Projectile.ai[2]].name)), 350, 0, dodgeable: false);
                        Projectile.Kill();
                    }
                }
                
            }
            Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
            dust.velocity += Projectile.velocity * 0.3f;
            dust.velocity *= 0.2f;
            dust.noGravity = true;
            Projectile.netUpdate = true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int g = 0; g < 20; g++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }


        }
    }
}
