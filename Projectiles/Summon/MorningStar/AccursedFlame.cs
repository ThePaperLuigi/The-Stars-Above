
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.MorningStar
{
    public class AccursedFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Morning Star");

        }

        public override void SetDefaults()
        {
            Projectile.width = 750;
            Projectile.height = 750;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;

        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override void AI()
        {
            //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

            Projectile.ai[0] += 1f;

            for (int d = 0; d < 44; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Green, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }

            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.CursedTorch, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
            }
            for (int d = 0; d < 60; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
            }
            for (int d = 0; d < 70; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }

            // Fade in
            Projectile.alpha--;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {



        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

            target.AddBuff(BuffID.CursedInferno, 180);

        }
    }
}
