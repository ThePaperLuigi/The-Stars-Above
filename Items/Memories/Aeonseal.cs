using StarsAbove.Items.Prisms;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Memories
{
    public class Aeonseal : WeaponMemory
	{


        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemType<Materials.NeonTelemetry>(), 50)
                .AddCustomShimmerResult(ItemType<Materials.NeonTelemetry>(), 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
