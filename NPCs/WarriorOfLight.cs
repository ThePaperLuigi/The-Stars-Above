using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace StarsAbove.NPCs
{
    [AutoloadBossHead]
    public class WarriorOfLight : ModNPC
    {
        public static readonly int arenaWidth = (int)(1.2f * 2400);
        public static readonly int arenaHeight = (int)(1.2f * 1600);
        public override void SetStaticDefaults()
        {
            // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
            // DisplayName.SetDefault("Example Person");
            Main.npcFrameCount[NPC.type] = 15;
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            DisplayName.SetDefault("The Warrior Of Light");
            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                
               
                PortraitPositionYOverride = 0f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

        }
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.aiStyle = 0;
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (Main.expertMode)
                {
                    NPC.lifeMax = 1770000;
                   
                }
                else
                {
                    NPC.lifeMax = 1140000;
                    
                }
               
                
            }
            else
            {

                if (Main.expertMode)
                {
                    NPC.lifeMax = 485000;

                }
                else
                {
                    NPC.lifeMax = 385000;

                }
            }
            
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.width = 220;
            NPC.height = 270;
            NPC.scale = 1.1f;
            Main.npcFrameCount[NPC.type] = 15;
            NPC.value = Item.buyPrice(1, 40, 75, 45);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            Music =  MusicID.LunarBoss;
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
        Vector2 CoruscantSaberSaved;
        Vector2 SolemnConfiteorSaved;
        int phase = 1;
        private int portalFrame
        {
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }
        int lightningSkies = 0;
        int lightningSkiesTimer = 0;
        int blazingSkies = 0;
        int blazingSkiesTimer = 0;
        int QuintuplecastSkies = 0;
        int QuintuplecastTimer = 0;
        int desperadoShots = 0;
        int desperadoTimer = 0;


        int swingAnimation = 0;
        int castAnimation = 0;
        int introVelocityY = 8;
        int introVelocityTimer = 0;

        bool fightLost = false;
        bool surpassingInfinity = false;
        bool stunCasted = false;
        bool undertaleActive;
        int undertaleTimer;
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
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.WarriorBossBag>()));

            // Trophies are spawned with 1/10 chance
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.WarriorTrophyItem>(), 10));

            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.WarriorBossRelicItem>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            // Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
            // Boss masks are spawned with 1/7 chance
            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.LightswornPrism>(), 4));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.DullTotemOfLight>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.AegisOfHopesLegacyPrecursor>(), 8));

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
            potionType = ItemID.SuperHealingPotion;
            
            
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedWarrior, -1);
            DownedBossSystem.downedWarrior = true;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }
            //also maybe Totems of Light to craft more stuff?
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.defense += numPlayers * 10;
        }
        
        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = 0;
            NPC.spriteDirection = NPC.direction;
            swingAnimation--;
            castAnimation--;

            if (isCasting)
            {
                if (isSwinging)
                {
                    if (swingAnimation > 60)
                        NPC.frame.Y = 7 * frameHeight;
                    if (swingAnimation < 60)
                        NPC.frame.Y = 6 * frameHeight;
                    if (swingAnimation <= 55)
                        NPC.frame.Y = 5 * frameHeight;
                }
                else
                {


                   
                        NPC.frameCounter++;
                }
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


            if (isCasting && !isSwinging)
            {
                NPC.frame.Y = frameHeight * 8 + nframe * frameHeight;
            }

            if(NPC.ai[3] >= 900)
            {
                NPC.frame.Y = frameHeight * 14;
            }



            // if (isSwinging)

        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }

       

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int portalWidth = 48;
            int portalDepth = 18;
            Color color = new Color(255, 97, 55);
            int centerX = (int)NPC.Center.X;
            int centerY = (int)NPC.Center.Y;
            Main.instance.LoadProjectile(ProjectileID.PortalGunGate);
            for (int x = centerX - arenaWidth / 2; x < centerX + arenaWidth / 2; x += portalWidth)
            {
                int frameNum = (portalFrame / 6 + x / portalWidth) % Main.projFrames[ProjectileID.PortalGunGate];
                Rectangle frame = new Rectangle(0, frameNum * (portalWidth + 2), portalDepth, portalWidth);
                Vector2 drawPos = new Vector2(x + portalWidth / 2, centerY - arenaHeight / 2) - Main.screenPosition;
                spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)-Math.PI / 2f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
                drawPos.Y += arenaHeight;
                spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)Math.PI / 2f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
            }
            for (int y = centerY - arenaHeight / 2; y < centerY + arenaHeight / 2; y += portalWidth)
            {
                int frameNum = (portalFrame / 6 + y / portalWidth) % Main.projFrames[ProjectileID.PortalGunGate];
                Rectangle frame = new Rectangle(0, frameNum * (portalWidth + 2), portalDepth, portalWidth);
                Vector2 drawPos = new Vector2(centerX - arenaWidth / 2, y + portalWidth / 2) - Main.screenPosition;
                spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)Math.PI, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
                drawPos.X += arenaWidth;
                spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, 0f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
            }
        }

        public override bool CheckDead()
        {
            if (phase == 1 || phase == 2 || !surpassingInfinity)
            {
                NPC.life = 1;
                return false;
            }
            if (NPC.ai[3] == 0f)
            {
                
                SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_WarriorOfLightDeathQuote, NPC.Center);

                SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_WarriorOfLightDeathQuote, NPC.Center);
                

                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void AI()
        {
            int hpThreshold2ndPhase;
            int hpThreshold3rdPhase;
            int hpThresholdFinalPhase;
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                hpThreshold2ndPhase = 1050000;
                hpThreshold3rdPhase = 650000;
                hpThresholdFinalPhase = 225000;
                if(Main.expertMode)
                {
                    hpThreshold2ndPhase = 1650000;
                    hpThreshold3rdPhase = 1050000;
                    hpThresholdFinalPhase = 425000;
                }    
            }
            else
            {
                hpThreshold2ndPhase = 375000;
                hpThreshold3rdPhase = 190000;
                hpThresholdFinalPhase = 100000;
                
            }
            

            NPC.netUpdate = true;
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            //This is the death effect from ExampleMod
            if (NPC.ai[3] > 0f)//This is death effect
            {
                Music =  MusicLoader.GetMusicSlot(Mod,  "Sounds/Music/BossFinish");
                NPC.dontTakeDamage = true;
                NPC.ai[3] += 1f; // increase our death timer.
                                 //npc.velocity = Vector2.UnitY * npc.velocity.Length();
                
                if (NPC.ai[3] > 1200f)//480 was old death time.
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
                if (NPC.ai[3] == 400f)
                {
                   
                    Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                    //if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X + 30, placement2.Y - 80, 0, 0, Mod.Find<ModProjectile>("Wormhole").Type, 0, 0f, Main.myPlayer); }
                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y - 150, 0, 0, Mod.Find<ModProjectile>("TsukiyomiTeleportLong").Type, 0, 0f, Main.myPlayer); }
                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y - 150, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, Main.myPlayer); }
                }
                if(NPC.ai[3] >= 400)
                {
                    //modPlayer.lookAtWarrior = true;
                }
                if (NPC.ai[3] == 720f)
                {
                    Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("WormholeLong").Type, 0, 0f, Main.myPlayer); }
                    
                }
                if (NPC.ai[3] >= 1100)
                {
                    NPC.AddBuff(BuffType<Buffs.WarriorEnd>(), 60);
                }
                if (NPC.ai[3] >= 1140f)
                {
                    for (int d = 0; d < 305; d++)
                    {
                        Dust.NewDust(NPC.Center, 0, 0, 21, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_WarriorOfLightDefeated, NPC.Center);
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    
                       DownedBossSystem.downedWarrior = true;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                        }
                    modPlayer.lookAtWarrior = false;
                    modPlayer.WarriorOfLightActive = false;
                    modPlayer.WarriorBarActive = false;
                    modPlayer.undertaleActive = false;
                }
                return;
            }

           
            if (phase > 1)
            {
                
                NPC.defense = 20;
                if (phase != 3)
                {
                    
                    Music = MusicLoader.GetMusicSlot(Mod,  "Sounds/Music/ToTheEdge");
                    //MusicPriority = MusicPriority.BossHigh;
                }
                else
                {
                    if(surpassingInfinity)
                    {
                        Music = MusicLoader.GetMusicSlot(Mod,  "Sounds/Music/TwoDragonsFinalPhase");
                       // 
                    }
                    else
                    {
                        Music = MusicLoader.GetMusicSlot(Mod,  "Sounds/Music/TwoDragons");
                        //
                    }
                    //Main.monolithType = 3;
                    
                }
                
                NPC.netUpdate = true;
            }
            if(nextAttack == "Concentrativity" && castTime == 65)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_WarriorStun, NPC.Center);
            }
            if (nextAttack == "A World Rent Asunder")
            {
                
                if (Main.expertMode)
                {

                    NPC.lifeMax = 300000;

                    if (NPC.life < NPC.lifeMax)
                    {

                        //Attack generation

                        NPC.life += 500;

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

                

                   
                 NPC.life = hpThresholdFinalPhase;
                    
                

            }
            if (NPC.active)
            {
                modPlayer.WarriorLocation = NPC.Center;
                modPlayer.WarriorOfLightActive = true;
                modPlayer.WarriorCastTime = castTime;
                modPlayer.WarriorCastTimeMax = castTimeMax;
                modPlayer.WarriorOfLightNextAttack = nextAttack;
                NPC.netUpdate = true;
            }
            else
            {
                modPlayer.WarriorOfLightActive = false;
                modPlayer.WarriorBarActive = false;
            }
            if (!Main.dedServ)
            {


                portalFrame++;
                portalFrame %= 6 * Main.projFrames[ProjectileID.PortalGunGate];
            }


                Player P = Main.player[NPC.target];//THIS IS THE BOSS'S MAIN TARGET
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if(Main.player[NPC.target].dead)
            {
                
                modPlayer.undertaleActive = false;
                if (fightLost == false)
                {
                    
                    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The Warrior Of Light has won..."), 210, 60, 60);}
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
                modPlayer.WarriorOfLightActive = false;
                modPlayer.WarriorBarActive = false;
                NPC.velocity.Y -= 0.1f;
                NPC.timeLeft = 0;
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

                    
                        portalFrame++;
                        portalFrame %= 6 * Main.projFrames[ProjectileID.PortalGunGate];
                    
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
                    for (int i = 0; i < 30; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * ((castTimeMax * 5) - (castTime * 5)));
                        offset.Y += (float)(Math.Cos(angle) * ((castTimeMax * 5) - (castTime * 5)));

                        Dust d = Dust.NewDustPerfect(NPC.Center + offset, 269, NPC.velocity, 200, default(Color), 0.7f);
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

                modPlayer.WarriorBarActive = true;
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
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),player.Center.X, player.Center.Y, 0, 0,Mod.Find<ModProjectile>("SaberAOE").Type,0,0f,Main.myPlayer);}

                        }

                    }
                    if (nextAttack == "Sinpurge")
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                            {
                                float Speed = 0f;  //projectile speed
                                Vector2 vector8 = new Vector2(player.position.X, player.position.Y);
                                int damage = 0;  //projectile damage
                                int type = Mod.Find<ModProjectile>("GasterBlaster").Type;

                                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));

                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0,0,player.position.Y/100);}
                            }
                                

                        }

                    }
                    if (nextAttack == "Sinpurge II")
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                            {
                                float Speed = 0f;  //projectile speed
                                Vector2 vector8 = new Vector2(player.position.X - 100, player.position.Y);
                                int damage = 0;  //projectile damage
                                int type = Mod.Find<ModProjectile>("GasterBlaster").Type;

                                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));

                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0, 0, player.position.Y / 100);}

                                
                                Vector2 vector82 = new Vector2(player.position.X + 100, player.position.Y);
                               

                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector82.X, vector82.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0, 0, player.position.Y / 100 + 11);}
                            }


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
                    if (nextAttack == "Absolute Titanomachy")
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/CounterImpact"));
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
                        
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        
                        
                    }
                    //Phase 2 attacks
                    if (nextAttack == "Solemn Confiteor")
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
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
                    if (nextAttack == "Absolute Thunder IV")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = -200;
                        lightningSkies = 3;
                       
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
                        lightningSkies = 4;
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
                    if (nextAttack == "Absolute Rend Heaven")
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                       

                    }
                    if (nextAttack == "Absolute Linear Mystics")
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

                        for (int i = 0; i < 2000; i += 50)
                        {
                            float Speed = 4f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X - 600 + i, P.position.Y - 800 + i);
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("AbsoluteGeometry").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_TeleportPrep, NPC.Center);
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }




                    }
                    if (nextAttack == "Unabated Radiance")
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
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, NPC.Center);
                        for (int i = 0; i < 1200; i += 100)
                        {
                            float Speed = 13f;  //projectile speed
                                               //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(NPC.position.X - 1200 + i, NPC.position.Y - 1200);
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("Lightblast").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }
                        for (int i4 = 0; i4 < 1200; i4 += 100)
                        {
                            float Speed = 13f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(NPC.position.X - 1200, NPC.position.Y - 1200 + i4);
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("Lightblast").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }
                        for (int i2 = 1200; i2 > 0; i2 -= 100)
                        {
                            float Speed = 13f;  //projectile speed
                                               //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(NPC.position.X + 1200 - i2, NPC.position.Y + 1200);
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("Lightblast").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }

                        for (int i3 = 1200; i3 > 0; i3 -= 100)
                        {
                            float Speed = 13f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(NPC.position.X + 1200 , NPC.position.Y + 1200 - i3);
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("Lightblast").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
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
                            
                            
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
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


                            //if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                               

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
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
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_RadiantBraver, NPC.Center);
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
                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_RadiantBraver, NPC.Center);
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 0;
                        desperadoShots = 30;

                    }
                    if (nextAttack == "Radiant Meteor")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_RadiantBraver, NPC.Center);
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;
                                  
                        }

                        for (int d = 0; d < 4400; d += 400)
                        {

                            Vector2 placement = new Vector2((P.Center.X) + d, P.position.Y);
                            int type = ProjectileID.DD2BetsyFireball;
                            

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2000, placement.Y - 500, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1950, placement.Y - 600, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1900, placement.Y - 700, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1850, placement.Y - 800, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2000, placement.Y - 900, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1950, placement.Y - 1000, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1900, placement.Y - 1100, 0, 3,type,40,0f,Main.myPlayer);}
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1850, placement.Y - 1200, 0, 3,type,40,0f,Main.myPlayer);}
                        }
                        
                        
                    }
                    if (nextAttack == "Rays of Punishment")
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }

                       

                        for (int i = 0; i < 8; i++)
                        {
                            float Speed = 20f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                            int damage = 60;  //projectile damage
                            int type = ProjectileID.FlamingScythe;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            float Speed = 20f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                            int damage = 60;  //projectile damage
                            int type = ProjectileID.EyeBeam;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }
                    }
                    if (nextAttack == "Blades of Light")
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }



                        for (int i = 0; i < 2400; i+= 100)
                        {
                            float Speed = 20f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(NPC.position.X - 1200 + i, NPC.position.Y);
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("AbsoluteBlade").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_swordAttackFinish, NPC.Center);
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }
                        for (int i2 = 0; i2 < 2400; i2 += 100)
                        {
                            float Speed = 20f;  //projectile speed
                                                //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            Vector2 vector8 = new Vector2(NPC.position.X - 1200 + i2, NPC.position.Y );
                            int damage = 60;  //projectile damage
                            int type = Mod.Find<ModProjectile>("AbsoluteBlade2").Type;

                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_swordAttackFinish, NPC.Center);
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                        }
                    }
                    //Here are special phase changing attacks
                    if (nextAttack == "Ascendance")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, NPC.Center);

                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("WarriorVFX").Type,0,0f,Main.myPlayer);}
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("WarriorVFX2").Type,0,0f,Main.myPlayer);}

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}
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
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;
                                

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                player.AddBuff(BuffType<Buffs.DownForTheCount>(), 680);  //
                           

                        }

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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
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
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}
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
                            if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
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
                        

                    }

                }
            }
            else
            {
                modPlayer.WarriorBarActive = false;
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

            undertaleTimer--;
            if(undertaleTimer <= 0)
            {
                undertaleActive = false;
            }

            if (blazingSkies > 0)
            {
                if (phase != 3)
                {
                    NPC.ai[1] = 400;
                }

                blazingSkiesTimer++;
                if(phase > 1 && (phase != 3))
                {
                    blazingSkiesTimer++;
                }
            }
            if ((blazingSkiesTimer >= 30 && phase != 3) || (blazingSkiesTimer >= 140 && phase >= 3))
            {
                float Speed = 20f;  //projectile speed
                                    //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 60;  //projectile damage
                int type = Mod.Find<ModProjectile>("BlazingSkies").Type;


                if (phase >= 3)
                {
                    if (surpassingInfinity)
                    {
                        type = ProjectileID.DD2BetsyFireball;
                        damage = 90;
                        
                    }
                    else
                    {
                        type = Mod.Find<ModProjectile>("AbsoluteBlade2").Type;
                    }
                        
                    for (int d = 0; d < 2400; d += 400)
                    {

                        Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y - 800);
                            

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1000, placement.Y, 0, 1,type,damage,0f,Main.myPlayer);}

                    }

                }
                else
                {
                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}

                }
                
                

                
                
                blazingSkiesTimer = 0;
                blazingSkies--;
            }

            if (lightningSkies > 0)
            {
                NPC.ai[1] = 300;
                lightningSkiesTimer++;

            }
            if (lightningSkiesTimer >= 30)
            {
                float Speed = 0f;  //projectile speed
                                    //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 60;  //projectile damage
                int type = 465;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                lightningSkiesTimer = 0;
                lightningSkies--;
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

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),ProjectileID.CultistBossFireBall,damage,0f,Main.myPlayer);}
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),ProjectileID.FlamingScythe,damage,0f,Main.myPlayer);}
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
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                desperadoTimer = 0;
                desperadoShots--;
            }

            

            NPC.ai[1]++;
            if (NPC.ai[1] >= 0)
            {
                if (fightStart == true)
                {
                    
                    Vector2 initialMoveTo = P.Center + new Vector2(0f, -1000);
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
                    SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_WarriorOfLightIntroQuote, NPC.Center);
                    NPC.ai[1] = 200;

                       
                    
                    fightStart = false;
                }
            }
            introVelocityTimer++;
            if (introVelocityY > 0)
            {
                if (introVelocityTimer >= 15)
                {
                    NPC.velocity = new Vector2(0, introVelocityY);
                    introVelocityY--;
                    introVelocityTimer = 0;
                }
                if (!Main.dedServ)
                {

                    Vector2 bossPosition = new Vector2(NPC.Center.X, NPC.Center.Y + 150);
                    Dust.NewDust(bossPosition, 0, 0, 269, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);

                }
            }
            else
            {
                if (phase == 1)
                {
                    NPC.velocity = Vector2.Zero;
                }
            }
            
            if (phase > 1)
            {
                NPC.ai[1]+=3;
            }
            if (phase == 3)
            {
                NPC.ai[1]+=2;
            }
            if (NPC.life <= hpThreshold2ndPhase && phase == 1)
            {
                NPC.ai[1] += 20;
                NPC.dontTakeDamage = true;
            }
            //Movement code
            //So you can keep track of how long the NPC has been charging.
            if (!Main.player[NPC.target].dead)
            {


                if (!isCasting)
                {
                    // if (!fightStart)
                    //{
                    if (phase != 1)
                    {
                        NPC.ai[0] -= 1f;
                        if (NPC.ai[0] <= 100)
                        {
                            if (!Main.dedServ)
                            {
                                for (int i = 0; i < 10; i++)
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
                                for (int i = 0; i < 30; i++)
                                {//Circle
                                    Vector2 offset = new Vector2();
                                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                    offset.X += (float)(Math.Sin(angle) * ((150) - (NPC.ai[0])));
                                    offset.Y += (float)(Math.Cos(angle) * ((150) - (NPC.ai[0])));

                                    Dust d = Dust.NewDustPerfect(NPC.Center + offset, 269, NPC.velocity, 200, default(Color), 0.7f);
                                    d.fadeIn = 1f;
                                    d.noGravity = true;
                                }
                            }
                        }
                        if (NPC.ai[0] <= 0f) //Checks whether the NPC is ready to start another charge.
                        {
                            if (!Main.dedServ)
                            {

                                for (int d = 0; d < 25; d++)
                                {
                                    Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                                }
                                for (int d = 0; d < 25; d++)
                                {
                                    Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                                }
                                for (int i = 0; i < 70; i++)
                                {
                                    int dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                                    Main.dust[dustIndex].velocity *= 1.4f;
                                }
                                // Fire Dust spawn
                                for (int i = 0; i < 80; i++)
                                {
                                    int dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                                    Main.dust[dustIndex].noGravity = true;
                                    Main.dust[dustIndex].velocity *= 5f;
                                    dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                                    Main.dust[dustIndex].velocity *= 3f;
                                }
                                // Large Smoke Gore spawn

                            }
                            Vector2 moveTo = P.Center + new Vector2(Main.rand.Next(-400, 400), Main.rand.Next(-600, 0)); //This is 200 pixels above the center of the player.
                            NPC.position = moveTo;
                            if (!Main.dedServ)
                            {

                                for (int d = 0; d < 25; d++)
                                {
                                    Dust.NewDust(NPC.Center, 0, 0, 269, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                                }
                                for (int d = 0; d < 25; d++)
                                {
                                    Dust.NewDust(NPC.Center, 0, 0, 90, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                                }
                                for (int i = 0; i < 70; i++)
                                {
                                    int dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                                    Main.dust[dustIndex].velocity *= 1.4f;
                                }
                                // Fire Dust spawn
                                for (int i = 0; i < 80; i++)
                                {
                                    int dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                                    Main.dust[dustIndex].noGravity = true;
                                    Main.dust[dustIndex].velocity *= 5f;
                                    dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                                    Main.dust[dustIndex].velocity *= 3f;
                                }
                                // Large Smoke Gore spawn
                                for (int g = 0; g < 4; g++)
                                {
                                    int goreIndex = Gore.NewGore(null,new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                                    goreIndex = Gore.NewGore(null,new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                                    goreIndex = Gore.NewGore(null,new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                                    goreIndex = Gore.NewGore(null,new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                                }
                            }
                            NPC.ai[0] = 200f;
                            //This is the time before the NPC will start another charge.
                            //There are 60 ticks in one second, so this will make the NPC charge for 3 and 1/3 seconds before changing directions.
                        }


                    }
                    /*
                                    if (phase == 2)
                                        {
                                            Vector2 moveTo = P.Center + new Vector2(0f, -200); //This is 200 pixels above the center of the player.
                                            float speed = 1f;
                                            Vector2 move = moveTo - npc.Center;
                                            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                                            if (magnitude > speed)
                                            {
                                                move *= speed / magnitude;
                                            }
                                            npc.velocity = move;
                                            if (!Main.dedServ)
                                            {

                                                Vector2 bossPosition = new Vector2(npc.Center.X, npc.Center.Y + 150);
                                                Dust.NewDust(bossPosition, 0, 0, 269, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);

                                            }
                                    }
                                    if (phase == 3)
                                    {
                                        Vector2 moveTo = P.Center + new Vector2(0f, -200); //This is 200 pixels above the center of the player.
                                        float speed = 3f;
                                        Vector2 move = moveTo - npc.Center;
                                        float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                                        if (magnitude > speed)
                                        {
                                            move *= speed / magnitude;
                                        }
                                        npc.velocity = move;
                                        if (!Main.dedServ)
                                        {

                                            Vector2 bossPosition = new Vector2(npc.Center.X, npc.Center.Y + 150);
                                            Dust.NewDust(bossPosition, 0, 0, 269, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);

                                        }
                                    }*/
                    // }
                }
                else
                {
                    NPC.velocity = Vector2.Zero;
                    NPC.ai[0] = 120f;
                }
            }
            //Attack generation
            //Attack generation
            
            int castTimer = 500;
            if (Main.expertMode)
            {
                castTimer = 450;
            }
            if (NPC.ai[1] >= castTimer)
            {
                
                NPC.netUpdate = true;

                if (!isCasting)
                {
                    
                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (NPC.life >= hpThreshold2ndPhase && phase == 1)
                    {
                       if (NPC.ai[2] == 0)
                       {
                            nextCast = "The Bitter End";//
                       }
                       if (NPC.ai[2] == 1)
                       {
                           nextCast = "Blazing Skies";
                       }
                       if (NPC.ai[2] == 2)
                       {
                           nextCast = "Coruscant Saber";
                            NPC.ai[2] = 0;
                       }




                        if (nextCast == "The Bitter End")
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheBitterEnd, NPC.Center);
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;


                        }
                        if (nextCast == "Blazing Skies")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_RefulgentEther, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blazing Skies";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Coruscant Saber")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_BegoneSpawnOfShadow, NPC.Center);
                            Terraria.Audio.SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_BegoneSpawnOfShadow, NPC.Center);

                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SaberAOE").Type,0,0f,Main.myPlayer);}
                            CoruscantSaberSaved = vector8;
                            nextAttack = "Coruscant Saber";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (!isCasting)
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheBitterEnd, NPC.Center);
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }
                    // Phase 2 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (NPC.life >= hpThreshold3rdPhase && phase == 2) 
                    {
                        //Boss Rotation
                        if (NPC.ai[2] == 0)
                        {
                            nextCast = "The Bitter End";//
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Absolute Summoning";
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "Absolute Rend Heaven";
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "Solemn Confiteor";//
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Sinpurge";//The Bitter End
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Absolute Blizzard III";
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Blades of Light";//
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Absolute Linear Mystics";
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "Coruscant Saber II";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "Unabated Radiance";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Blazing Skies II";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Absolute Fire III";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Coruscant Saber II";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Absolute Thunder IV";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Absolute Rend Heaven";//
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Blazing Skies II";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "Coruscant Saber II";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "The Bitter End";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "Absolute Fire III";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Sinpurge";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Coruscant Saber II";
                           
                        }
                        if (NPC.ai[2] == 22)
                        {
                            nextCast = "Absolute Rend Heaven";
                            
                        }
                        if (NPC.ai[2] == 23)
                        {
                            nextCast = "Unabated Radiance";
                            
                        }
                        if (NPC.ai[2] == 24)
                        {
                            nextCast = "Blades of Light";
                            
                        }
                        if (NPC.ai[2] == 25)
                        {
                            nextCast = "Absolute Rend Heaven";
                            
                        }
                        if (NPC.ai[2] == 26)
                        {
                            nextCast = "Coruscant Saber II";
                            
                        }
                        if (NPC.ai[2] == 27)
                        {
                            nextCast = "Absolute Blizzard III";
                            
                        }
                        if (NPC.ai[2] == 28)
                        {
                            nextCast = "Absolute Fire III";
                        }
                        if (NPC.ai[2] == 29)
                        {
                            nextCast = "Blazing Skies II";
                        }
                        if (NPC.ai[2] == 30)
                        {
                            nextCast = "Coruscant Saber II";

                        }
                        if (NPC.ai[2] == 31)
                        {
                            nextCast = "Absolute Rend Heaven";

                        }
                        if (NPC.ai[2] == 32)
                        {
                            nextCast = "Unabated Radiance";

                        }
                        if (NPC.ai[2] == 33)
                        {
                            nextCast = "Blades of Light";

                        }
                        if (NPC.ai[2] == 34)
                        {
                            nextCast = "Absolute Rend Heaven";

                        }
                        if (NPC.ai[2] == 35)
                        {
                            nextCast = "Coruscant Saber II";

                        }
                        if (NPC.ai[2] == 36)
                        {
                            nextCast = "Absolute Linear Mystics";
                            NPC.ai[2] = 0;
                        }
                        //End of Rotation
                        if (nextCast == "Absolute Rend Heaven")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_HopeGrantMeStrength, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Rend Heaven";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                            for (int d = 0; d < 3500; d += 500)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                type = Mod.Find<ModProjectile>("AbsoluteRendHeaven").Type;
                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                            }
                        }
                        if (nextCast == "The Bitter End")
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheBitterEnd, NPC.Center);
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Linear Mystics")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_MankindsFirstHeroAndHisFinalHope, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Linear Mystics";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Blazing Skies II")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_RefulgentEther, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blazing Skies II";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Blades of Light")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_ForVictoryIRenderUpMyAll, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blades of Light";
                            castTime = 0;
                            castTimeMax = 70;
                            isCasting = true;
                        }
                        if (nextCast == "Unabated Radiance")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_CladInPrayerIAmInvincible, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Unabated Radiance";
                            castTime = 0;
                            castTimeMax = 70;
                            isCasting = true;
                        }
                        if (nextCast == "Coruscant Saber II")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_BegoneSpawnOfShadow, NPC.Center);
                            castDelay = 0;
                            
                            nextAttack = "Coruscant Saber II";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Sinpurge")
                        {
                            castAnimation = 70;
                           
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_IWillNotFall, NPC.Center);
                            castDelay = 0;

                            nextAttack = "Sinpurge";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Solemn Confiteor")
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_GleamingSteelLightMyPath, NPC.Center);
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SolemnConfiteor").Type,0,0f,Main.myPlayer);}
                            SolemnConfiteorSaved = vector8;
                            nextAttack = "Solemn Confiteor";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Fire III")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_MySoulKnowsNoSurrender, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Fire III";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Blizzard III")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_MySoulKnowsNoSurrender, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Blizzard III";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Thunder IV")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YourDemiseShallBeOurSalvation, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Thunder IV";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Holy")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_DarknessMustBeDestroyed, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Holy";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Summoning")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_ToMeWarriorsOfLight, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Summoning";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (!isCasting)
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheBitterEnd, NPC.Center);
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }
                    // Phase 3 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (phase == 3 && (NPC.life > hpThresholdFinalPhase || surpassingInfinity)) 
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y - 400, NPCType<NPCs.SpectreOfLight>(), NPC.whoAmI);
                        blazingSkies = 20;
                        //Boss Rotation
                        
                        if (NPC.ai[2] == 0)
                        {

                            nextCast = "Quintuplecast";//
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "The Bitter End";
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "Sinpurge II";//
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Absolute Linear Mystics";
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Terror Unleashed";
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Sinpurge II";
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "SOUL Extraction";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "To The Limit";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Radiant Braver";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Unabated Radiance";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Sinpurge II";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Light Rampant";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Terror Unleashed";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Absolute Linear Mystics";//
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Quintuplecast";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "Rays of Punishment";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "To The Limit";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Radiant Desperado";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Coruscant Saber II";
                        }
                        if (NPC.ai[2] == 22)
                        {
                            nextCast = "Blades of Light";
                        }
                        if (NPC.ai[2] == 23)
                        {
                            nextCast = "Rays of Punishment";
                        }
                        if (NPC.ai[2] == 24)
                        {
                            nextCast = "SOUL Extraction";
                        }
                        if (NPC.ai[2] == 25)
                        {
                            nextCast = "Quintuplecast";
                        }
                        if (NPC.ai[2] == 26)
                        {
                            nextCast = "Absolute Summoning";
                        }
                        if (NPC.ai[2] == 27)
                        {
                            nextCast = "Terror Unleashed";
                        }
                        if (NPC.ai[2] == 28)
                        {
                            nextCast = "Absolute Titanomachy";
                        }
                        if (NPC.ai[2] == 29)
                        {
                            nextCast = "To The Limit";
                        }
                        if (NPC.ai[2] == 30)
                        {
                            nextCast = "Radiant Meteor";//"rotation 1
                        }
                        if (NPC.ai[2] == 31)
                        {

                            nextCast = "Blades of Light";//
                        }
                        if (NPC.ai[2] == 32)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 33)
                        {
                            nextCast = "The Bitter End";
                        }
                        if (NPC.ai[2] == 34)
                        {
                            nextCast = "Absolute Titanomachy";//
                        }
                        if (NPC.ai[2] == 35)
                        {
                            nextCast = "Light Rampant";
                        }
                        if (NPC.ai[2] == 36)
                        {
                            nextCast = "Absolute Summoning";
                        }
                        if (NPC.ai[2] == 37)
                        {
                            nextCast = "Sinpurge II";//
                        }
                        if (NPC.ai[2] == 38)
                        {
                            nextCast = "Unabated Refulgence";
                        }
                        if (NPC.ai[2] == 39)
                        {
                            nextCast = "SOUL Extraction";
                        }
                        if (NPC.ai[2] == 40)
                        {
                            nextCast = "To The Limit";//
                        }
                        if (NPC.ai[2] == 41)
                        {
                            nextCast = "Radiant Braver";
                        }
                        if (NPC.ai[2] == 42)
                        {
                            nextCast = "Coruscant Saber II";
                        }
                        if (NPC.ai[2] == 43)
                        {
                            nextCast = "The Bitter End";//
                        }
                        if (NPC.ai[2] == 44)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 45)
                        {
                            nextCast = "Terror Unleashed";
                        }
                        if (NPC.ai[2] == 46)
                        {
                            nextCast = "Rays of Punishment";//
                        }
                        if (NPC.ai[2] == 47)
                        {
                            nextCast = "Quintuplecast";
                        }
                        if (NPC.ai[2] == 48)
                        {
                            nextCast = "Absolute Holy";
                        }
                        if (NPC.ai[2] == 49)
                        {
                            nextCast = "Unabated Refulgence";
                        }
                        if (NPC.ai[2] == 50)
                        {
                            nextCast = "To The Limit";
                        }
                        if (NPC.ai[2] == 51)
                        {
                            nextCast = "Radiant Desperado";
                        }
                        if (NPC.ai[2] == 52)
                        {
                            nextCast = "Coruscant Saber II";
                        }
                        if (NPC.ai[2] == 53)
                        {
                            nextCast = "Absolute Titanomachy";
                        }
                        if (NPC.ai[2] == 54)
                        {
                            nextCast = "Absolute Blizzard III";
                        }
                        if (NPC.ai[2] == 55)
                        {
                            nextCast = "SOUL Extraction";
                        }
                        if (NPC.ai[2] == 56)
                        {
                            nextCast = "Absolute Linear Mystics";
                        }
                        if (NPC.ai[2] == 57)
                        {
                            nextCast = "Absolute Summoning";
                        }
                        if (NPC.ai[2] == 58)
                        {
                            nextCast = "To The Limit";
                        }
                        if (NPC.ai[2] == 59)
                        {
                            nextCast = "Radiant Braver";
                        }
                        if (NPC.ai[2] == 60)
                        {
                            nextCast = "To The Limit";
                        }
                        if (NPC.ai[2] == 61)
                        {
                            nextCast = "Radiant Meteor";
                            NPC.ai[2] = 0;
                        }
                        

                        //End of Rotation
                        if (nextCast == "Absolute Rend Heaven")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_ComeShowMeYourStrength, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Rend Heaven";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                            for (int d = 0; d < 3500; d += 500)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                type = Mod.Find<ModProjectile>("AbsoluteRendHeaven").Type;
                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                            }
                        }
                        if (nextCast == "The Bitter End")
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_HaveAtYou, NPC.Center);
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Linear Mystics")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheGameIsUp, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Linear Mystics";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Titanomachy")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_ItsTimeWeSettledThis, NPC.Center);


                            castDelay = 0;
                            nextAttack = "Absolute Titanomachy";
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
                        if (nextCast == "Blazing Skies II")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheGameIsUp, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blazing Skies II";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Blades of Light")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YoureNoMatchForMe, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Blades of Light";
                            castTime = 0;
                            castTimeMax = 70;
                            isCasting = true;
                        }
                        if (nextCast == "Unabated Radiance")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_LightClaimYou, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Unabated Radiance";
                            castTime = 0;
                            castTimeMax = 70;
                            isCasting = true;
                        }
                        if (nextCast == "Coruscant Saber II")
                        {
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheGameIsUp, NPC.Center);
                            castDelay = 0;

                            nextAttack = "Coruscant Saber II";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Sinpurge II")
                        {
                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YoureNotGoingAnywhere, NPC.Center);
                            castDelay = 0;

                            nextAttack = "Sinpurge II";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Solemn Confiteor")
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YoureNoMatchForMe, NPC.Center);
                            castAnimation = 70;
                            Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                            castDelay = 0;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, 0, 0,Mod.Find<ModProjectile>("SolemnConfiteor").Type,0,0f,Main.myPlayer);}
                            SolemnConfiteorSaved = vector8;
                            nextAttack = "Solemn Confiteor";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                            for (int i = 0; i < Main.maxPlayers; i++)
                            {
                                Player player = Main.player[i];
                                if (player.active && modPlayer.inWarriorOfLightFightTimer > 0)
                                    player.AddBuff(BuffID.Chilled, 60);  //

                            }
                        }
                        if (nextCast == "Absolute Fire III")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheGameIsUp, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Fire III";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Blizzard III")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheGameIsUp, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Blizzard III";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Thunder IV")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_ComeShowMeYourStrength, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Thunder IV";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Quintuplecast")
                        {
                            castAnimation = 70;
                            //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LimitBreakCharge"));
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_ItsTimeWeSettledThis, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Quintuplecast";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Holy")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_LightClaimYou, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Holy";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Absolute Summoning")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_AnswerMyCall, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Absolute Summoning";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "To The Limit")
                        {
                            if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The Warrior of Light is transcending his limits!"), 210, 60, 60);}

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_TheLightWillCleanseYourSins, NPC.Center);
                            castDelay = 0;
                            nextAttack = "To The Limit";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Radiant Braver")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YoureNotGoingAnywhere, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Radiant Braver";
                            castTime = 0;
                            castTimeMax = 80;
                            for (int d = 0; d < 900; d += 300)
                            {

                                Vector2 placement = new Vector2((P.Center.X) + d, P.position.Y);
                                int type;
                                type = Mod.Find<ModProjectile>("RadiantBraver").Type;

                                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 300, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}


                            }
                            isCasting = true;
                        }
                        if (nextCast == "Radiant Desperado")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YoureNoMatchForMe, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Radiant Desperado";
                            castTime = 0;
                            castTimeMax = 150;
                            isCasting = true;
                        }
                        if (nextCast == "Radiant Meteor")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YourLifeIsMineForTheTaking, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Radiant Meteor";
                            castTime = 0;
                            castTimeMax = 200;
                            isCasting = true;
                        }
                        if (nextCast == "Terror Unleashed")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_AFeebleShieldProtectsNothing, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Terror Unleashed";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "SOUL Extraction")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_LetsTrySomethingElse, NPC.Center);
                            castDelay = 0;
                            nextAttack = "SOUL Extraction";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Light Rampant")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_IveToyedWithYouLongEnough, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Light Rampant";
                            castTime = 0;
                            castTimeMax = 100;
                            isCasting = true;
                        }
                        if (nextCast == "Rays of Punishment")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YoureNotGoingAnywhere, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Rays of Punishment";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (!isCasting)
                        {
                            isSwinging = true;
                            swingAnimation = 120;
                            SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_HaveAtYou, NPC.Center);
                            castDelay = 0;
                            nextAttack = "The Bitter End";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }



                    // Special attacks /////////////////////////////////////////////////////////////////////////////////////////////

                    if (phase == 3 && !surpassingInfinity && NPC.life < hpThresholdFinalPhase)//Final phase threshold
                    {
                        castAnimation = 70;
                        nextCast = "";
                        surpassingInfinity = true;
                        castDelay = 0;
                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_FinalPhaseGrunt, NPC.Center);
                        phase = 3;
                        castTime = 0;
                        castTimeMax = 315;
                        isCasting = true;
                        introVelocityY = -10;
                        nextAttack = "Surpassing Infinity";
                        NPC.dontTakeDamage = true;
                        NPC.netUpdate = true;



                    }

                    if (NPC.life < hpThreshold3rdPhase && phase == 2 && stunCasted != true)//3rd phase threshold
                    {
                        castAnimation = 70;

                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YouStillStand, NPC.Center);
                        castDelay = 0;

                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                        nextAttack = "Concentrativity";
                        NPC.netUpdate = true;
                        NPC.ai[2] = 0;

                    }
                    if (phase == 2 && stunCasted == true)
                    {
                        castAnimation = 70;
                        
                        
                        castDelay = 0;
                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_IveToyedWithYouLongEnough, NPC.Center);
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The fabric of this world begins to fray!"), 210, 60, 60);}
                        phase = 3;
                        castTime = 0;
                        castTimeMax = 315;
                        isCasting = true;
                        introVelocityY = -10;
                        nextAttack = "A World Rent Asunder";
                        NPC.netUpdate = true;
                       
                        
                        
                    }
                    if (NPC.life < hpThreshold2ndPhase && phase == 1)//2nd phase threshold
                    {
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Your final days have arrived..."), 210, 60, 60);}
                        castAnimation = 70;
                        phase = 2;
                        SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_IAmSalvationGivenForm, NPC.Center);
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/iceCracking"));
                        castDelay = 0;
                        nextAttack = "Ascendance";
                        castTime = 0;
                        castTimeMax = 200;
                        isCasting = true;
                        NPC.netUpdate = true;
                        NPC.ai[2] = 0;

                    }


                    NPC.ai[2]++;
                    //npc.ai[4]++;



                }
                
                
                 NPC.ai[1] = 0;
                
                
            }
            NPC.netUpdate = true;

           

        }
        /*public bool fightStart = true;
        public bool onFall = true;
        public int nframe;
        public int castTime;
        public int castTimeMax;
        public int castDelay;
        public bool isCasting;
        public bool isSwinging;
        public int randomCast;
        public string nextAttack;
        public string lastAttack;
        Vector2 CoruscantSaberSaved;
        public int phase = 1;

        public int blazingSkies = 0;
        public int blazingSkiesTimer = 0;*/



        /*public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(fightStart);
            writer.Write(castTime);
            writer.Write(castTimeMax);
            writer.Write(castDelay);
            writer.Write(isCasting);
            writer.Write(isSwinging);
            writer.Write(randomCast);
            writer.Write(nextAttack);
            writer.Write(lastAttack);
            writer.WriteVector2(CoruscantSaberSaved);
            writer.Write(phase);
            writer.Write(swingAnimation);
            writer.Write(castAnimation);

            writer.Write(blazingSkies);
            writer.Write(blazingSkiesTimer);
            writer.Write(lightningSkies);
            writer.Write(lightningSkiesTimer);
            writer.Write(QuintuplecastSkies);
            writer.Write(QuintuplecastTimer);
        }
        public void RecieveExtraAI(BinaryReader reader)
        {
            fightStart = reader.ReadBoolean(); // fight started?
                                               // = reader.ReadInt32(); // "sdfsdfjdsf"
                                               // bool someBool = reader.ReadBoolean(); // true
                                               //
            castTime = reader.ReadInt32();
            castTimeMax = reader.ReadInt32();
            castDelay = reader.ReadInt32();
            isCasting = reader.ReadBoolean();
            isSwinging = reader.ReadBoolean();
            randomCast = reader.ReadInt32();
            nextAttack = reader.ReadString();
            lastAttack = reader.ReadString();
            CoruscantSaberSaved = reader.ReadVector2();
            phase = reader.ReadInt32();
            swingAnimation = reader.ReadInt32();
            castAnimation = reader.ReadInt32();

            blazingSkies = reader.ReadInt32();
            blazingSkiesTimer = reader.ReadInt32();
            lightningSkies = reader.ReadInt32();
            lightningSkiesTimer = reader.ReadInt32();
            QuintuplecastSkies = reader.ReadInt32();
            QuintuplecastTimer = reader.ReadInt32();

        }*/
        

    }
}//