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
using StarsAbove.Buffs.Subworlds;
using SubworldLibrary;
using StarsAbove.Utilities;
using StarsAbove.Tiles.StellarFoci;

namespace StarsAbove
{
    public class CountStellarFoci : ModSystem
    {
        public int ResistanceT1;
        public int ResistanceT2;
        public int ResistanceT3;

        public int AgilityT1;
        public int AgilityT2;
        public int AgilityT3;

        public int LuckT1;
        public int LuckT2;
        public int LuckT3;

        public int PowerT1;
        public int PowerT2;
        public int PowerT3;

        public int WealthT1;
        public int WealthT2;
        public int WealthT3;

        public int ConstitutionT1;
        public int ConstitutionT2;
        public int ConstitutionT3;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            ResistanceT1 = tileCounts[ModContent.TileType<ResistanceFocusTier1Tile>()] / 12;
            ResistanceT2 = tileCounts[ModContent.TileType<ResistanceFocusTier2Tile>()] / 12;
            ResistanceT3 = tileCounts[ModContent.TileType<ResistanceFocusTier3Tile>()] / 12;

            AgilityT1 = tileCounts[ModContent.TileType<AgilityFocusTier1Tile>()] / 12;
            AgilityT2 = tileCounts[ModContent.TileType<AgilityFocusTier2Tile>()] / 12;
            AgilityT3 = tileCounts[ModContent.TileType<AgilityFocusTier3Tile>()] / 12;

            LuckT1 = tileCounts[ModContent.TileType<LuckFocusTier1Tile>()] / 12;
            LuckT2 = tileCounts[ModContent.TileType<LuckFocusTier2Tile>()] / 12;
            LuckT3 = tileCounts[ModContent.TileType<LuckFocusTier3Tile>()] / 12;

            WealthT1 = tileCounts[ModContent.TileType<WealthFocusTier1Tile>()] / 12;
            WealthT2 = tileCounts[ModContent.TileType<WealthFocusTier2Tile>()] / 12;
            WealthT3 = tileCounts[ModContent.TileType<WealthFocusTier3Tile>()] / 12;

            PowerT1 = tileCounts[ModContent.TileType<PowerFocusTier1Tile>()] / 12;
            PowerT2 = tileCounts[ModContent.TileType<PowerFocusTier2Tile>()] / 12;
            PowerT3 = tileCounts[ModContent.TileType<PowerFocusTier3Tile>()] / 12;

            ConstitutionT1 = tileCounts[ModContent.TileType<ConstitutionFocusTier1Tile>()] / 12;
            ConstitutionT2 = tileCounts[ModContent.TileType<ConstitutionFocusTier2Tile>()] / 12;
            ConstitutionT3 = tileCounts[ModContent.TileType<ConstitutionFocusTier3Tile>()] / 12;

        }
    }
    public class CelestialCartographyPlayer : ModPlayer
    {
        // Animated Starfarer Menu variables.
        public float quadraticFloatTimer;
        public float quadraticFloat;
        public float quadraticFloatIdle;

        //Celestial Cartography

        public bool CelestialCartographyActive;

        public float CelestialCompassVisibility;//Also used for compass intro animations. Increases when the menu is active, decreases when it isn't.
        public int CelestialCompassFrameTimer;
        public int CelestialCompassFrame;
        public int CelestialCompassRotation;
        public float CelestialCompassInitialVelocity = 30f;//This is a flat addition added later (not to CelestialCompassRotation)

        public float CelestialCompassRotation2;//This variable is added to by Velocity 2
        public float CelestialCompassInitialVelocity2 = 10f;

        public float StarmapStarsAlpha;//Sparkly stars
        public float StarmapStarsAlphaTimer;

        public int CelestialCompassRotation3;



        public bool locationPopUp;
        public string locationName = "";//Default name.

        //Information about the location.
        public string locationMapName = "";
        public string locationDescription = "";
        public string locationThreat = "";
        public string locationRequirement = "";
        public string locationLoot = "";

        public float locationDescriptionAlpha = 0;

        public float locationPopUpTimer;
        public float locationPopUpProgress;

        public float locationPopUpPlacement;
        public float locationPopUpAlpha = 0;

        public float loadingScreenOpacity = 0f;

        //Stellaglyph
        public bool nearStellaglyph;
        public bool nearGateway;

        public int stellaglyphTier;

        //Stellar Foci

        //Focuses can be made with a Plinth and subworld materials. Possibly stronger Foci will need Power Foci to let them work?

        //These values are set and values reset at the end of all updates.

        //The amount of Stellar Foci. If the amount of Stellar Foci exceed the limit of the current Stellaglyph, all buffs will be disabled.
        public int stellarFociAmount = 0;

        //Changes depending on the Stellaglyph.
        public int stellarFociMax = 0;

        //Increases player attack during Cosmic Voyages. (Ruby)
        public float attackFocus = 0f;

        //Increases player defense during Cosmic Voyages. (Sapphire)
        public int defenseFocus = 0;

        //Increases the player luck during Cosmic Voyages. (Diamond)
        public float luckFocus = 0f;

        //Increases the movement speed during Cosmic Voyages. (Emerald)
        public float speedFocus = 0f;

        //Increases Max HP and MP during Cosmic Voyages. (Amethyst)
        public int maxStatFocus = 0;
        // +5 HP, +20 MP per Tier 1 Focus

        //Increases money dropped during Cosmic Voyages. (Topaz)
        public int moneyFocus = 0;


        public override void PreUpdate()
        {
            //This additionally adds the buffs.
            CheckVoyageEligibility();


            //Recalculate all the Stellar Foci passives.
            CalculateStellarFoci();

            

            LocationDescriptionAnimation();
            QuadraticFloatAnimation();
            CelestialCartography();
            LocationPopUp();
            StarmapStarAnimation();


        }
        public override void PostUpdate()
        {
            if (stellarFociAmount > stellarFociMax && Player.HasBuff(BuffType<StellaglyphReady>()))
            {
                //A buff to signify you're above the limit.
                Player.AddBuff(BuffType<StellarFocusOverlimit>(), 10);

            }
            if (stellarFociAmount <= stellarFociMax && SubworldSystem.Current != null)
            {

                Player.statLifeMax2 += maxStatFocus / 2;
                Player.statManaMax2 += maxStatFocus;


            }

        }
        public override void PostUpdateRunSpeeds()
        {
            ManageFociBuffs();

        }

        private void CalculateStellarFoci()
        {
            if (SubworldSystem.Current == null)
            {
                stellarFociAmount = 0;

                attackFocus = 0f;

                defenseFocus = 0;

                luckFocus = 0f;

                speedFocus = 0f;

                maxStatFocus = 0;

                moneyFocus = 0;


            }

            //Increase defense by 3 for each Tier 1 Resistance Focus
            defenseFocus += ModContent.GetInstance<CountStellarFoci>().ResistanceT1 * 3;
            //Increase by 5
            defenseFocus += ModContent.GetInstance<CountStellarFoci>().ResistanceT2 * 5;
            //Increase by 7
            defenseFocus += ModContent.GetInstance<CountStellarFoci>().ResistanceT3 * 7;

            attackFocus += ModContent.GetInstance<CountStellarFoci>().PowerT1 * 0.02f;
            attackFocus += ModContent.GetInstance<CountStellarFoci>().PowerT2 * 0.03f;
            attackFocus += ModContent.GetInstance<CountStellarFoci>().PowerT3 * 0.04f;

            luckFocus += ModContent.GetInstance<CountStellarFoci>().LuckT1 * 0.04f;
            luckFocus += ModContent.GetInstance<CountStellarFoci>().LuckT2 * 0.08f;
            luckFocus += ModContent.GetInstance<CountStellarFoci>().LuckT3 * 0.12f;

            speedFocus += ModContent.GetInstance<CountStellarFoci>().AgilityT1 * 0.03f;
            speedFocus += ModContent.GetInstance<CountStellarFoci>().AgilityT2 * 0.05f;
            speedFocus += ModContent.GetInstance<CountStellarFoci>().AgilityT3 * 0.07f;

            moneyFocus += ModContent.GetInstance<CountStellarFoci>().LuckT1 * 7;
            moneyFocus += ModContent.GetInstance<CountStellarFoci>().LuckT2 * 15;
            moneyFocus += ModContent.GetInstance<CountStellarFoci>().LuckT3 * 30;

            maxStatFocus += ModContent.GetInstance<CountStellarFoci>().ConstitutionT1 * 10;
            maxStatFocus += ModContent.GetInstance<CountStellarFoci>().ConstitutionT2 * 16;
            maxStatFocus += ModContent.GetInstance<CountStellarFoci>().ConstitutionT3 * 24;


            stellarFociAmount +=
                ModContent.GetInstance<CountStellarFoci>().ResistanceT1 +
                ModContent.GetInstance<CountStellarFoci>().ResistanceT2 +
                ModContent.GetInstance<CountStellarFoci>().ResistanceT3 +
                ModContent.GetInstance<CountStellarFoci>().PowerT1 +
                ModContent.GetInstance<CountStellarFoci>().PowerT2 +
                ModContent.GetInstance<CountStellarFoci>().PowerT3 +
                ModContent.GetInstance<CountStellarFoci>().LuckT1 +
                ModContent.GetInstance<CountStellarFoci>().LuckT2 +
                ModContent.GetInstance<CountStellarFoci>().LuckT3 +
                ModContent.GetInstance<CountStellarFoci>().ConstitutionT1 +
                ModContent.GetInstance<CountStellarFoci>().ConstitutionT2 +
                ModContent.GetInstance<CountStellarFoci>().ConstitutionT3 +
                ModContent.GetInstance<CountStellarFoci>().WealthT1 +
                ModContent.GetInstance<CountStellarFoci>().WealthT2 +
                ModContent.GetInstance<CountStellarFoci>().WealthT3 +
                ModContent.GetInstance<CountStellarFoci>().AgilityT1 +
                ModContent.GetInstance<CountStellarFoci>().AgilityT2 +
                ModContent.GetInstance<CountStellarFoci>().AgilityT3;
        }

        private void ManageFociBuffs()
        {
            if (stellarFociAmount <= stellarFociMax && SubworldSystem.Current != null)
            {

                Player.GetDamage(DamageClass.Generic) += attackFocus;

                Player.statDefense += defenseFocus;

                Player.maxRunSpeed *= 1 + speedFocus;
                Player.accRunSpeed *= 1 + speedFocus;

                Player.luck += luckFocus;

            }


        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            CalculateMoneyFocus(target);
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            CalculateMoneyFocus(target);
        }

        private void CalculateMoneyFocus(NPC target)
        {
            if (!target.active)//On kill
            {
                if (stellarFociAmount <= stellarFociMax && SubworldSystem.Current != null && moneyFocus > 0)
                {
                    int k = Item.NewItem(null, (int)target.position.X + Main.rand.Next(-3, 3), (int)target.position.Y + Main.rand.Next(-3, 3), target.width, target.height, ItemID.SilverCoin,
                        moneyFocus, //Money drop.
                        false);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, k, 1f);
                    }
                }
            }
        }

        private bool CheckVoyageEligibility()
        {
            if (NPC.downedAncientCultist && !NPC.downedMoonlord)
            {
                if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LunarEvents"), 255, 255, 100); }
                return false;
            }
            
            if (nearGateway)
            {
                //add buff to signify this
                Player.AddBuff(BuffType<PortalReady>(), 10);
                
                return true;
            }
            if (nearStellaglyph)
            {
                Player.AddBuff(BuffType<StellaglyphReady>(), 10);
                
                return true;
            }
            
            return false;
        }
        
        
        private void StarmapStarAnimation()
        {
            if(CelestialCartographyActive)
            {
                StarmapStarsAlpha = quadraticFloat;
            }
            else
            {
                StarmapStarsAlpha = 0;
            }
        }
        private void LocationPopUp()
        {
            if (locationName != "" && !locationPopUp)
            {
                locationPopUpTimer = 0;
                locationPopUpAlpha = 0;
                locationPopUp = true;
            }
            if(locationPopUpTimer > 1 && locationPopUp)
            {
                locationPopUp = false;
                locationName = "";
            }

            //locationPopUpProgress = InOutQuad(locationPopUpTimer);
            //locationPopUpPlacement = MathHelper.Lerp(0, 1, locationPopUpProgress);

            if(locationPopUpTimer < 0.3f)
            {
                locationPopUpAlpha += 0.1f;
            }
            if (locationPopUpTimer > 0.7f)
            {
                locationPopUpAlpha -= 0.1f;
            }
            locationPopUpAlpha = Math.Clamp(locationPopUpAlpha, 0, 1);
            locationPopUpTimer += 0.006f;
            loadingScreenOpacity -= 0.03f;
        }
        private void LocationDescriptionAnimation()
        {
           
            if (locationMapName != "")
            {
                locationDescriptionAlpha += 0.1f;
            }
            else
            {
                locationDescriptionAlpha -= 0.1f;
            }
            locationDescriptionAlpha = Math.Clamp(locationDescriptionAlpha, 0, 1);
        }

        public static float InQuad(float t) => t * t;
        public static float OutQuad(float t) => 1 - InQuad(1 - t);
        public static float InOutQuad(float t)
        {
            if (t < 0.5) return InQuad(t * 2) / 2;
            return 1 - InQuad((1 - t) * 2) / 2;
        }

        public static float EaseIn(float t)
        {
            return t * t;
        }
        static float Flip(float x)
        {
            return 1 - x;
        }
        public static float EaseOut(float t)
        {
            return Flip((float)Math.Sqrt(Flip(t)));
        }
        public static float EaseInOut(float t)
        {
            return MathHelper.Lerp(EaseIn(t), EaseOut(t), t);
        }
        bool quadraticFloatReverse = false;

        private void QuadraticFloatAnimation()
        {

            quadraticFloat = InOutQuad(quadraticFloatTimer);

            if (quadraticFloatReverse)
            {
                quadraticFloatTimer -= 0.02f;
            }
            else
            {
                quadraticFloatTimer += 0.02f;
            }
            if (quadraticFloatTimer > 1f)
            {
                quadraticFloatTimer = 1f;
                quadraticFloatReverse = true;
            }
            if (quadraticFloatTimer < 0f)
            {
                quadraticFloatTimer = 0f;
                quadraticFloatReverse = false;
            }


            
        }
        private void CelestialCartography()//Intro and idle animation for the Celestial Cartography UI.
        {
            if (CelestialCartographyActive)
            {
                CelestialCompassFrameTimer++;

                if (CelestialCompassFrameTimer > 5)
                {
                    CelestialCompassFrameTimer = 0;
                    if (CelestialCompassFrame++ > 7)
                    {
                        CelestialCompassFrame = 0;
                    }

                }
                CelestialCompassRotation3 += 2;
                if (CelestialCompassRotation3 > 360)
                {
                    CelestialCompassRotation = 0;
                }
                CelestialCompassVisibility += 0.1f;
                if (CelestialCompassVisibility > 1f)
                {
                    CelestialCompassVisibility = 1f;//Finished
                }

                if (CelestialCompassInitialVelocity > 0f)//Slow down the initial rotation.
                {
                    CelestialCompassInitialVelocity -= 4f;
                }
                else
                {
                    CelestialCompassInitialVelocity = 0f;
                }
                if (CelestialCompassInitialVelocity2 > 0f)//Slow down the initial rotation.
                {
                    CelestialCompassInitialVelocity2 -= 0.4f;
                }
                else
                {
                    CelestialCompassInitialVelocity2 = 0f;
                }
                CelestialCompassRotation2 += CelestialCompassInitialVelocity2;

            }
            else//UI not active.
            {
                CelestialCompassVisibility -= 0.1f;
                if (CelestialCompassVisibility < 0f)
                {
                    CelestialCompassVisibility = 0f;
                }
                if (CelestialCompassInitialVelocity < 30f)//Slow down the initial rotation.
                {
                    CelestialCompassInitialVelocity += 8f;
                }
                else
                {
                    CelestialCompassInitialVelocity = 30f;
                }
                if (CelestialCompassInitialVelocity2 < 10f)//Slow down the initial rotation.
                {
                    CelestialCompassInitialVelocity2 += 1f;
                }
                else
                {
                    CelestialCompassInitialVelocity2 = 10f;
                }
                if (CelestialCompassRotation2 > 0)//Reset second rotation.
                {
                    CelestialCompassRotation2 -= 10;
                }
                else
                {
                    CelestialCompassRotation2 = 0;
                }
            }
        }
        public override void ResetEffects()
        {
            nearGateway = false;
            nearStellaglyph = false;


        }
    }

};