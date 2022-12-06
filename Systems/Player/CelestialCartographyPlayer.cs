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

namespace StarsAbove
{
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

        public float locationDescriptionAlpha = 1;

        public float locationPopUpTimer;
        public float locationPopUpProgress;

        public float locationPopUpPlacement;
        public float locationPopUpAlpha = 1;

        public float loadingScreenOpacity = 0f;

        //Stellaglyph
        public bool nearStellaglyph;
        public bool nearGateway;

        public int stellaglyphTier;

        //Stellar Foci

        //Focuses can be made with a Plinth and subworld materials. Possibly stronger Foci will need Power Foci to let them work?

        //These values are set and values reset at the end of all updates.

        //Increases player attack during Cosmic Voyages.
        public float attackFocus;
        public bool basicAttackFocusActive;

        //Increases player defense during Cosmic Voyages.
        public float defenseFocus;
        public bool basicDefenseFocusActive;

        //Increases the player luck during Cosmic Voyages.
        public float luckFocus;
        public bool basicLuckFocusActive;

        //Increases the movement speed during Cosmic Voyages.
        public float speedFocus;
        public bool basicSpeedFocusActive;
        
        //Increases the potency of other Foci slightly.
        public bool strengthEnhancerFocus;
        public bool strengthEnhancerFocusActive;


        public override void PreUpdate()
        {
            //This additionally adds the buffs.
            CheckVoyageEligibility();

            LocationDescriptionAnimation();
            QuadraticFloatAnimation();
            CelestialCartography();
            LocationPopUp();
            StarmapStarAnimation();

        }

        private bool CheckVoyageEligibility()
        {
            if (NPC.downedAncientCultist && !NPC.downedMoonlord)
            {
                if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue("Lunar energy prevents the activation of the Bifrost..."), 255, 255, 100); }
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