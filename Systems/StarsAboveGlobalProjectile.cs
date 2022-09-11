
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove
{
	
	public class StarsAboveGlobalProjectile : GlobalProjectile
	{

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if ((projectile.ModProjectile?.Mod == ModLoader.GetMod("StarsAbove") || Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().aprismatism == 2) && (!projectile.hostile || projectile.friendly))
			{ // Here we make sure to only change Copper Shortsword by checking Projectile.type in an if statement

				if (Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
					projectile.DamageType = DamageClass.Melee;
				}
				if (Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
				{
					projectile.DamageType = DamageClass.Magic;
				}
				if (Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
				{
					projectile.DamageType = DamageClass.Ranged;
				}
				if (Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
				{

					projectile.DamageType = DamageClass.Summon;
				}
				if (Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
					{
						projectile.DamageType = calamityMod.Find<DamageClass>("RogueDamageClass");
						if (!projectile.minion)
                        {
							
						}
						

					}
				}


			}




		}
       

    }
}