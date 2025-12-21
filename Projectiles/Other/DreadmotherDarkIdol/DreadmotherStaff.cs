using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Magic.RedMage;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.DreadmotherDarkIdol
{
    public class DreadmotherStaff : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Red Mage's Rapier");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
                                                                        //DrawOffsetX = 40;
                                                                        //DrawOriginOffsetY = 81;
        }

        public override void SetDefaults()
        {
            Projectile.width = 76;               //The width of projectile hitbox
            Projectile.height = 76;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic;
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
           

            Player player = Main.player[Projectile.owner];
            if (firstSpawn)
            {
                Projectile.timeLeft = 35;

                firstSpawn = false;
            }
            player.heldProj = Projectile.whoAmI;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);

            Projectile.scale = 1f;

            Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - player.Center.Y, Main.MouseWorld.X - player.Center.X) + MathHelper.ToRadians(180));

            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            
            Projectile.alpha -= 10;

            if (Projectile.ai[1] >= 240 || Projectile.ai[1] < 90)
            {
                player.direction = -1;
            }
            else
            {
                player.direction = 1;
            }
            deg = Projectile.ai[1];

            
            Projectile.ai[0]++;
            double rad = deg * (Math.PI / 180);
            double dist = 10;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);



        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            Player player = Main.player[Projectile.owner];

        }
        public override void OnKill(int timeLeft)
        {

            for (int d = 0; d < 12; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Shadowflame, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 1f);

            }

            base.OnKill(timeLeft);
        }

    }
}
