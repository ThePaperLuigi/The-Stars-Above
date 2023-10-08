
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

namespace StarsAbove.Systems.Items
{
    public class ItemMemorySystem : GlobalItem
    {
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
        public bool TarotCard;
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


        public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            //base.OnCreated(item, context);
        }


    }
}