using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;

using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;
using StarsAbove.Items.BossBags;
using StarsAbove.Items.Loot;
using StarsAbove.Systems;

namespace StarsAbove.NPCs
{
    [AutoloadBossHead]
    public class Arbitration : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Arbitration");
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Position = new Vector2(10, 15),
                //Scale = 0.9f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = 0f,
                //PortraitPositionXOverride = 10f,

            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

        }
        public override void SetDefaults()
        {
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                NPC.lifeMax = 444666;
                NPC.defense = 40;
            }
            else
            {
                NPC.lifeMax = 226660;
                NPC.defense = 25;
            }
           
            NPC.boss = true;
            NPC.aiStyle = 10;
           
            NPC.damage = 0;
           
            NPC.knockBackResist = 0f;
            NPC.width = 214;
            NPC.height = 176;
            NPC.scale = 1.3f;
            Main.npcFrameCount[NPC.type] = 6;
            NPC.value = Item.buyPrice(0, 1, 75, 45);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.buffImmune[24] = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };

            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/FirstWarning");
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
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				
				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ArbitrationBossBag>()));

            // Trophies are spawned with 1/10 chance
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.ArbitrationTrophyItem>(), 10));

            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.ArbitrationBossRelicItem>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            StellarSpoils.SetupBossStellarSpoils(npcLoot);

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.VoidsentPrism>(), 4));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Essences.EssenceOfBloodshed>(), 2)).OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Essences.EssenceOfMimicry>(), 2));


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
            

            if (!DownedBossSystem.downedArbiter)
            {
                DownedBossSystem.downedArbiter = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = 0;
            NPC.spriteDirection = 0;



            

            if (isCasting || isSwinging)
            {
                if(alignment == 0)//Chaotic Alignment
                {
                    NPC.frame.Y = 1 * frameHeight;
                }

                if (alignment == 1)//Order Alignment
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                if (alignment == 2)//Seraphim
                {
                    NPC.frame.Y = 5 * frameHeight;
                }
            }
            else
            {

                if (alignment == 0)//Chaotic Alignment
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                if (alignment == 1)//
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                if (alignment == 2)//
                {
                    NPC.frame.Y = 4 * frameHeight;
                }
            }

            // if (isSwinging)

        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
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
                    SoundEngine.PlaySound(StarsAboveAudio.Arbitration_WasIsAndWillForeverBe, NPC.Center);
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ArbiterDefeated"));
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    if (!DownedBossSystem.downedArbiter)
                    {
                        DownedBossSystem.downedArbiter = true;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                        }
                    }
                    modPlayer.ArbiterActive = false;
                    modPlayer.ArbiterBarActive = false;
                    modPlayer.undertaleActive = false;
                }
                return;
            }






            //PreCast effects

            if (NPC.active)
            {
                modPlayer.ArbiterActive = true;
                modPlayer.ArbiterCastTime = castTime;
                modPlayer.ArbiterCastTimeMax = castTimeMax;
                modPlayer.ArbiterNextAttack = nextAttack;
                modPlayer.ArbiterPhase = alignment;
                NPC.netUpdate = true;
            }
            else
            {
                modPlayer.ArbiterActive = false;
                modPlayer.ArbiterBarActive = false;
            }
            if (phase == 2)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SecondWarning");
                
               
                
                
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
                    SoundEngine.PlaySound(StarsAboveAudio.Arbitration_WasIsAndWillForeverBe, NPC.Center);
                    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You have been judged..."), 210, 60, 60);}
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
                modPlayer.ArbiterActive = false;
                modPlayer.ArbiterBarActive = false;
                NPC.velocity.Y -= 0.1f;
                NPC.timeLeft = 0;
            }



           
            if (nextAttack == "Threads of Fate" && castTime == 28)
            {
                for (int d = 0; d < 3; d++)
                {
                    float Speed = 20f;  //projectile speed
                                        //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(500, 600), P.position.Y - 800);
                    Vector2 vector9 = new Vector2(P.position.X - Main.rand.Next(500, 600), P.position.Y - 800);
                    int damage = 90;  //projectile damage
                    int type;

                    type = Mod.Find<ModProjectile>("ThreadsOfFate").Type;



                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f) + Main.rand.Next(-200,200)));
                    float rotation2 = (float)Math.Atan2(vector9.Y - (P.position.Y + (P.height * 0.5f)), vector9.X - (P.position.X + (P.width * 0.5f) + Main.rand.Next(-200, 200)));

                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector9.X, vector9.Y, (float)((Math.Cos(rotation2) * Speed) * -1), (float)((Math.Sin(rotation2) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                }
            }

            if(nextAttack == "The Void Consumes All" && castTime < 10)
            {
                savedAlignment = alignment;
            }

           
            if (nextAttack == "The Void Consumes All" && castTime == 179)
            {
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,mod.ProjectileType("radiate"),0,0f,Main.myPlayer);}
                alignment = 2;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 181)
            {
                alignment = savedAlignment;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 202)
            {
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,mod.ProjectileType("radiate"),0,0f,Main.myPlayer);}
                alignment = 2;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 204)
            {
                alignment = savedAlignment;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 255)
            {
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
               // if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,mod.ProjectileType("radiate"),0,0f,Main.myPlayer);}
                alignment = 2;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 257)
            {
                alignment = savedAlignment;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 305)
            {
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
               // if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,mod.ProjectileType("radiate"),0,0f,Main.myPlayer);}
                alignment = 2;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 307)
            {
                alignment = savedAlignment;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 321)
            {
                Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement2.X, placement2.Y, 0, 0,Mod.Find<ModProjectile>("radiate").Type,0,0f,Main.myPlayer);}
                alignment = 2;
            }
            if (nextAttack == "The Void Consumes All" && castTime == 323)
            {
                alignment = savedAlignment;
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
                    

                    if (alignment == 2)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            // Charging dust
                            Vector2 vector = new Vector2(
                                Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                                Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                            Dust d = Main.dust[Dust.NewDust(
                                NPC.Center + vector, 1, 1,
                                107, 0, 0, 255,
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

                            Dust d = Dust.NewDustPerfect(NPC.Center + offset, 0, NPC.velocity, 107, default(Color), 0.7f);
                            
                            d.fadeIn = 1f;
                            d.noGravity = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            // Charging dust
                            Vector2 vector = new Vector2(
                                Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                                Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                            Dust d = Main.dust[Dust.NewDust(
                                NPC.Center + vector, 1, 1,
                                14, 0, 0, 255,
                                new Color(1f, 1f, 1f), 1.5f)];
                            d.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
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

                            Dust d = Dust.NewDustPerfect(NPC.Center + offset, 0, NPC.velocity, 14, default(Color), 0.7f);
                            d.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
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

                modPlayer.ArbiterBarActive = true;
                //Here are the attacks and their effects ///////////////////////////////////////
                if (castTime >= castTimeMax)
                {
                    if(alignment == 2)
                    {
                        for (int d = 0; d < 2000; d += 200)
                        {

                            Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                            int type;
                            
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 1000, placement.Y - 600, 0, 1,ProjectileID.PhantasmalBolt,40,0f,Main.myPlayer);}
                            
                        }

                    }
                    
                    if (nextAttack == "Beneath a Bleak Sky")
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
                            int type = ProjectileID.DeathLaser;
                            if(alignment == 2)
                            {
                                type = ProjectileID.PhantasmalBolt;
                            }
                            
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                    if (nextAttack == "Hello, World")
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
                                int randomDebuff1 = Main.rand.Next(0, 2);
                                if (randomDebuff1 == 0)
                                {
                                    player.AddBuff(BuffType<Buffs.Pyretic>(), 540);
                                   
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 219, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (randomDebuff1 == 1)
                                {
                                    player.AddBuff(BuffType<Buffs.DeepFreeze>(), 540);
                                    
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 221, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                int randonBuff = Main.rand.Next(0, 2);
                                if (randonBuff == 0)
                                {
                                    player.AddBuff(BuffType<Buffs.LeftDebuff>(), 440);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (randonBuff == 1)
                                {
                                    player.AddBuff(BuffType<Buffs.RightDebuff>(), 440);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }

                            }

                        }

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
                    if(nextAttack == "Synaptic Syntax")
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


                        
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,Mod.Find<ModProjectile>("ChaosBlaster3").Type,0,0f,Main.myPlayer);}





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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                    }
                    if (nextAttack == "Threads of Fate")
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                    if (nextAttack == "Titanomachy")
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


                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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


                            //if (player.active && modPlayer.inArbiterFightTimer > 0)


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }


                        for (int i = 0; i < 20; i++)
                        {
                            // Random upward vector.
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-16, 16), Main.rand.NextFloat(-9, -20));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, vector2, ProjectileID.DD2BetsyFireball, 40, 0, 0, NPC.whoAmI, 1);}
                        }

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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
                            {

                            }
                            //player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PhaseChange"));
                        QuintuplecastSkies = 80;




                    }
                    if (nextAttack == "Abrasion")
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
                    if (nextAttack == "The Void Consumes All")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_PrepDarkness, NPC.Center);
                        NPC.dontTakeDamage = false;
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }

                    }
                    
                    if (nextAttack == "Galactic Swarm")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        for (int d = 0; d < 12; d++)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y + Main.rand.Next(-365, 365), NPCType<NPCs.OffworldNPCs.AstralCell>(), NPC.whoAmI);
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                            if (player.active && modPlayer.inArbiterFightTimer > 0)
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
                    

                }
            }
            else
            {
                modPlayer.ArbiterBarActive = false;
            }
            if (alignment == 2)
            {

                modPlayer.IsVoidActive = true;
                
            }
            else
            {
                modPlayer.IsVoidActive = false;
            }
            if (teleportAway)
            {
                NPC.ai[1] += 20;
            }
            undertaleTimer--;
            if (undertaleTimer <= 0)
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
                if (phase > 1 && (phase != 3))
                {
                    //blazingSkiesTimer++;
                }
            }
            if (blazingSkiesTimer >= 20)
            {



                float Speed = 2f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                Vector2 vector9 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type;

                type = ProjectileID.PhantasmalBolt;
                if(alignment == 2)
                {
                   
                    Speed = 3f;
                }

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
            if (eyeProjectileTimer >= 20)
            {
                float Speed = 3f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-1200, 1200), P.position.Y - 800);
                int damage = 80;  //projectile damage
                int type = ProjectileID.FlamingScythe;
                if(phase == 2)
                {
                    type = ProjectileID.NebulaLaser;
                    Speed = 7f;
                }

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                //SoundEngine.PlaySound(SoundID.Item, vector8);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                eyeProjectileTimer = 0;
                eyeProjectile--;
            }

            if (QuintuplecastSkies > 0)
            {
                NPC.ai[1] = 300;
                QuintuplecastTimer++;

            }
            if (QuintuplecastTimer >= 4)
            {
                float Speed2 = Main.rand.NextFloat(3, 10);  //projectile speed
                                                            //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector82 = new Vector2(P.position.X + Main.rand.Next(-1200, 1200), P.position.Y - 800);
                int damage2 = 55;  //projectile damage
                int type2 = Mod.Find<ModProjectile>("InkBlot").Type;

                float rotation2 = (float)Math.Atan2(-70, 0);

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector82.X, vector82.Y, (float)((Math.Cos(rotation2) * Speed2) * -1), (float)((Math.Sin(rotation2) * Speed2) * -1),type2,damage2,0f,Main.myPlayer);}



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
                int damage = 80;  //projectile damage
                int type = 302;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                desperadoTimer = 0;
                desperadoShots--;
            }


            NPC.ai[1] += 3;
            if (!Main.player[NPC.target].dead)
            {


                if (!isCasting)
                {
                    // if (!fightStart)
                    //{
                    if (!inIntro && !fightStart)
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
                    //npc.velocity = Vector2.Zero;
                    NPC.ai[0] = 120f;
                }
            }
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
                    SoundEngine.PlaySound(StarsAboveAudio.Arbitration_LongHaveWeWaited, NPC.Center);
                    NPC.ai[1] = -300;



                    fightStart = false;
                }
            }
            introVelocityTimer++;
            if (introVelocityY > 0)
            {
                if (introVelocityTimer >= 15)
                {
                    //npc.velocity = new Vector2(0, (introVelocityY));
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
                if (NPC.life >= NPC.lifeMax - 1)
                {
                    //npc.velocity = Vector2.Zero;
                }
            }


            if (phase > 1)
            {
                NPC.ai[1] += 2;
            }


            //Attack generation
            int hpThreshold;
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                hpThreshold = 210000;
            }
            else
            {
                hpThreshold = 100000;
            }
           
            
            //Attack generation
            if (NPC.ai[1] >= 500)//DEBUG IT SHOULD BE 500
            {

                NPC.netUpdate = true;

                if (!isCasting)
                {


                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (NPC.life >= hpThreshold)
                    {

                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{npc.ai[2]}"), 210, 60, 60);}
                        //Boss Rotation
                        if (NPC.ai[2] == 0)
                        {
                            nextCast = "Suppuration";//
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Beneath a Bleak Sky";
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "Threads of Fate";//Linear Mystics
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "Order through Chaos";//Spilled Violet
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Abrasion";
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Chaos through Order";
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "Total Isolation";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Suppuration";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Order through Chaos";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Total Isolation";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Overflow Error";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "Chaos through Order";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "Suppuration";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "Beneath a Bleak Sky";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Total Isolation";
                        }
                        if (NPC.ai[2] == 22)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 23)
                        {
                            nextCast = "Order through Chaos";
                        }
                        if (NPC.ai[2] == 24)
                        {
                            nextCast = "Overflow Error";
                        }
                        if (NPC.ai[2] == 25)
                        {
                            nextCast = "Total Isolation";
                        }
                        if (NPC.ai[2] == 26)
                        {
                            nextCast = "Hello, World";
                        }
                        if (NPC.ai[2] == 27)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 28)
                        {
                            nextCast = "Chaos through Order";
                        }
                        if (NPC.ai[2] == 29)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 30)
                        {
                            nextCast = "Suppuration";
                        }
                        if (NPC.ai[2] == 31)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 32)
                        {
                            nextCast = "Ablation";
                            NPC.ai[2] = 0;

                        }//Phase 2 (kind of)
                        
                        //End of Rotation
                        if (nextCast == "Linear Mystics")
                        {

                            castAnimation = 70;


                            castDelay = 0;
                            nextAttack = "Linear Mystics";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Hello, World")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_DespairInOurPresence, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Hello, World";
                            castTime = 0;
                            castTimeMax = 150;
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
                        if (nextCast == "Overflow Error")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_TheFirstAreWeTheLastAreWe, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Overflow Error";
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
                        if (nextCast == "Synaptic Syntax")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_DepriveThemOfLife, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Synaptic Syntax";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
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

                        if (nextCast == "Abrasion")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterGrunt, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Abrasion";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                        }
                        if (nextCast == "Galactic Swarm")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
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
                        if (nextCast == "Beneath a Bleak Sky")
                        {
                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_DespairInOurPresence, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Beneath a Bleak Sky";
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
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ABlightTakesThisLand, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Ablation";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Gravitational Anomaly")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
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
                        if (nextCast == "Prototokia Aster")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                            castDelay = 0;
                            nextAttack = "Prototokia Aster";
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
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterLaugh, NPC.Center);                            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
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
SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterLaugh, NPC.Center);
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
                        
                        if (nextCast == "Suppuration")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_TheyAreRightToFear, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Suppuration";
                            castTime = 0;
                            castTimeMax = 90;
                            isCasting = true;
                        }
                        if (nextCast == "Total Isolation")
                        {
                            castAnimation = 70;
SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterLaugh, NPC.Center);
                            //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RefulgentEther"));
                            castDelay = 0;
                            nextAttack = "Total Isolation";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Threads of Fate")
                        {
                            castAnimation = 70;
                            if(Main.rand.Next(0,2) == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.Arbitration_FateCanNotBeAverted, NPC.Center);
                            }
                            else
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.Arbitration_FulfillDestiny, NPC.Center);
                            }
                            
                            castDelay = 0;
                            nextAttack = "Threads of Fate";
                            castTime = 0;
                            castTimeMax = 50;
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
                            nextAttack = "Threads of Fate";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
                            NPC.ai[2] = 0;

                        }
                        NPC.netUpdate = true;
                    }
                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////
                    if (phase == 2)
                    {

                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{npc.ai[2]}"), 210, 60, 60);}
                        //Boss Rotation
                        if (NPC.ai[2] == 0)
                        {
                            nextCast = "Suppuration";//
                        }
                        if (NPC.ai[2] == 1)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 2)
                        {
                            nextCast = "Threads of Fate";//Linear Mystics
                        }
                        if (NPC.ai[2] == 3)
                        {
                            nextCast = "Beneath a Bleak Sky";//Spilled Violet
                        }
                        if (NPC.ai[2] == 4)
                        {
                            nextCast = "Abrasion";
                        }
                        if (NPC.ai[2] == 5)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 6)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 7)
                        {
                            nextCast = "Overflow Error";
                        }
                        if (NPC.ai[2] == 8)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 9)
                        {
                            nextCast = "Threads of Fate";//
                        }
                        if (NPC.ai[2] == 10)
                        {
                            nextCast = "Titanomachy";
                        }
                        if (NPC.ai[2] == 11)
                        {
                            nextCast = "Suppuration";
                        }
                        if (NPC.ai[2] == 12)
                        {
                            nextCast = "Total Isolation";//
                        }
                        if (NPC.ai[2] == 13)
                        {
                            nextCast = "Overflow Error";
                        }
                        if (NPC.ai[2] == 14)
                        {
                            nextCast = "Titanomachy";
                        }
                        if (NPC.ai[2] == 15)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 16)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 17)
                        {
                            nextCast = "Beneath a Bleak Sky";
                        }
                        if (NPC.ai[2] == 18)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 19)
                        {
                            nextCast = "Overflow Error";
                        }
                        if (NPC.ai[2] == 20)
                        {
                            nextCast = "Abrasion";
                        }
                        if (NPC.ai[2] == 21)
                        {
                            nextCast = "Suppuration";
                        }
                        if (NPC.ai[2] == 22)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 23)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 24)
                        {
                            nextCast = "Titanomachy";
                        }
                        if (NPC.ai[2] == 25)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 26)
                        {
                            nextCast = "Overflow Error";
                        }
                        if (NPC.ai[2] == 27)
                        {
                            nextCast = "Ablation";
                        }
                        if (NPC.ai[2] == 28)
                        {
                            nextCast = "Synaptic Syntax";
                        }
                        if (NPC.ai[2] == 29)
                        {
                            nextCast = "Suppuration";
                        }
                        if (NPC.ai[2] == 30)
                        {
                            nextCast = "Beneath a Bleak Sky";
                        }
                        if (NPC.ai[2] == 31)
                        {
                            nextCast = "Threads of Fate";
                        }
                        if (NPC.ai[2] == 32)
                        {
                            nextCast = "Abrasion";
                            NPC.ai[2] = 0;

                        }//Phase 2 (kind of)

                        //End of Rotation
                        if (nextCast == "Linear Mystics")
                        {

                            castAnimation = 70;


                            castDelay = 0;
                            nextAttack = "Linear Mystics";
                            castTime = 0;
                            castTimeMax = 30;
                            isCasting = true;
                        }
                        if (nextCast == "Hello, World")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_DespairInOurPresence, NPC.Center);

                            castDelay = 0;
                            nextAttack = "Hello, World";
                            castTime = 0;
                            castTimeMax = 150;
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
                        if (nextCast == "Overflow Error")
                        {

                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_TheFirstAreWeTheLastAreWe, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Overflow Error";
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
                        if (nextCast == "Synaptic Syntax")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_DepriveThemOfLife, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Synaptic Syntax";
                            castTime = 0;
                            castTimeMax = 50;
                            isCasting = true;
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

                        if (nextCast == "Abrasion")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterGrunt, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Abrasion";
                            castTime = 0;
                            castTimeMax = 60;
                            isCasting = true;
                        }
                        if (nextCast == "Galactic Swarm")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
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
                        if (nextCast == "Beneath a Bleak Sky")
                        {
                            castAnimation = 70;

                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_DespairInOurPresence, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Beneath a Bleak Sky";
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
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ABlightTakesThisLand, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Ablation";
                            castTime = 0;
                            castTimeMax = 80;
                            isCasting = true;
                        }
                        if (nextCast == "Gravitational Anomaly")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
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
                        if (nextCast == "Prototokia Aster")
                        {
                            castAnimation = 70;
                            // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                            castDelay = 0;
                            nextAttack = "Prototokia Aster";
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
SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterLaugh, NPC.Center);
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
SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterLaugh, NPC.Center);
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

                        if (nextCast == "Suppuration")
                        {
                            castAnimation = 70;
                            SoundEngine.PlaySound(StarsAboveAudio.Arbitration_TheyAreRightToFear, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Suppuration";
                            castTime = 0;
                            castTimeMax = 90;
                            isCasting = true;
                        }
                        if (nextCast == "Total Isolation")
                        {
                            castAnimation = 70;
SoundEngine.PlaySound(StarsAboveAudio.Arbitration_ArbiterLaugh, NPC.Center);
                            //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RefulgentEther"));
                            castDelay = 0;
                            nextAttack = "Total Isolation";
                            castTime = 0;
                            castTimeMax = 40;
                            isCasting = true;
                        }
                        if (nextCast == "Threads of Fate")
                        {
                            castAnimation = 70;
                            if (Main.rand.Next(0, 2) == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.Arbitration_FateCanNotBeAverted, NPC.Center);
                            }
                            else
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.Arbitration_FulfillDestiny, NPC.Center);
                            }

                            castDelay = 0;
                            nextAttack = "Threads of Fate";
                            castTime = 0;
                            castTimeMax = 50;
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
                        if (nextCast == "Titanomachy")
                        {
                            castAnimation = 70;
                            if (Main.rand.Next(0, 2) == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.Arbitration_Oblivion, NPC.Center);
                            }
                            else
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.Arbitration_PierceTheVeil, NPC.Center);
                            }
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_TitanPrep, NPC.Center);
                            castDelay = 0;
                            nextAttack = "Titanomachy";
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
                                    type = Mod.Find<ModProjectile>("Titanomachy1").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                                }
                                if (nextType == 2)
                                {
                                    type = Mod.Find<ModProjectile>("Titanomachy2").Type;
                                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 2500, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
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
                                nextAttack = "Threads of Fate";
                                castTime = 0;
                                castTimeMax = 50;
                                isCasting = true;
                                NPC.ai[2] = 0;

                            }
                            NPC.netUpdate = true;
                        
                    }
                    
                    NPC.netUpdate = true;




                    // Special attacks /////////////////////////////////////////////////////////////////////////////////////////////
                    if (NPC.life < hpThreshold && phase == 1)
                    {
                        castAnimation = 70;
                        //if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("BossLaevateinn"), 0, 4, npc.whoAmI, 0, 1);}

                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Time and space begin to crumble, their laws forsaken!"), 255, 0, 0);}
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_EnterDarkness, NPC.Center);
                        SoundEngine.PlaySound(StarsAboveAudio.Arbitration_TheEndOfDaysDrawsNear, NPC.Center);
                        castDelay = 0;
                        phaseTransition = true;
                        castTime = 0;
                        castTimeMax = 330;
                        isCasting = true;
                        nextAttack = "The Void Consumes All";
                        NPC.netUpdate = true;
                        NPC.dontTakeDamage = true;
                        phase = 2;
                        NPC.ai[2] = 0;
                        

                    }


                    NPC.ai[2]++;



                }


                NPC.ai[1] = 0;


            }
            NPC.netUpdate = true;

        }


    }
}