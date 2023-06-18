using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Ozma;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaDrainAttack : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 300;               //The width of projectile hitbox
			Projectile.height = 300;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 40;
		}
		float rotationSpeed = 10f;
		float expandStrength = 0f;
		
	
		public override void AI()
		{






			Projectile.alpha -= 20;
			expandStrength += 0.001f;
			Projectile.scale += 0.001f;
			


			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.01f;
			bool rotateClockwise = true;
			//The rotation is set here
			
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

		}
		
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			Player projOwner = Main.player[Projectile.owner];
			
			// Play explosion sound
			
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("fastRadiate").Type, 0, 0f, 0, 0);
			for (int i = 0; i < 100; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-15, 15), 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;

				dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 91, Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10), 100, default(Color), 0.6f);
				Main.dust[dustIndex].velocity *= 3f;
			}



		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for (int d = 0; d < 2; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

			}
			Player projOwner = Main.player[Projectile.owner];
			Player player = Main.player[Projectile.owner];


			Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
			CombatText.NewText(textPos, new Color(81, 62, 247, 240), $"{Math.Min(damageDone/10, 5)}", false, false);
			player.statMana += Math.Min(damageDone / 10, 5);
			CombatText.NewText(textPos, new Color(49, 234, 63, 240), $"{Math.Min(damageDone / 10, 5)}", false, false);
			player.statLife += Math.Min(damageDone / 10, 5);
			if(hit.Crit)
			{
				projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
			}
			 
		}
		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
