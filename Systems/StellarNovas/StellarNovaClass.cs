using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Graphics.Effects;

namespace StarsAbove
{
	public class SetupStellarNovas
    {
		int id;
		int novaGaugeRequired;
		int novaCritChance;
		int novaCritChanceModifier;

		string abilityName;
		string abilitySubName;
		string abilityDescription;

		string starfarerBonusAsphodene;
		string starfarerBonusEridani;

        public string GetInfo(int id, string grab, int baseDamage)
        {
            switch (id)
            {
                case 1:
                    return LangHelper.GetTextValue("StellarNova.StellarNovaInfo.TheofaniaInanis." + grab);
                case 2:
                    return LangHelper.GetTextValue("StellarNova.StellarNovaInfo.ArsLaevateinn." + grab);
                case 3:
                    return LangHelper.GetTextValue("StellarNova.StellarNovaInfo.KiwamiRyuken." + grab);
                case 4:
                    return LangHelper.GetTextValue("StellarNova.StellarNovaInfo.GardenOfAvalon." + grab);
                case 5:
                    return LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EdinGenesisQuasar." + grab, baseDamage);
                default:
                    break;
            }
            return "";
        }
        public int GetNovaDamage(int id, int baseDamage)
        {
            switch (id)
            {
                case 1:
                    return baseDamage;
                case 2:
                    return baseDamage + 250;
                case 3:
                    return baseDamage / 2;
                case 4:
                    return baseDamage / 500;
                case 5:
                    return baseDamage / 15;
                default:
                    break;
            }
            return 100;
        }
        public int GetNovaCost(int id)
        {
            switch (id)
            {
                case 1:
                    return 90;
                case 2:
                    return 110;
                case 3:
                    return 50;
                case 4:
                    return 150;
                case 5:
                    return 180;
                default:
                    break;
            }
            return 100;
        }
        public int GetNovaCritChance(int id)
        {
            switch (id)
            {
                case 1:
                    return 50;
                case 2:
                    return 35;
                case 3:
                    return 70;
                case 4:
                    return 35;
                case 5:
                    return 25;
                default:
                    break;
            }
            return 100;
        }
        public int GetNovaCritDamageMod(int id, int baseDamage)
        {
            switch (id)
            {
                case 1:
                    return (int)(baseDamage * 1.45f);
                case 2:
                    return (int)(baseDamage * 2.8);
                case 3:
                    return baseDamage;
                case 4:
                    return 100;
                case 5:
                    return (int)((baseDamage / 10) * 1.3);
                default:
                    break;
            }
            return 1;
            
        }
    }

}
