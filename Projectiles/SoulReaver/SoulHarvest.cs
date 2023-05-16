
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SoulReaver
{
    public class SoulHarvest : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Soul Harvest");
			
		}

		public override void SetDefaults() {
			Projectile.width = 350;
			Projectile.height = 350;
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

		
		public override void AI() {
			//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

			Projectile.ai[0] += 1f;
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1f);
			}
			for (int d = 0; d < 44; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
			}
			for (int d = 0; d < 26; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center,0, 0, DustID.BubbleBurst_Purple, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1f);
			}
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1f);
			}
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
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

			if (!target.active && Main.player[Projectile.owner].HasBuff(BuffType<Buffs.SoulReaver.SoulSplit>()) && !target.SpawnedFromStatue && target.damage > 0)
			{
				
				if(crit)
                {
					Main.player[Projectile.owner].ClearBuff(BuffType<Buffs.SoulReaver.SoulSplit>());
					Main.player[Projectile.owner].AddBuff(BuffType<Buffs.SoulReaver.SoulSplit>(), 120);

				}
			}
			
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {


           
        }
    }
}
