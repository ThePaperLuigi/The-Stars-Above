using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.GoddessOfMemory
{
    public abstract class DebugGoddessOfMemoryArena : ModItem
    {
        protected abstract int ArenaTileType { get; }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = false;
            Item.value = 0;
            Item.rare = ItemRarityID.Red;
            Item.createTile = ArenaTileType;
            Item.ResearchUnlockCount = 0;
        }
    }
}
