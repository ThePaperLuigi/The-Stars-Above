using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using SubworldLibrary;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.RedMage
{
	public class ScorchPrep : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Vermilion Riposte");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 250;               //The width of projectile hitbox
			Projectile.height = 250;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 70;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = false;
		}
		float rotationSpeed = 10f;
		float expandStrength = 0f;
		
	
		public override void AI()
		{






			Projectile.alpha -= 20;
			expandStrength += 0.001f;
			Projectile.scale += 0.001f;
			


			//float rotationsPerSecond = rotationSpeed;
			//rotationSpeed = 0.4f;
			//bool rotateClockwise = true;
			//The rotation is set here
			
			//Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

		}
		
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			Player projOwner = Main.player[Projectile.owner];
			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -70;
			// Play explosion sound
			//SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, Projectile.Center);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("Scorch").Type, Projectile.damage*5, 0f, 0, 0);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("radiateDamage").Type, 0, 0f, 0, 0);
			for (int i = 0; i < 100; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemRuby, Main.rand.NextFloat(-25, 25), Main.rand.NextFloat(-25, 25), 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;

				dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}



		}
		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
