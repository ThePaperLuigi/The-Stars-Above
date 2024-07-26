using StarsAbove.Items.Materials;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Armor.NeopursuantArmor
{
    [AutoloadEquip(EquipType.Body)]
	public class NeopursuantRoguegarb : ModItem
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
            Item.defense = 12;

        }
        int armorSetType = 0;
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.12f;
            player.GetCritChance(DamageClass.Generic) += 8;
            player.statManaMax2 += 40;
            player.GetModPlayer<WeaponPlayer>().roguegarbEquipped = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<NeopursuantHeadbooster>())
            {
                armorSetType = 1;
            }
            else if (head.type == ModContent.ItemType<NeopursuantHiGuard>())
            {
                armorSetType = 2;
            }
            else if (head.type == ModContent.ItemType<NeopursuantDualShield>())
            {
                armorSetType = 3;
            }
            return (head.type == ModContent.ItemType<NeopursuantHeadbooster>() || head.type == ModContent.ItemType<NeopursuantHiGuard>() || head.type == ModContent.ItemType<NeopursuantDualShield>())
                && legs.type == ModContent.ItemType<NeopursuantLeggings>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            if (armorSetType > 0)
            {
                player.UpdateVisibleAccessories(new Item(ModContent.ItemType<NeopursuantRoguegarbCape>()), false);

            }
            switch (armorSetType)
            {
                case 1:
                    //Headbooster
                    player.setBonus = LangHelper.GetTextValue("Common.NeopursuantSetBonusInfiltrator");
                    if(player.immune)
                    {
                        player.GetDamage(DamageClass.Generic) += 0.12f;
                    }
                    break;
                case 2:
                    //Dual Shield
                    player.setBonus = LangHelper.GetTextValue("Common.NeopursuantSetBonusViralUpload");
                    player.GetModPlayer<StarsAbovePlayer>().viralUploadActive = true;
                    break;

                case 3:
                    //Hi-Guard
                    player.setBonus = LangHelper.GetTextValue("Common.NeopursuantSetBonusEnviroSavant");
                    player.AddBuff(BuffID.Hunter, 2);
                    player.AddBuff(BuffID.Dangersense, 2);
                    player.GetModPlayer<WeaponPlayer>().enviroSavantActive = true;

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
	
