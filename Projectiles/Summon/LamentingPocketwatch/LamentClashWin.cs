using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.LamentingPocketwatch
{
    //
    public class LamentClashWin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 60;
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
        }

        bool playedSound = false;
        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 2)
                {
                    Projectile.frame++;
                }
                else
                {

                }

            }
            if (Projectile.frame == 2 && !playedSound)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_LamentClashWin, Projectile.Center);

                playedSound = true;
            }
            if (Projectile.timeLeft < 10)
            {
                Projectile.alpha += 25;
            }

        }
    }
}