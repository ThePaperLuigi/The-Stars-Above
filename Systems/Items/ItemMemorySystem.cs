
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
using Terraria.GameInput;
using System.Reflection;
using Terraria.Audio;
using StarsAbove.Buffs.TagDamage;
using Terraria.WorldBuilding;
using StarsAbove.Projectiles.Summon.ArachnidNeedlepoint;
using static Humanizer.In;

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
        public bool PrimeCut;//6
        public bool PhantomMask;//7
        public bool NetheriteBar;//8
        public bool PearlescentOrb;//9
        public bool ArgentumShard;//10
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

        //v2.0.5
        public bool WetCrowbar;
        public bool CrystalshotCartridge;
        public bool OutbackWrangler;
        public bool LonelyBand;
        public bool StrangeScrap;
        
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

        //Support mods (increase defense given, increase speed given, etc) UNUSED
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
        public List<int> ItemsThatHaveMultiplayerEffects = new List<int>() {
            ItemType<Suistrume>(),
            ItemType<Chronoclock>(),
            ItemType<LegendaryShield>(),
            ItemType<HunterSymphony>(),
        };
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
            if(PowerMoon && player.altFunctionUse == 2  && !player.HasBuff(BuffType<PowerMoonCooldown>()))
            {
                player.AddBuff(BuffType<PowerMoonCooldown>(), 60 * 4);
                player.GetModPlayer<StarsAbovePlayer>().novaGauge += 4;
            }
            if (RuinedCrown && player.altFunctionUse == 2 && !player.HasBuff(BuffType<RuinedCrownCooldown>()))
            {
                player.AddBuff(BuffType<RuinedCrownCooldown>(), 60 * 10);
                player.AddBuff(BuffType<RuinedCrownBuff>(), 60 * 2);

            }
            if (CrystalshotCartridge && player.altFunctionUse == 2 && !player.HasBuff(BuffType<CrystalshotCooldown>()))
            {
                Vector2 velocity = Vector2.Normalize(player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 10f;

                int numberProjectiles = player.GetModPlayer<ItemMemorySystemPlayer>().crystalshot; //random shots
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(45)); // 30 degree spread.
                                                                                                                            // If you want to randomize the speed to stagger the projectiles
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<CrystalshotBullet>(), (int)(item.damage*0.4f), 3, player.whoAmI);
                }

                for (int d = 0; d < 21; d++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(47));
                    float scale = 2f - (Main.rand.NextFloat() * .9f);
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(player.Center, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 16; d++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(47));
                    float scale = 2f - (Main.rand.NextFloat() * .9f);
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(player.Center, 0, 0, 31, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                    Main.dust[dustIndex].noGravity = true;
                }

            }

            if (Trumpet)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_Trumpet, player.Center);
            }
            return base.UseItem(item, player);
        }
        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            if (LonelyBand && ItemsThatHaveMultiplayerEffects.Contains(item.type) && Main.netMode == NetmodeID.SinglePlayer)
            {
                reduce -= 0.8f;
            }
            base.ModifyManaCost(item, player, ref reduce, ref mult);
        }
        int dekuNutCD;
        public override void OnConsumeMana(Item item, Player player, int manaConsumed)
        {
            if(DekuNut)
            {
                if(dekuNutCD <= 0)
                {
                    player.Heal(1);
                    dekuNutCD = 3;
                    return;
                }
                dekuNutCD--;
            }

            base.OnConsumeMana(item, player, manaConsumed);
        }
        public override void OnConsumeItem(Item item, Player player)
        {
            if(item.healLife > 0 && AetherBarrel)
            {
                player.GetModPlayer<ItemMemorySystemPlayer>().powderCharges = 2;
            }


            base.OnConsumeItem(item, player);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(ChronalDecelerator)
            {
                velocity *= 2f;
            }
            if(MercenaryAuracite)
            {
                knockback = 0f;
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
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
            BuffMemories(player, item);
            Memories(player, item);
        }
        public override void HoldItem(Item item, Player player)
        {
            SyncMemoriesToModPlayer(player);

            
        }

        private void SyncMemoriesToModPlayer(Player player)
        {
            var modPlayer = player.GetModPlayer<ItemMemorySystemPlayer>();
            modPlayer.ChoiceGlasses = ChoiceGlasses;//1
            modPlayer.RedSpiderLily = RedSpiderLily;//2
            modPlayer.AetherBarrel = AetherBarrel;//3
            modPlayer.NookMiles = NookMiles;//4
            modPlayer.CapeFeather = CapeFeather;//5
            modPlayer.PrimeCut = PrimeCut;//6
            modPlayer.PhantomMask = PhantomMask;//7
            modPlayer.NetheriteBar = NetheriteBar;//8
            modPlayer.PearlescentOrb = PearlescentOrb;//9
            modPlayer.ArgentumShard = ArgentumShard;//10
            modPlayer.PowerMoon = PowerMoon;//11
            modPlayer.YoumuHilt = YoumuHilt;//12
            modPlayer.Rageblade = Rageblade;//13
            modPlayer.ElectricGuitarPick = ElectricGuitarPick;//14
            modPlayer.DekuNut = DekuNut;//15
            modPlayer.MatterManipulator = MatterManipulator;//16
            modPlayer.BottledChaos = BottledChaos;//17
            modPlayer.Trumpet = Trumpet;//18
            modPlayer.GuppyHead = GuppyHead;//19
            modPlayer.Pawn = Pawn;//20
            modPlayer.ReprintedBlueprint = ReprintedBlueprint;//21
            modPlayer.ResonanceGem = ResonanceGem;//22
            modPlayer.RuinedCrown = RuinedCrown;//23
            modPlayer.DescenderGemstone = DescenderGemstone;//24
            modPlayer.MonsterNail = MonsterNail;//25
            modPlayer.MindflayerWorm = MindflayerWorm;//26
            modPlayer.MercenaryAuracite = MercenaryAuracite;//27
            modPlayer.ChronalDecelerator = ChronalDecelerator;//28
            modPlayer.SimulacraShifter = SimulacraShifter;//29
            modPlayer.BlackLightbulb = BlackLightbulb;//30
            modPlayer.SigilOfHope = SigilOfHope;//31
            modPlayer.JackalMask = JackalMask;//32
            modPlayer.KnightsShovelhead = KnightsShovelhead;//33

            modPlayer.WetCrowbar = WetCrowbar;
            modPlayer.CrystalshotCartridge = CrystalshotCartridge;
            modPlayer.OutbackWrangler = OutbackWrangler;
            modPlayer.LonelyBand = LonelyBand;
            modPlayer.StrangeScrap = StrangeScrap;
            //Each weapon has a random tarot card effect assigned.
            modPlayer.TarotCard = TarotCard;//100

            //Garridine's Protocores
            modPlayer.ProtocoreMonoclaw = ProtocoreMonoclaw;//201
            modPlayer.ProtocoreManacoil = ProtocoreManacoil;//202
            modPlayer.ProtocoreShockrod = ProtocoreShockrod;//203

            //Sigils
            modPlayer.RangedSigil = RangedSigil;//301
            modPlayer.MagicSigil = MagicSigil;//302
            modPlayer.MeleeSigil = MeleeSigil;//303
            modPlayer.SummonSigil = SummonSigil;//304

            //Aeonseals
            modPlayer.AeonsealDestruction = AeonsealDestruction;//401
            modPlayer.AeonsealHunt = AeonsealHunt;//402
            modPlayer.AeonsealErudition = AeonsealErudition;//403
            modPlayer.AeonsealHarmony = AeonsealHarmony;//404
            modPlayer.AeonsealNihility = AeonsealNihility;//405
            modPlayer.AeonsealPreservation = AeonsealPreservation;//406
            modPlayer.AeonsealAbundance = AeonsealAbundance;//407

            modPlayer.AeonsealTrailblazer = AeonsealTrailblazer;//408
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if(ReprintedBlueprint && player.ShoppingZone_AnyBiome)
            {
                damage += (0.1f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if (CapeFeather && player.velocity.Y != 0)
            {
                damage += (0.1f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if(player.HasBuff(BuffType<ChoiceGlassesLock>()))
            {
                damage += (0.18f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if(MeleeSigil && player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
            {
                damage += (0.12f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if (MagicSigil && player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
            {
                damage += (0.12f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if (RangedSigil && player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {
                damage += (0.12f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if (SummonSigil && player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
            {
                damage += (0.12f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
            }
            if (TarotCard)
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
            if (PrimeCut)
            {
                if (player.HasBuff(BuffID.WellFed) || player.HasBuff(BuffID.WellFed2) || player.HasBuff(BuffID.WellFed3))
                {
                    damage += (0.04f + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                }
            }
            if (PhantomMask)
            {
                if (player.slotsMinions == 1)
                {
                    damage += ((0.07f*(player.maxMinions-1)) + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
                }
            }
            if(ArgentumShard)
            {
                if(player.statMana > 80)
                {
                    damage += ((0.09f * (player.statMana/80)) + damageModAdditive * damageModMultiplicative) * memoryGlobalMod;
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
            PrimeCut = false;//6
            PhantomMask = false;//7
            NetheriteBar = false;//8
            PearlescentOrb = false;//9
            ArgentumShard = false;//10
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

            WetCrowbar = false;
            CrystalshotCartridge = false;
            OutbackWrangler = false;
            LonelyBand = false;
            StrangeScrap = false;

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
        private void BuffMemories(Player player, Item item)
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
                damageModAdditive += 0.08f;
            }
            player.GetModPlayer<ItemMemorySystemPlayer>().cooldownMod = cooldownReductionMod;
        }
        //All other memories.
        private void Memories(Player player, Item item)
        {
            if(StrangeScrap)
            {
                player.GetModPlayer<ItemMemorySystemPlayer>().strangeScrapPriceCopper = (int)MathHelper.Clamp(player.GetModPlayer<ItemMemorySystemPlayer>().strangeScrapPriceCopper, 0, 8000000);
                item.shopCustomPrice = (int?)(player.GetModPlayer<ItemMemorySystemPlayer>().strangeScrapPriceCopper);

            }
        }
        private void CheckMemories(Item item, int slot, Player player)
        {
            //This would be a lot easier with reflection, but I don't want to risk problems with that!

            SetMemory(slot, 1, "ChoiceGlasses", ref ChoiceGlasses, item, player);
            SetMemory(slot, 2, "RedSpiderLily", ref RedSpiderLily, item, player);
            SetMemory(slot, 3, "AetherBarrel", ref AetherBarrel, item, player);
            SetMemory(slot, 4, "NookMilesTicket", ref NookMiles, item, player);
            SetMemory(slot, 5, "CapedFeather", ref CapeFeather, item, player);
            SetMemory(slot, 6, "PrimeCut", ref PrimeCut, item, player);
            SetMemory(slot, 7, "PhantomMask", ref PhantomMask, item, player);
            SetMemory(slot, 8, "NetheriteBar", ref NetheriteBar, item, player);
            SetMemory(slot, 9, "PearlescentOrb", ref PearlescentOrb, item, player);
            SetMemory(slot, 10, "ArgentumShard", ref ArgentumShard, item, player);
            SetMemory(slot, 11, "PowerMoon", ref PowerMoon, item, player);
            SetMemory(slot, 12, "YoumuHilt", ref YoumuHilt, item, player);
            SetMemory(slot, 13, "Rageblade", ref Rageblade, item, player);
            SetMemory(slot, 14, "ElectricGuitarPick", ref ElectricGuitarPick, item, player);
            SetMemory(slot, 15, "DekuNut", ref DekuNut, item, player);
            SetMemory(slot, 16, "MatterManipulator", ref MatterManipulator, item, player);
            SetMemory(slot, 17, "BottledChaos", ref BottledChaos, item, player);
            SetMemory(slot, 18, "Trumpet", ref Trumpet, item, player);
            SetMemory(slot, 19, "GuppyHead", ref GuppyHead, item, player);
            SetMemory(slot, 20, "Pawn", ref Pawn, item, player);
            SetMemory(slot, 21, "ReprintedBlueprint", ref Pawn, item, player);
            SetMemory(slot, 22, "ResonanceGem", ref ResonanceGem, item, player);

            SetMemory(slot, 26, "RuinedCrown", ref RuinedCrown, item, player);
            SetMemory(slot, 27, "DescenderGemstone", ref DescenderGemstone, item, player);
            SetMemory(slot, 28, "MonsterTooth", ref MonsterNail, item, player);
            SetMemory(slot, 29, "MindflayerWorm", ref MindflayerWorm, item, player);
            SetMemory(slot, 30, "MercenaryAuracite", ref MercenaryAuracite, item, player);

            SetMemory(slot, 31, "SigilOfHope", ref SigilOfHope, item, player);
            SetMemory(slot, 32, "ChronalDeccelerator", ref ChronalDecelerator, item, player);

            SetMemory(slot, 33, "KnightsShovelhead", ref KnightsShovelhead, item, player);

            SetMemory(slot, 34, "SimulacraShifter", ref SimulacraShifter, item, player);
            SetMemory(slot, 35, "BlackLightbulb", ref BlackLightbulb, item, player);
            SetMemory(slot, 36, "OnyxJackal", ref JackalMask, item, player);

            SetMemory(slot, 37, "WetCrowbar", ref WetCrowbar, item, player);
            SetMemory(slot, 38, "CrystalshotCartridge", ref CrystalshotCartridge, item, player);
            SetMemory(slot, 39, "OutbackWrangler", ref OutbackWrangler, item, player);
            SetMemory(slot, 40, "LonelyBand", ref LonelyBand, item, player);
            SetMemory(slot, 41, "StrangeScrap", ref StrangeScrap, item, player);

            SetMemory(slot, 301, "RangedSigil", ref RangedSigil, item, player);
            SetMemory(slot, 302, "MagicSigil", ref MagicSigil, item, player);
            SetMemory(slot, 303, "MeleeSigil", ref MeleeSigil, item, player);
            SetMemory(slot, 304, "SummonSigil", ref SummonSigil, item, player);
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
        private void SetMemory(int slot, int check, string itemName, ref bool itemFlag, Item item, Player player)
        {
            if (slot == check)
            {
                itemFlag = true;
                
                int itemType = Mod.Find<ModItem>(itemName).Type;
                string itemIcon = $"[i:{itemType}]";

                memoryTooltip += itemIcon;
                if (memoryTooltipInfo == "")
                {
                    memoryTooltipInfo += itemIcon + ": " + LangHelper.GetTextValue($"Items.{itemName}.TooltipAttached");
                }
                else
                {
                    memoryTooltipInfo += "\n" + itemIcon + ": " + LangHelper.GetTextValue($"Items.{itemName}.TooltipAttached");
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
        public bool PrimeCut;//6
        public bool PhantomMask;//7
        public bool NetheriteBar;//8
        public bool PearlescentOrb;//9
        public bool ArgentumShard;//10
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

        public bool WetCrowbar;//
        public bool CrystalshotCartridge;//
        public bool OutbackWrangler;//
        public bool LonelyBand;//
        public bool StrangeScrap;//

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

        public float cooldownMod;

        public int powderCharges = 0;
        public int oldHP;
        public int accursedEdgeStacks = 0;
        public float ragebladeStacks = 0f;
        public int crystalshot;
        public int strangeScrapPriceCopper;
        public int wranglerCrits;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Player.GetModPlayer<BossPlayer>().QTEActive && StarsAbove.weaponMemoryKey.JustPressed)
            {
                if (SimulacraShifter && !Player.HasBuff(BuffType<SimulacraShifterCooldown>()))
                {
                    Player.AddBuff(BuffType<Invincibility>(), 6 * 60);
                    Player.AddBuff(BuffType<SimulacraShifterCooldown>(), (int)((60 * 120) * 1f - cooldownMod));
                    float dustAmount = 10f;
                    float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 1.8f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
                if (MindflayerWorm && !Player.HasBuff(BuffType<MindflayerWormCooldown>()))
                {
                    Player.AddBuff(BuffType<MindflayerWormCooldown>(), (int)((60 * 40) * 1f - cooldownMod));
                    float dustAmount = 70f;
                    float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        
                        if (npc.active && !npc.boss && npc.Distance(Player.Center) < 500)
                        {
                            if (Main.rand.NextFloat() > 0.2f)
                            {
                                npc.AddBuff(BuffType<Stun>(), 180);
                                for (int i2 = 0; i2 < dustAmount; i2++)
                                {
                                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i2 * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                                    spinningpoint5 = spinningpoint5.RotatedBy(npc.Center.ToRotation() + randomConstant);
                                    int dust = Dust.NewDust(npc.Center, 0, 0, DustID.GemAmethyst);
                                    Main.dust[dust].scale = 1f;
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].position = npc.Center + spinningpoint5;
                                    Main.dust[dust].velocity = npc.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
                                }
                            }

                        }
                    }
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemAmethyst);
                        Main.dust[dust].scale = 1.8f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 36f;
                    }
                }
                if (PearlescentOrb && !Player.HasBuff(BuffType<EnderpearlCooldown>()))
                {
                    //Teleport
                    Vector2 vector32 = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
                    Player.Teleport(vector32, 1, 0);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
                    Player.AddBuff(BuffType<EnderpearlCooldown>(), (int)((60 * 40) * 1f - cooldownMod));

                }
                if (NetheriteBar && !Player.HasBuff(BuffType<NetheriteIngotBuffCooldown>()))
                {
                    //Defense
                    Player.AddBuff(BuffType<NetheriteIngotBuff>(), 120);
                    Player.AddBuff(BuffType<NetheriteIngotBuffCooldown>(), (int)((60 * 18) * 1f - cooldownMod));
                    float dustAmount = 10f;
                    float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 1.8f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
                if (OutbackWrangler && !Player.HasBuff(BuffType<NetheriteIngotBuffCooldown>()))
                {
                    for (int ir = 0; ir < 20; ir++)
                    {
                        Vector2 position = Player.Center;
                        Dust d = Dust.NewDustPerfect(position, DustID.LifeDrain, null, 240, default, 0.7f);
                        d.fadeIn = 0.3f;
                        d.noLight = true;
                        d.noGravity = true;

                    }
                    Player.statMana = 0;
                    Player.manaRegenDelay = 480;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];

                        if (proj.active && proj.owner == Player.whoAmI &&
                            proj.minion && proj.minionSlots > 0)
                        {
                            for (int ir = 0; ir < 20; ir++)
                            {
                                Vector2 position = proj.Center;
                                Dust d = Dust.NewDustPerfect(position, DustID.LifeDrain, null, 240, default, 0.7f);
                                d.fadeIn = 0.3f;
                                d.noLight = true;
                                d.noGravity = true;

                            }
                            wranglerCrits++;
                            proj.Kill();
                        }
                    }
                }
                if (YoumuHilt && !Player.HasBuff(BuffType<PhantomHiltCooldown>()))
                {
                    //Defense
                    Player.AddBuff(BuffType<PhantomHiltBuff>(), 240);
                    Player.AddBuff(BuffType<PhantomHiltCooldown>(), (int)((60 * 30) * 1f - cooldownMod));
                    float dustAmount = 10f;
                    float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 1.8f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
                if (GuppyHead && !Player.HasBuff(BuffType<GuppyHeadCooldown>()))
                {
                    for(int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<GuppyFlies>(), Player.GetWeaponDamage(Player.HeldItem) / 3, 0, Player.whoAmI, 0, 0);
                    }

                    Player.AddBuff(BuffType<GuppyHeadCooldown>(), (int)((60 * 20) * 1f - cooldownMod));
                    float dustAmount = 10f;
                    float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 1.8f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }

            }
            base.ProcessTriggers(triggersSet);
        }
        public override void PreUpdate()
        {
            if (Rageblade)
            {
                if (Player.GetModPlayer<StarsAbovePlayer>().inCombat <= 0)
                {
                    ragebladeStacks = 0;
                }
            }
            else
            {
                ragebladeStacks = 0;

            }
            if(MercenaryAuracite)
            {
                Player.GetCritChance(DamageClass.Generic) += MercenaryCritChance;
            }
            if(crystalshot > 0)
            {
                Player.AddBuff(BuffType<CrystalshotPrepped>(), 10);
            }
            base.PreUpdate();
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Player.HasBuff(BuffType<PhantomHiltBuff>()))
            {
                Player.maxRunSpeed *= 1.4f;
                Player.accRunSpeed *= 1.4f;

            }
            
        }
        public float MercenaryCritChance = 0f;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.active)
            {
                OnKillNPC(target);
            }
            if(JackalMask)
            {
                Player.MinionAttackTargetNPC = target.whoAmI;
                
            }
            if (CrystalshotCartridge && Main.rand.NextBool(15))
            {
                int k = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("Crystallize").Type, 1, false);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, k, 1f);
                }

            }
            if (Player.HasBuff(BuffType<RuinedCrownBuff>()))
            {
                Player.Heal((int)(damageDone * 0.2f));
                Player.ClearBuff(BuffType<RuinedCrownBuff>());
            }
            if (hit.Crit && GuppyHead)
            {
                if(Player.HasBuff(BuffType<GuppyHeadCooldown>()))
                {
                    Player.buffTime[Player.FindBuffIndex(BuffType<GuppyHeadCooldown>())] -= 60;
                }
            }
            if(MercenaryAuracite)
            {
                if(hit.Crit)
                {
                    MercenaryCritChance = 0f;
                }
                else
                {
                    MercenaryCritChance += 0.5f;
                }
            }
            if(Rageblade)
            {
                ragebladeStacks += 0.01f;
                if (ragebladeStacks > 0.20f)
                {
                    ragebladeStacks = 0.20f;
                }
            }
            if (KnightsShovelhead && !Player.HasBuff(BuffType<ComboCooldown>()))
            {
                Player.AddBuff(BuffType<ComboCooldown>(), 10);
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
            if(ElectricGuitarPick && !Player.HasBuff(BuffType<ElectricGuitarPickCooldown>()))
            {
                Player.AddBuff(BuffType < ElectricGuitarPickCooldown >(), (int)((60 * 2) * 1f - cooldownMod));
                //Spawn lightning on hit!
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<MemoryLightning>(), damageDone / 8, 0, Player.whoAmI, Main.rand.Next(0, 360) + 1000f, 1);
                if (Main.rand.NextBool())
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<MemoryLightning>(), damageDone / 8, 0, Player.whoAmI, Main.rand.Next(0, 360) + 1000f, 1);
                    if (Main.rand.NextBool())
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<MemoryLightning>(), damageDone / 8, 0, Player.whoAmI, Main.rand.Next(0, 360) + 1000f, 1);
                    }
                }
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(JackalMask)
            {
                if(!proj.minion)
                {
                    target.AddBuff(BuffType<OnyxJackalTagDamage>(), 240);
                }
            }
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }
        public void OnKillNPC(NPC target)
        {
            if(MatterManipulator)
            {
                Player.AddBuff(BuffID.Mining, 60 * 30);
            }
            if(Pawn)
            {
                Player.AddBuff(BuffID.Endurance, 60 * 12);
                Player.AddBuff(BuffID.Ironskin, 60 * 12);
            }
            if(MonsterNail)
            {
                if(Main.rand.NextBool(4))
                {
                    int k = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X, (int)target.position.Y, target.width, target.height, ItemID.Heart, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }
                }
                
            }
            if(StrangeScrap)
            {
                Main.NewText(strangeScrapPriceCopper);

                strangeScrapPriceCopper += 10;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if(BottledChaos)
            {
                modifiers.DamageVariationScale *= 3f;
            }
            if (WetCrowbar && target.life == target.lifeMax)
            {
                modifiers.NonCritDamage += 0.3f;
                modifiers.CritDamage += 0.5f;
            }
            if (Player.HasBuff(BuffType<AccursedEdge>()))
            {
                modifiers.FinalDamage += 0.4f;
            }
            if(MercenaryAuracite)
            {
                if(target.aiStyle == NPCAIStyleID.Fighter)
                {
                    modifiers.FinalDamage += 0.3f;
                }
            }
            if(wranglerCrits > 0)
            {
                modifiers.SetCrit();
            }
            if(Player.HasBuff(BuffType<RuinedCrownBuff>()))
            {
                modifiers.FinalDamage += 0.15f;
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
            if (ragebladeStacks > 0)
            {
                Player.AddBuff(BuffType<RagebladeBuff>(), 10);
            }
            base.PreUpdateBuffs();
        }

        public override void PostUpdateBuffs()
        {
            
            base.PostUpdateBuffs();
        }
        public override void ResetEffects()
        {
            ChoiceGlasses = false;//1
            RedSpiderLily = false;//2
            AetherBarrel = false;//3
            NookMiles = false;//4
            CapeFeather = false;//5
            PrimeCut = false;//6
            PhantomMask = false;//7
            NetheriteBar = false;//8
            PearlescentOrb = false;//9
            ArgentumShard = false;//10
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