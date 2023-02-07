                    if (modPlayer.chosenStellarNova == 3)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.4");
                    }
                    if (modPlayer.chosenStellarNova == 4)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.5");
                    }
                    if (modPlayer.chosenStellarNova == 5)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.6");
                    }
                }
                if (modPlayer.randomNovaDialogue == 2)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.7");
                }
                if (modPlayer.randomNovaDialogue == 3)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.8");
                }
                if (modPlayer.randomNovaDialogue == 4)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.9");
                }
                if (modPlayer.randomNovaDialogue == 5)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Asphodene.10");
                }


            }
            if (modPlayer.chosenStarfarer == 2)
            {
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/E" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * modPlayer.NovaCutInOpacity);

                if (modPlayer.NovaCutInTimer >= 100)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE0"), hitbox, Color.White * modPlayer.NovaCutInOpacity);

                }
                if (modPlayer.NovaCutInTimer is < 100 and > 97)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE1"), hitbox, Color.White * modPlayer.NovaCutInOpacity);

                }
                if (modPlayer.NovaCutInTimer is <= 97 and > 95)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE2"), hitbox, Color.White * modPlayer.NovaCutInOpacity);

                }
                if (modPlayer.NovaCutInTimer <= 95)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE3"), hitbox, Color.White * modPlayer.NovaCutInOpacity);

                }
                if (modPlayer.randomNovaDialogue == 0)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.1");
                }
                if (modPlayer.randomNovaDialogue == 1)
                {
                    if (modPlayer.chosenStellarNova == 1)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.2");
                    }
                    if (modPlayer.chosenStellarNova == 2)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.3");
                    }
                    if (modPlayer.chosenStellarNova == 3)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.4");
                    }
                    if (modPlayer.chosenStellarNova == 4)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.5");
                    }
                    if (modPlayer.chosenStellarNova == 5)
                    {
                        modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.6");
                    }
                }
                if (modPlayer.randomNovaDialogue == 2)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.7");
                }
                if (modPlayer.randomNovaDialogue == 3)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.8");
                }
                if (modPlayer.randomNovaDialogue == 4)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.9");
                }
                if (modPlayer.randomNovaDialogue == 5)
                {
                    modPlayer.novaDialogue = LangHelper.GetTextValue("StellarNova.StellarNovaDialogue.StellarNovaQuotes.Eridani.10");
                }


            }
            modPlayer.novaDialogue = LangHelper.Wrap(modPlayer.novaDialogue, 20);
            if (!disableDialogue)
            {
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/NovaTextBox"), hitbox, Color.White * modPlayer.NovaCutInOpacity);

            }
            else
            {
                modPlayer.novaDialogue = "";

            }

            if (ShadesVisible)
            {

                if (modPlayer.chosenStarfarer == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AShades"), hitbox, Color.White * modPlayer.NovaCutInOpacity);


                }
                if (modPlayer.chosenStarfarer == 2)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EShades"), hitbox, Color.White * modPlayer.NovaCutInOpacity);


                }

            }
        }










        Recalculate();


    }


    public override void Update(GameTime gameTime)
    {
        if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInTimer > 0 && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 0))
        {
            area.Remove();
            return;

        }
        else
        {
            Append(area);
        }

        var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

        // Setting the text per tick to update and show our resource values.
        text.SetText($"{modPlayer.novaDialogue}");



        //text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
        base.Update(gameTime);
    }
}
