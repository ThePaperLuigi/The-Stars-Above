
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
    public class ItemPrismSystem : StarsAboveGlobalItem
    {
        public bool isPrism = false;

        StarsAboveGlobalItem StarsAboveGlobalItem = new StarsAboveGlobalItem();
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item entity)
        {

        }
        
        public override void SaveData(Item item, TagCompound tag)
        {
            if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
                
            }
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
               
            }
        }
        
    }
}