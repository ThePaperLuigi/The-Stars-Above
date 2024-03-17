using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Other.ArchitectLuminance
{
    public class Armament : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Armament");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 2;

        }

        public override void SetDefaults()
        {
            Projectile.width = 200;               //The width of projectile hitbox
            Projectile.height = 200;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            DrawOriginOffsetY = -15;
            DrawOffsetX = -26;
        }
        float rotationSpeed = 10f;
        public override void AI()
        {
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.Other.ArchitectsLuminance.ArchitectLuminanceBuff>()))
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = player.direction;
            //projOwner.heldProj = projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += Projectile.velocity.X / 20f;


            // Adding Pi to rotation if facing left corrects the drawing
            //projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);

            if (projOwner.velocity == Vector2.Zero)
            {

            }
            else
            {

            }
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


            if (Projectile.ai[0] > 20)
            {

                if (closest.CanBeChasedBy() && closestDistance < 1200f)
                {
                    Projectile.ai[0] = 0;
                    int type = ProjectileID.LunarFlare;


                    Vector2 position = Projectile.Center;
                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 36f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(closest.position - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, player.whoAmI);

                }
            }
            if (projOwner.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
            {
                Projectile.ai[0]++;
            }
            else
            {
                Projectile.ai[0] = 0;
            }

        }



    }
}
