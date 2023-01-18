using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;

using StarsAbove.Projectiles.Tsukiyomi;
using SubworldLibrary;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;

namespace StarsAbove.NPCs
{
    [AutoloadBossHead]
    public class Tsukiyomi2 : ModNPC
    {
        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 15;


            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (Main.expertMode)
                {
                    NPC.lifeMax = 1100000;

                }
                else
                {
                    NPC.lifeMax = 900000;

                }
                NPC.defense = 50;

            }
            else
            {

                if (Main.expertMode)
                {
                    NPC.lifeMax = 440000;

                }
                else
                {
                    NPC.lifeMax = 300000;

                }
                NPC.defense = 30;

            }


            NPC.boss = true;
            NPC.aiStyle = 0;
            
            NPC.damage = 0;
           
            NPC.knockBackResist = 0f;


            NPC.width = 150;
            NPC.height = 150;


            NPC.scale = 1f;
            
            NPC.value = Item.buyPrice(0, 1, 75, 45);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.buffImmune[24] = true;

            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/WickedBattle");
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };

            NPC.netAlways = true;
        }






        bool fightStart = true;
        bool onFall = true;
        int nframe = 0;
        int castTime = 0;
        int castTimeMax = 100;
        int castDelay = 0;
        bool isCasting = false;
        bool isSwinging = false;
        string nextCast;
        string nextAttack = "";
        string lastAttack = "";

        bool usedDesperation;


        Vector2 CoruscantSaberSaved;
        Vector2 SolemnConfiteorSaved;
        int phase = 1;

        int eyeProjectile = 0;
        int eyeProjectileTimer = 0;
        int blazingSkies = 0;
        int blazingSkiesTimer = 0;
        int QuintuplecastSkies = 0;
        int QuintuplecastTimer = 0;
        int desperadoShots = 0;
        int desperadoTimer = 0;

        int fadeIn = 255;
        bool vortex;
        bool solar;
        bool stardust;
        bool nebula;

        int swingAnimation = 0;
        int castAnimation = 0;
        int introVelocityY = 10;
        int introVelocityTimer = 0;
        int strayManaTimer;
        bool ArsLaevateinnActive;
        bool inIntro;
        int introAnimation;
        bool fightLost = false;
        bool surpassingInfinity = false;
        bool stunCasted = false;
        bool undertaleActive;
        int undertaleTimer;
        bool teleportAway;
        int savedAlignment;
        int alignment = 0;//0 is Chaos, 1 is Order, 2 is Seraphim
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            int associatedNPCType = ModContent.NPCType<WarriorOfLight>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
            });
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tsukiyomi, the Progeny of Salvation");
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Position = new Vector2(0,80),
                //Scale = 0.9f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = 50f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            // potionType = ItemID.SuperHealingPotion;
            potionType = ItemID.None;

            //Item.NewItem(NPC.GetSource_FromAI(),(int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SpatialMemoriam"));

            /*if (!DownedBossSystem.downedTsuki)
            {
                DownedBossSystem.downedTsuki = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            }*/
        }

        public override void FindFrame(int frameHeight)
        {
            

            NPC.rotation = 0;
            NPC.spriteDirection = 0;
            swingAnimation--;
            castAnimation--;

            if (NPC.dontTakeDamage && !inIntro && NPC.ai[3] != 1f)
            {
                
               NPC.frame.Y = 10 * frameHeight;
                
            }
            else
            {
                NPC.frameCounter++;
            }
            if (NPC.frameCounter >= 10)
            {
                nframe++;
                NPC.frame.Y += frameHeight;
                if (nframe == 5)
                {
                    nframe = 0;
                    NPC.frame.Y = 0;
                }
                NPC.frameCounter = 0;
            }
            if(isCasting && !NPC.dontTakeDamage)
            {
                NPC.frame.Y = frameHeight*5 + nframe*frameHeight;
            }
            // if (isSwinging)

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.defense += numPlayers * 10;
        }


        bool phaseTransition;

        public bool isInvincible;

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }

       

        
        public override bool CheckDead()
        {
            if (!usedDesperation)
            {
                NPC.life = 1;
                return false;
            }
            if (NPC.ai[3] == 0f)
            {
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X + 30, placement2.Y - 80, 0, 0,Mod.Find<ModProjectile>("Wormhole").Type,0,0f,Main.myPlayer);}
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y - 33, 0, 0,Mod.Find<ModProjectile>("TsukiyomiTeleport").Type,0,0f,Main.myPlayer);}
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}
                SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_PerhapsIWasWrong, NPC.Center);

                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }
        public override void DrawBehind(int index)
        {

            Main.instance.DrawCacheNPCsMoonMoon.Add(index);
        }


        public override void AI()
        {

            fadeIn--;
            NPC.alpha--;
            NPC.netUpdate = true;
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            //This is the death effect from ExampleMod
            if (NPC.ai[3] > 0f)//This is death effect
            {
                
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TsukiyomiEnd");
                DownedBossSystem.downedTsuki = true;
                NPC.dontTakeDamage = true;
                NPC.ai[3] += 2f; // increase our death timer.
                                 //npc.velocity = Vector2.UnitY * npc.velocity.Length();
                NPC.velocity.X *= 0.95f; // lose inertia
                //if expert mode, boss flies up and new phase starts?
                //npc.velocity.Y = npc.velocity.Y - 0.02f;
                if (NPC.velocity.Y < 0.1f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.01f;
                }
                if (NPC.velocity.Y > 0.1f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.01f;
                }
                if (NPC.ai[3] > 420f)
                {
                    //		npc.Opacity = 1f - (npc.ai[3] - 120f) / 60f;
                }
                if (Main.rand.NextBool(5) && NPC.ai[3] < 420f)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        // Charging dust
                        Vector2 vector = new Vector2(
                            Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                            Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                        Dust d = Main.dust[Dust.NewDust(
                            NPC.Center + vector, 1, 1,
                            269, 0, 0, 255,
                            new Color(1f, 1f, 1f), 1.5f)];
                        d.velocity = -vector / 16;
                        d.velocity -= NPC.velocity / 8;
                        d.noLight = true;
                        d.noGravity = true;
                    }
                    // This dust spawn adapted from the Pillar death code in vanilla.
                    for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, 242, 0f, 0f, 0, default(Color), 1f)];
                        dust.position = NPC.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(NPC.width * 1.5f, NPC.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                        dust.velocity.X = 0f;
                        dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + NPC.velocity.Y - 4f) * 3f;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
                    }
                }

                if (NPC.ai[3] >= 480f)
                {
                    for (int d = 0; d < 305; d++)
                    {
                        Dust.NewDust(NPC.Center, 0, 0, 21, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                    }
                    
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TsukiyomiDefeated"));
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    
                    SubworldSystem.Exit();
                    
                    if (modPlayer.tsukiyomiDialogue == 0)
                    {
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The Spatial Disk begins to resonate. Left click to interact."), 241, 255, 180);}
                        modPlayer.tsukiyomiDialogue = 1;
                    }
                   
                    modPlayer.TsukiyomiActive = false;
                    modPlayer.TsukiyomiBarActive = false;
                    modPlayer.undertaleActive = false;
                }
                return;
            }




            introAnimation--;
           
            
            if (introAnimation == 250)
            {
                SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_JustGettingStarted, NPC.Center);
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X + 10, placement2.Y - 60, 0, 0,Mod.Find<ModProjectile>("Phase2Ring").Type,0,0f,Main.myPlayer);}
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}
                //modPlayer.tsukiyomiPrompt5 = true;
                //modPlayer.seenTsukiyomiIntro = true;
            }
            
            if(usedDesperation)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/WickedFlight");
            }
            

            if (NPC.active)
            {
                modPlayer.TsukiyomiLocation = NPC.Center;
                modPlayer.lookAtTsukiyomi = true;

                modPlayer.IsVoidActive = true;
                modPlayer.TsukiyomiActive = true;
                modPlayer.TsukiyomiCastTime = castTime;
                modPlayer.TsukiyomiCastTimeMax = castTimeMax;
                modPlayer.TsukiyomiNextAttack = nextAttack;
                modPlayer.TsukiyomiPhase = alignment;
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                    {



                        player.AddBuff(BuffType<Buffs.SubworldModifiers.MoonTurmoil>(), 300);




                    }


                }
                NPC.netUpdate = true;

                /*for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                    {

                        player.AddBuff(BuffType<Buffs.CosmicIre>(), 2);  //Make sure to replace "buffType" and "timeInFrames" with actual values

                    }


                }*/
            }
            else
            {
                modPlayer.lookAtTsukiyomi = false;

                modPlayer.TsukiyomiActive = false;
                modPlayer.TsukiyomiBarActive = false;
            }
            

            Player P = Main.player[NPC.target];//THIS IS THE BOSS'S MAIN TARGET
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (Main.player[NPC.target].dead)
            {

                if (fightLost == false)
                {
                    SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_JustGiveUp, NPC.Center);
                    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The galaxy stands still once more..."), 210, 60, 60);}
                    Vector2 vector8 = new Vector2(NPC.Center.X, NPC.Center.Y);
                    for (int d = 0; d < 100; d++)
                    {
                        Dust.NewDust(vector8, 0, 0, 269, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 65; d++)
                    {
                        Dust.NewDust(vector8, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(vector8, 0, 0, 50, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(vector8, 0, 0, 55, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                    }
                    fightLost = true;
                }

                NPC.active = false;

                modPlayer.TsukiyomiActive = false;
                modPlayer.TsukiyomiBarActive = false;
                NPC.velocity.Y -= 0.1f;
                NPC.timeLeft = 0;
                if (SubworldSystem.Current != null)
                {
                    //Subworld.Exit(noVote: true);

                }
            }



            if (nextAttack == "Concentrativity" && castTime == 65)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_WarriorStun, NPC.Center);
            }
            if (nextAttack == "Hypertuned Threads of Fate" && castTime == 28)
            {
                for (int d = 0; d < 3; d++)
                {
                    float Speed = 20f;  //projectile speed
                                        //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(500, 600), P.position.Y - 800);
                    Vector2 vector9 = new Vector2(P.position.X - Main.rand.Next(500, 600), P.position.Y - 800);
                    int damage = 30;  //projectile damage
                    int type;

                    type = Mod.Find<ModProjectile>("ThreadsOfFate").Type;



                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f) + Main.rand.Next(-200,200)));
                    float rotation2 = (float)Math.Atan2(vector9.Y - (P.position.Y + (P.height * 0.5f)), vector9.X - (P.position.X + (P.width * 0.5f) + Main.rand.Next(-200, 200)));

                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector9.X, vector9.Y, (float)((Math.Cos(rotation2) * Speed) * -1), (float)((Math.Sin(rotation2) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                }
            }
            if (nextAttack == "Elechos Unforgotten" && castTime == 28)
            {
                for (int d = 0; d < 4000; d += 200)
                {

                    Vector2 placement = new Vector2((P.Center.X) + d, P.position.Y);
                    int type = Mod.Find<ModProjectile>("TsukiyomiStar").Type;


                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1600, placement.Y - 500, 0, 3,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1950, placement.Y - 600, 0, 23,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1900, placement.Y - 700, 0, 13,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2850, placement.Y - 800, 0, 23,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1000, placement.Y - 900, 0, 13,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2950, placement.Y - 1000, 0, 23,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1900, placement.Y - 1100, 0, 13,type,40,0f,Main.myPlayer);}
                    //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1850, placement.Y - 1200, 0, 23,type,40,0f,Main.myPlayer);}
                }
            }

            //Attacks//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ATTACKS
            if (isCasting == true)
            {
                if (isSwinging == true)
                {

                }
                else
                {


                }

                if (!Main.dedServ)
                {
                    
                    if(!NPC.dontTakeDamage)
                    {
                        /*
                        for (int i = 0; i < 2; i++)
                        {
                            // Charging dust
                            Vector2 vector = new Vector2(
                                Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                                Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                            Dust d = Main.dust[Dust.NewDust(
                                NPC.Center + vector, 1, 1,
                                20, 0, 0, 255,
                                new Color(1f, 1f, 1f), 1.5f)];
                            d.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
                            d.velocity = -vector / 16;
                            d.velocity -= NPC.velocity / 8;
                            d.noLight = true;
                            d.noGravity = true;
                        }
                        for (int i = 0; i < 30; i++)
                        {//Circle
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * ((castTimeMax * 5) - (castTime * 5)));
                            offset.Y += (float)(Math.Cos(angle) * ((castTimeMax * 5) - (castTime * 5)));

                            Dust d = Dust.NewDustPerfect(new Vector2(NPC.Center.X + 29,NPC.Center.Y - 43) + offset, 20, NPC.velocity, 20, default(Color), 0.7f);
                            d.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
                            d.fadeIn = 1f;
                            d.noGravity = true;
                        }*/
                        for (int i = 0; i < 5; i++)
                        {//Circle
                            

                            Dust d = Main.dust[Dust.NewDust(new Vector2(NPC.Center.X + 29, NPC.Center.Y - 44), 0, 2, 20, Main.rand.NextFloat(-0.2f,0.2f), Main.rand.NextFloat(-0.5f, -4.5f), 20, default(Color), 0.7f)];
                            d.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
                            d.fadeIn = 1f;
                            d.noGravity = true;
                        }
                    }
                  
                        
                    
                    
                }
                castDelay++;

                if (castDelay >= 2)
                {
                    castDelay = 0;
                    castTime++;
                }
                if (nextAttack == "Ascendance")
                {
                    NPC.dontTakeDamage = true;
                    NPC.life = NPC.lifeMax;
                }
                if (nextAttack == "The Flood Of Light")
                {

                    NPC.life += 1;
                }

                modPlayer.TsukiyomiBarActive = true;
                //Here are the attacks and their effects ///////////////////////////////////////
                if (castTime >= castTimeMax)
                {
                    if (usedDesperation)
                    {
                        for (int d = 0; d < 2000; d += 200)
                        {

                            Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                            int type = Mod.Find<ModProjectile>("TsukiyomiStar").Type;






                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1000, placement.Y - 600, 0, 3,type,100,0f,Main.myPlayer);}

                        }
                    }
                    else
                    {
                        
                    }

                       



                    if (nextAttack == "Aetherial Subduction")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.AddBuff(BuffType<Buffs.Subduced>(), 240);  //Make sure to replace "buffType" and "timeInFrames" with actual values

                        }
                    }
                    if (nextAttack == "Elechos Unforgotten")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 4000; d += 200)
                        {

                            Vector2 placement = new Vector2((P.Center.X) + d, P.position.Y);
                            int type = Mod.Find<ModProjectile>("TsukiyomiStar").Type;


                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2000, placement.Y - 500, 0, 23,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1950, placement.Y - 600, 0, 5,type,40,0f,Main.myPlayer);}
                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1900, placement.Y - 700, 0, 23,type,40,0f,Main.myPlayer);}
                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1850, placement.Y - 800, 0, 13,type,40,0f,Main.myPlayer);}
                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2000, placement.Y - 900, 0, 23,type,40,0f,Main.myPlayer);}
                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1950, placement.Y - 1000, 0, 13,type,40,0f,Main.myPlayer);}
                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1900, placement.Y - 1100, 0, 23,type,40,0f,Main.myPlayer);}
                           // if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1850, placement.Y - 1200, 0, 13,type,40,0f,Main.myPlayer);}
                        }
                    }
                    if (nextAttack == "Coruscant Saber")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Vector2 vector8 = CoruscantSaberSaved;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberDamage").Type,80,0f,Main.myPlayer);}
                        for (int d = 0; d < 100; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 269, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 35; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }
                    }
                    if (nextAttack == "Coruscant Saber II")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Vector2 vector8 = CoruscantSaberSaved;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberDamage").Type,140,0f,Main.myPlayer);}
                        for (int d = 0; d < 100; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 269, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 35; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }
                    }
                    if (nextAttack == "Blotted Whims")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        blazingSkies = 8;

                    }
                    if (nextAttack == "Sanctified Slaughter")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        eyeProjectile = 8;

                    }
                    if (nextAttack == "Perfect Strokes")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        eyeProjectile = 20;

                    }
                    if (nextAttack == "Sanctified Slaughter II")
                    {


                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        float Speed = 8f;  //projectile speed
                                           //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X - 1200, P.position.Y);
                        int damage = 90;  //projectile damage
                        int type;
                        SoundEngine.PlaySound(SoundID.Roar, vector8);
                        type = Mod.Find<ModProjectile>("PlanteraProjectile").Type;



                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        //eyeProjectile = 8;

                    }
                    if (nextAttack == "Thousand Strikes")
                    {


                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;

                        //eyeProjectile = 8;

                    }
                    if (nextAttack == "Thousand Strikes ")
                    {


                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;

                        //eyeProjectile = 8;

                    }
                    if (nextAttack == "Blazing Skies II")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        blazingSkies = 20;

                    }
                    if (nextAttack == "Ablation")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        eyeProjectile = 10;

                    }
                    if (nextAttack == "The Bitter End")
                    {
                        isSwinging = false;
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        swingAnimation = 0;
                        float Speed = 10f;  //projectile speed
                        Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + 300);
                        int damage = 50 + (phase * 50);  //projectile damage
                        int type = Mod.Find<ModProjectile>("TheBitterEnd").Type;

                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}


                    }
                    //Phase 2 attacks
                    if (nextAttack == "Synaptic Static")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Vector2 vector8 = SolemnConfiteorSaved;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SolemnConfiteorDamage").Type,400,0f,Main.myPlayer);}
                        for (int d = 0; d < 200; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 86, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 65; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 90, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 35; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 186, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 35; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 179, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }
                        /*
                        float Speed = 10f;  //projectile speed
                        Vector2 vector8 = new Vector2(npc.position.X , npc.position.Y);
                        int damage = 100;  //projectile damage
                        int type = 658;

                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}*/


                    }
                    if (nextAttack == "Absolute Fire III")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = -200;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.AddBuff(BuffType<Buffs.Pyretic>(), 180);  //Make sure to replace "buffType" and "timeInFrames" with actual values

                        }


                    }
                    if (nextAttack == "Absolute Blizzard III")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = -200;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.AddBuff(BuffType<Buffs.DeepFreeze>(), 180);  //Make sure to replace "buffType" and "timeInFrames" with actual values

                        }

                    }
                    if (nextAttack == "Terror Unleashed")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active)
                            {
                                player.AddBuff(BuffID.WitheredArmor, 600);
                            }

                        }

                    }
                    if (nextAttack == "Total Isolation")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active)
                            {
                                player.AddBuff(BuffID.Obstructed, 240);
                            }

                        }

                    }
                    if (nextAttack == "A Dash of Chaos")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active)
                            {
                                int randonBuff = Main.rand.Next(0, 2);
                                if (randonBuff == 0)
                                {
                                    player.AddBuff(BuffType<Buffs.LeftDebuff>(), 240);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (randonBuff == 1)
                                {
                                    player.AddBuff(BuffType<Buffs.RightDebuff>(), 240);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                            }

                        }

                    }
                    if (nextAttack == "Ink Over")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You've been colored with ink!"), 210, 60, 60);}
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active)
                            {
                                int index = player.FindBuffIndex(BuffType<RedPaint>());
                                if (index > -1)
                                {
                                    player.DelBuff(index);
                                }
                                int index2 = player.FindBuffIndex(BuffType<BluePaint>());
                                if (index2 > -1)
                                {
                                    player.DelBuff(index2);

                                }
                                int index3 = player.FindBuffIndex(BuffType<YellowPaint>());
                                if (index3 > -1)
                                {
                                    player.DelBuff(index3);
                                }

                                int colorApplied = Main.rand.Next(0, 3);
                                if (colorApplied == 0)
                                {
                                    player.AddBuff(BuffType<Buffs.RedPaint>(), 1800);
                                    Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                    CombatText.NewText(textPos, new Color(255, 0, 0, 240), $"Covered in red paint!", false, false);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 219, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (colorApplied == 1)
                                {
                                    player.AddBuff(BuffType<Buffs.BluePaint>(), 1800);
                                    Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                    CombatText.NewText(textPos, new Color(0, 0, 255, 240), $"Covered in blue paint!", false, false);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 221, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (colorApplied == 2)
                                {
                                    player.AddBuff(BuffType<Buffs.YellowPaint>(), 1800);
                                    Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                    CombatText.NewText(textPos, new Color(255, 255, 0, 240), $"Covered in yellow paint!", false, false);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 222, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }

                            }
                            
                        }

                    }
                    if (nextAttack == "Pandaemonium")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                       // if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Reality tears at the seams!"), 210, 60, 60);}
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active)
                            {
                                //player.AddBuff(164, 1800);
                                player.AddBuff(BuffType<Buffs.Vulnerable>(), 600);
                                

                            }

                        }
                        desperadoShots = 2;
                    }
                    if (nextAttack == "Suppuration")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                        Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y);
                        
                        
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 200, placement.Y + 200, 0, 0,Mod.Find<ModProjectile>("ChaosBlaster").Type,0,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X + 200, placement.Y + 200, 0, 0,Mod.Find<ModProjectile>("ChaosBlaster2").Type,0,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 200, placement.Y - 200, 0, 0,Mod.Find<ModProjectile>("ChaosBlaster").Type,0,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X + 200, placement.Y - 200, 0, 0,Mod.Find<ModProjectile>("ChaosBlaster2").Type,0,0f,Main.myPlayer);}





                    }
                    if(nextAttack == "Macrocosmos")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                        Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y);


                        
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,Mod.Find<ModProjectile>("BulletWormhole2").Type,0,0f,Main.myPlayer);}
                        //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,mod.ProjectileType("Wormhole"),0,0f,Main.myPlayer);}





                    }
                    if (nextAttack == "Microcosmos")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                        Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y);



                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,Mod.Find<ModProjectile>("BulletWormhole3").Type,0,0f,Main.myPlayer);}
                        //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,mod.ProjectileType("Wormhole"),0,0f,Main.myPlayer);}





                    }
                    if (nextAttack == "Gravitational Anomaly")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active)
                            {

                                player.AddBuff(164, 500);
                            }

                        }

                    }
                    if (nextAttack == "Ars Laevateinn")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        ArsLaevateinnActive = true;

                    }
                    if (nextAttack == "Blade of the End")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                        Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y);

                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                            {
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;
                                //player.AddBuff(BuffType<DownForTheCount>(), 240);
                            }

                        }



                        float Speed = 24f;  //projectile speed
                                            //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                        int damage = 40;  //projectile damage
                        int type = Mod.Find<ModProjectile>("BossTheofania").Type;

                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, NPC.Center);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}




                    }
                    
                    if (nextAttack == "Painted Attendants")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X, (int)NPC.Center.Y, NPCType<NPCs.PaintedAttendantA>(), NPC.whoAmI);
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X, (int)NPC.Center.Y, NPCType<NPCs.PaintedAttendantB>(), NPC.whoAmI);

                    }
                    if (nextAttack == "Aegis of Frost")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        
                        isInvincible = true;

                    }
                    if (nextAttack == "Absolute Thunder IV")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = -200;
                        eyeProjectile = 3;

                    }
                    //Phase 3 Attacks
                    if (nextAttack == "Quintuplecast")
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LimitBreakActive"));
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = -200;
                        eyeProjectile = 4;
                        QuintuplecastSkies = 5;

                    }
                    if (nextAttack == "Absolute Holy")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        if (Main.netMode != 1)
                        {
                            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                            Vector2 vel = new Vector2(-1, -1);
                            vel *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel2 = new Vector2(1, 1);
                            vel2 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel2.X, vel2.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel3 = new Vector2(1, -1);
                            vel3 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel3.X, vel3.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel4 = new Vector2(-1, 1);
                            vel4 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel4.X, vel4.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel5 = new Vector2(0, -1);
                            vel5 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel5.X, vel5.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel6 = new Vector2(0, 1);
                            vel6 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel6.X, vel6.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel7 = new Vector2(1, 0);
                            vel7 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel7.X, vel7.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel8 = new Vector2(-1, 0);
                            vel8 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel8.X, vel8.Y, 348, 60, 0, Main.myPlayer);}
                        }
                    }
                    if (nextAttack == "Crack The Sky")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        if (Main.netMode != 1)
                        {
                            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                            Vector2 vel = new Vector2(-1, -1);
                            vel *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel2 = new Vector2(1, 1);
                            vel2 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel2.X, vel2.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel3 = new Vector2(1, -1);
                            vel3 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel3.X, vel3.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel4 = new Vector2(-1, 1);
                            vel4 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel4.X, vel4.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel5 = new Vector2(0, -1);
                            vel5 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel5.X, vel5.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel6 = new Vector2(0, 1);
                            vel6 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel6.X, vel6.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel7 = new Vector2(1, 0);
                            vel7 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel7.X, vel7.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel8 = new Vector2(-1, 0);
                            vel8 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel8.X, vel8.Y, 465, 60, 0, Main.myPlayer);}

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                    }
                    if (nextAttack == "Hypertuned Threads of Fate")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;

                    }
                    if (nextAttack == "Linear Mystics")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;

                        for (int i = 0; i < 15; i++)
                        {
                            float Speed = Main.rand.NextFloat(4, 15);  //projectile speed
                                                                       //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 500);
                            int damage = 0;  //projectile damage
                            int type = Mod.Find<ModProjectile>("Geometry").Type;

                            float rotation = (float)Math.Atan2(-70, 0);

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }

                    }
                    if (nextAttack == "Overflow Error")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;

                        for (int i = 0; i < 25; i++)
                        {
                            float Speed = Main.rand.NextFloat(4, 15);  //projectile speed
                                                                       //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 500);
                            int damage = 0;  //projectile damage
                            int type = Mod.Find<ModProjectile>("Entropy").Type;

                            float rotation = (float)Math.Atan2(-70, 0);

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }

                    }
                    if (nextAttack == "Rend Heaven")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, NPC.Center);

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }

                    }
                    if (nextAttack == "Splattered Sundering")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_splat, NPC.Center);
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }

                    }
                    if (nextAttack == "To The Limit")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakCharge, NPC.Center);
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int d = 0; d < 45; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                    }
                    if (nextAttack == "Hypertuned Titanomachy")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_TitanCast, NPC.Center);
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int d = 0; d < 45; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                    }
                    if (nextAttack == "SOUL Extraction")
                    {

                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LimitBreakCharge"));
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 400;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {

                            Player player = Main.player[i];


                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                undertaleActive = true;
                            modPlayer.undertalePrep = true;

                        }

                        undertaleTimer = 600;

                        for (int d = 0; d < 45; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                    }
                    if (nextAttack == "Light Rampant")
                    {

                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LimitBreakCharge"));
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 400;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {

                            Player player = Main.player[i];


                            //if (player.active && modPlayer.inTsukiyomiFightTimer > 0)


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-1, 1));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.CultistBossIceMist, 40, 0, 0, NPC.whoAmI, 1);}
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-18, 18));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.FrostShard, 40, 0, 0, NPC.whoAmI, 1);}
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.FrostWave, 40, 0, 0, NPC.whoAmI, 1);}
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-8, 8));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.CultistBossFireBall, 40, 0, 0, NPC.whoAmI, 1);}
                        }
                        for (int d = 0; d < 45; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }

                    }
                    if (nextAttack == "Radiant Braver")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;


                    }
                    if (nextAttack == "Radiant Desperado")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        desperadoShots = 30;

                    }
                    if (nextAttack == "Meteor Shower")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }


                        for (int i = 0; i < 20; i++)
                        {
                            // Random upward vector.
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-16, 16), Main.rand.NextFloat(-9, -20));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.DD2BetsyFireball, 40, 0, 0, NPC.whoAmI, 1);}
                        }

                    }
                    if (nextAttack == "Malefic IV")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }



                        for (int i = 0; i < 16; i++)
                        {
                            float Speed = Main.rand.NextFloat(2, 8);  //projectile speed
                                                                      //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-800, 800), P.position.Y - 500);
                            int damage = 35;  //projectile damage
                            int type = ProjectileType<TsukiyomiStar>();

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }

                        float Speed1 = 6f;  //projectile speed
                                           //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector81 = new Vector2(P.position.X - 800, P.position.Y - 800);
                        Vector2 vector82 = new Vector2(P.position.X + 800, P.position.Y - 800);
                        int damage1 = 95;  //projectile damage
                        int type1 = Mod.Find<ModProjectile>("SpatialRip").Type;

                        float rotation1 = (float)Math.Atan2(vector81.Y - (P.position.Y + (P.height * 0.5f)), vector81.X - (P.position.X + (P.width * 0.5f)));
                        float rotation2 = (float)Math.Atan2(vector82.Y - (P.position.Y + (P.height * 0.5f)), vector82.X - (P.position.X + (P.width * 0.5f)));
                        SoundEngine.PlaySound(SoundID.Shatter, vector81);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector81.X, vector81.Y, (float)((Math.Cos(rotation1) * Speed1) * -1), (float)((Math.Sin(rotation1) * Speed1) * -1),type1,damage1,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector82.X, vector82.Y, (float)((Math.Cos(rotation2) * Speed1) * -1), (float)((Math.Sin(rotation2) * Speed1) * -1),type1,damage1,0f,Main.myPlayer);}

                    }
                    if (nextAttack == "Quasar Forthcoming")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }



                        for (int i = 0; i < 45; i++)
                        {
                            float Speed = Main.rand.NextFloat(3, 5);  //projectile speed
                                                                      //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-800, 800), P.position.Y - 500);
                            int damage = 60;  //projectile damage
                            int type = ProjectileID.CultistBossFireBallClone;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }

                    }
                    if (nextAttack == "Spilled Violet")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                            {

                            }
                            //player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PhaseChange"));
                        QuintuplecastSkies = 80;




                    }
                    if (nextAttack == "Starfield")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        
                        blazingSkies = 30;



                    }
                    if (nextAttack == "Theofania Inanis")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }

                        float Speed = 24f;  //projectile speed
                                            //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                        int damage = 60;  //projectile damage
                        int type = Mod.Find<ModProjectile>("BossTheofania").Type;

                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, NPC.Center);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}


                    }
                    if (nextAttack == "Anosios Triumverate")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        /*for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }*/

                        float Speed = 10f;  //projectile speed
                                                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X, P.position.Y - 500);
                        int damage = 0;  //projectile damage
                        int type = Mod.Find<ModProjectile>("PlusPlanet").Type;

                        float rotation = (float)Math.Atan2(-70, 0);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X - 250, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X + 250, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}


                    }
                    if (nextAttack == "Thousand Blades")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        /*for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }*/

                        float Speed = 10f;  //projectile speed
                                            //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X, P.position.Y - 500);
                        int damage = 0;  //projectile damage
                        int type = Mod.Find<ModProjectile>("PlusPlanet").Type;

                        float rotation = (float)Math.Atan2(-70, 0);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}


                    }
                    if (nextAttack == "Spatial Rip")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;


                        float Speed = 4f;  //projectile speed
                                           //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-800, 800), P.position.Y - 800);
                        int damage = 60;  //projectile damage
                        int type = Mod.Find<ModProjectile>("SpatialRip").Type;

                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        SoundEngine.PlaySound(SoundID.Shatter, vector8);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}

                    }
                    if (nextAttack == "Gateway")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";

                        NPC.dontTakeDamage = true;
                        /* for (int d = 0; d < 105; d++)
                         {
                             Dust.NewDust(npc.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                         }
                         for (int d = 0; d < 105; d++)
                         {
                             Dust.NewDust(npc.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                         }*/

                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X + 30, placement2.Y - 80, 0, 0,Mod.Find<ModProjectile>("Wormhole").Type,0,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y - 33, 0, 0,Mod.Find<ModProjectile>("TsukiyomiTeleport").Type,0,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}


                    }
                    if (nextAttack == "Recall")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        NPC.dontTakeDamage = false;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("reverseRadiate").Type,0,0f,Main.myPlayer);}

                    }
                    //Here are special phase changing attacks
                    if (nextAttack == "Ascendance")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.dontTakeDamage = false;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }


                    }
                    if (nextAttack == "Relinquish")
                    {
                        nebula = false;
                        vortex = false;
                        solar = false;
                        stardust = false;
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }


                    }
                    if (nextAttack == "Total Transpose")
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PrepDarkness"));
                        //npc.dontTakeDamage = false;
                        alignment = 2;
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                            {
                                Vector2 newPosition = player.position;
                                newPosition.X += 2500;


                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;
                                player.Teleport(newPosition, 1, 0);
                                NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);
                            }


                        }
                        if(!Main.dedServ)
                        {
                            NPC.position.X += 2500;
                        }
                        phase = 3;

                        NPC.dontTakeDamage = false;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("reverseRadiate").Type,0,0f,Main.myPlayer);}

                    }
                    if (nextAttack == "Galaxias")
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PrepDarkness"));
                        //npc.dontTakeDamage = false;
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                       
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                       

                    }
                    if (nextAttack == "Order through Chaos")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);

                        alignment = 1;
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiateOrder").Type,0,0f,Main.myPlayer);}
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }

                    }
                    if (nextAttack == "Chaos through Order")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiateChaos").Type,0,0f,Main.myPlayer);}
                        alignment = 0;
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }

                    }
                    if (nextAttack == "Starlit Channeling")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);

                        stardust = true;
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }


                    }
                    
                    if (nextAttack == "Concentrativity")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        stunCasted = true;

                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.AddBuff(BuffType<Buffs.DownForTheCount>(), 680);  //


                        }
                        phase = 2;

                    }
                    if (nextAttack == "A World Rent Asunder")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_PhaseChange, NPC.Center);
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }
                        NPC.dontTakeDamage = false;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }


                    }
                    if (nextAttack == "Surpassing Infinity")
                    {

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_PhaseChange, NPC.Center);
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inTsukiyomiFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }
                        NPC.dontTakeDamage = false;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }


                    }
                    if (nextAttack == "The Flood Of Light")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;

                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(NPC.velocity.X, NPC.velocity.Y).RotatedByRandom(MathHelper.ToRadians(40));
                            int type = Main.rand.Next(new int[] { ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 1, 1, NPC.whoAmI);}
                        }
                        modPlayer.LostToTsukiyomi = true;
                        for (int d = 0; d < 305; d++)
                        {
                            Dust.NewDust(NPC.position, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }

                    }

                }
            }
            else
            {
                modPlayer.TsukiyomiBarActive = false;
            }
           
            if (phase == 3)
            {
                //Main.monolithType = 1;
                NPC.ai[1]++;
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheoryOfBeauty");

            }
            else
            {
                //mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheExtreme");
            }    
            undertaleTimer--;
            if (undertaleTimer <= 0)
            {
                undertaleActive = false;
            }
            blazingSkiesTimer++;
            if (blazingSkies > 0)
            {
                if (phase != 3)
                {
                    //npc.ai[1] = 400;
                }

               
                if (phase > 1 && (phase != 3))
                {
                    //blazingSkiesTimer++;
                }
            }
            if (blazingSkiesTimer >= 120)
            {



                float Speed = 1f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                Vector2 vector9 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type = Mod.Find<ModProjectile>("Starmatter").Type;



                
                
                    
                
                

                vector8 = new Vector2(P.position.X - 1000, P.position.Y - Main.rand.Next(-600, 600));
                vector9 = new Vector2(P.position.X + 1000, P.position.Y - Main.rand.Next(-600, 600));


                float rotation = (float)Math.Atan2(0, vector8.X - (P.position.X + (P.width * 0.5f)));
                float rotation2 = (float)Math.Atan2(0, vector9.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector9.X, vector9.Y, (float)((Math.Cos(rotation2) * Speed) * -1), (float)((Math.Sin(rotation2) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                blazingSkiesTimer = 0;
                blazingSkies--;


            }
            if (strayManaTimer >= 2)
            {
                int k = Item.NewItem(NPC.GetSource_FromAI(),(int)NPC.position.X + Main.rand.Next(-480, 480), (int)NPC.position.Y + Main.rand.Next(-280, 280), NPC.width, NPC.height, Mod.Find<ModItem>("StrayMana").Type, 1, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, k, 1f);
                }
                strayManaTimer = 0;
            }
            if (eyeProjectile > 0)
            {
                //npc.ai[1] = 300;
                eyeProjectileTimer++;

            }
            if (eyeProjectileTimer >= 120)
            {
                float Speed = 3f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-1200, 1200), P.position.Y - 800);
                int damage = 40;  //projectile damage
                int type = Mod.Find<ModProjectile>("Starmatter").Type;
                

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                //SoundEngine.PlaySound(SoundID.Item, vector8);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                eyeProjectileTimer = 0;
                eyeProjectile--;
            }

            if (QuintuplecastSkies > 0)
            {
                
                QuintuplecastTimer++;

            }
            if (QuintuplecastTimer >= 20)
            {
                float Speed2 = Main.rand.NextFloat(3, 10);  //projectile speed
                                                            //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector82 = new Vector2(P.position.X + Main.rand.Next(-200, 200), P.position.Y - 800);
                int damage2 = 55;  //projectile damage
                int type2 = Mod.Find<ModProjectile>("BossTheofania").Type;

                float rotation2 = (float)Math.Atan2(-70, 0);

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector82.X, vector82.Y, (float)((Math.Cos(rotation2) * Speed2) * -1), (float)((Math.Sin(rotation2) * Speed2) * -1),type2,damage2,0f,Main.myPlayer);}



                QuintuplecastTimer = 0;
                QuintuplecastSkies--;
            }
            if (desperadoShots > 0)
            {
                
                desperadoTimer++;

            }
            if (desperadoTimer >= 8)
            {
                float Speed = 8f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-1200, 1200), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type = Mod.Find<ModProjectile>("BulletWormhole2").Type;


                float rotation = (float)Math.Atan2(0, 0);
                //SoundEngine.PlaySound(SoundID.Item, vector8);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}

                desperadoTimer = 0;
                desperadoShots--;
            }


            NPC.ai[1] += 2;
            
            if(inIntro)
            {
                NPC.ai[1] = 0;
                NPC.dontTakeDamage = true;
            }
            if(introAnimation <= 0 && inIntro)
            {
                inIntro = false;
                NPC.dontTakeDamage = false;
            }
            if (NPC.ai[1] >= 0)
            {
                if (fightStart == true)
                {
                    inIntro = true;
                    introAnimation = 500;
                    
                    Vector2 initialMoveTo = NPC.Center + new Vector2(-75f, -900);
                    NPC.position = initialMoveTo;

                    if (!Main.dedServ)
                    {

                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                    }
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LongHaveWeWaited"));
                   



                    fightStart = false;
                }
            }
            introVelocityTimer++;
            if (introVelocityY > 0)
            {
                if (introVelocityTimer >= 15)
                {
                    NPC.velocity = new Vector2(0, (introVelocityY));
                    introVelocityY--;
                    introVelocityTimer = 0;
                }
                if (!Main.dedServ)
                {

                    Vector2 bossPosition = new Vector2(NPC.Center.X, NPC.Center.Y + 150);
                    //Dust.NewDust(bossPosition, 0, 0, 269, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);

                }
            }
            else
            {
                
                NPC.velocity = Vector2.Zero;
                
            }


            if (phase > 1)
            {
                NPC.ai[1] += 2;
            }
            int hpThreshold;
            
            //Attack generation
           
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                hpThreshold = 450000;
            }
            else
            {
                hpThreshold = 200000;
            }
            
            //Attack generation
            if (NPC.ai[1] >= 500)//DEBUG IT SHOULD BE 500
            {

                NPC.netUpdate = true;

                if (!isCasting)
                {


                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (phase != 3)
                    {

                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{npc.ai[2]}"), 210, 60, 60);}
                        //Boss Rotation
                        if (NPC.ai[2] == 0)
                        {
                            nextCast = "Macrocosmos";// Star attack
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Anosios Triumverate";// Planet attack
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "Elechos Unforgotten";//
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "Pandaemonium";//
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Hypertuned Titanomachy";
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Malefic IV";//Star attack
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Elechos Unforgotten";
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Blade of the End";//Rain of light
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "Hypertuned Titanomachy";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "Anosios Triumverate";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Microcosmos";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Hypertuned Threads of Fate";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Elechos Unforgotten";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Macrocosmos";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Hypertuned Threads of Fate";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Elechos Unforgotten";
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Blade of the End";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "Malefic IV";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "Anosios Triumverate";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "Aetherial Subduction";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Anosis Triumverate";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Malefic IV";

                        }
                        if (NPC.ai[2] == 22)
                        {
                            nextCast = "Microcosmos";
                        }
                        if (NPC.ai[2] == 23)
                        {
                            nextCast = "Elechos Unforgotten";
                        }
                        if (NPC.ai[2] == 24)
                        {
                            nextCast = "Microcosmos";//
                        }
                        if (NPC.ai[2] == 25)
                        {
                            nextCast = "Hypertuned Titanomachy";
                        }
                        if (NPC.ai[2] == 26)
                        {
                            nextCast = "Hypertuned Threads of Fate";
                        }
                        if (NPC.ai[2] == 27)
                        {
                            nextCast = "Elechos Unforgotten";
                        }
                        if (NPC.ai[2] == 28)
                        {
                            nextCast = "Blade of the End";
                        }
                        if (NPC.ai[2] == 29)
                        {
                            nextCast = "Malefic IV";
                        }
                        if (NPC.ai[2] == 30)
                        {
                            nextCast = "Anosios Triumverate";
                        }
                        if (NPC.ai[2] == 31)
                        {
                            nextCast = "Aetherial Subduction";
                        }
                        if (NPC.ai[2] == 32)
                        {
                            nextCast = "Pandaemonium";
                        }
                        if (NPC.ai[2] == 33)
                        {
                            nextCast = "Malefic IV";

                            NPC.ai[2] = 0;

                        }
                        if (NPC.life < hpThreshold && !usedDesperation)//Final phase threshold
                        {
                            castAnimation = 70;
                            nextCast = "";

                            //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("BossLaevateinn"), 0, 4, npc.whoAmI, 0, 1);}
                            usedDesperation = true;
                            if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Cosmic energy begins to coalese around you!"), 11, 241, 158);}
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EnterDarkness"));
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_IRefuseToLetYouWin, NPC.Center);
                            castDelay = 0;
                            phaseTransition = true;
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                            nextAttack = "Galaxias";
                            NPC.netUpdate = true;
                            //npc.ai[2] = 0;


                        }
                        //End of Rotation
                        if (nextCast == "Blade of the End")
                        {

                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_YouAreNothing, NPC.Center);
                            modPlayer.tsukiyomiPrompt6 = true;
                            castDelay = 0;
                            nextAttack = "Blade of the End";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        
                        if (nextCast == "Macrocosmos")
                        {

                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_HaveToTryHarder, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Macrocosmos";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Microcosmos")
                        {

                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_HaveToTryHarder, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Microcosmos";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Pandaemonium")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_DeathOfAThousandStars, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Pandaemonium";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Painted Attendants")
                        {

                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WithHaste"));
                            castDelay = 0;
                            nextAttack = "Painted Attendants";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Anosios Triumverate")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_Struggle, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Anosios Triumverate";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Relinquish")
                        {

                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fools"));
                            castDelay = 0;
                            nextAttack = "Relinquish";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Hypertuned Titanomachy")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_TearYouApart, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Hypertuned Titanomachy";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                            int nextType = 0;
                            for (int d = 0; d < 5040; d += 420)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                if (nextType == 1)
                                {
                                    type = Mod.Find<ModProjectile>("Titanomachy1").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                                }
                                if (nextType == 2)
                                {
                                    type = Mod.Find<ModProjectile>("Titanomachy2").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                                    nextType = 0;
                                }


                                nextType++;
                            }
                        }
                        
                        if (nextCast == "Blotted Whims")
                        {
                            castAnimation = 70;


                            castDelay = 0;
                            nextAttack = "Blotted Whims";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Gateway")
                        {
                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_IWillBridgeTheGap, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Gateway";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Recall")
                        {
                            castAnimation = 70;


                            castDelay = 0;
                            nextAttack = "Recall";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Perfect Strokes")
                        {
                            castAnimation = 70;
                            castDelay = 0;
                            nextAttack = "Perfect Strokes";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                        }
                        if (nextCast == "Coruscant Saber II")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ComeShowMeMore"));
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberAOE2").Type,0,0f,Main.myPlayer);}
                            CoruscantSaberSaved = vector8;
                            nextAttack = "Coruscant Saber II";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Synaptic Static")
                        {
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheHeartsOfMen"));
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("ShadowbladeAOE").Type,0,0f,Main.myPlayer);}
                            SolemnConfiteorSaved = vector8;
                            nextAttack = "Synaptic Static";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }

                        if (nextCast == "Starfield")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_YouCantKeepDodging, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Starfield";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                        }
                        if (nextCast == "Galactic Swarm")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_YouCantKeepDodging, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Galactic Swarm";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Splattered Sundering")
                        {
                            castAnimation = 70;


                            castDelay = 0;
                            nextAttack = "Splattered Sundering";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Aetherial Subduction")
                        {
                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_AreYouGettingTired, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Aetherial Subduction";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Elechos Unforgotten")
                        {
                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_ToDustYouWillReturn, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Elechos Unforgotten";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "A Dash of Chaos")
                        {
                            castAnimation = 70;


                            castDelay = 0;
                            nextAttack = "A Dash of Chaos";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Meteor Shower")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                            castDelay = 0;
                            nextAttack = "Meteor Shower";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "Spatial Rip")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                            castDelay = 0;
                            nextAttack = "Spatial Rip";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Ablation")
                        {
                            castAnimation = 70;
                           // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ABlightTakesThisLand"));
                            castDelay = 0;
                            nextAttack = "Ablation";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Gravitational Anomaly")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_JustGiveUp, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Gravitational Anomaly";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "The Garden of Avalon")
                        {

                            castAnimation = 70;
                            castDelay = 0;
                            nextAttack = "The Garden of Avalon";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Theofania Inanis")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                            castDelay = 0;
                            nextAttack = "Theofania Inanis";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Spilled Violet")
                        {
                            castAnimation = 70;
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Spilled Violet";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Order through Chaos")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TsukiyomiLaugh" + Main.rand.Next(1, 5)));
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Order through Chaos";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Starlit Channeling")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Starlit Channeling";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Chaos through Order")
                        {
                            castAnimation = 70;
                           // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TsukiyomiLaugh" + Main.rand.Next(1, 5)));
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Chaos through Order";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Nebula Channeling")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Nebula Channeling";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Quasar Forthcoming")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Quasar Forthcoming";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Sanctified Slaughter II")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Sanctified Slaughter II";
                            castTime = 0;
                            castTimeMax = 250;
                            isCasting = true;
                        }
                        
                        if (nextCast == "Malefic IV")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_ThereIsNowhereYouCanRun, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Malefic IV";
                            castTime = 0;
                            castTimeMax = 90;
                            isCasting = true;
                        }
                        if (nextCast == "Total Isolation")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TsukiyomiLaugh" + Main.rand.Next(1, 5)));
                            //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RefulgentEther"));
                            castDelay = 0;
                            nextAttack = "Total Isolation";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Hypertuned Threads of Fate")
                        {
                            castAnimation = 70;

                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TsukiyomiVoiceLines/InControl"));

                            castDelay = 0;
                            nextAttack = "Hypertuned Threads of Fate";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Rend Heaven")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RuinationIsCome"));
                            castDelay = 0;
                            nextAttack = "Rend Heaven";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            for (int d = 0; d < 3500; d += 500)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                type = Mod.Find<ModProjectile>("RendHeaven").Type;
                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                            }
                        }
                        if (nextCast == "Splattered Sundering")
                        {
                            castAnimation = 70;

                            castDelay = 0;
                            nextAttack = "Splattered Sundering";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            int nextType = 0;
                            for (int d = 0; d < 5040; d += 420)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                if (nextType == 1)
                                {
                                    type = Mod.Find<ModProjectile>("RedSplatter").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                                }
                                if (nextType == 2)
                                {
                                    type = Mod.Find<ModProjectile>("BlueSplatter").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                                }
                                if (nextType == 3)
                                {
                                    type = Mod.Find<ModProjectile>("YellowSplatter").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                                    nextType = 0;
                                }

                                nextType++;
                            }
                        }
                        if (!isCasting)
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fools"));
                            castDelay = 0;
                            nextAttack = "Hypertuned Threads of Fate";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }
                    // Phase 2 ///////////////////////////////////////////////////////////////////////////////////////////// (phase 2 is phase change, this is technically phase 3.)
                    
                    
                    
                    NPC.netUpdate = true;




                    // Special attacks /////////////////////////////////////////////////////////////////////////////////////////////
                    if (NPC.life < hpThreshold && !usedDesperation)
                    {
                       
                        

                    }


                    NPC.ai[2]++;



                }


                NPC.ai[1] = 0;


            }
            NPC.netUpdate = true;

        }


    }
}