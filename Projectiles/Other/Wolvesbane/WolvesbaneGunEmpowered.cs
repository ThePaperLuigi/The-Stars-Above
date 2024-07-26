
using Microsoft.Xna.Framework;
 
using StarsAbove.Projectiles.Generics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.Wolvesbane
{
    public class WolvesbaneGunEmpowered : StarsAboveGun
    {
        public override string Texture => "StarsAbove/Projectiles/Other/Wolvesbane/WolvesbaneGunEmpowered";
        public override string TextureFlash => "StarsAbove/Projectiles/Other/Wolvesbane/WolvesbaneEmpoweredFlash";
        //Use the extra recoil/reload code.
        public override bool UseRecoil => false;
        //The dust that appears from the barrel after shooting.
        public override int SmokeDustID => Terraria.ID.DustID.Smoke;

        //The dust that fires from the barrel after shooting.
        public override int FlashDustID => Terraria.ID.DustID.GemRuby;
        //The distance the gun's muzzle is relative to the player. Remember this also is influenced by base distance.
        public override int MuzzleDistance => 32;
        //The distance the gun is relative to the player.
        public override float BaseDistance => 45;
        public override int StartingState => 0;
        public override bool KillOnIdle => true;
        public override int ScreenShakeTime => 100; //100 is disabled
        public override float ScaleModifier => 0.8f;
        public override float RecoilStrength => 5f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 64;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }
        public override bool PreAI()
        {
            Projectile.scale = 0.7f;
            Player player = Main.player[Projectile.owner];
            DrawOriginOffsetY = -6;
            return true;
        }
        //For posterity, the draw code of this gun is going to have each part of the upgraded gun seperate and they will draw in with a white flash.
        public override void OnKill(int timeLeft)
        {


        }

    }
}
