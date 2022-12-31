using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{
    public class AnomalyByte : StargazerRelic
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Anomalous Byte");

			Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +
				"\nCritical strikes are disabled" +
                "\nWhen damage is dealt, either deal 40% more damage (40% chance), 40% less damage (30% chance), 400% more damage (20% chance), or 1 damage (10% chance)" +
                "\nPositive results will additionally become critical strikes" +
				"\n[c/ADEEFF:Only one Stargazer Relic can be equipped at a time]" +
				"\n'###?'");
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<AnomalyBytePlayer>().AnomalyByteEquipped = true;
		}


	}

	public class AnomalyBytePlayer : ModPlayer
    {
		public bool AnomalyByteEquipped;

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
			if(AnomalyByteEquipped)
            {
				int randomDamage;
				randomDamage = Main.rand.Next(0, 11);
				crit = false;
				if(randomDamage == 0)//Critical fail
                {
					damage = 1;
                }
				//If value is 1 or 2
				if(randomDamage > 0 && randomDamage <= 2)//Critical success
                {
					damage *= 2;
					crit = true;
                }
				//If value is 3,4,5, or 6
				if (randomDamage > 2 && randomDamage <= 6)//Fail
				{
					damage = (int)(damage*0.6f);
				}
				//If value is 7,8,9 or 10
				if (randomDamage > 6)//Success
				{
					damage = (int)(damage * 1.4f);
					crit = true;

				}
			}

           
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (AnomalyByteEquipped)
			{
				int randomDamage;
				randomDamage = Main.rand.Next(0, 11);
				crit = false;
				if (randomDamage == 0)//Critical fail
				{
					damage = 1;
				}
				//If value is 1 or 2
				if (randomDamage > 0 && randomDamage <= 2)//Critical success
				{
					damage *= 2;
					crit = true;
				}
				//If value is 3,4,5, or 6
				if (randomDamage > 2 && randomDamage <= 6)//Fail
				{
					damage = (int)(damage * 0.6f);
				}
				//If value is 7,8,9 or 10
				if (randomDamage > 6)//Success
				{
					damage = (int)(damage * 1.4f);
					crit = true;

				}
			}
		}
        public override void ResetEffects()
        {
			AnomalyByteEquipped = false;

        }
    }
}
