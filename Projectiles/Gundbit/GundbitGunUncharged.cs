
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Projectiles.Generics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Gundbit
{
    public class GundbitGunUncharged : StarsAboveGun
	{
		public override string Texture => "StarsAbove/Projectiles/Gundbit/GundbitGunUncharged";

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 82;
			Projectile.height = 32;
		}
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			return true;
		}
		
		public override void Kill(int timeLeft)
		{
			

		}

	}
}
