using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{
    public class AnomalyByte : StargazerRelic
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Anomalous Byte");

			/* Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +
				"\nCritical strikes are disabled" +
                "\nWhen damage is dealt, either deal 40% more damage (40% chance), 40% less damage (30% chance), 400% more damage (20% chance), or 1 damage (10% chance)" +
                "\nPositive results will additionally become critical strikes" +
				"\n[c/ADEEFF:Only one Stargazer Relic can be equipped at a time]" +
				"\n'###?'"); */
			
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

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			if (AnomalyByteEquipped)
			{
				int randomDamage;
				randomDamage = Main.rand.Next(0, 11);
				if (randomDamage == 0)//Critical fail
				{
					modifiers.DisableCrit();
					modifiers.FinalDamage *= 0f;
					modifiers.FinalDamage.Flat += 1;
				}
				//If value is 1 or 2
				if (randomDamage > 0 && randomDamage <= 2)//Critical success
				{
					modifiers.SourceDamage += 1f;
					modifiers.SetCrit();
				}
				//If value is 3,4,5, or 6
				if (randomDamage > 2 && randomDamage <= 6)//Fail
				{
					modifiers.DisableCrit();

					modifiers.SourceDamage *= 0.6f;
				}
				//If value is 7,8,9 or 10
				if (randomDamage > 6)//Success
				{
					modifiers.SourceDamage += 0.4f;
					modifiers.SetCrit();

				}
			}
		}
        
        public override void ResetEffects()
        {
			AnomalyByteEquipped = false;

        }
    }
}
