using SubworldLibrary;
using Terraria.ModLoader;


namespace StarsAbove.Systems
{

    public class StarsAboveGlobalTile : GlobalTile
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

namespace StarsAbove.Systems
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