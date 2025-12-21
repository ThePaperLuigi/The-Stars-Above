using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Extra;

namespace StarsAbove.Projectiles.Melee.BloodBlade
{
    public class BloodArtPrep : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Blade");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 140;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;               //The width of projectile hitbox
            Projectile.height = 200;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?

            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 50;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            DrawOffsetX = 16;
        }
        float rotationSpeed = 10f;
        bool firstSpawn = true;
        //float initialSpeed = 27f;
        int initialDistance = 250;
        bool isActive = true;
        int savedDamage;
        int respawnTimer;
        bool cosmicConceptionStart;
        public override void AI()
        {
            if (firstSpawn)
            {
                savedDamage = Projectile.damage;
                Projectile.ai[1] = 312;
                firstSpawn = false;
            }
            Player player = Main.player[Projectile.owner];

            player.itemTime = 2;


            if (Projectile.timeLeft < 10)
            {
                initialDistance += 20;
                Projectile.scale -= 0.05f;
            }

            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            //Factors for calculations
            double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 250 - initialDistance; //Distance away from the player

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value

            Projectile.ai[1] += 18f;
            //Projectile.ai[1] += initialSpeed;
            Projectile.alpha -= 5;
            /*
			if(initialSpeed > 0f)
            {
				initialSpeed-= 0.2f;
            }*/
            if (initialDistance > 0)
            {
                initialDistance -= 5;
            }
            float rotationsPerSecond = rotationSpeed;
            rotationSpeed -= 0.1f;
            bool rotateClockwise = true;

            //The rotation is set here
            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-90f);
        }

        public override void OnKill(int timeLeft)
        {

            SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Main.player[Projectile.owner].Center);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, Vector2.Zero, ProjectileType<BladeArtDragon>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, Vector2.Zero, ProjectileType<fastRadiate>(), 0, 0f, Projectile.owner, 0f, 0f);

            Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if(Projectile.timeLeft > 30)
            {
                default(Effects.RedTrail).Draw(Projectile);

            }

            return true;
        }

    }
}
