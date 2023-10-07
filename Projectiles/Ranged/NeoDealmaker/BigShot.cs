using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Ranged.NeoDealmaker
{
    public class BigShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Neo Dealmaker");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;               //The width of projectile hitbox
            Projectile.height = 24;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 8;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }
        public override void AI()
        {




            base.AI();

        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.YellowTrail).Draw(Projectile);

            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.active)
            {
                int k = Item.NewItem(target.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, target.width, target.height, ItemID.SilverCoin, 5, false);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, k, 1f);
                }
            }

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffID.Midas))
            {
                modifiers.CritDamage += 1.25f;
                modifiers.NonCritDamage += 1.15f;

            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }




        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 18; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.3f);

            }
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
