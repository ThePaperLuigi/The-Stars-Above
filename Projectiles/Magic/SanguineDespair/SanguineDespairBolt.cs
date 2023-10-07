using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Magic.SanguineDespair
{
    public class SanguineDespairBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sanguine Despair");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 42;               //The width of projectile hitbox
            Projectile.height = 42;              //The height of projectile hitbox
                                                 //Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 3;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.RedTrail).Draw(Projectile);

            return true;
        }
        public override void AI()
        {
            Projectile.alpha -= 5;
            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
            {

                Player player = Main.player[Projectile.owner];
                // If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
                if (player.channel)
                {
                    float maxDistance = 18f; // This also sets the maximun speed the projectile can reach while following the cursor.
                    Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    // Here we can see that the speed of the projectile depends on the distance to the cursor.
                    if (distanceToCursor > maxDistance)
                    {
                        distanceToCursor = maxDistance / distanceToCursor;
                        vectorToCursor *= distanceToCursor;
                    }

                    int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
                    int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 1000f);
                    int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
                    int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 1000f);

                    // This code checks if the precious velocity of the projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
                    // We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
                    if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
                    {
                        Projectile.netUpdate = true;
                    }

                    Projectile.velocity = vectorToCursor;

                }
                // If the player stops channeling, do something else.
                else if (Projectile.ai[0] == 0f)
                {

                    // This code block is very similar to the previous one, but only runs once after the player stops channeling their weapon.
                    Projectile.netUpdate = true;

                    float maxDistance = 14f; // This also sets the maximun speed the projectile can reach after it stops following the cursor.
                    Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    //If the projectile was at the cursor's position, set it to move in the oposite direction from the player.
                    if (distanceToCursor == 0f)
                    {
                        vectorToCursor = Projectile.Center - player.Center;
                        distanceToCursor = vectorToCursor.Length();
                    }

                    distanceToCursor = maxDistance / distanceToCursor;
                    vectorToCursor *= distanceToCursor;

                    Projectile.velocity = vectorToCursor;

                    if (Projectile.velocity == Vector2.Zero)
                    {
                        Projectile.Kill();
                    }

                    Projectile.ai[0] = 1f;
                }
            }

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            }

            base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player projOwner = Main.player[Projectile.owner];
            projOwner.GetModPlayer<WeaponPlayer>().renegadeGauge++;
            if (hit.Crit)
            {
                projOwner.GetModPlayer<WeaponPlayer>().renegadeGauge++;
            }
            if (target.boss)
            {
                projOwner.GetModPlayer<WeaponPlayer>().renegadeGauge += 2;
            }
            if (projOwner.GetModPlayer<WeaponPlayer>().renegadeGauge++ > 100)
            {
                projOwner.GetModPlayer<WeaponPlayer>().renegadeGauge = 100;
            }


        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {


            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffType<MortalWounds>()))
            {
                modifiers.CritDamage += 0.3f;
                modifiers.NonCritDamage += 0.1f;
            }
            for (int d = 0; d < 18; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-2, 2), 150, default, 1f);

            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 18; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-2, 2), 150, default, 1f);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 0.4f);

            }
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
