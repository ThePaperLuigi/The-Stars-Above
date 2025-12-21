using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Ranged.InheritedCaseM4A1;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.InheritedCaseM4A1
{
    public class M16A1Shield : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
                                                                        //DrawOffsetX = 40;
                                                                        //DrawOriginOffsetY = 81;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;               //The width of projectile hitbox
            Projectile.height = 80;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        int direction;//0 is right 1 is left
        float rotationStrength = 0.1f;
        bool firstSpawn = true;
        double deg;


        public override void AI()
        {

            Player projOwner = Main.player[Projectile.owner];
            var modPlayer = projOwner.GetModPlayer<WeaponPlayer>();

            if (projOwner.HasBuff(BuffType<AuxiliaryArmamentBuff>()))
            {
                Projectile.timeLeft = 10;

            }
            if (!modPlayer.M4A1Held)
            {
                Projectile.Kill();
            }


            Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X) + MathHelper.ToRadians(180));

            if (projOwner.dead && !projOwner.active)
            {
                Projectile.Kill();
            }

            if (Projectile.ai[2] > 0)//Block projectiles
            {
                Projectile closest = null;
                float closestDistance = 60;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile projectile = Main.projectile[i];

                    if (projectile.hostile && projectile.Distance(Projectile.Center) < closestDistance && projectile.damage > 0 && projectile.active)
                    {
                        for (int d = 0; d < 15; d++)
                        {
                            int dustIndex = Dust.NewDust(projectile.Center, 0, 0, DustID.Electric, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, default, 0.5f);
                            Main.dust[dustIndex].noGravity = true;
                        }
                        Projectile.ai[2]--;
                        Projectile.alpha = 0;
                        projectile.Kill();
                        projectile.active = false;
                    }

                    

                }

               
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha,0,160);
            Projectile.alpha += 10;

            deg = Projectile.ai[1];

            
            Projectile.ai[0]++;
            double rad = deg * (Math.PI / 180);
            double dist = 40;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(180f);



        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            Player player = Main.player[Projectile.owner];

        }
        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.AddBuff(BuffType<M16A1Counterfire>(), 2 * 60);
            player.AddBuff(BuffType<M16A1ShieldCooldown>(), 8 * 60);
            for (int d = 0; d < 12; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default, 0.7f);

            }

            base.OnKill(timeLeft);
        }

    }
}
