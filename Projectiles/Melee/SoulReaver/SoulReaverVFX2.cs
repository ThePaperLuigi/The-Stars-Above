using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Melee.SoulReaver
{
    public class SoulReaverVFX2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Reaver");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 150;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 68;               //The width of projectile hitbox
            Projectile.height = 68;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 180;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Summon;

        }
        float rotationSpeed = 0f;
        bool firstSpawn = true; int orbit;
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallPurpleTrail).Draw(Projectile);

            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (firstSpawn)
            {
                Projectile.ai[1] = 240;

                firstSpawn = false;
            }
            if (player.dead && !player.active || player.GetModPlayer<WeaponPlayer>().bowCharge <= 0)
            {
                Projectile.Kill();
            }
            if (player.HasBuff(BuffType<Buffs.Melee.SoulReaver.SoulSplit>()))
            {
                orbit = (int)MathHelper.Lerp(300, 0, player.GetModPlayer<WeaponPlayer>().bowCharge / 100);
            }
            else
            {
                orbit = (int)MathHelper.Lerp(700, 0, player.GetModPlayer<WeaponPlayer>().bowCharge / 100);
            }
            if (orbit <= 10)
            {
                Projectile.Kill();
            }
            //Factors for calculations
            double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = orbit; //Distance away from the player
            Vector2 adjustedPosition = new Vector2(player.Center.X, player.Center.Y - 20);

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            Projectile.ai[1] += 9.9f;


            float rotationsPerSecond = rotationSpeed;
            rotationSpeed -= 0.4f;
            bool rotateClockwise = true;


            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-90f);
            Projectile.ai[0]++;


        }



    }
}
