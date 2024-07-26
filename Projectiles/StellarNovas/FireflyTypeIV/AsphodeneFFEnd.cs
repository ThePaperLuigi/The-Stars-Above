
using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.Bosses.Tsukiyomi;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.StellarNovas.FireflyTypeIV
{
    public class AsphodeneFFEnd : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Asphodene");
			Main.projFrames[Projectile.type] = 14;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 255;
			Projectile.damage = 0;
			Projectile.light = 1f;
			Projectile.hide = false;
			Projectile.timeLeft = 280;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			
		}

        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallWhiteTrail).Draw(Projectile);

            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/StellarNovas/FireflyTypeIV/AsphodeneFFEndWhite");
            Texture2D texturebase = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/StellarNovas/FireflyTypeIV/AsphodeneFFEnd");

            Rectangle frame = texture.Frame(1, 1, 0, 0);
            Vector2 origin = frame.Size() / 2f;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);
            Main.EntitySpriteDraw(texturebase, Projectile.Center - Main.screenPosition, frame, Color.White * Projectile.ai[2], Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);

            return false;
        }

        public override void AI() {
			Player projOwner = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {

            }
            Projectile.ai[0]++;
            Projectile.ai[2] += 0.03f;

            if (Projectile.ai[0] >= 40)
			{
				Projectile.Kill();
			}
		}

        public override void OnKill(int timeLeft)
        {

            for (int g = 0; g < 4; g++)
            {
                int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
        }
    }
}
