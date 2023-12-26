using StarsAbove.Subworlds;
using SubworldLibrary;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class StarsAboveEnterSubworldTestCommand : ModCommand
    {
        public override string Command => "tsatestenter";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            SubworldSystem.Enter<Observatory>();

        }



    }
    public class StarsAboveEnterSubworldExitCommand : ModCommand
    {
        public override string Command => "tsatestexit";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            SubworldSystem.Exit();

        }



    }
}
