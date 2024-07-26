using Microsoft.Xna.Framework;
using StarsAbove.Systems.Items;
using StarsAbove.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Prisms
{
    public class BloodyBanquetPrism : StellarPrism
    {
        public override int SetBonus { get; set; } = (int)ItemPrismSystem.MajorSetBonuses.BloodyBanquet;
        public override string SetBonusSimpleName { get; set; } = "BloodyBanquet";
        public override string SetBonusName { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.BloodyBanquet.Name");
        public override string SetBonusDescription1 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.BloodyBanquet.SetBonus1");
        public override string SetBonusDescription2 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.BloodyBanquet.SetBonus2");
        public override string SetBonusDescription3 { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.BloodyBanquet.SetBonus3");

        public override string FlavorTooltip { get; set; } = LangHelper.GetTextValue("StellarNova.StellarPrisms.BloodyBanquet.FlavorTooltip");

    }
}