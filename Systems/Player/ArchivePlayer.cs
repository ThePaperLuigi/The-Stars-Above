using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;
using StarsAbove.NPCs.Vagrant;
using StarsAbove.Utilities;

namespace StarsAbove
{
    public class ArchivePlayer: ModPlayer
    {
        public bool archivePopulated = false;
        public bool archiveActive;

        //The Archive has 4 buttons, they correspond to Idle Dialogue, Boss Dialogue, Weapon Dialogue, and Starfarer Prompts.
        //Selecting a button changes the minimum/maximum values of the indicator. Pressing the left and right buttons cycle through the dialogues.
        //The dialogues start at 1, not 0.
        // Once a dialogue has been selected, the Starfarer Menu will close, and the dialogue will appear.
        //The Starfarer Menu will shift to the left to make room. Otherwise, it's centered.
        public int archiveChosenList = 0; //0 = Idle | 1 = Boss | 2 = Weapon | 3 = VN

        public int archiveListNumber = 1;//This is reset to 1 after you've chosen a different list.
        public int archiveListMax = 1;//This is the total amount of dialogues available.

        public string archiveListInfo = "";

        public bool canViewArchive = false; //Disable viewing the dialogue if it hasn't been unlocked.

        public List<IdleArchiveListing> IdleArchiveList = new List<IdleArchiveListing>();
        public List<BossArchiveListing> BossArchiveList = new List<BossArchiveListing>();
        public List<BossArchiveListingCalamity> BossArchiveListCalamity = new List<BossArchiveListingCalamity>();
        public List<WeaponArchiveListing> WeaponArchiveList = new List<WeaponArchiveListing>();
        public List<VNArchiveListing> VNArchiveList = new List<VNArchiveListing>();


        public int IdleArchiveListMax = 2;
        public int BossArchiveListMax = 2;
        public int BossArchiveListMaxCalamity = 2;
        public int WeaponArchiveListMax = 2;
        public int VNArchiveListMax = 2;

        public override void PreUpdate()
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();
            #region archive
            /////////////////////////////////////////////////////ARCHIVE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            if (!archivePopulated)
            {
                IdleArchiveList.Add(new IdleArchiveListing(
                      "", //Name of the archive listing.
                      $"", //Description of the listing.
                      false, //Unlock requirements.
                      0,
                      "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
               LangHelper.GetTextValue($"Archive.DefaultIdleDialogue.Name", Player.name), //Name of the archive listing.
               LangHelper.GetTextValue($"Archive.DefaultIdleDialogue.Description", Player.name), //Description of the listing.
               true, //Unlock requirements.
               2,
               "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 1", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       3,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 2", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       4,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 3", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       5,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 4", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       6,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 5", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       7,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 6", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       8,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 7", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       9,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 8", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       10,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 9", //Name of the archive listing.
                       "Pre Hardmode idle dialogue.", //Description of the listing.
                       true, //Unlock requirements.
                       11,
                       "")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 10", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       12,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 11", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       13,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 12", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       14,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 13", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       15,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 14", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       16,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 15", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       17,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 16", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       18,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 17", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       19,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle Conversation 18", //Name of the archive listing.
                       "Hardmode idle dialogue.", //Description of the listing.
                       Main.hardMode, //Unlock requirements.
                       20,
                       "Enter Hardmode")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "'A World Shrouded in Light'", //Name of the archive listing.
                       "Idle dialogue during Light Everlasting.", //Description of the listing.
                       NPC.downedMoonlord, //Unlock requirements.
                       21,
                       "Unlocked after witnessing Light Everlasting for the first time.")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Entering the Observatory", //Name of the archive listing.
                       "Dialogue on the Observatory Hyperborea.", //Description of the listing.
                       player.seenObservatory, //Unlock requirements.
                       22,
                       "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Explaining Cosmic Voyages", //Name of the archive listing.
                       "An explanation of the mechanics of Cosmic Voyages.", //Description of the listing.
                       player.seenObservatory, //Unlock requirements.
                       24,
                       "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle in the Observatory", //Name of the archive listing.
                       "Neutral dialogue within the Observatory Hyperborea.", //Description of the listing.
                       player.seenObservatory, //Unlock requirements.
                       23,
                       "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                IdleArchiveList.Add(new IdleArchiveListing(
                       "Idle in Space", //Name of the archive listing.
                       "Neutral dialogue when on a normal-type Cosmic Voyage. Unused.", //Description of the listing.
                       player.seenObservatory, //Unlock requirements.
                       23,
                       "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                      "", //Name of the archive listing.
                      $"", //Description of the listing.
                      false, //Unlock requirements.
                      0,
                      "")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Slime King Dethroned", //Name of the archive listing.
                       "Unlocked after defeating King Slime.", //Description of the listing.
                       player.slimeDialogue == 2, //Unlock requirements.
                       51,
                       "Defeat King Slime.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Eye of Cthulhu Pierced", //Name of the archive listing.
                       "Unlocked after defeating Eye of Cthulhu.", //Description of the listing.
                       player.eyeDialogue == 2, //Unlock requirements.
                       52,
                       "Defeat Eye of Cthulhu.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Worldly Evil Sanctified", //Name of the archive listing.
                       "Unlocked after defeating the Corruption/Crimson boss.", //Description of the listing.
                       player.corruptBossDialogue == 2, //Unlock requirements.
                       53,
                       "Defeat the world's Corruption/Crimson boss.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Queen Bee Exterminated", //Name of the archive listing.
                       "Unlocked after defeating Queen Bee.", //Description of the listing.
                       player.BeeBossDialogue == 2, //Unlock requirements.
                       54,
                       "Defeat Queen Bee.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Skeletron Buried", //Name of the archive listing.
                       "Unlocked after defeating Skeletron.", //Description of the listing.
                       player.SkeletonDialogue == 2, //Unlock requirements.
                       55,
                       "Defeat Skeletron.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Deerclops Extinct", //Name of the archive listing.
                       "Unlocked after defeating Deerclops.", //Description of the listing.
                       player.DeerclopsDialogue == 2, //Unlock requirements.
                       76,
                       "Defeat Deerclops.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Wall of Flesh Purged", //Name of the archive listing.
                       "Unlocked after defeating the Wall of Flesh.", //Description of the listing.
                       player.WallOfFleshDialogue == 2, //Unlock requirements.
                       56,
                       "Defeat the Wall of Flesh.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Queen Slime Overthrown", //Name of the archive listing.
                       "Unlocked after defeating Queen Slime", //Description of the listing.
                       player.QueenSlimeDialogue == 2, //Unlock requirements.
                       74,
                       "Defeat Queen Slime")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "The Twins Scrapped", //Name of the archive listing.
                       "Unlocked after defeating the Twins.", //Description of the listing.
                       player.TwinsDialogue == 2, //Unlock requirements.
                       57,
                       "Defeat the Twins.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "The Destroyer Deleted", //Name of the archive listing.
                       "Unlocked after defeating the Destroyer.", //Description of the listing.
                       player.DestroyerDialogue == 2, //Unlock requirements.
                       58,
                       "Defeat the Destroyer.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Skeletron Prime Erased", //Name of the archive listing.
                       "Unlocked after defeating Skeletron Prime.", //Description of the listing.
                       player.SkeletronPrimeDialogue == 2, //Unlock requirements.
                       59,
                       "Defeat Skeletron Prime.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "All Mechanical Bosses Rended", //Name of the archive listing.
                       "Unlocked after defeating all of the Mechanical Bosses.", //Description of the listing.
                       player.AllMechsDefeatedDialogue == 2, //Unlock requirements.
                       60,
                       "Defeat all of the Mechanical Bosses.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Plantera Uprooted", //Name of the archive listing.
                       "Unlocked after defeating Plantera.", //Description of the listing.
                       player.PlanteraDialogue == 2, //Unlock requirements.
                       61,
                       "Defeat Plantera.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Golem Deactivated", //Name of the archive listing.
                       "Unlocked after defeating Golem.", //Description of the listing.
                       player.GolemDialogue == 2, //Unlock requirements.
                       62,
                       "Defeat Golem.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Duke Fishron Hunted", //Name of the archive listing.
                       "Unlocked after defeating Duke Fishron.", //Description of the listing.
                       player.DukeFishronDialogue == 2, //Unlock requirements.
                       63,
                       "Defeat Duke Fishron.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Empress of Light Dimmed", //Name of the archive listing.
                       "Unlocked after defeating the Empress of Light.", //Description of the listing.
                       player.EmpressDialogue == 2, //Unlock requirements.
                       75,
                       "Defeat the Empress of Light.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Lunatic Cultist Crucified", //Name of the archive listing.
                       "Unlocked after defeating the Lunatic Cultist.", //Description of the listing.
                       player.CultistDialogue == 2, //Unlock requirements.
                       64,
                       "Defeat the Lunatic Cultist.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Moon Lord Defeated", //Name of the archive listing.
                       "Unlocked after defeating the Moon Lord.", //Description of the listing.
                       player.MoonLordDialogue == 2, //Unlock requirements.
                       65,
                       "Defeat the Moon Lord.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Terraria's Hero", //Name of the archive listing.
                       "Unlocked after defeating all vanilla Terraria bosses. Grants an Essence.", //Description of the listing.
                       player.AllVanillaBossesDefeatedDialogue == 2, //Unlock requirements.
                       67,
                       "Defeat all vanilla bosses.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Hero of the Realm", //Name of the archive listing.
                       "Unlocked after defeating all vanilla Terraria bosses, and cleansing the world of Light Everlasting. Grants an Essence.", //Description of the listing.
                       player.EverythingDefeatedDialogue == 2, //Unlock requirements.
                       68,
                       "Defeat all vanilla bosses and the Warrior of Light in Expert Mode."));
                BossArchiveList.Add(new BossArchiveListing(
                       "Perseus's Appeal: The Burnished King", //Name of the archive listing.
                       "Grants the item to summon the Burnished King.", //Description of the listing.
                       player.nalhaunBossItemDialogue == 2, //Unlock requirements.
                       301,
                       "???")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Perseus's Appeal: The Witch of Ink", //Name of the archive listing.
                       "Grants the item to summon the Witch of Ink", //Description of the listing.
                       player.penthBossItemDialogue == 2, //Unlock requirements.
                       302,
                       "???")); //Corresponding dialogue ID.

                BossArchiveList.Add(new BossArchiveListing(
                       "Perseus's Appeal: The Warrior of Light", //Name of the archive listing.
                       "Grants the item to summon the Warrior of Light.", //Description of the listing.
                       player.warriorBossItemDialogue == 2, //Unlock requirements.
                       304,
                       "???")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Nalhaun Kneeled", //Name of the archive listing.
                       "Unlocked after defeating Nalhaun, the Burnished King. Grants a material needed for confronting the final boss.", //Description of the listing.
                       player.nalhaunDialogue == 2, //Unlock requirements.
                       70,
                       "Defeat Nalhaun, the Burnished King.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Penthesilea Washed", //Name of the archive listing.
                       "Unlocked after defeating Penthesilea, the Witch of Ink. Grants a material needed for confronting the final boss.", //Description of the listing.
                       player.penthDialogue == 2, //Unlock requirements.
                       71,
                       "Defeat Penthesilea, the Witch of Ink.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "Warrior of Light Vanquished", //Name of the archive listing.
                       "Unlocked after defeating the Warrior of Light. Grants a material needed for confronting the final boss.", //Description of the listing.
                       player.WarriorOfLightDialogue == 2, //Unlock requirements.
                       66,
                       "Defeat the Warrior of Light.")); //Corresponding dialogue ID.
                BossArchiveList.Add(new BossArchiveListing(
                       "The First Starfarer Defeated", //Name of the archive listing.
                       "Unlocked after defeating Tsukiyomi, the First Starfarer. Grants an item used for crafting.", //Description of the listing.
                       player.tsukiyomiDialogue == 2, //Unlock requirements.
                       73,
                       "Defeat ???")); //Corresponding dialogue ID.
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                {
                    BossArchiveList.Add(new BossArchiveListing(
                       "Desert Scourge Defeated", //Name of the archive listing.
                       "Unlocked after defeating the Desert Scourge", //Description of the listing.
                       player.desertscourgeDialogue == 2, //Unlock requirements.
                       201,
                       "Defeat the Desert Scourge")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Crabulon Defeated", //Name of the archive listing.
                       "Unlocked after defeating Crabulon", //Description of the listing.
                       player.crabulonDialogue == 2, //Unlock requirements.
                       202,
                       "Defeat Crabulon")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Hive Mind Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Hive Mind", //Description of the listing.
                           player.hivemindDialogue == 2, //Unlock requirements.
                           203,
                           "Defeat the Hive Mind")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Perforators Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Perforators", //Description of the listing.
                           player.perforatorDialogue == 2, //Unlock requirements.
                           204,
                           "Defeat the Perforators")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Slime God Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Slime God", //Description of the listing.
                           player.slimegodDialogue == 2, //Unlock requirements.
                           205,
                           "Defeat the Slime God")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Cryogen Defeated", //Name of the archive listing.
                           "Unlocked after defeating Cryogen", //Description of the listing.
                           player.cryogenDialogue == 2, //Unlock requirements.
                           206,
                           "Defeat Cryogen")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Aquatic Scourge Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Aquatic Scourge", //Description of the listing.
                           player.aquaticscourgeDialogue == 2, //Unlock requirements.
                           207,
                           "Defeat the Aquatic Scourge")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Brimstone Elemental Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Brimstone Elemental", //Description of the listing.
                           player.brimstoneelementalDialogue == 2, //Unlock requirements.
                           208,
                           "Defeat the Brimstone Elemental")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Calamitas's Clone Defeated", //Name of the archive listing.
                           "Unlocked after defeating Calamitas's Clone", //Description of the listing.
                           player.calamitasDialogue == 2, //Unlock requirements.
                           209,
                           "Defeat Calamitas")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Leviathan Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Leviathan", //Description of the listing.
                           player.leviathanDialogue == 2, //Unlock requirements.
                           210,
                           "Defeat the Leviathan")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Astrum Aureus Defeated", //Name of the archive listing.
                           "Unlocked after defeating Astrum Aureus", //Description of the listing.
                           player.astrumaureusDialogue == 2, //Unlock requirements.
                           211,
                           "Defeat Astrum Aureus")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Plaguebringer Goliath", //Name of the archive listing.
                           "Unlocked after defeating the Plaguebringer Goliath", //Description of the listing.
                           player.plaguebringerDialogue == 2, //Unlock requirements.
                           212,
                           "Defeat the Plaguebringer Goliath")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Ravager Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Ravager", //Description of the listing.
                           player.ravagerDialogue == 2, //Unlock requirements.
                           213,
                           "Defeat the Ravager")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Astrum Deus Defeated", //Name of the archive listing.
                           "Unlocked after defeating Astrum Deus", //Description of the listing.
                           player.astrumdeusDialogue == 2, //Unlock requirements.
                           214,
                           "Defeat Astrum Deus")); //Corresponding dialogue ID.
                }
                //Weapons
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "", //Name of the archive listing.
                      $"", //Description of the listing.
                      false, //Unlock requirements.
                      0,
                      "")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                       "King Slime Weapon", //Name of the archive listing.
                       $"Grants the Essence for either the [i:{ItemType<Astral>()}] Aegis Driver or the [i:{ItemType<Umbral>()}] Rad Gun.", //Description of the listing.
                       player.KingSlimeWeaponDialogue == 2, //Unlock requirements.
                       104,
                       "Defeat King Slime")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                       "Eye of Cthulhu Weapon", //Name of the archive listing.
                       $"Grants the Essence for either " +
                       $"[i:{ItemType<Astral>()}] Carian Dark Moon " +
                       $"or " +
                       $"[i:{ItemType<Umbral>()}] Konpaku Katana.", //Description of the listing.
                       player.EyeBossWeaponDialogue == 2, //Unlock requirements.
                       136,
                       "Defeat Eye of Cthulhu")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Graveyard Weapon", //Name of the archive listing.
                     $"Grants the Essence for either " +
                       $"[i:{ItemType<Astral>()}] Kevesi Farewell " +
                       $"or " +
                       $"[i:{ItemType<Umbral>()}] Agnian Farewell.", //Description of the listing.
                    player.FarewellWeaponDialogue == 2, //Unlock requirements.
                    159,
                    "Visit a Graveyard biome.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Corruption/Crimson Boss Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Neo Dealmaker " +
                      $"or" +
                      $"[i:{ItemType<Umbral>()}] Ashen Ambition.", //Description of the listing.
                      player.CorruptBossWeaponDialogue == 2, //Unlock requirements.
                      137,
                      "Defeat the boss of the world's evil")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Corruption/Crimson Boss Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                      $"[i:{ItemType<Spatial>()}] Takonomicon. ", //Description of the listing.
                      player.TakodachiWeaponDialogue == 2, //Unlock requirements.
                      133,
                      "Defeat the boss of the world's evil, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Queen Bee Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Skofnung " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Inugami Ripsaw.", //Description of the listing.
                      player.QueenBeeWeaponDialogue == 2, //Unlock requirements.
                      103,
                      "Defeat the Queen Bee.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Tier 2 Stellaglyph Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Umbral>()}] Irminsul's Dream " +
                      $"or " +
                      $"[i:{ItemType<Astral>()}] Pod Zero-42.", //Description of the listing.
                      player.Stellaglyph2WeaponDialogue == 2, //Unlock requirements.
                      160,
                      "Obtain a Tier 2 Stellaglyph.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Skeletron Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Umbral>()}] Death in Four Acts " +
                      $"or " +
                      $"[i:{ItemType<Astral>()}] Der Freischutz.", //Description of the listing.
                      player.SkeletonWeaponDialogue == 2, //Unlock requirements.
                      101,
                      "Defeat Skeletron.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Skeletron Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                      $"[i:{ItemType<Spatial>()}] Misery's Company. ", //Description of the listing.
                      player.MiseryWeaponDialogue == 2, //Unlock requirements.
                      120,
                      "Defeat Skeletron, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Skeletron Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                      $"[i:{ItemType<Spatial>()}] Apalistik. ", //Description of the listing.
                      player.OceanWeaponDialogue == 2, //Unlock requirements.
                      123,
                      "Defeat Skeletron, then visit the beach.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Skeletron Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Persephone " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Kazimierz Seraphim.", //Description of the listing.
                      player.HellWeaponDialogue == 2, //Unlock requirements.
                      102,
                      "Defeat Skeletron, then visit the Underworld.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Skeletron Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Karlan Truesilver " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Every Moment Matters.", //Description of the listing.
                      player.WallOfFleshWeaponDialogue == 2, //Unlock requirements.
                      105,
                      "Defeat Skeletron.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Wall of Flesh Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                      $"[i:{ItemType<Spatial>()}] Luminary Wand. ", //Description of the listing.
                      player.LumaWeaponDialogue == 2, //Unlock requirements.
                      124,
                      "Defeat the Wall of Flesh, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Wall of Flesh Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                      $"[i:{ItemType<Spatial>()}] Force-of-Nature. ", //Description of the listing.
                      player.ForceWeaponDialogue == 2, //Unlock requirements.
                      131,
                      "Defeat the Wall of Flesh, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Hallowed Biome Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Aurum Edge. ", //Description of the listing.
                    player.GoldWeaponDialogue == 2, //Unlock requirements.
                    158,
                    "Visit the Hallowed biome.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Pirate Invasion Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Saltwater Scourge. ", //Description of the listing.
                    player.SaltwaterWeaponDialogue == 2, //Unlock requirements.
                    162,
                    "Defeat a pirate invasion, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Queen Slime Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Hunter's Symphony " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Sparkblossom's Beacon.", //Description of the listing.
                      player.QueenSlimeWeaponDialogue == 2, //Unlock requirements.
                      149,//Corresponding dialogue ID.
                      "Defeat Queen Slime."));
                WeaponArchiveList.Add(new WeaponArchiveListing(
                     "Queen Slime Weapon", //Name of the archive listing.
                     $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] Adornment of the Chaotic God.", //Description of the listing.
                     player.ChaosWeaponDialogue == 2, //Unlock requirements.
                     163,
                     "Defeat Queen Slime, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Vagrant of Space and Time Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Izanagi's Edge " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Hawkmoon.", //Description of the listing.
                      player.VagrantWeaponDialogue == 2, //Unlock requirements.
                      115,
                      "Defeat the Vagrant of Space and Time.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Vagrant of Space and Time Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] Chronoclock. ", //Description of the listing.
                      player.ClockWeaponDialogue == 2, //Unlock requirements.
                      164,
                      "Defeat the Vagrant of Space and Time.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Any Mechanical Boss Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Veneration of Butterflies " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Memento Muse.", //Description of the listing.
                      player.MechBossWeaponDialogue == 2, //Unlock requirements.
                      106,
                      "Defeat any mechanical boss.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Any Mechanical Boss Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Ride the Bull " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Drachenlance.", //Description of the listing.
                      player.MechBossWeaponDialogue == 2, //Unlock requirements.
                      107,
                      "Defeat any mechanical boss, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                     "Any Mechanical Boss Weapon", //Name of the archive listing.
                     $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] Xenoblade. ", //Description of the listing.
                     player.MonadoWeaponDialogue == 2, //Unlock requirements.
                     125,
                     "Defeat any mechanical boss, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                     "Skeletron Prime Weapon", //Name of the archive listing.
                     $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] Armaments of the Sky Striker. ", //Description of the listing.
                     player.SkyStrikerWeaponDialogue == 2, //Unlock requirements.
                     135,
                     "Defeat Skeletron Prime.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                     "All Mechanical Bosses Weapon", //Name of the archive listing.
                     $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] Hullwrought. ", //Description of the listing.
                     player.HullwroughtWeaponDialogue == 2, //Unlock requirements.
                     121,
                     "Defeat all the mechanical bosses.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                   "All Mechanical Bosses Weapon", //Name of the archive listing.
                   $"Grants the Essence for " +
                   $"[i:{ItemType<Spatial>()}] El Capitan's Hardware.", //Description of the listing.
                   player.HardwareWeaponDialogue == 2, //Unlock requirements.
                   154,
                   "Defeat all the mechanical bosses, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Nalhaun Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Phantom in the Mirror " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Hollowheart Albion.", //Description of the listing.
                      player.NalhaunWeaponDialogue == 2, //Unlock requirements.
                      117,
                      "Defeat Nalhaun, the Burnished King.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Plantera Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Crimson Outbreak " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Voice of the Fallen.", //Description of the listing.
                      player.PlanteraWeaponDialogue == 2, //Unlock requirements.
                      108,
                      "Defeat Plantera.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Plantera Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Kifrosse. ", //Description of the listing.
                    player.KifrosseWeaponDialogue == 2, //Unlock requirements.
                    129,
                    "Defeat Plantera, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Frost Queen Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Stygian Nymph " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Caesura of Despair.", //Description of the listing.
                      player.FrostMoonWeaponDialogue == 2, //Unlock requirements.
                      126,
                      "Defeat the Frost Queen.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Penthesilea Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Vision of Euthymia " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Kroniic Principality.", //Description of the listing.
                      player.PenthesileaWeaponDialogue == 2, //Unlock requirements.
                      118,
                      "Defeat Penthesilea, the Witch of Ink.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Penthesilea Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Penthesilea's Muse. ", //Description of the listing.
                    player.MuseWeaponDialogue == 2, //Unlock requirements.
                    128,
                    "Defeat Penthesilea, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Golem Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Plenilune Gaze " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Tartaglia.", //Description of the listing.
                      player.GolemWeaponDialogue == 2, //Unlock requirements.
                      109,
                      "Defeat Golem.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Golem Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Genocide. ", //Description of the listing.
                    player.GenocideWeaponDialogue == 2, //Unlock requirements.
                    132,
                    "Defeat Golem, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Golem Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Gloves of the Black Silence. ", //Description of the listing.
                    player.SilenceWeaponDialogue == 2, //Unlock requirements.
                    156,
                    "Defeat Golem, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Martian Madness Weapon", //Name of the archive listing.
                      $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] The Kiss of Death. ", //Description of the listing.
                      player.GoldlewisWeaponDialogue == 2, //Unlock requirements.
                      165,
                      "Defeat the martian invaders.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Arbitration Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Liberation Blazing " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Unforgotten.", //Description of the listing.
                      player.ArbitrationWeaponDialogue == 2, //Unlock requirements.
                      119,
                      "Defeat Arbitration.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Arbitration Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Claimh Solais. ", //Description of the listing.
                    player.ClaimhWeaponDialogue == 2, //Unlock requirements.
                    127,
                    "Defeat Arbitration, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Duke Fishron Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Key of the Sinner " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Crimson Sakura Alpha.", //Description of the listing.
                      player.DukeFishronWeaponDialogue == 2, //Unlock requirements.
                      116,
                      "Defeat Duke Fishron.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Lunatic Cultist Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Rex Lapis " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Yunlai Stiletto.", //Description of the listing.
                      player.LunaticCultistWeaponDialogue == 2, //Unlock requirements.
                      110,
                      "Defeat Lunatic Cultist.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Lunatic Cultist Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Twin Stars of Albiero. ", //Description of the listing.
                    player.TwinStarsWeaponDialogue == 2, //Unlock requirements.
                    134,
                    "Defeat Lunatic Cultist, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Lunatic Cultist Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Catalyst's Memory. ", //Description of the listing.
                    player.CatalystWeaponDialogue == 2, //Unlock requirements.
                    155,
                    "Defeat Lunatic Cultist, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Lunatic Cultist Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Umbra. ", //Description of the listing.
                    player.UmbraWeaponDialogue == 2, //Unlock requirements.
                    161,
                    "Defeat Lunatic Cultist, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Moon Lord Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Suistrume " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Naganadel.", //Description of the listing.
                      player.MoonLordWeaponDialogue == 2, //Unlock requirements.
                      111,
                      "Defeat Moon Lord.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Moon Lord Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Shadowless Cerulean. ", //Description of the listing.
                    player.ShadowlessWeaponDialogue == 2, //Unlock requirements.
                    122,
                    "Defeat Moon Lord, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Moon Lord Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Soul Reaver. ", //Description of the listing.
                    player.SoulWeaponDialogue == 2, //Unlock requirements.
                    157,
                    "Defeat Moon Lord, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                     "Moon Lord Weapon", //Name of the archive listing.
                     $"Grants the Essence for " +
                     $"[i:{ItemType<Spatial>()}] Virtue's Edge. ", //Description of the listing.
                     player.VirtueWeaponDialogue == 2, //Unlock requirements.
                     150,
                     "Defeat Moon Lord, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                      "Warrior of Light Weapon", //Name of the archive listing.
                      $"Grants the Essence for either " +
                      $"[i:{ItemType<Astral>()}] Key of the King's Law " +
                      $"or " +
                      $"[i:{ItemType<Umbral>()}] Light Unrelenting.", //Description of the listing.
                      player.WarriorWeaponDialogue == 2, //Unlock requirements.
                      112,
                      "Defeat Warrior of Light.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Warrior of Light Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Vermilion Riposte. ", //Description of the listing.
                    player.RedMageWeaponDialogue == 2, //Unlock requirements.
                    151,
                    "Defeat the Warrior of Light, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                   "Warrior of Light Weapon", //Name of the archive listing.
                   $"Grants the Essence for " +
                   $"[i:{ItemType<Spatial>()}] Burning Desire. ", //Description of the listing.
                   player.BlazeWeaponDialogue == 2, //Unlock requirements.
                   152,
                   "Defeat the Warrior of Light, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                   "Warrior of Light Weapon", //Name of the archive listing.
                   $"Grants the Essence for " +
                   $"[i:{ItemType<Spatial>()}] The Everlasting Pickaxe. ", //Description of the listing.
                   player.PickaxeWeaponDialogue == 2, //Unlock requirements.
                   153,
                   "Defeat the Warrior of Light, then wait.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Tsukiyomi Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Architect's Luminance. ", //Description of the listing.
                    player.ArchitectWeaponDialogue == 2, //Unlock requirements.
                    130,
                    "Defeat ???")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Tsukiyomi Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Cosmic Destroyer. ", //Description of the listing.
                    player.CosmicDestroyerWeaponDialogue == 2, //Unlock requirements.
                    138,
                    "Defeat ???")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Empress of Light Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Arachnid Needlepoint. ", //Description of the listing.
                    player.NeedlepointWeaponDialogue == 2, //Unlock requirements.
                    140,
                    "Defeat the Empress of Light.")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Golem Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Mercy. ", //Description of the listing.
                    player.MercyWeaponDialogue == 2, //Unlock requirements.
                    141,
                    "Defeat Golem, then wait. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Empress of Light Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Sakura's Vengeance. ", //Description of the listing.
                    player.SakuraWeaponDialogue == 2, //Unlock requirements.
                    142,
                    "Defeat Empress of Light. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Empress of Light Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Eternal Star. ", //Description of the listing.
                    player.EternalWeaponDialogue == 2, //Unlock requirements.
                    143,
                    "Defeat Moon Lord, then wait. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Moon Lord Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Vermilion Daemon. ", //Description of the listing.
                    player.DaemonWeaponDialogue == 2, //Unlock requirements.
                    144,
                    "Defeat Moon Lord, then wait. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Lunatic Cultist Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Ozma Ascendant. ", //Description of the listing.
                    player.OzmaWeaponDialogue == 2, //Unlock requirements.
                    145,
                    "Defeat Lunatic Cultist, then wait. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Queen Slime Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] Dreadnought Chemtank. ", //Description of the listing.
                    player.UrgotWeaponDialogue == 2, //Unlock requirements.
                    146,
                    "Defeat Queen Slime, then wait. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Pumpkin King Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] The Blood Blade. ", //Description of the listing.
                    player.BloodWeaponDialogue == 2, //Unlock requirements.
                    147,
                    "Defeat Pumpking King, then wait. ")); //Corresponding dialogue ID.
                WeaponArchiveList.Add(new WeaponArchiveListing(
                    "Deerclops Weapon", //Name of the archive listing.
                    $"Grants the Essence for " +
                    $"[i:{ItemType<Spatial>()}] The Morning Star. ", //Description of the listing.
                    player.MorningStarWeaponDialogue == 2, //Unlock requirements.
                    148,
                    "Defeat Deerclops, then wait. ")); //Corresponding dialogue ID.
                                                       //Corresponding dialogue ID.

                //VN
                VNArchiveList.Add(new VNArchiveListing(
                      "", //Name of the archive listing.
                      $"", //Description of the listing.
                      false, //Unlock requirements.
                      0,
                      "")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Intro Dialogue", //Name of the archive listing.
                       $"The Starfarer's introduction dialogue.", //Description of the listing.
                       player.chosenStarfarer == 1, //Unlock requirements.
                       3,
                       "Asphodene's intro dialogue.")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Eridani's Intro Dialogue", //Name of the archive listing.
                       $"The Starfarer's introduction dialogue.", //Description of the listing.
                       player.chosenStarfarer == 2, //Unlock requirements.
                       6,
                       "Eridani's intro dialogue.")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Vagrant Post-Battle (Asphodene)", //Name of the archive listing.
                       $"Perseus's introduction.", //Description of the listing.
                       player.chosenStarfarer == 1 && DownedBossSystem.downedVagrant, //Unlock requirements.
                       9,
                       "Defeat the Vagrant of Space and Time. (Asphodene)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Vagrant Post-Battle (Eridani)", //Name of the archive listing.
                       $"Perseus's introduction.", //Description of the listing.
                       player.chosenStarfarer == 2 && DownedBossSystem.downedVagrant, //Unlock requirements.
                       10,
                       "Defeat the Vagrant of Space and Time. (Eridani)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "The Astrolabe (Asphodene)", //Name of the archive listing.
                       $"Acquisition of the Astrolabe.", //Description of the listing.
                       player.chosenStarfarer == 1 && player.astrolabeIntroDialogue == 2, //Unlock requirements.
                       11,
                       "Defeat the Eye of Cthulhu.")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "The Astrolabe (Eridani)", //Name of the archive listing.
                       $"Acquisition of the Astrolabe.", //Description of the listing.
                       player.chosenStarfarer == 2 && player.astrolabeIntroDialogue == 2, //Unlock requirements.
                       12,
                       "Defeat the Eye of Cthulhu.")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "The Observatory's Introduction", //Name of the archive listing.
                       $"Explaining Cosmic Voyages and the Astrolabe.", //Description of the listing.
                       player.observatoryIntroDialogue == 2, //Unlock requirements.
                       13,
                       "Visit the Observatory.")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Yojimbo's Introduction (Asphodene)", //Name of the archive listing.
                       $"Yojimbo, the lumenkin bounty hunter, makes his appearance.", //Description of the listing.
                       player.yojimboIntroDialogue == 2, //Unlock requirements.
                       19,
                       "Meet Yojimbo during a Cosmic Voyage. (Asphodene)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Yojimbo's Introduction (Eridani)", //Name of the archive listing.
                       $"Yojimbo, the lumenkin bounty hunter, makes his appearance.", //Description of the listing.
                       player.yojimboIntroDialogue == 2, //Unlock requirements.
                       20,
                       "Meet Yojimbo during a Cosmic Voyage. (Eridani)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Yojimbo: About the galaxy...", //Name of the archive listing.
                       $"Yojimbo's comments on the state of the galaxy.", //Description of the listing.
                       false,//yojimboIntroDialogue == 2, //Unlock requirements.
                       00,
                       "Talk to Yojimbo during a Cosmic Voyage. (Random unlock)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                      "Yojimbo: About the Empire...", //Name of the archive listing.
                      $"Yojimbo's comments on the state of the galaxy.", //Description of the listing.
                      false,//yojimboIntroDialogue == 2, //Unlock requirements.
                      00,
                      "Talk to Yojimbo during a Cosmic Voyage. (Random unlock)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                      "Yojimbo: About the Ardor...", //Name of the archive listing.
                      $"Yojimbo's comments on the state of the galaxy.", //Description of the listing.
                      false,//yojimboIntroDialogue == 2, //Unlock requirements.
                      00,
                      "Talk to Yojimbo during a Cosmic Voyage. (Random unlock)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Garridine's Introduction (Asphodene)", //Name of the archive listing.
                       $"Garridine, the lupine machinist, makes her appearance.", //Description of the listing.
                       player.garridineIntroDialogue == 2, //Unlock requirements.
                       00,
                       "Meet Garridine during a Cosmic Voyage. (Asphodene)")); //Corresponding dialogue ID.
                VNArchiveList.Add(new VNArchiveListing(
                       "Garridine's Introduction (Eridani)", //Name of the archive listing.
                       $"Garridine, the lupine machinist, makes her appearance.", //Description of the listing.
                       player.garridineIntroDialogue == 2, //Unlock requirements.
                       00,
                       "Meet Garridine during a Cosmic Voyage. (Eridani)")); //Corresponding dialogue ID.
                IdleArchiveListMax = IdleArchiveList.Count;
                BossArchiveListMax = BossArchiveList.Count;
                WeaponArchiveListMax = WeaponArchiveList.Count;
                VNArchiveListMax = VNArchiveList.Count;
                archivePopulated = true;
            }

            archiveListInfo = "";
            if (archiveChosenList == 0)//0 is idle, 1 is boss, 2 is weapon (prompts use the OnEvent system.)
            {
                archiveListMax = IdleArchiveListMax - 1;//Begins at 2, which is fallback dialogue. 24
            }
            if (archiveChosenList == 1)//0 is idle, 1 is boss, 2 is weapon, (prompts use the OnEvent system.)
            {
                archiveListMax = BossArchiveListMax - 1;//23
            }
            if (archiveChosenList == 2)//0 is idle, 1 is boss, 2 is weapon, (prompts use the OnEvent system.)
            {
                archiveListMax = WeaponArchiveListMax - 1;//46
            }
            if (archiveChosenList == 3)//This will be the VN style dialogue archive. It doesn't have to be implemented on 1.1, as nothing is locked behind it except dialogue.
            {
                archiveListMax = VNArchiveListMax - 1;//
            }


            //TODO: Replace the ArchiveChosenList == system with automatically selecting the correct information from the Idle Archive Listing.
            //Also, text LangHelper.Wrap the description.
            if (archiveActive && archivePopulated)
            {
                if (archiveChosenList == 0)
                {
                    if (IdleArchiveList[archiveListNumber].IsViewable)
                    {
                        canViewArchive = true;
                        archiveListInfo = LangHelper.Wrap("" + IdleArchiveList[archiveListNumber].Name + ":" + "\n" + IdleArchiveList[archiveListNumber].ListInformation, 25);

                    }
                    else
                    {
                        canViewArchive = false;
                        archiveListInfo = LangHelper.Wrap(IdleArchiveList[archiveListNumber].UnlockConditions, 25);
                    }

                }
                if (archiveChosenList == 1)
                {
                    if (BossArchiveList[archiveListNumber].IsViewable)
                    {
                        canViewArchive = true;
                        archiveListInfo = LangHelper.Wrap("" + BossArchiveList[archiveListNumber].Name + ":" + "\n" + BossArchiveList[archiveListNumber].ListInformation, 25);

                    }
                    else
                    {
                        canViewArchive = false;
                        archiveListInfo = LangHelper.Wrap(BossArchiveList[archiveListNumber].UnlockConditions, 25);
                    }

                }
                if (archiveChosenList == 2)
                {
                    if (WeaponArchiveList[archiveListNumber].IsViewable)
                    {
                        canViewArchive = true;
                        archiveListInfo = LangHelper.Wrap("" + WeaponArchiveList[archiveListNumber].Name + ":" + "\n" + WeaponArchiveList[archiveListNumber].ListInformation, 25);

                    }
                    else
                    {
                        canViewArchive = false;
                        archiveListInfo = LangHelper.Wrap(WeaponArchiveList[archiveListNumber].UnlockConditions, 25);
                    }

                }
                if (archiveChosenList == 3)
                {
                    if (VNArchiveList[archiveListNumber].IsViewable)
                    {
                        canViewArchive = true;
                        archiveListInfo = LangHelper.Wrap("" + VNArchiveList[archiveListNumber].Name + ":" + "\n" + VNArchiveList[archiveListNumber].ListInformation, 25);

                    }
                    else
                    {
                        canViewArchive = false;
                        archiveListInfo = LangHelper.Wrap(VNArchiveList[archiveListNumber].UnlockConditions, 25);
                    }

                }

            }








            #endregion
        }
        public override void PostUpdate()
        {

            
           
        }



        public override void ResetEffects()
        {
           
            
        }

    }

};