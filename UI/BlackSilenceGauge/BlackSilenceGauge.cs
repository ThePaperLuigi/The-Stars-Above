
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.Celestial.BlackSilence;
using StarsAbove.Systems.WeaponSystems;
using StarsAbove.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.BlackSilenceGauge
{
    internal class BlackSilenceGauge : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
        private UIText Choice1Text;
        private UIText Choice2Text;
        private UIText Choice3Text;

        private UIElement area;
        private UIImage Backing;
        private Color gradientA;
        private Color gradientB;
        private Color finalColor;

        private UIImageButton Choice1;
        private UIImageButton Choice2;
        private UIImageButton Choice3;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
            area = new UIElement();
            //area.Left.Set(-area.Width.Pixels - 2050, 2f); // Place the resource bar to the left of the hearts.
            area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(600, 0f); // We will be placing the following 2 UIElements within this 282x60 area.
            area.Height.Set(200, 0f);
            area.HAlign = area.VAlign = 0.5f; // 2

            Backing = new UIImage(Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Backing"));
            Backing.Left.Set(0, 0f);
            Backing.Top.Set(0, 0f);
            Backing.Width.Set(600, 0f);
            Backing.Height.Set(200, 0f);
            Backing.HAlign = 0.5f;
            Backing.VAlign = 0.50f;

            Choice1 = new UIImageButton(Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Box"));//Red
            Choice1.OnLeftClick += Choice1Click;
            Choice1.Width.Set(144, 0f);
            Choice1.Height.Set(135, 0f);
            Choice1.HAlign = 0f;
            Choice1.VAlign = 0.50f;
            //Choice2.Top.Set(52, 0f);//

            Choice2 = new UIImageButton(Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Box"));//Blue
            Choice2.OnLeftClick += Choice2Click;
            Choice2.Width.Set(144, 0f);
            Choice2.Height.Set(135, 0f);
            Choice2.HAlign = 0.50f;
            Choice2.VAlign = 0.50f;

            Choice3 = new UIImageButton(Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Box"));//Yellow
            Choice3.OnLeftClick += Choice3Click;
            Choice3.Width.Set(144, 0f);
            Choice3.Height.Set(135, 0f);
            Choice3.HAlign = 1f;
            Choice3.VAlign = 0.50f;

            Choice1Text = new UIText("", 1.5f); // text to show stat
            Choice1Text.Width.Set(0, 0f);
            Choice1Text.Height.Set(0, 0f);
            Choice1Text.Top.Set(95, 0f);
            Choice1Text.Left.Set(0, 0f);
            Choice1Text.HAlign = 0f;
            Choice1Text.VAlign = 0.50f;

            Choice2Text = new UIText("", 1.5f); // text to show stat
            Choice2Text.Width.Set(0, 0f);
            Choice2Text.Height.Set(0, 0f);
            Choice2Text.Top.Set(95, 0f);
            Choice2Text.Left.Set(0, 0f);
            Choice2Text.HAlign = 0.5f;
            Choice2Text.VAlign = 0.50f;

            Choice3Text = new UIText("", 1.5f); // text to show stat
            Choice3Text.Width.Set(0, 0f);
            Choice3Text.Height.Set(0, 0f);
            Choice3Text.Top.Set(95, 0f);
            Choice3Text.Left.Set(0, 0f);
            Choice3Text.HAlign = 1f;
            Choice3Text.VAlign = 0.50f;

            gradientA = new Color(278, 227, 223); // 
            gradientB = new Color(60, 255, 299); //
            finalColor = new Color(0, 224, 255);

            //area.Append(Backing); Doesn't look good. Either rework or remove

            area.Append(Choice1);
            area.Append(Choice2);
            area.Append(Choice3);

            area.Append(Choice1Text);
            area.Append(Choice2Text);
            area.Append(Choice3Text);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>();
            if (!modPlayer.BlackSilenceHeld && modPlayer.BlackSilenceUIVisible == true)
                return;

            base.Draw(spriteBatch);
        }
        private void Choice1Click(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>();
            if (!(modPlayer.BlackSilenceHeld && modPlayer.BlackSilenceUIVisible == true))
                return;

            //Change the chosen weapon to the random assigned weapon.
            modPlayer.chosenWeapon = modPlayer.blackSilenceWeaponChoice1;
            if (modPlayer.chosenWeapon == 0)
            {
                Main.LocalPlayer.AddBuff(BuffType<DurandalBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 1)
            {
                Main.LocalPlayer.AddBuff(BuffType<ZelkovaBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 2)
            {
                Main.LocalPlayer.AddBuff(BuffType<RangaBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 3)
            {
                Main.LocalPlayer.AddBuff(BuffType<OldBoysBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 4)
            {
                Main.LocalPlayer.AddBuff(BuffType<AllasBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 5)
            {
                Main.LocalPlayer.AddBuff(BuffType<MookBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 6)
            {
                Main.LocalPlayer.AddBuff(BuffType<AtelierLogicBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 7)
            {
                Main.LocalPlayer.AddBuff(BuffType<CrystalAtelierBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 8)
            {
                Main.LocalPlayer.AddBuff(BuffType<WheelsIndustryBuff>(), 720);
            }

            //Maybe a sound effect?
            //SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiReloadBuff, Main.LocalPlayer.Center);
            modPlayer.weaponSwapPrep = true;

            //Hide the UI after we're done.
            modPlayer.BlackSilenceUIVisible = false;

        }

        private void Choice2Click(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>();
            if (!(Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>().BlackSilenceHeld && Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>().BlackSilenceUIVisible == true))
                return;

            //Change the chosen weapon to the random assigned weapon.
            modPlayer.chosenWeapon = modPlayer.blackSilenceWeaponChoice2;
            if (modPlayer.chosenWeapon == 0)
            {
                Main.LocalPlayer.AddBuff(BuffType<DurandalBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 1)
            {
                Main.LocalPlayer.AddBuff(BuffType<ZelkovaBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 2)
            {
                Main.LocalPlayer.AddBuff(BuffType<RangaBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 3)
            {
                Main.LocalPlayer.AddBuff(BuffType<OldBoysBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 4)
            {
                Main.LocalPlayer.AddBuff(BuffType<AllasBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 5)
            {
                Main.LocalPlayer.AddBuff(BuffType<MookBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 6)
            {
                Main.LocalPlayer.AddBuff(BuffType<AtelierLogicBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 7)
            {
                Main.LocalPlayer.AddBuff(BuffType<CrystalAtelierBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 8)
            {
                Main.LocalPlayer.AddBuff(BuffType<WheelsIndustryBuff>(), 720);
            }
            //Maybe a sound effect?
            //SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiReloadBuff, Main.LocalPlayer.Center);
            modPlayer.weaponSwapPrep = true;

            //Hide the UI after we're done.
            modPlayer.BlackSilenceUIVisible = false;






            // We can do stuff in here!
        }
        private void Choice3Click(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>();
            if (!(Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>().BlackSilenceHeld && Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>().BlackSilenceUIVisible == true))
                return;

            //Change the chosen weapon to the random assigned weapon.
            modPlayer.chosenWeapon = modPlayer.blackSilenceWeaponChoice3;
            if (modPlayer.chosenWeapon == 0)
            {
                Main.LocalPlayer.AddBuff(BuffType<DurandalBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 1)
            {
                Main.LocalPlayer.AddBuff(BuffType<ZelkovaBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 2)
            {
                Main.LocalPlayer.AddBuff(BuffType<RangaBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 3)
            {
                Main.LocalPlayer.AddBuff(BuffType<OldBoysBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 4)
            {
                Main.LocalPlayer.AddBuff(BuffType<AllasBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 5)
            {
                Main.LocalPlayer.AddBuff(BuffType<MookBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 6)
            {
                Main.LocalPlayer.AddBuff(BuffType<AtelierLogicBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 7)
            {
                Main.LocalPlayer.AddBuff(BuffType<CrystalAtelierBuff>(), 720);
            }
            if (modPlayer.chosenWeapon == 8)
            {
                Main.LocalPlayer.AddBuff(BuffType<WheelsIndustryBuff>(), 720);
            }
            //Maybe a sound effect?
            //SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiReloadBuff, Main.LocalPlayer.Center);
            modPlayer.weaponSwapPrep = true;

            //Hide the UI after we're done.
            modPlayer.BlackSilenceUIVisible = false;









        }





        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>();

            //Animate in.
            area.Top.Set(modPlayer.UIAnimateIn, 0);

            Rectangle Area1 = Choice1.GetInnerDimensions().ToRectangle();
            Rectangle Area2 = Choice2.GetInnerDimensions().ToRectangle();
            Rectangle Area3 = Choice3.GetInnerDimensions().ToRectangle();

            if (modPlayer.BlackSilenceUIVisible)
            {
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/BoxTop"), Area1, Color.White * modPlayer.UIOpacity);
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/BoxTop"), Area2, Color.White * modPlayer.UIOpacity);
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/BoxTop"), Area3, Color.White * modPlayer.UIOpacity);
                #region Display Pictures
                if (modPlayer.blackSilenceWeaponChoice1 == 0)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Durandal"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.durandalUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Zelkova"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.zelkovaUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 2)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Ranga"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.rangaUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 3)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/OldBoys"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.oldBoysUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 4)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Allas"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.allasUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 5)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Mook"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.mookUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 6)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/AtelierLogic"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.atelierLogicUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 7)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/CrystalAtelier"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.crystalAtelierUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice1 == 8)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/WheelsIndustry"), Area1, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.wheelsIndustryUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area1, Color.White * modPlayer.UIOpacity);
                    }
                }

                if (modPlayer.blackSilenceWeaponChoice2 == 0)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Durandal"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.durandalUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Zelkova"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.zelkovaUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 2)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Ranga"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.rangaUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 3)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/OldBoys"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.oldBoysUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 4)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Allas"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.allasUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 5)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Mook"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.mookUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 6)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/AtelierLogic"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.atelierLogicUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 7)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/CrystalAtelier"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.crystalAtelierUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice2 == 8)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/WheelsIndustry"), Area2, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.wheelsIndustryUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area2, Color.White * modPlayer.UIOpacity);
                    }
                }

                if (modPlayer.blackSilenceWeaponChoice3 == 0)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Durandal"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.durandalUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Zelkova"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.zelkovaUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 2)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Ranga"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.rangaUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 3)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/OldBoys"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.oldBoysUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 4)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Allas"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.allasUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 5)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/Mook"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.mookUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 6)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/AtelierLogic"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.atelierLogicUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 7)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/CrystalAtelier"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.crystalAtelierUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                if (modPlayer.blackSilenceWeaponChoice3 == 8)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/WheelsIndustry"), Area3, Color.White * modPlayer.UIOpacity);
                    if (!modPlayer.wheelsIndustryUsed)
                    {
                        //Empty Gloves
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUnused"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                    else
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/BlackSilenceGauge/FuriosoUsed"), Area3, Color.White * modPlayer.UIOpacity);
                    }
                }
                #endregion


            }

        }
        public override void Update(GameTime gameTime)
        {
            if (!(Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>().BlackSilenceUIVisible == true))
            {
                area.Remove();
                return;
            }
            else
            {
                Append(area);
            }
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            var modPlayer = Main.LocalPlayer.GetModPlayer<BlackSilencePlayer>();
            // Setting the text per tick to update and show our resource values.
            if (modPlayer.blackSilenceWeaponChoice1 == 0)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Durandal"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 1)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Zelkova"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 2)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Ranga"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 3)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.OldBoys"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 4)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Allas"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 5)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Mook"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 6)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.AtelierLogic"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 7)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.CrystalAtelier"));
            }
            if (modPlayer.blackSilenceWeaponChoice1 == 8)
            {
                Choice1Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.WheelsIndustry"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 0)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Durandal"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 1)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Zelkova"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 2)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Ranga"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 3)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.OldBoys"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 4)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Allas"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 5)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Mook"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 6)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.AtelierLogic"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 7)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.CrystalAtelier"));
            }
            if (modPlayer.blackSilenceWeaponChoice2 == 8)
            {
                Choice2Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.WheelsIndustry"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 0)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Durandal"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 1)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Zelkova"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 2)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Ranga"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 3)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.OldBoys"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 4)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Allas"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 5)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.Mook"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 6)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.AtelierLogic"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 7)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.CrystalAtelier"));
            }
            if (modPlayer.blackSilenceWeaponChoice3 == 8)
            {
                Choice3Text.SetText(LangHelper.GetTextValue($"UIElements.BlackSilence.WheelsIndustry"));
            }

            base.Update(gameTime);
        }
    }
}
