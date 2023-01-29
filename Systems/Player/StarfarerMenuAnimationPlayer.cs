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
    public class StarfarerMenuAnimation: ModPlayer
    {
        // Animated Starfarer Menu variables.
        public float quadraticFloatTimer;
        public float quadraticFloat;
        public float quadraticFloatIdle;

        public float StarfarerMenuIdleMovement;
        public float StarfarerMenuHeadRotation;

        public float StarfarerMenuIdleAnimationRotation;

        public float MousePositionFloatX;
        public float MousePositionFloatY;

        public float AsphodeneEyeMovementRX;
        public float AsphodeneEyeMovementLX;
        public float AsphodeneEyeMovementY;

        public float EridaniEyeMovementRX;
        public float EridaniEyeMovementLX;
        public float EridaniEyeMovementY;

        public int idleAnimationTimer;

        public float idleAnimationProgress;
        public float idleAnimationProgressAlt;
        public bool idleAnimationActive = false;//When active, stop eye tracking.
        public float idleAnimationAlpha = 0f;//Increments to 0 when idle is active, decreases to 0 as idle finishes.
        public float idleAnimationAlphaFast = 0f;//Increments to 0 when idle is active, decreases to 0 as idle finishes.

        public float idleAnimationReading = 0f; //For Eridani's idle animation; her eyes will dart back and forth as if she's reading the sentences.
        //This means that when the idle animation is active it quickly goes from 0 to 1f and then resets

        

        public override void PreUpdate()
        {
            QuadraticFloatAnimation();
            StarfarerMenuIdleAnimation();
            if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuUIOpacity > 0 && !idleAnimationActive && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerOutfitVisible == 0)
            {
                idleAnimationTimer += 1;
            }
            
            if (idleAnimationTimer > 660)//Change this to something like.. 18 seconds maybe? (1080)
            {
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuUIOpacity > 0)
                {
                    idleAnimationActive = true;
                    idleAnimationTimer = 0;
                }
                else
                {
                    idleAnimationTimer = 0;
                }
            }
            if(idleAnimationActive)
            {
                idleAnimationProgress += 0.0025f;

                idleAnimationReading += 0.008f;
                if (idleAnimationReading > 1f)
                {
                    idleAnimationReading = 0f;
                }
                idleAnimationAlpha += 0.03f;
                idleAnimationAlphaFast += 0.05f;
                if (idleAnimationAlpha > 1f)
                {
                    idleAnimationAlpha = 1f;
                }
                if (idleAnimationAlphaFast > 1f)
                {
                    idleAnimationAlphaFast = 1f;
                }
                //if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText($"{idleAnimationAlpha}", 241, 255, 180); }
            }
            else
            {
                idleAnimationProgress = -0.01f;

                idleAnimationReading = 0f;

                if (idleAnimationAlpha < 0f)
                {
                    idleAnimationAlpha = 0f;
                }
                if (idleAnimationAlphaFast < 0f)
                {
                    idleAnimationAlphaFast = 0f;
                }
                idleAnimationAlpha -= 0.03f;
                idleAnimationAlphaFast -= 0.05f;

            }



            if (idleAnimationProgress >= 1f)
            {
                idleAnimationActive = false;
                idleAnimationProgress = 1f;
                
            }
            

        }
        
       
        
        
        public override void ResetEffects()
        {
            
        }
        
        bool quadraticFloatReverse = false;

        private void QuadraticFloatAnimation()
        {

            quadraticFloat = Utilities.EaseHelper.InOutQuad(quadraticFloatTimer);
            quadraticFloatIdle = Utilities.EaseHelper.InOutQuad(idleAnimationProgressAlt);

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


            //if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"{quadraticFloatTimer} Timer"), 241, 255, 180); }
            //if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"{quadraticFloat} Float (rotation)"), 241, 255, 180); }
        }
        private void StarfarerMenuIdleAnimation()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            StarfarerMenuIdleMovement = MathHelper.Lerp(0, 10, quadraticFloat);
            StarfarerMenuHeadRotation = MathHelper.Lerp(0, -6, quadraticFloat);

            StarfarerMenuIdleAnimationRotation = MathHelper.Lerp(0, 11, quadraticFloatIdle);

            if(idleAnimationActive)//Progresses until full, then decreases depending on the idle animation's timer.
            {
                if (idleAnimationProgress > 0.6f)
                {
                    idleAnimationProgressAlt -= 0.01f;
                }
                else
                {
                    if (idleAnimationProgress < 0.4f)
                    {

                        idleAnimationProgressAlt += 0.01f;
                    }
                    else
                    {
                        StarfarerMenuIdleAnimationRotation = 11; //Wait period.
                    }
                }

                if (idleAnimationProgressAlt >= 1f)
                {


                    idleAnimationProgressAlt = 1f;
                }
                if (idleAnimationProgressAlt <= 0f)
                {


                    idleAnimationProgressAlt = 0f;
                }
            }
            
            //Float from 0 to 1 from left to right side of the screen
            MousePositionFloatX = ((Math.Min(Main.screenWidth, Main.MouseScreen.X) - 0) * 100) / (Main.screenWidth - 0) / 100;
            MousePositionFloatY = ((Math.Min(Main.screenHeight, Main.MouseScreen.Y) - 0) * 100) / (Main.screenHeight - 0) / 100;

            if (MousePositionFloatX < 0)
            {
                MousePositionFloatX = 0;
            }
            if (MousePositionFloatY < 0)
            {
                MousePositionFloatY = 0;
            }

           

            if (!idleAnimationActive)
            {
                AsphodeneEyeMovementRX = MathHelper.Lerp(0, 6, MousePositionFloatX);
                AsphodeneEyeMovementLX = MathHelper.Lerp(0, 2, MousePositionFloatX);
                AsphodeneEyeMovementY = MathHelper.Lerp(-1, 1, MousePositionFloatY);

                EridaniEyeMovementLX = MathHelper.Lerp(-2, 2, MousePositionFloatX);
                EridaniEyeMovementRX = MathHelper.Lerp(-1, 1, MousePositionFloatX);
                EridaniEyeMovementY = MathHelper.Lerp(-1, 1, MousePositionFloatY);

            }
            else
            {
                if(modPlayer.chosenStarfarer == 1)//Asphodene's idle animation; she looks down on her summoned sword.
                {
                    AsphodeneEyeMovementRX = MathHelper.Lerp(2, 6, idleAnimationProgressAlt);
                    AsphodeneEyeMovementLX = MathHelper.Lerp(1, 3, idleAnimationProgressAlt);
                    AsphodeneEyeMovementY = MathHelper.Lerp(2, 0, idleAnimationProgressAlt);
                }

                if (modPlayer.chosenStarfarer == 2)//Eridani's idle animation; she summons a book and begins to read.
                {
                    EridaniEyeMovementLX = MathHelper.Lerp(2, -2, idleAnimationReading);
                    EridaniEyeMovementRX = MathHelper.Lerp(1, -1, idleAnimationReading);
                    EridaniEyeMovementY = 2;
                }
            }
            

        }
        

    }

};