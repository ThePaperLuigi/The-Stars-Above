using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.Takodachi
{
    public class MagicCircle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Circle");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 2;

        }

        public override void SetDefaults()
        {
            Projectile.width = 200;               //The width of projectile hitbox
            Projectile.height = 200;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 250;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        float rotationSpeed = 10f;
        public override void AI()
        {
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.TakodachiLaserBuff>()))
            {
                Projectile.alpha += 20;
            }
            else
            {

                Projectile.alpha -= 10;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = player.direction;
            //projOwner.heldProj = projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2 - 26;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 - 15;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += Projectile.velocity.X / 20f;



        }



    }
}
