
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SubworldLibrary;
using StarsAbove.Buffs;
using StarsAbove.Projectiles.Otherworld;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Subworlds;
using StarsAbove.Systems;

namespace StarsAbove.Items.Placeable.CyberWorld.Monolith
{

    public class NeonVeilReplicaMonolithItem : ModItem
	{
		public override void SetStaticDefaults()
		{

        }

		public override void SetDefaults()
		{
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item44;
            Item.ResearchUnlockCount = 1;
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CyberWorld.Monolith.NeonVeilReplicaMonolith>(), 0);

        }

        public override void AddRecipes()
        {
           
        }
    }
}