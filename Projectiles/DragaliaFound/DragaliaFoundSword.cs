
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Projectiles.Generics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.DragaliaFound
{
    public class DragaliaFoundSword : StarsAboveSword
	{
		public override string Texture => "StarsAbove/Projectiles/DragaliaFound/DragaliaFoundSword";
		public override bool UseRecoil => false;
		public override float BaseDistance => 50;
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 132;
			Projectile.height = 132;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			//DrawOriginOffsetY = -6;
			return true;
		}
		public override void Kill(int timeLeft)
		{
			

		}

	}
}
