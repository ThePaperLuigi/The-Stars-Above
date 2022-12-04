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


namespace StarsAbove
{
    public class CelestialCartographyPlayer: ModPlayer
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

        public int CelestialCompassRotation3;

        public bool locationPopUp;
        public string locationName;
        public float locationPopUpTimer;
        public float locationPopUpProgress;

        public float locationPopUpPlacement;

        public override void PreUpdate()
        {
            QuadraticFloatAnimation();
            CelestialCartography();
            LocationPopUp();
            

        }


        private void LocationPopUp()
        {
            if (locationName != "")
            {
                locationPopUpTimer = 0;
                locationPopUp = true;
            }
            if(locationPopUpTimer > 1 && locationPopUp)
            {
                locationPopUp = false;
                locationName = "";
            }
            locationPopUpProgress = InOutQuad(locationPopUpTimer);
            locationPopUpPlacement = MathHelper.Lerp(0, 300, locationPopUpProgress);
            locationPopUpTimer += 0.02f;
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
                quadraticFloatTimer -= 0.005f;
            }
            else
            {
                quadraticFloatTimer += 0.005f;
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
            CelestialCartographyActive = false;
        }
    }

};