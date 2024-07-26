using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using StarsAbove.Systems.Items;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarsAbove.Items.Prisms
{

	public abstract class StellarPrism : ModItem
    {
        /* The plan for the Prism rework:
		 * 1. Prisms can roll on tier based on boss progression. There is a 80% chance for the prism to be the correct tier, and a 20% chance for it to be any tier below.
		 * 2. Prisms roll with 3 random stat bonuses (these can be any Nova stat (Damage, Crit Rate, Crit Damage, Energy Cost))
		 * 2a. One of these stat bonuses is a main stat and rolls 2 tiers higher. The same stat can't be rolled twice.
		 * 3. Prisms roll as a certain set: for instance, Prisms have an effect that activates with 1 or 2 of the set (usually the 2nd bonus is a buff to the first bonus)
		 * 4. There are unique prisms that only work on specific Stellar Novas that dramatically change their effects.
		 * 5. The stat bonuses are based on the tier of the prism. Each tier raises the minimum and maximum of the stat.
		 * 6. You should have a use for unwanted Prisms. Maybe items to make farming them easier?
		 * 
		 * In this class, the plan is:
		 * Create the stats when the prism is spawned based on tier and make sure they save
		 * 
		 * Using PreDrawInInventory/World, change the sprite of the Prism to match the set
		 * Write the tooltip dynamically
		 * 
		 * Example Prism:
		 * 
		 * Deadbloom Prism (set bonus name)
		 * ✦ 32% Crit Chance											(main stat, this is the highest stat and is treated as if it were +2 tiers higher than the other stats in terms of scaling)
		 * ✦ 3.8% Damage												(random rolled stat, maybe if this is post Skeletron the stat is like (2%-8%) with a variability of 6% 
		 * ✦ -4 Energy Cost
		 * 
		 * Deadbloom's Perfusion (1/2)												(Set bonuses look grey unless active.)
		 * Increases damage dealt to enemies with debuffs by 12%
		 * Deadbloom's Perfusion (1/3)
		 * Increases crit rate by 8%
		 * Crit damage against debuffed foes is increased by 30%
		 * 
		 * 'Deadblooms fester on fallen corpses only in the Great Causeway, an enigma as vexing as it is curious'
		 * 
		 * Crescent Meteor Prism (set bonus name)
		 * ✦ 42% Damage (main stat, this is the highest stat and is treated as if it were +2 tiers higher than the other stats in terms of scaling)
		 * ✦ 11% Crit Damage
		 * ✦ -7 Energy Cost
		 * 
		 * Set Bonus (1/2)
		 * Energy cost reduction from Prisms is reversed, turning into increased cost
		 * Gain 1% bonus damage and crit damage for every point of energy above the original energy cost of the Stellar Nova
		 * Set Bonus (1/3)
		 * Increases Nova Energy generation by 15%
		 * After the Stellar Nova is used, Nova energy is slowly regenerated for 2 seconds
		 * 
		 * 'A crescent meteor is surely a sign of good tidings to come in the Moirae Galaxy'
		 * 
		 * Seventh Sigil Prism (set bonus name)
		 * ✦ 42% Damage (main stat, this is the highest stat and is treated as if it were +2 tiers higher than the other stats in terms of scaling)
		 * ✦ 3.8% Damage
		 * ✦ +4 Energy Cost
		 * 
		 * Set Bonus (1/2)
		 * The Stellar Nova now inflicts Weakness Exposed on foes struck, increasing damage taken from all sources by 10%
		 * Set Bonus (1/3)
		 * After the Stellar Nova is used, gain Weakness Analysis, increasing damage dealt to foes with Weakness Exposed by 30% for 8 seconds
		 * This bonus is increased to 120% against non-boss foes
		 * 
		 * 'The Seventh Sigil is an expensive mercenary group that is most known for moonlighting as a galaxy-wide fashion service'
		 * 
		 * * Paracausal Lucidity Prism (set bonus name)
		 * ✦ -12 Energy Cost (main stat, this is the highest stat and is treated as if it were +2 tiers higher than the other stats in terms of scaling)
		 * ✦ 3.8% Damage
		 * ✦ 4.1% Crit Chance
		 * 
		 * Set Bonus (1/1)
		 * If the current Stellar Nova is Guardian's Light, transforms the Stellar Nova into Prismatic Brilliance
		 * 
		 */
        public override void SetStaticDefaults()
        {
            Item.GetGlobalItem<ItemPrismSystem>().isPrism = true;
            Item.maxStack = 1;
            Item.width = 20;
            Item.height = 20;
            Item.accessory = false;
        }
        public enum MainStatValue
        {
            Damage,
            CritDamage,
            CritRate,
            EnergyCost,
            EffectDuration
        }
        int mainStat = 0;
        int rarityValue = 0;
        public override void UpdateInventory(Player player)
        {
            Item.value = Item.buyPrice(gold: Item.rare);

            base.UpdateInventory(player);
        }
        Color rarityColor = Color.White;
        //In the inventory, the item has a unique colored square based on its tier
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Vector2 spriteSize = frame.Size() * scale;
            
            Color useColor = rarityColor;
            if (rarityValue <= 11)
            {
                Item.rare = rarityValue;

            }
            else
            {
                Item.rare = ModContent.GetInstance<StellarRarity>().Type;

            }
            if (Item.rare != ModContent.RarityType<StellarRarity>())
            {
                switch (Item.rare)
                {
                    case 1:
                        useColor = new Color(150, 150, 255);
                        break;
                    case 2:
                        useColor = new Color(150, 255, 150);
                        break;
                    case 3:
                        useColor = new Color(255, 200, 150);
                        break;
                    case 4:
                        useColor = new Color(255, 150, 150);
                        break;
                    case 5:
                        useColor = new Color(255, 150, 255);
                        break;
                    case 6:
                        useColor = new Color(210, 160, 255);
                        break;
                    case 7:
                        useColor = new Color(150, 255, 10);
                        break;
                    case 8:
                        useColor = new Color(255, 255, 10);
                        break;
                    case 9:
                        useColor = new Color(5, 200, 255);
                        break;
                    case 10:
                        useColor = new Color(255, 40, 100);
                        break;
                    case 11:
                        useColor = new Color(180, 40, 255);
                        break;
                    case 12:
                        Item.rare = ModContent.RarityType<StellarRarity>();
                        useColor = new Color(Main.masterColor + 0.5f, -Main.masterColor, 0.8f);
                        //useColor = new Color(255,255, 255);
                        break;

                }
            }
            else
            {
                useColor = new Color (Main.masterColor + 0.5f, -Main.masterColor, 0.8f);
            }

            Texture2D rarityIconTexture = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/UI/StellarNova/RarityIcon");

            spriteBatch.Draw(rarityIconTexture,
                position: new Vector2(position.X - spriteSize.Y * 0.9f, position.Y + spriteSize.Y/2 * 0.9f),
                //position: new Vector2(position.X, position.Y),

                sourceRectangle: new Rectangle(0, 0, 11,11),
                useColor,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(1f, 1f),
                SpriteEffects.None,
                layerDepth: 0f);

        }
        public override void SetDefaults()
		{
            if(isGenerated)
            {
                //Prevent Prism stats from rerolling when taking them in and out of Magic Storage.
                return;
            }
            List<Stat> availableStats = new List<Stat> { Stat.Damage, Stat.CritDamage, Stat.CritRate, Stat.EnergyCost, Stat.EffectDuration };

            bool slimeKing = NPC.downedSlimeKing;
            bool eye = NPC.downedBoss1;
            bool evilboss = NPC.downedBoss2;
            bool queenBee = NPC.downedQueenBee;
            bool skeletron = NPC.downedBoss3;
            bool hardmode = Main.hardMode;
            bool anyMech = NPC.downedMechBossAny;
            bool allMechs = NPC.downedMechBoss3 && NPC.downedMechBoss2 && NPC.downedMechBoss1;
            bool plantera = NPC.downedPlantBoss;
            bool golem = NPC.downedGolemBoss;
            bool cultist = NPC.downedAncientCultist;
            bool moonLord = NPC.downedMoonlord;

            int rarity = 1 +
                //(slimeKing ? 1 : 0) + //1
                (eye ? 1 : 0) + // 2
                (evilboss ? 1 : 0) +//3
                (queenBee ? 1 : 0) +//4
                (skeletron ? 1 : 0) +//5
                (hardmode ? 1 : 0) +//6
                (anyMech ? 1 : 0) +//7
                (allMechs ? 1 : 0) +//8
                (plantera ? 1 : 0) +//9
                (golem ? 1 : 0) +//10
                (cultist ? 1 : 0) +//11
                (moonLord ? 1 : 0);//12 Stellar rarity.

            rarityValue = rarity;

            if (rarity <= 11)
            {
                if (Main.rand.Next(11) <= 7) //70% chance for a good roll
                {
                    Item.rare = rarity;//Good roll
                }
                else
                {
                    Item.rare = rarityValue = Main.rand.Next(1, rarity);//Bad roll
                }
            }
            else
            {
                if (Main.rand.Next(11) <= 7)
                {
                    Item.rare = ModContent.GetInstance<StellarRarity>().Type;//Good roll
                    rarityValue = 12;
                }
                else
                {
                    Item.rare = rarityValue = Main.rand.Next(1, 12);//Bad roll
                }
            }

            


            Damage = 0;
            CritDamage = 0;
            CritRate = 0;
            EnergyCost = 0;
            EffectDuration = 0;

            for (int i = 0; i < 3; i++)
            {
                // Select a random stat from the available stats
                int randomIndex = Main.rand.Next(availableStats.Count);
                Stat selectedStat = availableStats[randomIndex];

                int baseline = rarityValue;

                switch (selectedStat)
                {
                    case Stat.Damage:
                        //Example: A max rarity prism (12) can roll from 23% to 30% bonus damage
                        //Meanwhile: a prism of rarity 5 can roll from 4% to 11%
                        Damage = Math.Max(1f, baseline * 2 + Main.rand.NextFloat(-1, 6));
                        if(i == 0)
                        {
                            //Make this the first one, have a special color, and make it stronger
                            Damage += Main.rand.NextFloat(baseline - 1, baseline + 2);
                            mainStat = (int)MainStatValue.Damage;

                        }
                        Damage = (float)Math.Round(Damage, 2);
                        break;
                    case Stat.CritDamage:
                        CritDamage = Math.Max(1f, baseline * 2 + Main.rand.NextFloat(-1, 6));
                        if (i == 0)
                        {
                            //Make this the first one, have a special color, and make it stronger
                            CritDamage += Main.rand.NextFloat(baseline - 1, baseline + 2);
                            mainStat = (int)MainStatValue.CritDamage;

                        }
                        CritDamage = (float)Math.Round(CritDamage, 2);
                        break;
                    case Stat.CritRate:
                        CritRate = Math.Max(1f, baseline/2 + Main.rand.NextFloat(-1, 4));
                        if (i == 0)
                        {
                            //Make this the first one, have a special color, and make it stronger
                            CritRate += Main.rand.NextFloat(baseline/2 - 1, baseline/ 2 + 2);
                            mainStat = (int)MainStatValue.CritRate;

                        }
                        CritRate = (float)Math.Round(CritRate, 2);

                        break;
                    case Stat.EnergyCost:
                        EnergyCost = (int)Math.Max(1, baseline/2 + Main.rand.NextFloat(0, 2));
                        if (i == 0)
                        {
                            //Make this the first one, have a special color, and make it stronger
                            EnergyCost += (int)Main.rand.NextFloat(baseline/4, baseline / 4 + 2);
                            mainStat = (int)MainStatValue.EnergyCost;

                        }

                        break;
                    case Stat.EffectDuration:
                        EffectDuration = Math.Max(1, baseline / 6 + Main.rand.NextFloat(0, 1));
                        if (i == 0)
                        {
                            //Make this the first one, have a special color, and make it stronger
                            EffectDuration += Main.rand.NextFloat(baseline / 6, baseline / 6 + 2);
                            mainStat = (int)MainStatValue.EffectDuration;

                        }
                        EffectDuration = (float)Math.Round(EffectDuration, 1);
                        break;
                }

                // Remove the selected stat from the list of available stats
                availableStats.RemoveAt(randomIndex);
            }

            //Not implemented.
            if(false)
            {
                //Add a secondary set bonus
                ExtraSetBonus = Main.rand.Next(0, 8);//hardcoded!!
                switch(ExtraSetBonus)
                {
                    case (int)ItemPrismSystem.MinorSetBonuses.Alchemic:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Alchemic.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Alchemic.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Castellic:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Castellic.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Castellic.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Everflame:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Everflame.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Everflame.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Lucent:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Lucent.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Lucent.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Phylactic:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Phylactic.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Phylactic.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Radiant:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Radiant.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Radiant.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Refulgent:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Refulgent.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Refulgent.Bonus");
                        break;
                    case (int)ItemPrismSystem.MinorSetBonuses.Verdant:
                        ExtraSetBonusName = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Verdant.Name");
                        ExtraSetBonusDescription = LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus.Verdant.Bonus");
                        break;
                }
            }
            var globalItem = Item.GetGlobalItem<ItemPrismSystem>();

            globalItem.isPrism = true;

            globalItem.Damage = Damage;
            globalItem.CritDamage = CritDamage;
            globalItem.CritRate = CritRate;
            globalItem.EnergyCost = EnergyCost;
            globalItem.EffectDuration = EffectDuration;
            globalItem.MajorSetBonus = SetBonus;
            isGenerated = true;

        }
        public enum Stat
        {
            Damage,
            CritDamage,
            CritRate,
            EnergyCost,
            EffectDuration
        }
		public float Damage { get; set; }
        public float CritDamage { get; set; }
        public float CritRate { get; set; }
        public int EnergyCost { get; set; }
        public float EffectDuration { get; set; }
        public abstract int SetBonus { get; set; }
        public abstract string SetBonusName { get; set; }
        public abstract string SetBonusSimpleName { get; set; }
        public virtual string SetBonusDescription1 { get; set; }
        public virtual string SetBonusDescription2 { get; set; }
        public virtual string SetBonusDescription3 { get; set; }
        public virtual int ExtraSetBonus { get; set; } = 0;
        public virtual string ExtraSetBonusName { get; set; } = "";
        public virtual string ExtraSetBonusDescription { get; set; } = "";
        public virtual string FlavorTooltip { get; set; }
        public virtual bool IsSpecial { get; set; }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine info = new TooltipLine(Mod, "StarsAbove: info", LangHelper.GetTextValue("StellarNova.StellarPrisms.Info")) { OverrideColor = Color.LightSkyBlue };
            tooltips.Add(info);
            switch (mainStat)
            {
                case (int)MainStatValue.Damage:
                    TooltipLine tooltipDamage = new TooltipLine(Mod,
                    "StarsAbove: Damage",
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.Damage", Damage))
                    { OverrideColor = Color.Gold };
                    tooltips.Add(tooltipDamage);
                    break;

                case (int)MainStatValue.CritDamage:
                    TooltipLine tooltipCDamage = new TooltipLine(Mod,
                   "StarsAbove: CritDamage",
                   LangHelper.GetTextValue("StellarNova.StellarPrisms.CritDamage", CritDamage))
                    { OverrideColor = Color.Gold };
                    tooltips.Add(tooltipCDamage);
                    break;

                case (int)MainStatValue.CritRate:
                    TooltipLine tooltipCR = new TooltipLine(Mod,
                   "StarsAbove: CritRate",
                   LangHelper.GetTextValue("StellarNova.StellarPrisms.CritRate", CritRate))
                    { OverrideColor = Color.Gold };
                    tooltips.Add(tooltipCR);
                    break;

                case (int)MainStatValue.EnergyCost:
                    TooltipLine tooltip = new TooltipLine(Mod,
                    "StarsAbove: EnergyCost",
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.EnergyCost", EnergyCost))
                    { OverrideColor = Color.Gold };
                    tooltips.Add(tooltip);
                    break;
                case (int)MainStatValue.EffectDuration:
                    TooltipLine tooltipED = new TooltipLine(Mod,
                    "StarsAbove: EffectDuration",
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.EffectDuration", EffectDuration))
                    { OverrideColor = Color.Gold };
                    tooltips.Add(tooltipED);
                    break;
            }
            if(Damage != 0 && mainStat != (int)MainStatValue.Damage)
            {
                TooltipLine tooltip = new TooltipLine(Mod, 
                    "StarsAbove: Damage", 
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.Damage", Damage)) { OverrideColor = Color.LightGreen };
                tooltips.Add(tooltip);
            }
            if (CritDamage != 0 && mainStat != (int)MainStatValue.CritDamage)
            {
                TooltipLine tooltip = new TooltipLine(Mod, 
                    "StarsAbove: CritDamage", 
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.CritDamage", CritDamage)) { OverrideColor = Color.LightGreen };
                tooltips.Add(tooltip);
            }
            if (CritRate != 0 && mainStat != (int)MainStatValue.CritRate)
            {
                TooltipLine tooltip = new TooltipLine(Mod, 
                    "StarsAbove: CritRate", 
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.CritRate", CritRate)) { OverrideColor = Color.LightGreen };
                tooltips.Add(tooltip);
            }
            if (EnergyCost != 0 && mainStat != (int)MainStatValue.EnergyCost)
            {
                TooltipLine tooltip = new TooltipLine(Mod, 
                    "StarsAbove: EnergyCost", 
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.EnergyCost", EnergyCost)) { OverrideColor = Color.LightGreen };
                tooltips.Add(tooltip);
            }
            if (EffectDuration != 0 && mainStat != (int)MainStatValue.EffectDuration)
            {
                TooltipLine tooltip = new TooltipLine(Mod,
                    "StarsAbove: EffectDuration",
                    LangHelper.GetTextValue("StellarNova.StellarPrisms.EffectDuration", EffectDuration))
                { OverrideColor = Color.LightGreen };
                tooltips.Add(tooltip);
            }
            if (IsSpecial)
            {
                //These have no set bonuses and instead have unique effects

            }
            else
            {
                TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SetBonus1", SetBonusName + $" (1)") { OverrideColor = Color.PeachPuff };
                tooltips.Add(tooltip);

                TooltipLine tooltipDesc = new TooltipLine(Mod, "StarsAbove: SetBonus1Info", LangHelper.GetTextValue("StellarNova.StellarPrisms." + SetBonusSimpleName + ".SetBonus1")) { OverrideColor = Color.LightGray };
                tooltips.Add(tooltipDesc);

                TooltipLine tooltip2 = new TooltipLine(Mod, "StarsAbove: SetBonus2", SetBonusName + $" (2)") { OverrideColor = Color.PeachPuff };
                tooltips.Add(tooltip2);

                TooltipLine tooltipDesc2 = new TooltipLine(Mod, "StarsAbove: SetBonus2Info", LangHelper.GetTextValue("StellarNova.StellarPrisms." + SetBonusSimpleName + ".SetBonus2")) { OverrideColor = Color.LightGray };
                tooltips.Add(tooltipDesc2);

                TooltipLine tooltip3 = new TooltipLine(Mod, "StarsAbove: SetBonus3", SetBonusName + $" (3)") { OverrideColor = Color.PeachPuff };
                tooltips.Add(tooltip3);

                TooltipLine tooltipDesc3 = new TooltipLine(Mod, "StarsAbove: SetBonus3Info", LangHelper.GetTextValue("StellarNova.StellarPrisms." + SetBonusSimpleName + ".SetBonus3")) { OverrideColor = Color.LightGray };
                tooltips.Add(tooltipDesc3);
            }
            if (ExtraSetBonus != 0)
            {
                TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SetBonusExtra", ExtraSetBonusName + $" (2)") { OverrideColor = Color.PeachPuff };
                tooltips.Add(tooltip);

                TooltipLine tooltipDesc = new TooltipLine(Mod, "StarsAbove: SetBonusExtraInfo", LangHelper.GetTextValue("StellarNova.StellarPrisms.ExtraSetBonus." + ExtraSetBonusName + ".Bonus")) { OverrideColor = Color.LightGray };
                tooltips.Add(tooltipDesc);
            }
            TooltipLine flavor = new TooltipLine(Mod, "StarsAbove: FlavorTooltip", "'" + FlavorTooltip + "'") { OverrideColor = Color.White };
            tooltips.Add(flavor);
        }
        public bool isGenerated;
        public override void SaveData(TagCompound tag)
        {
            if (Damage != 0)
            {
                tag["damage"] = Damage;
            }
            if (CritDamage != 0)
            {
                tag["critdamage"] = CritDamage;
            }
            if (CritRate != 0)
            {
                tag["critrate"] = CritRate;
            }
            if (EnergyCost != 0)
            {
                tag["energycost"] = EnergyCost;
            }
            if (EffectDuration != 0)
            {
                tag["effectduration"] = EffectDuration;
            }
            if (mainStat != 0)
            {
                tag["mainStat"] = mainStat;
            }
            if (SetBonus != 0)
            {
                tag["SetBonus"] = SetBonus;
            }
            if (ExtraSetBonus != 0)
            {
                tag["ExtraSetBonus"] = ExtraSetBonus;
                tag["ExtraSetBonusName"] = ExtraSetBonusName;
                tag["ExtraSetBonusDescription"] = ExtraSetBonusDescription;

            }
            tag["rarity"] = rarityValue;
            tag["isGenerated"] = isGenerated;

        }
        public override void LoadData(TagCompound tag)
        {
            Damage = tag.GetFloat("damage");
            CritDamage = tag.GetFloat("critdamage");
            CritRate = tag.GetFloat("critrate");
            EnergyCost = tag.GetInt("energycost");
            EffectDuration = tag.GetFloat("effectduration");

            SetBonus = tag.GetInt("SetBonus");
            ExtraSetBonus = tag.GetInt("ExtraSetBonus");
            ExtraSetBonusName = tag.GetString("ExtraSetBonusName");
            ExtraSetBonusDescription = tag.GetString("ExtraSetBonusDescription");

            mainStat = tag.GetInt("mainStat");

            rarityValue = tag.GetInt("rarity");
            if (rarityValue == 0)
            {
                rarityValue = Item.rare;
            }
            isGenerated = tag.GetBool("isGenerated");

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
}