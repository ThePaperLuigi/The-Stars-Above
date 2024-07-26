using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.Phasmasaber;
using StarsAbove.Projectiles.Celestial.BuryTheLight;
using StarsAbove.Projectiles.Other.Wolvesbane;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.Phasmasaber
{
    public class PhasmasaberRangedOrbital : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 45;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;               //The width of projectile hitbox
            Projectile.height = 36;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = false;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 999;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 150;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Melee;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            
            base.OnHitNPC(target, hit, damageDone);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            if (player.channel && Projectile.alpha < 10)
            {
                default(Effects.WhiteTrail).Draw(Projectile);

            }

            return true;
        }
        public override void AI()
        {
            Projectile.timeLeft = 999;
            Player player = Main.player[Projectile.owner];

            if (player.dead && !player.active || (!player.HasBuff(BuffType<SpectralIllusionBuff>())) || !player.channel)
            {
                Projectile.alpha += 30;
            }
            
            Projectile.alpha -= 10;
            Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, 0, 255);
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
            
            Player p = Main.player[Projectile.owner];

            
            //Factors for calculations
            double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 165; //Distance away from the player
            Vector2 adjustedPosition = Main.MouseWorld;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.rotation = Projectile.DirectionTo(adjustedPosition).ToRotation() + MathHelper.ToRadians(-135);

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            Projectile.ai[1] += 5f;
            if (Projectile.ai[1] >= 360)
            {
                SoundEngine.PlaySound(SoundID.Item1, player.Center);

                Projectile.ai[1] = 0;
            }
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 4)
            {
                Projectile.ai[0] = 0; SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);

                if (p.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(p.GetSource_FromThis(), Projectile.Center, -Projectile.Center.DirectionFrom(Main.MouseWorld) * 12, ProjectileType<PhasmasaberRangedCrystal>(), Projectile.damage/3, 0, p.whoAmI);
                }
            }
        }
       
    }
}
