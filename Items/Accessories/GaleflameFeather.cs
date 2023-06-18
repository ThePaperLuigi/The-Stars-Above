using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{
    public class GaleflameFeather : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Galeflame Feather");

			/* Tooltip.SetDefault("" +
				"\nGain 5% increased damage in the air" +
				"\nImmunity frames in the air grant an additional 18% increased damage and Swiftness"+
				"\n'Some may say it's overused'"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<WeaponPlayer>().GaleflameFeather = true;
			if(player.velocity.Y != 0)
            {
				player.GetDamage(DamageClass.Generic) += 0.05f;
				if(player.immune)
                {
					player.GetDamage(DamageClass.Generic) += 0.18f;
					player.AddBuff(BuffID.Swiftness, 2);
                }
            }
			//player.respawnTimer += 600;

		}

		

		public override void AddRecipes() {
			
		}
	}
}
