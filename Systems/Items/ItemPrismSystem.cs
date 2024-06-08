
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
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using StarsAbove.Items.Memories;
using StarsAbove.Items.Memories.TarotCard;
using StarsAbove.Buffs.Memories;
using StarsAbove.Projectiles.Memories;
using System;
using Terraria.GameInput;
using System.Reflection;
using Terraria.Audio;
using StarsAbove.Buffs.TagDamage;
using Terraria.WorldBuilding;
using StarsAbove.Projectiles.Summon.ArachnidNeedlepoint;
using static Humanizer.In;
using StarsAbove.Systems;

namespace StarsAbove.Systems.Items
{
    public class ItemPrismSystem : GlobalItem
    {
        public bool isPrism = false;

        public int MajorSetBonus;
        public int MinorSetBonus;

        public float Damage = 0;
        public float CritDamage = 0;
        public float CritRate = 0;
        public int EnergyCost = 0;
        public float EffectDuration = 0;
        public enum MajorSetBonuses
        {
            Deadbloom,
            CrescentMeteor,
            LuminousHallow,
            DreadMechanical,
            SeventhSigil,
            Luminite,
        }
        public int MinorSetBonusesNumber = 8; //+1 because this starts at 0
        public enum MinorSetBonuses
        {
            Alchemic,
            Castellic,
            Everflame,
            Lucent,
            Phylactic,
            Radiant,
            Refulgent,
            Verdant,
        }
        public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }
        public override void SetDefaults(Item entity)
        {

        }
        
        public override void SaveData(Item item, TagCompound tag)
        {
            if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") && isPrism)
            {
                tag["p"] = isPrism;
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
            }
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
                isPrism = tag.GetBool("p");
                Damage = tag.GetFloat("damage");
                CritDamage = tag.GetFloat("critdamage");
                CritRate = tag.GetFloat("critrate");
                EnergyCost = tag.GetInt("energycost");
                EffectDuration = tag.GetFloat("effectduration");
            }
        }
        
    }
}