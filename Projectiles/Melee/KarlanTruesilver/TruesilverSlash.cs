using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Melee.KarlanTruesilver
{
    // This file showcases the concept of piercing.
    // This file also shows an animated projectile
    // This file also shows advanced drawing to center the drawn projectile correctly
    /*
	
	NPC.immune determines if an npc can be hit by a item or projectile owned by a particular player (it is an array, each slot corresponds to different players (whoAmI))
	NPC.immune is decremented towards 0 every update
	Melee items set NPC.immune to player.itemAnimation, which starts at item.useAnimation and decrements towards 0

	Projectiles, however, provide mechanisms for custom immunity.
	1. MaxPenetrate = 1: A projectile with penetrate set to 1 will hit regardless of the npc's immunity counters
		Ex: Wooden Arrow. 
	2. No code and penetrate > 1 or -1: npc.immune[owner] will be set to 10. 
		The NPC will be hit if not immune and will become immune to all damage for 10 ticks
		Ex: Unholy Arrow
	3. Override OnHitNPC: If not immune, when it hits it manually set an immune other than 10
		Ex: Arkhalis: Sets it to 5
		Ex: Sharknado Minion: Sets to 20
		Video: https://gfycat.com/DisloyalImprobableHoatzin Notice how Sharknado minion hits prevent Arhalis hits for a brief moment.
	4. projectile.usesIDStaticNPCImmunity and projectile.idStaticNPCHitCooldown: Specifies that a type of projectile has a shared immunity timer for each npc.
		Use this if you want other projectiles a chance to damage, but don't want the same projectile type to hit an npc rapidly.
		Ex: Ghastly Glaive is the only one who uses this.
	5. projectile.usesLocalNPCImmunity and projectile.localNPCHitCooldown: Specifies the projectile manages it's own immunity timers for each npc
		Use this if you want the multiple projectiles of the same type to have a chance to attack rapidly, but don't want a single projectile to hit rapidly. A -1 value prevents the same projectile from ever hitting the npc again.
		Ex: Lightning Aura sentries use this. (localNPCHitCooldown = 3, but other code controls how fast the projectile itself hits) 
			Overlapping Auras all have a chance to hit after each other even though they share the same ID.

	Try the above by uncommenting out the respective bits of code in the projectile below.
	*/

    internal class TruesilverSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 160;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            //1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
            //2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
            //2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
            //5: projectile.usesLocalNPCImmunity = true;
            //5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            //5b: projectile.localNPCHitCooldown = 20; // o
        }



        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 255f);
        }


        public override void AI()
        {

            Projectile.ai[0] += 1f;
            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            if (Projectile.ai[0] > 40f)
            {
                // Fade out
                Projectile.alpha += 10;
                if (Projectile.alpha > 255)
                {
                    Projectile.alpha = 255;
                }
            }
            else
            {
                // Fade in
                Projectile.alpha -= 25;
                if (Projectile.alpha < 100)
                {
                    Projectile.alpha = 100;
                }
            }
            // Slow down
            Projectile.velocity *= 0.98f;
            Projectile.scale = (int)(Projectile.scale * 1.9);
            // Loop through the 4 animation frames, spending 5 ticks on each.
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 4;
                }
            }
            // Kill this projectile after 1 second
            if (Projectile.ai[0] >= 60f)
            {
                Projectile.Kill();
            }
            Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
            }
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 247, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

            }
        }

        // Some advanced drawing because the texture image isn't centered or symetrical.
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = (Texture2D)TextureAssets.Projectile[Projectile.type];
            int frameHeight = TextureAssets.Projectile[Projectile.type].Height() / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = Projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20;

            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage /= 2;
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(target.Center, 0, 0, 20, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 1.2f);

            }




        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            modifiers.CritDamage.Flat += player.statLife / 10;
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
