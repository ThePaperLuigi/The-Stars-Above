using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using StarsAbove.Systems;
using StarsAbove.Buffs.StringOfCurses;

namespace StarsAbove.Projectiles.Ranged.StringOfCurses
{
    public class StringOfCursesShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;               //The width of projectile hitbox
            Projectile.height = 4;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.GreenTrail).Draw(Projectile);

            return true;
        }
        public override void AI()
        {




            base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(hit.Crit)
            {
                target.AddBuff(ModContent.BuffType<Necrosis>(), 240);

            }
            if (!target.active)
            {
                Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().novaGauge += 2;
                if(Main.player[Projectile.owner].HasBuff(ModContent.BuffType<Cursewrought>()))
                {
                    Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().novaGauge += 2;
                    Main.player[Projectile.owner].Heal(10);
                }
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Cursewrought>(), 7 * 60);

            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage -= 0.5f;
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Terra, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 0.7f);

            }
            
        }
    }
}
