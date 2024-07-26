using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Armor.NeopursuantArmor
{
    [AutoloadEquip(EquipType.Body)]
	public class NeopursuantPlasteel : ModItem
	{
		public override void SetStaticDefaults()
		{
			
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
			Item.value = 100;
			Item.rare = ItemRarityID.LightRed;
			Item.vanity = false;
			Item.ResearchUnlockCount = 1;
            Item.defense = 18;

		}
		int armorSetType = 0;
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetModPlayer<WeaponPlayer>().plasteelEquipped = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
			if(head.type == ModContent.ItemType<NeopursuantHeadbooster>())
			{
				armorSetType = 1;
			}
            else if (head.type == ModContent.ItemType<NeopursuantDualShield>())
            {
                armorSetType = 2;
            }
            else if (head.type == ModContent.ItemType<NeopursuantHiGuard>())
            {
                armorSetType = 3;
            }
            return (head.type == ModContent.ItemType<NeopursuantHeadbooster>() || head.type == ModContent.ItemType<NeopursuantHiGuard>() || head.type == ModContent.ItemType<NeopursuantDualShield>())
				&& legs.type == ModContent.ItemType<NeopursuantLeggings>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
			if(armorSetType > 0)
			{
                player.UpdateVisibleAccessories(new Item(ModContent.ItemType<NeopursuantPlasteelCape>()), false);

            }
            switch(armorSetType)
            {
                case 1:
                    //Headbooster
                    player.setBonus = LangHelper.GetTextValue("Common.NeopursuantSetBonusFullMetalArcanist");
                    player.GetModPlayer<StarsAbovePlayer>().fullMetalArcanistActive = true;
                    break;
                case 2:
                    //Dual Shield
                    player.setBonus = LangHelper.GetTextValue("Common.NeopursuantSetBonusBackupBattery");
                    if(player.statMana <= 0)
                    {
                        player.statMana = player.statManaMax2;
                        player.AddBuff(BuffID.Frozen, 120);
                    }
                    break;

                case 3:
                    //Hi-Guard
                    player.setBonus = LangHelper.GetTextValue("Common.NeopursuantSetBonusCataphractArms");
                    player.statDefense += (int)(player.GetModPlayer<StarsAbovePlayer>().trueNovaGaugeMax * 0.05);
                    break;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                  .AddIngredient(ItemID.CobaltBar, 15)
                  .AddIngredient(ModContent.ItemType<NeonTelemetry>(), 10)
                  .AddTile(TileID.Anvils)
                  .Register();
            CreateRecipe(1)
                  .AddIngredient(ItemID.PalladiumBar, 15)
                  .AddIngredient(ModContent.ItemType<NeonTelemetry>(), 10)
                  .AddTile(TileID.Anvils)
                  .Register();
        }
    }
	
}