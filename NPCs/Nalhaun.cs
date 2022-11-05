using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;
using StarsAbove.Items.BossBags;

namespace StarsAbove.NPCs
{
    [AutoloadBossHead]
    public class Nalhaun : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
            // DisplayName.SetDefault("Example Person");
            Main.npcFrameCount[NPC.type] = 12;
            DisplayName.SetDefault("Nalhaun, The Burnished King");
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Position = new Vector2(0, -20),
                //Scale = 0.9f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = -30f,
                //PortraitPositionXOverride = -30f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetDefaults()
        {
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                NPC.lifeMax = 240000;
                NPC.defense = 25;
            }
            else
            {
                NPC.lifeMax = 84000;
                NPC.defense = 15;
            }
           
            NPC.boss = true;
            NPC.aiStyle = 0;
            
            NPC.damage = 0;
            
            NPC.knockBackResist = 0f;
            NPC.width = 240;
            NPC.height = 240;
            NPC.scale = 1.2f;
            NPC.value = Item.buyPrice(0, 1, 75, 45);
            NPC.alpha = 0;
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.buffImmune[24] = true;
            NPC.dontTakeDamage = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };

            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ShadowsCastByTheMighty");
            //Music =  mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShadowsCastByTheMighty");

            NPC.netAlways = true;
            NPC.behindTiles = true;
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

        bool phaseTransition;

        public bool isInvincible;
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<NalhaunBossBag>()));

            // Trophies are spawned with 1/10 chance
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.NalhaunTrophyItem>(), 10));

            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.NalhaunBossRelicItem>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            // Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
            // Boss masks are spawned with 1/7 chance
            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.BurnishedPrism>(), 4));

            // This part is not required for a boss and is just showcasing some advanced stuff you can do with drop rules to control how items spawn
            // We make 12-15 ExampleItems spawn randomly in all directions, like the lunar pillar fragments. Hereby we need the DropOneByOne rule,
            // which requires these parameters to be defined
            /*int itemType = ModContent.ItemType<Items.Materials.EnigmaticDust>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 12,
                MaximumItemDropsCount = 15,
            };

            notExpertRule.OnSuccess(new DropOneByOne(itemType, parameters));*/

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
            
            
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedNalhaun, -1);
            DownedBossSystem.downedNalhaun = true;
            if (Main.netMode == NetmodeID.Server)
            {
               NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }
            
           
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.defense += numPlayers * 10;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = 0;
            NPC.spriteDirection = 0;
            swingAnimation--;
            castAnimation--;
            if(!teleportAway)
            {
                introAnimation -= 5;
            }
            if(introAnimation < -10)
            {
                introAnimation = -10;
            }
            
            
            if(inIntro || teleportAway)
            {
                if (introAnimation < 800 && introAnimation > 100)
                    NPC.frame.Y = 0 * frameHeight;
                if (introAnimation < 80 && introAnimation > 60)
                    NPC.frame.Y = 1 * frameHeight;
                if (introAnimation < 60 && introAnimation > 40)
                    NPC.frame.Y = 2 * frameHeight;
                if (introAnimation < 40 && introAnimation > 20)
                    NPC.frame.Y = 3 * frameHeight;
                if (introAnimation < 20)
                    NPC.frame.Y = 4 * frameHeight;
                if (introAnimation < 0 && NPC.velocity == Vector2.Zero)
                    inIntro = false;
            }
            else
            {
                if (isCasting || isSwinging)
                {

                    if (castAnimation < 70)
                        NPC.frame.Y = 6 * frameHeight;
                    if (castAnimation < 60)
                        NPC.frame.Y = 7 * frameHeight;
                    if (castAnimation < 50)
                        NPC.frame.Y = 8 * frameHeight;
                    if (castAnimation < 40)
                        NPC.frame.Y = 9 * frameHeight;
                    if (castAnimation < 30)
                        NPC.frame.Y = 10 * frameHeight;
                    if (castAnimation < 20)
                        NPC.frame.Y = 11 * frameHeight;

                }
                else
                {
                    NPC.frame.Y = 5 * frameHeight;
                }
               
            }
               
                
            
            



            // if (isSwinging)

        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }
        

        public override bool CheckDead()
        {
            if (phase == 1 )
            {
                NPC.life = 1;
                return false;
            }
            if (NPC.ai[3] == 0f)
            {
                
                
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
                Music =  MusicLoader.GetMusicSlot(Mod,  "Sounds/Music/silence");
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
                    SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_NalhaunDeathQuote, NPC.Center);
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/NalhaunDefeated"));
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    if (!DownedBossSystem.downedNalhaun)
                    {
                       DownedBossSystem.downedNalhaun = true;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                        }
                    }
                    modPlayer.NalhaunActive = false;
                    modPlayer.NalhaunBarActive = false;
                    modPlayer.undertaleActive = false;
                }
                return;
            }

            
            
            if(teleportAway)
            {
                introAnimation+=5;
                if(introAnimation >= 200)
                {
                    introAnimation = 200;
                }
                NPC.dontTakeDamage = true;
                //Main.monolithType = 1;
            }
            else
            {
               
            }
           
            if (inIntro || phaseTransition)
            {
                //npc.ai[1] = 200;
                NPC.dontTakeDamage = true;
            }
            else
            {
                if(!teleportAway)
                NPC.dontTakeDamage = false;
            }
            if (phase > 1)
            {
                
                NPC.defense = 20;
                if (phase == 2)
                {
                   
                    Music =  MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheMightOfTheHellblade");
                    
                    //Music =  mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/TheMightOfTheHellblade");
                    
                    NPC.ai[1] += 2;
                }
                
                    
                
               
                NPC.netUpdate = true;
            }
            //PreCast effects
           
            if (NPC.active)
            {
                modPlayer.NalhaunActive = true;
                modPlayer.NalhaunCastTime = castTime;
                modPlayer.NalhaunCastTimeMax = castTimeMax;
                modPlayer.NalhaunNextAttack = nextAttack;
                modPlayer.isNalhaunInvincible = isInvincible;
                NPC.netUpdate = true;
            }
            else
            {
                modPlayer.NalhaunActive = false;
                modPlayer.NalhaunBarActive = false;
            }



            Player P = Main.player[NPC.target];//THIS IS THE BOSS'S MAIN TARGET
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if(Main.player[NPC.target].dead)
            {
                
                if (fightLost == false)
                {
                    SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_PityDisplay, NPC.Center);
                    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You fall to the Burnished King..."), 210, 60, 60);}
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
                modPlayer.NalhaunActive = false;
                modPlayer.NalhaunBarActive = false;
                NPC.active = false;
                NPC.velocity.Y -= 0.1f;
                NPC.timeLeft = 0;
            }
            if (nextAttack == "Transplacement")
            {
                NPC.ai[1] = 400;
            }
            
            if (nextAttack == "Heavensfall" && castTime == 28)
            {
                for (int d = 0; d < 8; d++)
                {
                    float Speed = 20f;  //projectile speed
                                        //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-600, 600), P.position.Y - 800);
                    int damage = 60;  //projectile damage
                    int type;

                    type = Mod.Find<ModProjectile>("SkyBeam").Type;



                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                    if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}}
                }
            }
            if (nextAttack == "Rend Heaven" && castTime == 20)
            {

            }
            if (nextAttack == "A World Rent Asunder")
            {

                if (Main.expertMode)
                {
                    NPC.lifeMax = 300000;
                    if (NPC.life < NPC.lifeMax)
                    {
                        NPC.life += 9500;
                    }
                    else
                    {
                        NPC.life = NPC.lifeMax;
                    }
                }
                else
                {
                    NPC.dontTakeDamage = true;
                }

            }
            if (nextAttack == "Surpassing Infinity")
            {




                NPC.life = 100000;



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
                   
                    
                    for (int i = 0; i < 2; i++)
                    {
                        // Charging dust
                        Vector2 vector = new Vector2(
                            Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                            Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                        Dust d = Main.dust[Dust.NewDust(
                            NPC.Center + vector, 1, 1,
                            132, 0, 0, 255,
                            new Color(1f, 1f, 1f), 1.5f)];
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

                        Dust d = Dust.NewDustPerfect(NPC.Center + offset, 20, NPC.velocity, 200, default(Color), 0.7f);
                        d.fadeIn = 1f;
                        d.noGravity = true;
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

                modPlayer.NalhaunBarActive = true;
                //Here are the attacks and their effects ///////////////////////////////////////
                if (castTime >= castTimeMax)
                {
                    if (nextAttack == "Coruscant Saber")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Vector2 vector8 = CoruscantSaberSaved;
                        
                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberDamage").Type,80,0f,Main.myPlayer);}}
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
                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberDamage").Type,140,0f,Main.myPlayer);}}
                        for (int d = 0; d < 100; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 269, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 35; d++)
                        {
                            Dust.NewDust(vector8, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }
                    }
                    if (nextAttack == "Blazing Skies")
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

                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
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
                        
                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        
                        
                    }
                    //Phase 2 attacks
                    if (nextAttack == "Shadowblade")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Vector2 vector8 = SolemnConfiteorSaved;
                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("ShadowbladeDamage").Type,400,0f,Main.myPlayer);}}
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

                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}*/


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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
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
                    if (nextAttack == "Brain Drain")
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
                                
                                player.AddBuff(BuffID.Confused, 300);
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
                    if (nextAttack == "The Sword of Flames")
                    {
                        
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 200;
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if (Main.netMode != NetmodeID.MultiplayerClient) { if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, Main.myPlayer);} }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, NPC.Center);

                        Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y - 200);
                        int type;
                        type = Mod.Find<ModProjectile>("BossLaevateinn").Type;
                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}}


                        if (Main.netMode != NetmodeID.Server)
                        {
                            if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Ars Laevateinn prepares to strike!"), 210, 60, 60);}
                        }


                    }
                    if (nextAttack == "To Ashes")
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
                        phase = 2;
                        ArsLaevateinnActive = false;
                        phaseTransition = false;

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
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Nalhaun is protected by a shield of frost!"), 210, 60, 60);}
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
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel2 = new Vector2(1, 1);
                            vel2 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel2.X, vel2.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel3 = new Vector2(1, -1);
                            vel3 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel3.X, vel3.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel4 = new Vector2(-1, 1);
                            vel4 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel4.X, vel4.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel5 = new Vector2(0, -1);
                            vel5 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel5.X, vel5.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel6 = new Vector2(0, 1);
                            vel6 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel6.X, vel6.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel7 = new Vector2(1, 0);
                            vel7 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel7.X, vel7.Y, 348, 60, 0, Main.myPlayer);}}
                            Vector2 vel8 = new Vector2(-1, 0);
                            vel8 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel8.X, vel8.Y, 348, 60, 0, Main.myPlayer);}}
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
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel2 = new Vector2(1, 1);
                            vel2 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel2.X, vel2.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel3 = new Vector2(1, -1);
                            vel3 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel3.X, vel3.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel4 = new Vector2(-1, 1);
                            vel4 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel4.X, vel4.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel5 = new Vector2(0, -1);
                            vel5 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel5.X, vel5.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel6 = new Vector2(0, 1);
                            vel6 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel6.X, vel6.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel7 = new Vector2(1, 0);
                            vel7 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel7.X, vel7.Y, 465, 60, 0, Main.myPlayer);}}
                            Vector2 vel8 = new Vector2(-1, 0);
                            vel8 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, vel8.X, vel8.Y, 465, 60, 0, Main.myPlayer);}}

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                    }
                    if (nextAttack == "Heavensfall")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        
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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

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
                            
                            
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
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


                            //if (player.active && modPlayer.inNalhaunFightTimer > 0)
                               

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-1, 1));
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.CultistBossIceMist, 40, 0, 0, NPC.whoAmI, 1);}}
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-18, 18));
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.FrostShard, 40, 0, 0, NPC.whoAmI, 1);}}
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8));
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.FrostWave, 40, 0, 0, NPC.whoAmI, 1);}}
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-8, 8));
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.CultistBossFireBall, 40, 0, 0, NPC.whoAmI, 1);}}
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
                    
                    if (nextAttack == "Shadowblast")
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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }

                       

                        for (int i = 0; i < 4; i++)
                        {
                            float Speed = 4f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                            int damage = 60;  //projectile damage
                            int type = ProjectileID.CultistBossFireBallClone;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            float Speed = 5f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                            int damage = 60;  //projectile damage
                            int type = ProjectileID.CultistBossFireBallClone;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                        for (int i = 0; i < 6; i++)
                        {
                            float Speed = 6f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                            int damage = 60;  //projectile damage
                            int type = ProjectileID.CultistBossFireBallClone;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                    }
                    if (nextAttack == "Transplacement")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        
                        teleportAway = true;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        

                    }
                    if (nextAttack == "Recall")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        teleportAway = false;
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }


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

                    if (nextAttack == "Absolute Summoning")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y - 400, NPCType<NPCs.SpectreOfLight>(), NPC.whoAmI);
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y - 400, NPCType<NPCs.SpectreOfLight>(), NPC.whoAmI);
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y - 400, NPCType<NPCs.SpectreOfLight>(), NPC.whoAmI);
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y - 400, NPCType<NPCs.SpectreOfLight>(), NPC.whoAmI);

                    }
                    if (nextAttack == "Concentrativity")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.dontTakeDamage = false;
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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;
                                

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
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
                            if (player.active && modPlayer.inNalhaunFightTimer > 0)
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

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 1, 1, NPC.whoAmI);}}
                        }
                        modPlayer.LostToNalhaun = true;
                        for (int d = 0; d < 305; d++)
                        {
                            Dust.NewDust(NPC.position, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }

                    }

                }
            }
            else
            {
                modPlayer.NalhaunBarActive = false;
            }
            if(undertaleActive)
            {
               
                modPlayer.undertaleActive = true;
                NPC.ai[1] = 400;
                blazingSkiesTimer = 0;
            }
            else
            {
                modPlayer.undertaleActive = false;
            }
            if(teleportAway)
            {
                NPC.ai[1] += 20;
            }
            undertaleTimer--;
            if(undertaleTimer <= 0)
            {
                undertaleActive = false;
            }

            if (blazingSkies > 0)
            {
                if (phase != 3)
                {
                    //npc.ai[1] = 400;
                }

                blazingSkiesTimer++;
                if(phase > 1 && (phase != 3))
                {
                    //blazingSkiesTimer++;
                }
            }
            if (blazingSkiesTimer >= 30)
            {
                strayManaTimer++;
                

                float Speed = 3f;  //projectile speed
                                    //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type;
                
                type = ProjectileID.LostSoulHostile;
                   
                
                vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 800);
                
                

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
               
                blazingSkiesTimer = 0;
                blazingSkies--;

                
            }
            if(strayManaTimer >= 2)
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
                NPC.ai[1] = 300;
                eyeProjectileTimer++;

            }
            if (eyeProjectileTimer >= 30)
            {
                float Speed = 12f;  //projectile speed
                                    //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-600, 600), P.position.Y - 800);
                int damage = 60;  //projectile damage
                int type = Mod.Find<ModProjectile>("EyeProjectile").Type;
                
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                SoundEngine.PlaySound(SoundID.Roar, vector8);
                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                eyeProjectileTimer = 0;
                eyeProjectile--;
            }

            if (QuintuplecastSkies > 0)
            {
                NPC.ai[1] = 300;
                QuintuplecastTimer++;

            }
            if (QuintuplecastTimer >= 30)
            {
                float Speed = 20f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type = ProjectileID.DD2BetsyFireball;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),ProjectileID.CultistBossFireBall,damage,0f,Main.myPlayer);}}
                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),ProjectileID.FlamingScythe,damage,0f,Main.myPlayer);}}
                QuintuplecastTimer = 0;
                QuintuplecastSkies--;
            }
            if (desperadoShots > 0)
            {
                NPC.ai[1] = 300;
                desperadoTimer++;

            }
            if (desperadoTimer >= 5)
            {
                float Speed = 10f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(NPC.Center.X, NPC.Center.Y);
                int damage = 60;  //projectile damage
                int type = 302;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                desperadoTimer = 0;
                desperadoShots--;
            }

            if (!Main.dedServ)//The "platform"
            {
                if (!inIntro)
                {
                    for (int d = 0; d < 2; d++)
                    {
                        Dust.NewDust(new Vector2(NPC.Center.X + Main.rand.Next(-70, 70), NPC.Center.Y + 140), 0, 0, 132, 0f, 0f, 150, default(Color), 1.5f);
                    }
                }
                else
                {
                    Dust.NewDust(new Vector2(NPC.Center.X + Main.rand.Next(-70, 70), NPC.Center.Y + 140), 0, 0, 132, 0f, 0f, 150, default(Color), 1.5f);
                }
                
            }

            NPC.ai[1]+= 2;
            
            if (NPC.ai[1] >= 0)
            {
                if (fightStart == true)
                {
                    inIntro = true;
                    introAnimation = 800;

                    Vector2 initialMoveTo = P.Center + new Vector2(-120f, -1200);
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
                    SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_NalhaunIntroQuote, NPC.Center);
                    NPC.ai[1] = 200;

                       
                    
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
                if(!fightLost)
                {
                    NPC.velocity = Vector2.Zero;

                }
            }


            if (phase > 1)
            {
                NPC.ai[1]+=2;
            }
           
            if (NPC.life <= 50000 && phase == 1)
            {
                NPC.ai[1] += 20;
                NPC.dontTakeDamage = true;
            }
            //Movement code
            //So you can keep track of how long the NPC has been charging.
            //Attack generation
            int hpThreshold;
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                hpThreshold = 200000;
            }
            else
            {
               
                hpThreshold = 50000;
            }
            
            if (NPC.ai[1] >= 500)
            {
                
                NPC.netUpdate = true;

                if (!isCasting)
                {
                    
                   
                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (NPC.life >= hpThreshold) 
                    {
                        blazingSkies = 12;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{npc.ai[2]}"), 210, 60, 60);}
                        //Boss Rotation
                        if (NPC.ai[2] == 0)
                        {
                            nextCast = "Heavensfall";//
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Rend Heaven";
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "Brain Drain";
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "Shadowblade";//
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Crack The Sky";
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Heavensfall";
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Rend Heaven";//
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Sanctified Slaughter";
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "Transplacement";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "Thousand Strikes";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Recall";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Sanctified Slaughter II";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Shadowblade";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Heavensfall";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Crack The Sky";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Brain Drain";
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Rend Heaven";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "Transplacement";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "Sanctified Slaughter";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "Rend Heaven";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Thousand Strikes";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Recall";
                            NPC.ai[2] = 0;
                        }
                        //End of Rotation
                        if (nextCast == "Heavensfall")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_Fools, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Heavensfall";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Blazing Skies II")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_WereYouExpectingRust, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blazing Skies II";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Coruscant Saber II")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_ComeShowMeMore, NPC.Center);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberAOE2").Type,0,0f,Main.myPlayer);}}
                            CoruscantSaberSaved = vector8;
                            nextAttack = "Coruscant Saber II";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Shadowblade")
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_TheHeartsOfMen, NPC.Center);
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("ShadowbladeAOE").Type,0,0f,Main.myPlayer);}}
                            SolemnConfiteorSaved = vector8;
                            nextAttack = "Shadowblade";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }

                        if (nextCast == "Brain Drain")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_EvenTheStrongestShields, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Brain Drain";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "Sanctified Slaughter")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_TheGodsWillNotBeWatching, NPC.Center);
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Sanctified Slaughter";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "Sanctified Slaughter II")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_TheGodsWillNotBeWatching, NPC.Center);
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Sanctified Slaughter II";
                            castTime = 0;
                            castTimeMax = 250;
                            isCasting = true;
                        }
                        if (nextCast == "Thousand Strikes")
                        {
                            castAnimation = 70;
                           
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Thousand Strikes";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            float Speed = 0f;  //projectile speed
                                               //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X - 800, P.position.Y);
                            int damage = 0;  //projectile damage
                            int type;
                            
                            type = Mod.Find<ModProjectile>("NalhaunSwing").Type;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_EscapeIsNotSoEasilyGranted, NPC.Center);


                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                        if (nextCast == "Thousand Strikes ")
                        {
                            castAnimation = 70;

                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Thousand Strikes ";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            float Speed = 0f;  //projectile speed
                                               //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + 800, P.position.Y);
                            int damage = 0;  //projectile damage
                            int type;

                            type = Mod.Find<ModProjectile>("NalhaunSwing2").Type;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_EscapeIsNotSoEasilyGranted, NPC.Center);


                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                        if (nextCast == "Absolute Blizzard III")
                        {
                            castAnimation = 70;
                           // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MySoulKnowsNoSurrender"));
                            castDelay = 0;
                            nextAttack = "Absolute Blizzard III";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Crack The Sky")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_AThousandBolts, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Crack The Sky";
                            castTime = 0;
                            castTimeMax = 300;
                            isCasting = true;
                        }
                        if (nextCast == "Aegis of Frost")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_MyDefenses, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Aegis of Frost";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Transplacement")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MyDefenses"));
                            castDelay = 0;
                            nextAttack = "Transplacement";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Recall")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MyDefenses"));
                            castDelay = 0;
                            nextAttack = "Recall";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Rend Heaven")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_RuinationIsCome, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Rend Heaven";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            for (int d = 0; d < 3500; d += 500)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X ) + d, NPC.position.Y);
                                int type;
                                type = Mod.Find<ModProjectile>("RendHeaven").Type;
                                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}}
                            }
                        }

                        if (!isCasting)
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_Fools, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Heavensfall";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }
                    // Phase 2 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (phase == 2 && !phaseTransition) 
                    {
                        //NPC.NewNPC(NPC.GetSource_FromAI(),(int)npc.Center.X + Main.rand.Next(-365, 365), (int)npc.Center.Y - 400, NPCType<NPCs.SpectreOfLight>(), npc.whoAmI);
                        blazingSkies = 20;
                        //Boss Rotation
                        
                        if (NPC.ai[2] == 0)
                        {

                            nextCast = "Rend Heaven";//
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Crack The Sky";
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "Heavensfall";
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "The Sword of Flames";//
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Shadowblast";
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Transplacement";
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Thousand Strikes";//
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Recall";
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "Sanctified Slaughter II";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "Brain Drain";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Transplacement";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Thousand Strikes ";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Recall";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Sanctified Slaughter";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Shadowblast";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Rend Heaven";//
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Heavensfall";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "The Sword of Flames";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "Shadowblast";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "Rend Heaven";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Sanctified Slaughter";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Heavensfall";
                        }
                        if (NPC.ai[2] == 22)
                        {
                            nextCast = "Shadowblast";
                        }
                        if (NPC.ai[2] == 23)
                        {
                            nextCast = "Brain Drain";
                        }
                        if (NPC.ai[2] == 24)
                        {
                            nextCast = "The Sword of Flames";
                        }
                        if (NPC.ai[2] == 25)
                        {
                            nextCast = "Rend Heaven";
                        }
                        if (NPC.ai[2] == 26)
                        {
                            nextCast = "Shadowblast";
                        }
                        if (NPC.ai[2] == 27)
                        {
                            nextCast = "Sanctified Slaughter II";
                        }
                        if (NPC.ai[2] == 28)
                        {
                            nextCast = "Crack The Sky";
                        }
                        if (NPC.ai[2] == 29)
                        {
                            nextCast = "Rend Heaven";
                        }
                        if (NPC.ai[2] == 30)
                        {
                            nextCast = "Heavensfall";
                            NPC.ai[2] = 0;
                        }
                        //End of Rotation
                        if (nextCast == "The Bitter End")
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Blazing Skies II")
                        {
                            castAnimation = 70;
                            castDelay = 0;
                            nextAttack = "Blazing Skies II";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        
                        if (nextCast == "Coruscant Saber II")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberAOE2").Type,0,0f,Main.myPlayer);}}
                            CoruscantSaberSaved = vector8;
                            nextAttack = "Coruscant Saber II";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                            for (int i = 0; i < Main.maxPlayers; i++)
                            {
                                Player player = Main.player[i];
                                if (player.active && modPlayer.inNalhaunFightTimer > 0)
                                    player.AddBuff(BuffID.Chilled, 60);  //

                            }
                        }
                        if (nextCast == "Thousand Strikes ")
                        {
                            castAnimation = 70;

                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Thousand Strikes ";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            float Speed = 0f;  //projectile speed
                                               //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + 800, P.position.Y);
                            int damage = 0;  //projectile damage
                            int type;

                            type = Mod.Find<ModProjectile>("NalhaunSwing2").Type;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_EscapeIsNotSoEasilyGranted, NPC.Center);


                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                        
                        if (nextCast == "Shadowblast")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_WereYouExpectingRust, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Shadowblast";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "The Sword of Flames")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_UponMyHolyBlade, NPC.Center);
                            
                            castDelay = 0;
                            nextAttack = "The Sword of Flames";
                            castTime = 0;
                            castTimeMax = 120;
                            isCasting = true;
                        }
                        if (nextCast == "Heavensfall")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_Fools, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Heavensfall";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Blazing Skies II")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_WereYouExpectingRust, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blazing Skies II";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Coruscant Saber II")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_ComeShowMeMore, NPC.Center);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberAOE2").Type,0,0f,Main.myPlayer);}}
                            CoruscantSaberSaved = vector8;
                            nextAttack = "Coruscant Saber II";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Shadowblade")
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_TheHeartsOfMen, NPC.Center);
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("ShadowbladeAOE").Type,0,0f,Main.myPlayer);}}
                            SolemnConfiteorSaved = vector8;
                            nextAttack = "Shadowblade";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }

                        if (nextCast == "Brain Drain")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_EvenTheStrongestShields, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Brain Drain";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "Sanctified Slaughter")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_TheGodsWillNotBeWatching, NPC.Center);
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Sanctified Slaughter";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "Sanctified Slaughter II")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_TheGodsWillNotBeWatching, NPC.Center);
                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Sanctified Slaughter II";
                            castTime = 0;
                            castTimeMax = 250;
                            isCasting = true;
                        }
                        if (nextCast == "Thousand Strikes")
                        {
                            castAnimation = 70;

                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                            castDelay = 0;
                            nextAttack = "Thousand Strikes";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            float Speed = 0f;  //projectile speed
                                               //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X - 800, P.position.Y);
                            int damage = 0;  //projectile damage
                            int type;

                            type = Mod.Find<ModProjectile>("NalhaunSwing").Type;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_EscapeIsNotSoEasilyGranted, NPC.Center);


                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}}
                        }
                        if (nextCast == "Absolute Blizzard III")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MySoulKnowsNoSurrender"));
                            castDelay = 0;
                            nextAttack = "Absolute Blizzard III";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Crack The Sky")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_AThousandBolts, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Crack The Sky";
                            castTime = 0;
                            castTimeMax = 300;
                            isCasting = true;
                        }
                        if (nextCast == "Aegis of Frost")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_MyDefenses, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Aegis of Frost";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Transplacement")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MyDefenses"));
                            castDelay = 0;
                            nextAttack = "Transplacement";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Recall")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MyDefenses"));
                            castDelay = 0;
                            nextAttack = "Recall";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Rend Heaven")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_RuinationIsCome, NPC.Center);
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
                                if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}}
                            }
                        }
                        if (!isCasting)
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/HaveAtYou"));
                            castDelay = 0;
                            nextAttack = "Heavensfall";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }



                    // Special attacks /////////////////////////////////////////////////////////////////////////////////////////////
                    
                    if (NPC.life < hpThreshold && phase == 1 && !ArsLaevateinnActive)
                    {
                        castAnimation = 70;
                       

                            Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y - 200);
                            int type;
                            type = Mod.Find<ModProjectile>("BossLaevateinn").Type;
                            if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}}

                        //if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("BossLaevateinn"), 0, 4, npc.whoAmI, 0, 1);}}
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}}
                        SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_UponMyHolyBlade, NPC.Center);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, NPC.Center);
                        castDelay = 0;
                        phaseTransition = true;
                        castTime = 0;
                        castTimeMax = 200;
                        isCasting = true;
                        nextAttack = "Ars Laevateinn";
                        NPC.netUpdate = true;
                        phase = 2;
                        

                    }
                    if (phase == 2 && ArsLaevateinnActive)
                    {
                        castAnimation = 70;

                        SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_AndNowTheScalesWillTip, NPC.Center);
                        castDelay = 0;

                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                        nextAttack = "To Ashes";
                        NPC.netUpdate = true;
                        ArsLaevateinnActive = false;
                        NPC.ai[2] = 0;

                    }

                    NPC.ai[2]++;



                }
                
                
                 NPC.ai[1] = 0;
                
                
            }
            NPC.netUpdate = true;
        }
        
        private void NextAttackCheck(string nextAttack, Player P)
        {
            
        }

    }
}//