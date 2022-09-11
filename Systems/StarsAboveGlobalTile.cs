
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove
{
	
	public class StarsAboveGlobalTile : GlobalTile
	{
        public override bool CanExplode(int i, int j, int type)
        {
            if(SubworldSystem.Current != null)
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

    }
}

namespace StarsAbove
{

    public class StarsAboveGlobalWall : GlobalWall
    {
        public override bool CanExplode(int i, int j, int type)
        {
            if (SubworldSystem.Current != null)
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

    }
}