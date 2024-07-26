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
using StarsAbove.Items.Weapons;
using StarsAbove.Items.Weapons.Summon;
using StarsAbove.Items.Weapons.Ranged;
using StarsAbove.Items.Weapons.Other;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Melee;
using StarsAbove.Items.Weapons.Magic;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;
using StarsAbove.Buffs.Subworlds;
using SubworldLibrary;
using StarsAbove.Utilities;
using StarsAbove.Tiles.StellarFoci;

namespace StarsAbove.Systems
{

    public class CosmoturgyPlayer : ModPlayer
    {
        //Crystal animation
        public float quadraticFloatTimer;
        public float quadraticFloat;
        public float quadraticFloatIdle;

        public float imbueSuccessAnimationTimer = 0;

        public float smoothRotation;
        public float weaponGlowOpacity;
        public int crystalFrame;
        public int crystalFrameTimer;

        public bool cosmoturgyUIActive;
        public float cosmoturgyUIOpacity;

        public string description = "";

        public override void PreUpdate()
        {
            UIAnimation();

            if (cosmoturgyUIActive)
            {
                if (Player.velocity != Vector2.Zero && cosmoturgyUIOpacity >= 1f)
                {
                    cosmoturgyUIActive = false;
                }
                Player.GetModPlayer<StarsAbovePlayer>().gaussianBlurProgress += 0.2f;
                cosmoturgyUIOpacity += 0.1f;
            }
            else
            {
                description = "";
                cosmoturgyUIOpacity -= 0.1f;
            }
            cosmoturgyUIOpacity = MathHelper.Clamp(cosmoturgyUIOpacity, 0f, 1f);

        }
        public override void PostUpdate()
        {

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

        private void UIAnimation()
        {
            imbueSuccessAnimationTimer -= 0.003f;
            imbueSuccessAnimationTimer = MathHelper.Clamp(imbueSuccessAnimationTimer, 0f, 1f);

            //Give time for the crystal to flash its animation before the weapon starts glowing
            if (imbueSuccessAnimationTimer < 0.92f)
            {
                weaponGlowOpacity = MathHelper.Lerp(0, 1, imbueSuccessAnimationTimer);
                crystalFrame = 0;
            }
            else
            {
                weaponGlowOpacity += 0.1f;
            }
            if (imbueSuccessAnimationTimer > 0.92f)
            {
                crystalFrameTimer++;
            }
            if (crystalFrameTimer > 3)
            {
                crystalFrameTimer = 0;
                crystalFrame++;
                crystalFrame = Math.Clamp(crystalFrame, 0, 6);
            }

            quadraticFloatTimer += 0.0005f;
            quadraticFloat = InOutQuad(pulse(quadraticFloatTimer));

            smoothRotation += 0.5f;
            if (smoothRotation > 360)
            {
                smoothRotation = 0f;
            }

        }
        float pulse(float time)
        {
            const float pi = (float)Math.PI;
            const float frequency = 10; // Frequency in Hz
            return (float)(0.5 * (1 + Math.Sin(2 * pi * frequency * time)));
        }

        public override void ResetEffects()
        {


        }
    }

};