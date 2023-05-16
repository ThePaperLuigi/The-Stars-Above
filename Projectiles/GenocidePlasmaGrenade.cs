using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Projectiles
{
	public class GenocidePlasmaGrenade : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Genocide Plasma Grenade");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 18;               //The width of projectile hitbox
			Projectile.height = 18;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		private float travelRadians;//Thank you to absoluteAquarian for this code!
		public Vector2 MidPoint;
		public float Radius;
		public bool RotateClockwise = true;
		public Vector2 InitialDirection;
		public Vector2 Target;
		

		public override void AI()
        {
			//
			
			if (Vector2.Distance(Projectile.Center, Target) < 1f)
            {
				
				Projectile.Kill();
            }


			if(Projectile.timeLeft > 580)
            {
				Projectile.friendly = false;
            }
			else
            {
				Projectile.friendly = true;
            }

			float rotationsPerSecond = 2f;

			Projectile.rotation += (RotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);















			base.AI();
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

			target.AddBuff(BuffType<Buffs.MortalWounds>(), 720);

			 
        }
		

        public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[Projectile.owner];
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Projectile.NewProjectile(Projectile.GetSource_FromThis(),projectile.Center.X, projectile.Center.Y, Vector2.Zero.X, Vector2.Zero.Y, ProjectileType<GenocideArtilleryBlast>(), projectile.damage * 3, 4, projOwner.whoAmI);
			Projectile.scale = 3f;

			//When the projectile dies, search for the closest NPC that matches the following conditions.
			//1. Alive | 2. Closest | 3. Does not have Mortal Wounds
			NPC closest = null;
			float closestDistance = 9999999;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, Projectile.Center);


				if (npc.active && npc.Distance(Projectile.position) < closestDistance && !npc.HasBuff(BuffType<MortalWounds>()))
				{
					closest = npc;
					closestDistance = npc.Distance(Projectile.position);
				}




			}

			if (closest.CanBeChasedBy() && closestDistance < 1200f && Projectile.ai[0] > 0)//If the enemy is a reasonable distance away and is hostile (projectile.ai[0] is the amount of bounces left (Should start at 3.)
			{
				
				int type = ProjectileType<Projectiles.GenocidePlasmaGrenade>();

				closest.AddBuff(BuffType<Stun>(), 30);

				Vector2 position = Projectile.Center;

				Projectile.ai[0]--;


				float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse

				float launchSpeed = 7f;
				Vector2 mousePosition = projOwner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
				Vector2 direction = Vector2.Normalize(closest.position - Projectile.Center);
				Vector2 velocity = direction * launchSpeed;

				Projectile.NewProjectile(Projectile.GetSource_FromThis(),position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, projOwner.whoAmI, Projectile.ai[0]);


			}

			Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, Vector2.Zero.X, Vector2.Zero.Y, ProjectileType<GenocidePlasmaGrenadeBlast>(), Projectile.damage, 4, projOwner.whoAmI);









			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			
			// Play explosion sound
			
			// Large Smoke Gore spawn
			

		}
	}
}
