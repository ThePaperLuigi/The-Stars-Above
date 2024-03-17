using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.CaesuraOfDespair
{
    public class IrysCrystalMain : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Irys");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 1;
            // = -15;
            //DrawOffsetX = -26;
        }

        public override void SetDefaults()
        {
            Projectile.width = 68;               //The width of projectile hitbox
            Projectile.height = 68;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Summon;

        }
        float rotationSpeed = 10f;
        public override void AI()
        {
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.Summon.CaesuraOfDespair.IrysBuff>()))
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = player.direction;
            projOwner.heldProj = Projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.Center;
            Projectile.direction = projOwner.direction;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 - 150;
            Projectile.Center += projOwner.gfxOffY * Vector2.UnitY;//Prevent glitchy animation.

            NPC closest = null;
            float closestDistance = 9999999;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                float distance = Vector2.Distance(npc.Center, Projectile.Center);


                if (npc.active && npc.Distance(Projectile.position) < closestDistance)
                {
                    closest = npc;
                    closestDistance = npc.Distance(Projectile.position);
                }




            }

            if (closest.CanBeChasedBy() && closestDistance < 1200f)
            {
                closest.AddBuff(BuffType<Buffs.Summon.CaesuraOfDespair.IrysGaze>(), 60);
                for (int i3 = 0; i3 < 50; i3++)
                {
                    Vector2 position2 = Vector2.Lerp(Projectile.Center, closest.Center, (float)i3 / 50);
                    Dust d = Dust.NewDustPerfect(position2, 219, null, 240, default, 0.3f);
                    d.fadeIn = 0.01f;
                    d.noLight = true;
                    d.noGravity = true;
                }

            }           //projectile.spriteDirection = projectile.direction;
                        //projectile.rotation += projectile.velocity.X / 20f;


            // Adding Pi to rotation if facing left corrects the drawing
            //projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);


        }



    }
}
