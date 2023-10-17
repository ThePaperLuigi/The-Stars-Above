
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
            if (globalItem.AstralWeapons.Contains(entity.type) || globalItem.UmbralWeapons.Contains(entity.type) || globalItem.SpatialWeapons.Contains(entity.type))
            {
                //1 fool, 2 magician, 3 priestess, 4 empress, 5 emperor, 6 heirophant, 7 lovers, 8 chariot,
                tarotCardType = Main.rand.Next(0, 21);

            }
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["M1"] = itemMemorySlot1;
            tag["M2"] = itemMemorySlot2;
            tag["M3"] = itemMemorySlot3;

            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            itemMemorySlot1 = tag.GetInt("M1");
            itemMemorySlot2 = tag.GetInt("M2");
            itemMemorySlot3 = tag.GetInt("M3");

            base.LoadData(item, tag);
        }
        int check;
        string memoryTooltip;
        string memoryTooltipInfo;

        public override void UpdateInventory(Item item, Player player)
        {
            ResetMemories();
            if (itemMemorySlot1 != 0)
            {
                CheckMemories(itemMemorySlot1, player);
            }
            if (itemMemorySlot2 != 0)
            {
                CheckMemories(itemMemorySlot2, player);
            }
            if (itemMemorySlot3 != 0)
            {
                CheckMemories(itemMemorySlot3, player);
            }

            //All effects are processed at the end just in case some weapons have 'buff other effect' effects
            BuffMemories(player);
            Memories(player);
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (CapeFeather && player.velocity.Y != 0)
            {
                damage += 0.1f + damageModAdditive * damageModMultiplicative * memoryGlobalMod;
            }



            base.ModifyWeaponDamage(item, player, ref damage);
        }
        private void ResetMemories()
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

            SigilOfHope = false;
        }

        //Memories which provide buffs to other memories are calculated first.
        private void BuffMemories(Player player)
        {
            if(SigilOfHope)
            {
                if(player.statLife <= player.statLifeMax2/2)
                {
                    memoryGlobalMod += 0.1f;//10% increase to memory effects (remember to multiply)
                }
            }
            if(ResonanceGem)
            {
                //
                cooldownReductionMod += 0.25f * memoryGlobalMod;
            }
        }
        //All other memories.
        private void Memories(Player player)
        {
            if(CapeFeather)
            {

            }
            if(KnightsShovelhead)
            {
                
            }
        }
        private void CheckMemories(int slot, Player player)
        {
            check = 5;//Caped Feather ID
            if (slot == check)
            {
                CapeFeather = true;
                player.GetModPlayer<ItemMemorySystemPlayer>().CapeFeather = true;
                string itemIcon = $"[i:{ItemType<CapedFeather>()}]";
                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items.CapedFeather.TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items.CapedFeather.TooltipAttached");

                }
                memoryCount++;
            }
            check = 22;//Resonance Gem
            if (slot == check)
            {
                ResonanceGem = true;
                player.GetModPlayer<ItemMemorySystemPlayer>().ResonanceGem = true;
                string itemIcon = $"[i:{ItemType<ResonanceGem>()}]";
                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items.ResonanceGem.TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items.ResonanceGem.TooltipAttached");

                }
                memoryCount++;
            }
            check = 31;//Sigil of Hope ID
            if (slot == check)
            {
                SigilOfHope = true;
                player.GetModPlayer<ItemMemorySystemPlayer>().SigilOfHope = true;
                string itemIcon = $"[i:{ItemType<SigilOfHope>()}]";
                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items.SigilOfHope.TooltipAttached");

                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items.SigilOfHope.TooltipAttached");

                }
                memoryCount++;
            }
            check = 33;//Knight's Shovelhead
            if (slot == check)
            {
                KnightsShovelhead = true;
                player.GetModPlayer<ItemMemorySystemPlayer>().KnightsShovelhead = true;
                string itemIcon = $"[i:{ItemType<KnightsShovelhead>()}]";
                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items.KnightsShovelhead.TooltipAttached");
                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items.KnightsShovelhead.TooltipAttached");
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
                
                if (StarsAbove.showMemoryInfoKey.Old)
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(KnightsShovelhead)
            {
                Player.velocity.Y -= 10f;
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            
            base.ModifyHitNPC(target, ref modifiers);
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
            tarotCardType = 0;

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