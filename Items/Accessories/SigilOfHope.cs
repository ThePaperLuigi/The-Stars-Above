using StarsAbove.Items.Prisms;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Accessories
{
    public class SigilOfHope : StargazerRelic
	{
		public override void SetStaticDefaults() {
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {

			if (NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBoss>()) || NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBossFinalPhase>()))
            {
				if(EverlastingLightEvent.isEverlastingLightActive)
                {
					player.statDefense += 60;
					player.GetDamage(DamageClass.Generic) += 0.5f;

				}
			}
				

		}

		

		public override void AddRecipes() {
			
		}
	}
}
