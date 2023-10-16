
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using StarsAbove.Items.Weapons;
using StarsAbove.Items.Weapons.Summon;
using StarsAbove.Items.Weapons.Ranged;
using StarsAbove.Items.Weapons.Other;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Melee;
using StarsAbove.Items.Weapons.Magic;
using StarsAbove.Items.Essences;
using StarsAbove.Prefixes;
using SubworldLibrary;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using StarsAbove.Items.Prisms;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Items.Materials;
using StarsAbove.Utilities;
using Terraria.UI.Chat;
using StarsAbove.Buffs;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Items.Loot;
using StarsAbove.Systems;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using StarsAbove.Items.Memories;

namespace StarsAbove.Systems.Items
{
    public class ItemMemorySystem : GlobalItem
    {
        public List<string> EquippedMemories = new List<string>()//Unused
        {
            "1","2","3"
        };
        public int memoryCount;

        public string memorySlot1 = "";
        public string memorySlot2 = "";
        public string memorySlot3 = "";

        public bool isMemory = false;

        public bool ChoiceGlasses;
        public bool RedSpiderLily;
        public bool AetherBarrel;
        public bool NookMiles;
        public bool CapeFeather;
        public bool RawMeat;
        public bool PhantomMask;
        public bool NetheriteIngot;
        public bool EnderPearl;
        public bool Shard;
        public bool PowerMoon;
        public bool YoumuHilt;
        public bool Rageblade;
        public bool ElectricGuitarPick;
        public bool DekuNut;
        public bool MatterManipulator;
        public bool BottledChaos;
        public bool Trumpet;
        public bool GuppyHead;
        public bool Pawn;
        public bool ReprintedBlueprint;
        public bool ResonanceGem;
        public bool RuinedCrown;
        public bool DescenderGemstone;
        public bool MonsterNail;
        public bool MindflayerWorm;
        public bool MercenaryAuracite;
        public bool ChronalDecelerator;
        public bool SimulacraShifter;
        public bool BlackLightbulb;
        public bool SigilOfHope;

        //Each weapon has a random tarot card effect assigned.
        public bool TarotCard;
        public int tarotCardType = 0;

        //Garridine's Protocores
        public bool ProtocoreMonoclaw;
        public bool ProtocoreManacoil;
        public bool ProtocoreShockrod;

        //Sigils
        public bool RangedSigil;
        public bool MagicSigil;
        public bool MeleeSigil;
        public bool SummonSigil;

        //Aeonseals
        public bool AeonsealDestruction;
        public bool AeonsealHunt;
        public bool AeonsealErudition;
        public bool AeonsealHarmony;
        public bool AeonsealNihility;
        public bool AeonsealPreservation;
        public bool AeonsealAbundance;

        public bool AeonsealTrailblazer;

        //Stats
        public int cooldownReduction;
        public float damageModAdditive;
        public float damageModMultiplicative;

        StarsAboveGlobalItem globalItem = new StarsAboveGlobalItem();
        public override bool InstancePerEntity => true;     
        public override void SetDefaults(Item entity)
        {
            if (globalItem.AstralWeapons.Contains(entity.type) || globalItem.UmbralWeapons.Contains(entity.type) || globalItem.SpatialWeapons.Contains(entity.type))
            {
                //1 fool, 2 magician, 3 priestess, 4 empress, 5 emperor, 6 heirophant, 7 lovers, 8 chariot,
                tarotCardType = Main.rand.Next(0, 21);

            }
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["M1"] = memorySlot1;
            tag["M2"] = memorySlot1;
            tag["M3"] = memorySlot1;

            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            memorySlot1 = tag.Get<string>("M1");
            memorySlot2 = tag.Get<string>("M2");
            memorySlot3 = tag.Get<string>("M3");

            base.LoadData(item, tag);
        }
        public override void UpdateInventory(Item item, Player player)
        {
            memoryCount = 0;
            SigilOfHope = false;

            string check = "";

            check = "Sigil Of Hope";//This does not work when translated, find a better solution
            if(memorySlot1 == check || memorySlot2 == check || memorySlot3 == check)
            {
                SigilOfHope = true;
                memoryCount++;
            }
            //End
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (globalItem.AstralWeapons.Contains(item.type) || globalItem.UmbralWeapons.Contains(item.type) || globalItem.SpatialWeapons.Contains(item.type))
            {
                string tooltipAddition = "";
                //Determine the aspect of aspected weapons.
                if (globalItem.AstralWeapons.Contains(item.type))
                {
                    tooltipAddition = $"[i:{ItemType<Astral>()}]";
                }
                if (globalItem.UmbralWeapons.Contains(item.type))
                {
                    tooltipAddition = $"[i:{ItemType<Umbral>()}]";
                }
                if (globalItem.SpatialWeapons.Contains(item.type))
                {
                    tooltipAddition = $"[i:{ItemType<Spatial>()}]";
                }
                //Add in the icons of the Memories.
                if(memoryCount > 0)
                {
                    tooltipAddition += " : ";

                    if(SigilOfHope)
                    {
                        tooltipAddition += $"[i:{ItemType<SigilOfHope>()}]";
                    }
                }

                TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: AspectIdentifier", tooltipAddition) { OverrideColor = Color.White };
                tooltips.Add(tooltip);
            }
        }
    }
}