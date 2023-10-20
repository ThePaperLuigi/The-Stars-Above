
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
using StarsAbove.Items.Memories.TarotCard;
using StarsAbove.Buffs.Memories;
using StarsAbove.Projectiles.Memories;
using System;

namespace StarsAbove.Systems.Items
{
    public class ItemMemorySystem : GlobalItem
    {
        public List<string> EquippedMemories = new List<string>()//Unused
        {
            "1","2","3"
        };
        public int memoryCount;

        public int itemMemorySlot1;
        public int itemMemorySlot2;
        public int itemMemorySlot3;

        public bool isMemory = false;

        public bool ChoiceGlasses;//1
        public bool RedSpiderLily;//2

        public bool AetherBarrel;//3
        public bool NookMiles;//4
        public bool CapeFeather;//5
        public bool RawMeat;//6
        public bool PhantomMask;//7
        public bool NetheriteIngot;//8
        public bool EnderPearl;//9
        public bool Shard;//10
        public bool PowerMoon;//11
        public bool YoumuHilt;//12
        public bool Rageblade;//13
        public bool ElectricGuitarPick;//14
        public bool DekuNut;//15
        public bool MatterManipulator;//16
        public bool BottledChaos;//17
        public bool Trumpet;//18
        public bool GuppyHead;//19
        public bool Pawn;//20
        public bool ReprintedBlueprint;//21
        public bool ResonanceGem;//22
        public bool RuinedCrown;//23
        public bool DescenderGemstone;//24
        public bool MonsterNail;//25
        public bool MindflayerWorm;//26
        public bool MercenaryAuracite;//27
        public bool ChronalDecelerator;//28
        public bool SimulacraShifter;//29
        public bool BlackLightbulb;//30
        public bool SigilOfHope;//31
        public bool JackalMask;//32
        public bool KnightsShovelhead;//33

        //Each weapon has a random tarot card effect assigned.
        public bool TarotCard;//100
        public int tarotCardType = -1;

        //Garridine's Protocores
        public bool ProtocoreMonoclaw;//201
        public bool ProtocoreManacoil;//202
        public bool ProtocoreShockrod;//203

        //Sigils
        public bool RangedSigil;//301
        public bool MagicSigil;//302
        public bool MeleeSigil;//303
        public bool SummonSigil;//304

        //Aeonseals
        public bool AeonsealDestruction;//401
        public bool AeonsealHunt;//402
        public bool AeonsealErudition;//403
        public bool AeonsealHarmony;//404
        public bool AeonsealNihility;//405
        public bool AeonsealPreservation;//406
        public bool AeonsealAbundance;//407

        public bool AeonsealTrailblazer;//408

        public int OldHP;
        public int HeldWeaponTypeChoice;

        //Increase damage of effects
        public float damageModAdditive;
        public float damageModMultiplicative;

        //Support mods (increase defense given, increase speed given, etc)
        public float supportModAdditive;
        public float supportModMultiplicative;

        //Cooldown related mods
        public float cooldownReductionMod;

        //Affects everything except cooldowns (damage, support) very OP
        public float memoryGlobalMod;

        StarsAboveGlobalItem globalItem = new StarsAboveGlobalItem();
        public override bool InstancePerEntity => true;     
        public override void SetDefaults(Item entity)
        {
            
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["M1"] = itemMemorySlot1;
            tag["M2"] = itemMemorySlot2;
            tag["M3"] = itemMemorySlot3;
            tag["TarotEffect"] = tarotCardType;

            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            itemMemorySlot1 = tag.GetInt("M1");
            itemMemorySlot2 = tag.GetInt("M2");
            itemMemorySlot3 = tag.GetInt("M3");
            tarotCardType = tag.GetInt("TarotEffect");

            base.LoadData(item, tag);
        }
        int check;
        string memoryTooltip;
        string memoryTooltipInfo;
        public override bool CanUseItem(Item item, Player player)
        {
            if(player.HasBuff(BuffType<ChoiceGlassesLock>()))
            {
                if(item.type != HeldWeaponTypeChoice)
                {
                    return false;
                }
            }

            return base.CanUseItem(item, player);
        }
        public override bool? UseItem(Item item, Player player)
        {
            if(ChoiceGlasses && !player.HasBuff(BuffType<ChoiceGlassesLock>()))
            {
                player.AddBuff(BuffType<ChoiceGlassesLock>(), 60 * 60);
                HeldWeaponTypeChoice = item.type;
            }


            return base.UseItem(item, player);
        }
        public override void OnConsumeItem(Item item, Player player)
        {
            if(item.healLife > 0)
            {
                player.GetModPlayer<ItemMemorySystemPlayer>().powderCharges = 2;
            }


            base.OnConsumeItem(item, player);
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if ((globalItem.AstralWeapons.Contains(item.type) || globalItem.UmbralWeapons.Contains(item.type) || globalItem.SpatialWeapons.Contains(item.type)) && tarotCardType == -1)
            {
                //0 fool, 2 magician, 3 priestess, 4 empress, 5 emperor, 6 heirophant, 7 lovers, 8 chariot, 9 justice,
                //10 hermit, 11 fortune, 12 strength, 13 hanged man, 14 death, 15 temperance, 16 devil, 17 tower, 18 star, 19 moon, 20 sun
                tarotCardType = Main.rand.Next(0, 21);
            }
            ResetMemories(player);
            if (itemMemorySlot1 != 0)
            {
                CheckMemories(item, itemMemorySlot1, player);
            }
            if (itemMemorySlot2 != 0)
            {
                CheckMemories(item, itemMemorySlot2, player);
            }
            if (itemMemorySlot3 != 0)
            {
                CheckMemories(item, itemMemorySlot3, player);
            }

            //All effects are processed at the end just in case some weapons have 'buff other effect' effects
            BuffMemories(player);
            Memories(player);
        }
        public override void HoldItem(Item item, Player player)
        {
            
            base.HoldItem(item, player);
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (CapeFeather && player.velocity.Y != 0)
            {
                damage += (0.1f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if(player.HasBuff(BuffType<ChoiceGlassesLock>()))
            {
                damage += (0.18f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if(TarotCard)
            {
                switch (tarotCardType)
                {
                    case 0:
                        damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                        if(player.statLife <= player.statLifeMax2/2 || player.HasBuff(BuffType<FateDivined>()))
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                        }
                        break;
                    case 1:
                        if(player.statMana >= player.statManaMax2)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 2:
                        if (player.ShoppingZone_AnyBiome)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 3:
                        if (!player.Male)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 4:
                        if (player.Male)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 5:
                        if (player.statLife == player.statLifeMax2)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 6:
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 7:
                        if (player.velocity.X >= 30 || player.velocity.X <= 30)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 8:
                        if (Main.invasionProgress > 0)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 9:
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 10:
                        if (Main.rand.Next(0,2) > 1)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 11:
                        if (item.knockBack > 6.1)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 12:
                        if (player.statLife < (player.statLifeMax2*0.75))
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 13:
                        damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                        if (player.statLife <= player.statLifeMax2 / 2)
                        {
                            damage -= (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                        }
                        break;
                    case 14:
                        if (player.HasBuff(BuffID.WellFed) || player.HasBuff(BuffID.WellFed2) || player.HasBuff(BuffID.WellFed3))
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 15:
                        if (player.ZoneUnderworldHeight)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 16:
                        if (player.ZoneDungeon)
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 17:
                        if (player.ZoneNormalSpace || SubworldSystem.AnyActive())
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 18:
                        if (!Main.IsItDay())
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                    case 19:
                        if (Main.IsItDay())
                        {
                            damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            if (player.statLife <= player.statLifeMax2 / 2 || player.HasBuff(BuffType<FateDivined>()))
                            {
                                damage += (0.08f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                            }
                        }
                        break;
                }
            }


            base.ModifyWeaponDamage(item, player, ref damage);
        }
        private void ResetMemories(Player player)
        {
            memoryTooltip = "";
            memoryTooltipInfo = "";
            memoryCount = 0;

            //Increase damage of effects
            damageModAdditive = 0;
            damageModMultiplicative = 1;

            //Support mods (increase defense given, increase speed given, etc)
            supportModAdditive = 0;
            supportModMultiplicative = 1;

            //Cooldown related mods
            cooldownReductionMod = 0;

            //Affects everything except cooldowns (damage, support) very OP, remember it multiplies
            memoryGlobalMod = 1;

            if(!player.HasBuff(BuffType<ChoiceGlassesLock>()))
            {
                HeldWeaponTypeChoice = 0;
            }
            ChoiceGlasses = false;//1
            RedSpiderLily = false;//2
            AetherBarrel = false;//3
            NookMiles = false;//4
            CapeFeather = false;//5
            RawMeat = false;//6
            PhantomMask = false;//7
            NetheriteIngot = false;//8
            EnderPearl = false;//9
            Shard = false;//10
            PowerMoon = false;//11
            YoumuHilt = false;//12
            Rageblade = false;//13
            ElectricGuitarPick = false;//14
            DekuNut = false;//15
            MatterManipulator = false;//16
            BottledChaos = false;//17
            Trumpet = false;//18
            GuppyHead = false;//19
            Pawn = false;//20
            ReprintedBlueprint = false;//21
            ResonanceGem = false;//22
            RuinedCrown = false;//23
            DescenderGemstone = false;//24
            MonsterNail = false;//25
            MindflayerWorm = false;//26
            MercenaryAuracite = false;//27
            ChronalDecelerator = false;//28
            SimulacraShifter = false;//29
            BlackLightbulb = false;//30
            SigilOfHope = false;//31
            JackalMask = false;//32
            KnightsShovelhead = false;//33

            //Each weapon has a random tarot card effect assigned.
            TarotCard = false;//100

            //Garridine's Protocores
            ProtocoreMonoclaw = false;//201
            ProtocoreManacoil = false;//202
            ProtocoreShockrod = false;//203

            //Sigils
            RangedSigil = false;//301
            MagicSigil = false;//302
            MeleeSigil = false;//303
            SummonSigil = false;//304

            //Aeonseals
            AeonsealDestruction = false;//401
            AeonsealHunt = false;//402
            AeonsealErudition = false;//403
            AeonsealHarmony = false;//404
            AeonsealNihility = false;//405
            AeonsealPreservation = false;//406
            AeonsealAbundance = false;//407

            AeonsealTrailblazer = false;//408
        }

        //Memories which provide buffs to other memories are calculated first.
        private void BuffMemories(Player player)
        {
            if(SigilOfHope)
            {
                if(player.statLife <= player.statLifeMax2/2)
                {
                    memoryGlobalMod += 0.3f;//30% increase to memory effects (remember to multiply)
                }
            }
            if(ResonanceGem)
            {
                //
                cooldownReductionMod += 0.25f * memoryGlobalMod;
            }
            if(NookMiles)
            {
                supportModAdditive += 0.3f;
            }
        }
        //All other memories.
        private void Memories(Player player)
        {
            if(ChoiceGlasses)
            {

            }
            if (CapeFeather)
            {

            }
            if(KnightsShovelhead)
            {
                
            }
        }
        private void CheckMemories(Item item, int slot, Player player)
        {
            check = 1;//Choice Glasses
            if (slot == check)
            {
                ChoiceGlasses = true;
                if (player.HeldItem == item)
                    player.GetModPlayer<ItemMemorySystemPlayer>().ChoiceGlasses = true;
                string itemIcon = $"[i:{ItemType<ChoiceGlasses>()}]";
                string itemName = "ChoiceGlasses";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 2;//RedSpiderLily
            if (slot == check)
            {
                    RedSpiderLily = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().RedSpiderLily = true;
                string itemIcon = $"[i:{ItemType<RedSpiderLily>()}]";
                string itemName = "RedSpiderLily";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 3;//AetherBarrel
            if (slot == check)
            {
                    AetherBarrel = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().AetherBarrel = true;
                string itemIcon = $"[i:{ItemType<AetherBarrel>()}]";
                string itemName = "AetherBarrel";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 4;//NookMiles
            if (slot == check)
            {
                    NookMiles = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().NookMiles = true;
                string itemIcon = $"[i:{ItemType<NookMilesTicket>()}]";
                string itemName = "NookMilesTicket";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 5;//Caped Feather ID
            if (slot == check)
            {
                    CapeFeather = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().CapeFeather = true;
                string itemIcon = $"[i:{ItemType<CapedFeather>()}]";
                string itemName = "CapedFeather";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 22;//Resonance Gem
            if (slot == check)
            {
                    ResonanceGem = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().ResonanceGem = true;
                string itemIcon = $"[i:{ItemType<ResonanceGem>()}]";
                string itemName = "ResonanceGem";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 31;//Sigil of Hope ID
            if (slot == check)
            {
                    SigilOfHope = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().SigilOfHope = true;
                string itemIcon = $"[i:{ItemType<SigilOfHope>()}]";
                string itemName = "SigilOfHope";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 33;//Knight's Shovelhead
            if (slot == check)
            {
                    KnightsShovelhead = true;
                if (player.HeldItem == item)

                    player.GetModPlayer<ItemMemorySystemPlayer>().KnightsShovelhead = true;
                string itemIcon = $"[i:{ItemType<KnightsShovelhead>()}]";
                string itemName = "KnightsShovelhead";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached");

                }
                memoryCount++;
            }
            check = 100;//TarotCard
            if (slot == check)
            {
                    TarotCard = true;
                if (player.HeldItem == item)
                    player.GetModPlayer<ItemMemorySystemPlayer>().TarotCard = true;
                string itemIcon = $"[i:{ItemType<TarotCard>()}]";
                string itemName = "TarotCard";
                string cardName = "";
                //1 fool, 2 magician, 3 priestess, 4 empress, 5 emperor, 6 heirophant, 7 lovers, 8 chariot, 9 justice,
                //10 hermit, 11 fortune, 12 strength, 13 hanged man, 14 death, 15 temperance, 16 devil, 17 tower, 18 star, 19 moon, 20 sun
                switch (tarotCardType)
                {
                    case 0:
                        cardName = "Fool";
                        break;
                    case 1:
                        cardName = "Magician";
                        break;
                    case 2:
                        cardName = "Priestess";
                        break;
                    case 3:
                        cardName = "Empress";
                        break;
                    case 4:
                        cardName = "Emperor";
                        break;
                    case 5:
                        cardName = "Heirophant";
                        break;
                    case 6:
                        cardName = "Lovers";
                        break;
                    case 7:
                        cardName = "Chariot";
                        break;
                    case 8:
                        cardName = "Justice";
                        break;
                    case 9:
                        cardName = "Hermit";
                        break;
                    case 10:
                        cardName = "Fortune";
                        break;
                    case 11:
                        cardName = "Strength";
                        break;
                    case 12:
                        cardName = "HangedMan";
                        break;
                    case 13:
                        cardName = "Death";
                        break;
                    case 14:
                        cardName = "Temperance";
                        break;
                    case 15:
                        cardName = "Devil";
                        break;
                    case 16:
                        cardName = "Tower";
                        break;
                    case 17:
                        cardName = "Star";
                        break;
                    case 18:
                        cardName = "Moon";
                        break;
                    case 19:
                        cardName = "Sun";
                        break;
                }
                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached." + cardName);

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items." + $"{itemName}" + ".TooltipAttached." + cardName);

                }
                memoryCount++;
            }
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
                
                if (StarsAbove.showMemoryInfoKey.Old && item.damage > 0)
                {
                    foreach (var l in tooltips)
                    {
                        if (l.Name.StartsWith("Tooltip"))
                        {
                            l.Hide();

                        }
                    }
                    if (memoryCount <= 0)
                    {
                        memoryTooltipInfo = LangHelper.GetTextValue($"UIElements.Cosmoturgy.NoMemoriesInWeapon");

                    }
                    TooltipLine tooltipMemoryInfo = new TooltipLine(Mod, "StarsAbove: MemoryInfo", memoryTooltipInfo) { OverrideColor = Color.White };
                    tooltips.Add(tooltipMemoryInfo);

                }
                else
                {
                    //Add in the icons of the Memories.
                    if (memoryCount > 0)
                    {
                        tooltipAddition += " : " + memoryTooltip;

                    }
                }

                TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: AspectIdentifier", tooltipAddition) { OverrideColor = Color.White };
                tooltips.Add(tooltip);
            }
        }
    }

    public class ItemMemorySystemPlayer : ModPlayer
    {
        public bool ChoiceGlasses;//1
        public bool RedSpiderLily;//2
        public bool AetherBarrel;//3
        public bool NookMiles;//4
        public bool CapeFeather;//5
        public bool RawMeat;//6
        public bool PhantomMask;//7
        public bool NetheriteIngot;//8
        public bool EnderPearl;//9
        public bool Shard;//10
        public bool PowerMoon;//11
        public bool YoumuHilt;//12
        public bool Rageblade;//13
        public bool ElectricGuitarPick;//14
        public bool DekuNut;//15
        public bool MatterManipulator;//16
        public bool BottledChaos;//17
        public bool Trumpet;//18
        public bool GuppyHead;//19
        public bool Pawn;//20
        public bool ReprintedBlueprint;//21
        public bool ResonanceGem;//22
        public bool RuinedCrown;//23
        public bool DescenderGemstone;//24
        public bool MonsterNail;//25
        public bool MindflayerWorm;//26
        public bool MercenaryAuracite;//27
        public bool ChronalDecelerator;//28
        public bool SimulacraShifter;//29
        public bool BlackLightbulb;//30
        public bool SigilOfHope;//31
        public bool JackalMask;//32
        public bool KnightsShovelhead;//33

        //Each weapon has a random tarot card effect assigned.
        public bool TarotCard;//100
        public int tarotCardType = 0;

        //Garridine's Protocores
        public bool ProtocoreMonoclaw;//201
        public bool ProtocoreManacoil;//202
        public bool ProtocoreShockrod;//203

        //Sigils
        public bool RangedSigil;//301
        public bool MagicSigil;//302
        public bool MeleeSigil;//303
        public bool SummonSigil;//304

        //Aeonseals
        public bool AeonsealDestruction;//401
        public bool AeonsealHunt;//402
        public bool AeonsealErudition;//403
        public bool AeonsealHarmony;//404
        public bool AeonsealNihility;//405
        public bool AeonsealPreservation;//406
        public bool AeonsealAbundance;//407

        public bool AeonsealTrailblazer;//408

        public int powderCharges = 0;
        public int oldHP;
        public int accursedEdgeStacks = 0;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(KnightsShovelhead)
            {
                Player.velocity.Y -= 10f;
            }
            if(hit.Crit && powderCharges > 0)
            {
                if(Main.myPlayer == Player.whoAmI)
                {
                    Projectile.NewProjectile(Player.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileType<PowderChargeExplosion>(), damageDone, 0, Player.whoAmI);

                }
                powderCharges--;
            }
            if (Player.HasBuff(BuffType<AccursedEdge>()))
            {
                float dustAmount = 30f;
                float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(18f, 1f);
                    spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                    int dust = Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain);
                    Main.dust[dust].scale = 2.5f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = target.Center + spinningpoint5;
                    Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 16f;
                }
                Player.Heal((int)(Player.statLifeMax2 * 0.2f));
                accursedEdgeStacks = 0;
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if(Player.HasBuff(BuffType<AccursedEdge>()))
            {
                modifiers.FinalDamage += 0.4f;
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void PostUpdate()
        {
            if(RedSpiderLily)
            {
                if(Player.statLife < oldHP && !Player.HasBuff(BuffType<AccursedEdgeCooldown>()))
                {
                    Player.AddBuff(BuffType<AccursedEdgeCooldown>(), 120);
                    accursedEdgeStacks++;
                    float dustAmount = 10f;
                    float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(18f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain);
                        Main.dust[dust].scale = 1.8f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
            }
            oldHP = Player.statLife;

            base.PostUpdate();
        }
        public override void PreUpdateBuffs()
        {
            if(powderCharges > 0)
            {
                Player.AddBuff(BuffType<PowderCharge>(), 10);
            }
            if (accursedEdgeStacks >= 5)
            {
                Player.AddBuff(BuffType<AccursedEdge>(), 10);
            }
            base.PreUpdateBuffs();
        }
        
        public override void ResetEffects()
        {
            ChoiceGlasses = false;//1
            RedSpiderLily = false;//2
            AetherBarrel = false;//3
            NookMiles = false;//4
            CapeFeather = false;//5
            RawMeat = false;//6
            PhantomMask = false;//7
            NetheriteIngot = false;//8
            EnderPearl = false;//9
            Shard = false;//10
            PowerMoon = false;//11
            YoumuHilt = false;//12
            Rageblade = false;//13
            ElectricGuitarPick = false;//14
            DekuNut = false;//15
            MatterManipulator = false;//16
            BottledChaos = false;//17
            Trumpet = false;//18
            GuppyHead = false;//19
            Pawn = false;//20
            ReprintedBlueprint = false;//21
            ResonanceGem = false;//22
            RuinedCrown = false;//23
            DescenderGemstone = false;//24
            MonsterNail = false;//25
            MindflayerWorm = false;//26
            MercenaryAuracite = false;//27
            ChronalDecelerator = false;//28
            SimulacraShifter = false;//29
            BlackLightbulb = false;//30
            SigilOfHope = false;//31
            JackalMask = false;//32
            KnightsShovelhead = false;//33

            //Each weapon has a random tarot card effect assigned.
            TarotCard = false;//100

            //Garridine's Protocores
            ProtocoreMonoclaw = false;//201
            ProtocoreManacoil = false;//202
            ProtocoreShockrod = false;//203

            //Sigils
            RangedSigil = false;//301
            MagicSigil = false;//302
            MeleeSigil = false;//303
            SummonSigil = false;//304

            //Aeonseals
            AeonsealDestruction = false;//401
            AeonsealHunt = false;//402
            AeonsealErudition = false;//403
            AeonsealHarmony = false;//404
            AeonsealNihility = false;//405
            AeonsealPreservation = false;//406
            AeonsealAbundance = false;//407

            AeonsealTrailblazer = false;//408
            base.ResetEffects();
        }
    }
}