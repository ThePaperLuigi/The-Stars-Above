using Microsoft.Xna.Framework;
using StarsAbove.Systems.Items;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Prisms
{
    public class LuminousHallowPrism : StellarPrism
	{
        public override int SetBonus { get; set; } = (int)ItemPrismSystem.MajorSetBonuses.LuminousHallow;
        public override string SetBonusSimpleName { get; set; } = "LuminousHallow";
        public override string SetBonusName { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.LuminousHallow.Name");
        public override string SetBonusDescription1 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.LuminousHallow.SetBonus1");
        public override string SetBonusDescription2 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.LuminousHallow.SetBonus2");
        public override string SetBonusDescription3 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.LuminousHallow.SetBonus3");

        public override string FlavorTooltip { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.LuminousHallow.FlavorTooltip");

	}
}