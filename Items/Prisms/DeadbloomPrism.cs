using Microsoft.Xna.Framework;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Prisms
{
    public class DeadbloomPrism : StellarPrism
	{
        public override string SetBonus { get; set; } = "Deadbloom";
        public override string SetBonusName { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.Deadbloom.Name");
		public override string SetBonusDescription1 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.Deadbloom.SetBonus1");
        public override string SetBonusDescription2 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.Deadbloom.SetBonus2");
        public override string FlavorTooltip { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.Deadbloom.FlavorTooltip");

	}
}