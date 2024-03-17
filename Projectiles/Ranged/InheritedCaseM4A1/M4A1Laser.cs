using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Ranged.InheritedCaseM4A1
{
    public class M4A1Laser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Verthunder");     //The English name of the Projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;               //The width of Projectile hitbox
            Projectile.height = 4;              //The height of Projectile hitbox
                                                //Projectile.aiStyle = 1;             //The ai style of the Projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the Projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the Projectile deal damage to the player?
            Projectile.penetrate = 99;           //How many monsters the Projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 200;          //The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the Projectile, 255 for completely transparent. (aiStyle 1 quickly fades the Projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your Projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the Projectile
            Projectile.ignoreWater = true;          //Does the Projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the Projectile collide with tiles?
            Projectile.extraUpdates = 100;            //Set to above 0 if you want the Projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Ranged;

            //AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            return true; // return false because we are handling collision
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            //Pierce damage reduction
            Projectile.damage /= 2;
        }

        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 ProjectilePosition = Projectile.position;
                    ProjectilePosition -= Projectile.velocity * (i * 0.25f);
                    Projectile.alpha = 255;
                    // Important, changed 173 to 178!
                    int dust = Dust.NewDust(ProjectilePosition, 1, 1, DustID.GemTopaz, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 0, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = ProjectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.1f;

                }
            }
        }



        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 0.5f);
            }

        }
    }
}
