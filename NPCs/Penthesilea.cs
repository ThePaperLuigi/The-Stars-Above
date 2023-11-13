using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

using Terraria.DataStructures;
using Terraria.GameContent;
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
    public class PenthesileaOld : ModNPC
    {
        public static readonly int arenaWidth = (int)(1.2f * 2000);
        public static readonly int arenaHeight = (int)(1.2f * 1000);

        public static bool ColorblindEnabled = false;

        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.aiStyle = -1;

            NPC.lifeMax = 166000;
            NPC.defense = 15;

            NPC.damage = 0;
            NPC.knockBackResist = 0f;
            NPC.width = 160;
            NPC.height = 160;
            NPC.scale = 1f;
            Main.npcFrameCount[NPC.type] = 14;
            NPC.value = Item.buyPrice(0, 1, 75, 45);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.buffImmune[24] = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MageOfViolet");
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
        
        private int portalFrame
        {
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Penthesilea, the Witch of Ink");
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Position = new Vector2(10, 10),
                //Scale = 0.9f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
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
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PenthBossBag>()));

            // Trophies are spawned with 1/10 chance
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.PenthTrophyItem>(), 10));

            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.PenthBossRelicItem>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            StellarSpoils.SetupBossStellarSpoils(npcLoot);

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.PaintedPrism>(), 4));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.FaerieVoyagerAttirePrecursor>(), 8));


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
            
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedPenth, -1);
            
                DownedBossSystem.downedPenth = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            
        }
        public override void FindFrame(int frameHeight)
        {
            /*
            NPC.rotation = 0;
            NPC.spriteDirection = 0;



            NPC.frameCounter++;

            if (NPC.frameCounter >= 7)
            {
                nframe++;
                NPC.frame.Y += frameHeight;
                if (nframe == 8)
                {
                    nframe = 0;
                    NPC.frame.Y = 0;
                }
                NPC.frameCounter = 0;
            }
            */

            NPC.rotation = 0;
            NPC.spriteDirection = NPC.direction;
            swingAnimation--;
            castAnimation--;
            introAnimation--;

            if (NPC.frameCounter >= 10 && introAnimation <= 0 && !inIntro)
            {
                nframe++;
                NPC.frame.Y += frameHeight;
                if (nframe >= 4)
                {
                    nframe = 0;
                    NPC.frame.Y = 0;
                }
                NPC.frameCounter = 0;
            }

            if (inIntro)
            {
                if (introAnimation >= 48)
                    NPC.frame.Y = 9 * frameHeight;
                if (introAnimation < 40)
                    NPC.frame.Y = 8 * frameHeight;
                if (introAnimation < 32)
                    NPC.frame.Y = 7 * frameHeight;
                if (introAnimation < 24)
                    NPC.frame.Y = 6 * frameHeight;
                if (introAnimation < 16)
                    NPC.frame.Y = 5 * frameHeight;
                if (introAnimation < 8)
                    NPC.frame.Y = 4 * frameHeight;
                if(introAnimation <= 0)
                {
                    NPC.frame.Y = 0;
                    NPC.dontTakeDamage = false;
                    inIntro = false;
                }
                
                //NPC.frameCounter++;
                
            }
            else
            {
                NPC.frameCounter++;
            }

            if (isCasting)
            {
                NPC.frame.Y = frameHeight * 10 + nframe * frameHeight;
            }






        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.defense += numPlayers * 10;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int portalWidth = 48;
            int portalDepth = 18;
            Color color = new Color(242, 166, 231);
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
        bool phaseTransition;

        public bool isInvincible;

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }
        /*
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);


            // Retrieve reference to shader
            var deathShader = GameShaders.Misc["StarsAbove:DeathAnimation"];

            // Reset back to default value.
            deathShader.UseOpacity(1f);
            // We use npc.ai[3] as a counter since the real death.
            if (NPC.ai[3] > 100f)
            {
                // Our shader uses the Opacity register to drive the effect. See ExampleEffectDeath.fx to see how the Opacity parameter factors into the shader math. 
                deathShader.UseOpacity(1f - (NPC.ai[3] - 30f) / 550f);
            }

            // Call Apply to apply the shader to the SpriteBatch. Only 1 shader can be active at a time.
            deathShader.Apply(null);
            return true;
        }


        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            // As mentioned above, be sure not to forget this step.
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }*/

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
                
                Music = MusicLoader.GetMusicSlot(Mod,  "Sounds/Music/silence");
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
                    SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_IveEnduredFarWorse, NPC.Center);
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PenthDefeated"));
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    if (!DownedBossSystem.downedPenth)
                    {
                        DownedBossSystem.downedPenth = true;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                        }
                    }
                    modPlayer.PenthActive = false;
                    modPlayer.PenthBarActive = false;
                    modPlayer.undertaleActive = false;
                }
                return;
            }






            //PreCast effects

            if (NPC.active)
            {
                modPlayer.PenthActive = true;
                modPlayer.PenthCastTime = castTime;
                modPlayer.PenthCastTimeMax = castTimeMax;
                modPlayer.PenthNextAttack = nextAttack;
                NPC.netUpdate = true;
            }
            else
            {
                modPlayer.PenthActive = false;
                modPlayer.PenthBarActive = false;
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
                    SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_TryAgainIDareYou, NPC.Center);
                    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You fall to the Witch of Ink..."), 210, 60, 60);}
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
                modPlayer.PenthActive = false;
                modPlayer.PenthBarActive = false;
                NPC.active = false;
                NPC.timeLeft = 0;
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

                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                }
            }
            /*if ()
            {
                float Speed = 20f;  //projectile speed
                                    //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(NPC.position.X, NPC.position.Y);
                int damage = 0;  //projectile damage
                int type;

                type = Mod.Find<ModProjectile>("PenthSpin").Type;



                float rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer); }
            }*/
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
                        d.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
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
                        d.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
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

                modPlayer.PenthBarActive = true;
                //Here are the attacks and their effects ///////////////////////////////////////
                if (castTime >= castTimeMax)
                {
                    NPC.AddBuff(BuffType<CastFinished>(), 10);
                    for (int i = 0; i < 2; i++)
                    {
                        float Speed = Main.rand.NextFloat(2, 15);  //projectile speed
                                                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 500);
                        int damage = 0;  //projectile damage
                        int type = Mod.Find<ModProjectile>("Geometry").Type;

                        float rotation = (float)Math.Atan2(-70, 0);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        float Speed = Main.rand.NextFloat(2, 15);  //projectile speed
                                                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 500);
                        int damage = 0;  //projectile damage
                        int type = Mod.Find<ModProjectile>("InkClot").Type;

                        float rotation = (float)Math.Atan2(-70, 0);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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
                                    player.AddBuff(BuffType<Buffs.RedPaint>(), 3600);
                                    Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                    CombatText.NewText(textPos, new Color(255, 0, 0, 240), $"Covered in red paint!", false, false);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 219, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (colorApplied == 1)
                                {
                                    player.AddBuff(BuffType<Buffs.BluePaint>(), 3600);
                                    Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                    CombatText.NewText(textPos, new Color(0, 0, 255, 240), $"Covered in blue paint!", false, false);
                                    for (int d = 0; d < 25; d++)
                                    {
                                        Dust.NewDust(player.Center, 0, 0, 221, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                                    }
                                }
                                if (colorApplied == 2)
                                {
                                    player.AddBuff(BuffType<Buffs.YellowPaint>(), 3600);
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
                    if (nextAttack == "The Garden of Avalon")
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

                        Vector2 placement = new Vector2((NPC.Center.X), NPC.Center.Y - 800);
                        int type;
                        type = Mod.Find<ModProjectile>("BossGarden").Type;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),placement.X, placement.Y, 0, 0,type,0,0f,Main.myPlayer);}
                        //SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_GardenOfAvalonActivated, NPC.Center);
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inPenthFightTimer > 0)
                                player.AddBuff(BuffID.PotionSickness, 3600);  //Make sure to replace "buffType" and "timeInFrames" with actual values
                            player.AddBuff(BuffID.MoonLeech, 3600);  //Make sure to replace "buffType" and "timeInFrames" with actual values

                        }

                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The Garden of Avalon stifles your healing!"), 210, 255, 60);}


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
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<NPCs.PaintedAttendantA>(), NPC.whoAmI);
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<NPCs.PaintedAttendantB>(), NPC.whoAmI);

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
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Penth is protected by a shield of frost!"), 210, 60, 60);}
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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
                    if (nextAttack == "Magnum Opus")
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
                            {
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;
                                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was splashed away!"), 500, 0);

                            }


                        }

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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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


                            if (player.active && modPlayer.inPenthFightTimer > 0)
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


                            //if (player.active && modPlayer.inPenthFightTimer > 0)


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
                            {

                            }
                            //player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PhaseChange"));
                        QuintuplecastSkies = 40;




                    }
                    if (nextAttack == "Plutonic Barrage")
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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        blazingSkies = 100;



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
                    
                    
                    if (nextAttack == "Galactic Swarm")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        //for (int d = 0; d < 12; d++)
                        //{
                        //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y + Main.rand.Next(-365, 365), NPCType<NPCs.AstralCell>(), NPC.whoAmI);
                        //}



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
                            if (player.active && modPlayer.inPenthFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inPenthFightTimer > 0)
                                player.AddBuff(BuffType<Buffs.DownForTheCount>(), 680);  //


                        }
                        phase = 2;

                    }
                    
                    
                    

                }
            }
            else
            {
                modPlayer.PenthBarActive = false;
            }
            if (undertaleActive)
            {

                modPlayer.undertaleActive = true;
                NPC.ai[1] = 400;
                blazingSkiesTimer = 0;
            }
            else
            {
                modPlayer.undertaleActive = false;
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



                float Speed = 7f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type;

                type = Mod.Find<ModProjectile>("InkClot").Type;


                vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 800);



                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}

                blazingSkiesTimer = 0;
                blazingSkies--;


            }
            if (strayManaTimer >= 2)
            {
                int k = Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.position.X + Main.rand.Next(-480, 480), (int)NPC.position.Y + Main.rand.Next(-280, 280), NPC.width, NPC.height, Mod.Find<ModItem>("StrayMana").Type, 1, false);
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
                float Speed = 0f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                int damage = 60;  //projectile damage
                int type = Mod.Find<ModProjectile>("BrushStroke").Type;

                float rotation = 0f;
                //Main.PlaySound(SoundID.Roar, vector8, 0);
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
                int damage = 60;  //projectile damage
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
                    // if (!
                    // )
                    //{
                    
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
                    NPC.dontTakeDamage = true;
                    introAnimation = 120;
                    
                    Vector2 initialMoveTo = P.Center + new Vector2(-120f, -100);
                    NPC.position = initialMoveTo;
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player player = Main.player[i];
                        if (player.active && player.Distance(NPC.Center) < 300)
                        {
                            player.velocity = Vector2.Normalize(NPC.Center - player.Center) * -10f;
                        }
                            
                            
                             

                    }
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
                    SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_ISenseTheirResolve, NPC.Center);
                    NPC.ai[1] = -200;



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
            if (NPC.ai[1] >= 500)//DEBUG IT SHOULD BE 500
            {

                NPC.netUpdate = true;

                if (!isCasting)
                {


                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////


                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{npc.ai[2]}"), 210, 60, 60);}
                    //Boss Rotation
                    if (NPC.ai[2] == 0)
                    {
                        nextCast = "Ink Over";//
                    }
                    if (NPC.ai[2] == 1)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 2)
                    {
                        nextCast = "Linear Mystics";//Linear Mystics
                    }
                    if (NPC.ai[2] == 3)
                    {
                        nextCast = "Spilled Violet";//Spilled Violet
                    }
                    if (NPC.ai[2] == 4)
                    {
                        nextCast = "Painted Attendants";
                    }
                    if (NPC.ai[2] == 5)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 6)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 7)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 8)
                    {
                        nextCast = "Spilled Violet";
                    }
                    if (NPC.ai[2] == 9)
                    {
                        nextCast = "Splattered Sundering";//
                    }
                    if (NPC.ai[2] == 10)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 11)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 12)
                    {
                        nextCast = "Perfect Strokes";//
                    }
                    if (NPC.ai[2] == 13)
                    {
                        nextCast = "Painted Attendants";
                    }
                    if (NPC.ai[2] == 14)
                    {
                        nextCast = "Blotted Whims";
                    }
                    if (NPC.ai[2] == 15)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 16)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 17)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 18)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 19)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 20)
                    {
                        nextCast = "Spilled Violet";
                    }
                    if (NPC.ai[2] == 21)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 22)
                    {
                        nextCast = "Blotted Whims";
                    }
                    if (NPC.ai[2] == 23)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 24)
                    {
                        nextCast = "Blotted Whims";
                    }
                    if (NPC.ai[2] == 25)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 26)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 27)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 28)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 29)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 30)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 31)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 32)
                    {
                        nextCast = "Ink Over";

                    }//Phase 2 (kind of)
                    if (NPC.ai[2] == 33)
                    {
                        nextCast = "Splattered Sundering";//
                    }
                    if (NPC.ai[2] == 34)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 35)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 36)
                    {
                        nextCast = "Spilled Violet";
                    }
                    if (NPC.ai[2] == 37)
                    {
                        nextCast = "Painted Attendants";
                    }
                    if (NPC.ai[2] == 38)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 39)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 40)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 41)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 42)
                    {
                        nextCast = "Ink Over";//
                    }
                    if (NPC.ai[2] == 43)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 44)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 45)
                    {
                        nextCast = "Blotted Whims";//
                    }
                    if (NPC.ai[2] == 46)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 47)
                    {
                        nextCast = "Spilled Violet";
                    }
                    if (NPC.ai[2] == 48)
                    {
                        nextCast = "Perfect Strokes";
                    }
                    if (NPC.ai[2] == 49)
                    {
                        nextCast = "Ink Over";
                    }
                    if (NPC.ai[2] == 50)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 51)
                    {
                        nextCast = "Blotted Whims";
                    }
                    if (NPC.ai[2] == 52)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 53)
                    {
                        nextCast = "Splattered Sundering";
                    }
                    if (NPC.ai[2] == 54)
                    {
                        nextCast = "Spilled Violet";
                    }
                    if (NPC.ai[2] == 55)
                    {
                        nextCast = "Linear Mystics";
                    }
                    if (NPC.ai[2] == 56)
                    {
                        nextCast = "A Dash of Chaos";
                    }
                    if (NPC.ai[2] == 57)
                    {
                        nextCast = "Blotted Whims";
                    }
                    if (NPC.ai[2] == 58)
                    {
                        nextCast = "Painted Attendants";
                    }
                    if (NPC.ai[2] == 59)
                    {
                        nextCast = "Magnum Opus";
                        NPC.ai[2] = 0;

                    }
                    //End of Rotation
                    if (nextCast == "Linear Mystics")
                    {

                        castAnimation = 70;
                        
                        if (Main.rand.Next(0, 2) == 0)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_OutOfMyWay, NPC.Center);
                        }
                        else
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_QuickQuick, NPC.Center);
                        }
                        castDelay = 0;
                        nextAttack = "Linear Mystics";
                        castTime = 0;
                        castTimeMax = 30;
                        isCasting = true;
                    }
                    if (nextCast == "Painted Attendants")
                    {

                        castAnimation = 70;
                        SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_WithHaste, NPC.Center);
                        castDelay = 0;
                        nextAttack = "Painted Attendants";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "Magnum Opus")
                    {
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Overwhelming power surrounds you..."), 210, 60, 60);}
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel the next attack is truly unsurvivable!"), 210, 60, 60);}
                        castAnimation = 70;
                        SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_PenthLaugh, NPC.Center);
                        castDelay = 0;
                        nextAttack = "Magnum Opus";
                        castTime = 0;
                        castTimeMax = 1200;
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
                    if (nextCast == "Blazing Skies II")
                    {
                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WereYouExpectingRust"));
                        castDelay = 0;
                        nextAttack = "Blazing Skies II";
                        castTime = 0;
                        castTimeMax = 30;
                        isCasting = true;
                    }
                    if (nextCast == "Blotted Whims")
                    {
                        castAnimation = 70;
                        if (Main.rand.Next(0, 2) == 0)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_Enough, NPC.Center);
                        }
                        else
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_ToPieces, NPC.Center);
                        }
                        
                        castDelay = 0;
                        nextAttack = "Blotted Whims";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "Perfect Strokes")
                    {
                        castAnimation = 70;
                        SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_HowFoolish, NPC.Center);
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
                    if (nextCast == "Ink Over")
                    {
                        castAnimation = 70;
                       
                        castDelay = 0;
                        nextAttack = "Ink Over";
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
                        if(Main.rand.Next(0,2) == 0)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_CutThrough, NPC.Center);
                        }
                        else
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_ToShreds, NPC.Center);
                        }
                        
                        castDelay = 0;
                        nextAttack = "Splattered Sundering";
                        castTime = 0;
                        castTimeMax = 80;
                        isCasting = true;
                    }
                    if (nextCast == "A Dash of Chaos")
                    {
                        castAnimation = 70;
                        if (Main.rand.Next(0, 2) == 0)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_ThisllCheerMeUp, NPC.Center);
                        }
                        else
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_HowFun, NPC.Center);
                        }
                        
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
                        SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_WhateverItTakes, NPC.Center);
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
                        SoundEngine.PlaySound(StarsAboveAudio.Penthesilea_AlrightMyTurn, NPC.Center);
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                        castDelay = 0;
                        nextAttack = "Spilled Violet";
                        castTime = 0;
                        castTimeMax = 80;
                        isCasting = true;
                    }
                    if (nextCast == "Vorpal Channeling")
                    {
                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                        castDelay = 0;
                        nextAttack = "Vorpal Channeling";
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
                    if (nextCast == "Solar Channeling")
                    {
                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                        castDelay = 0;
                        nextAttack = "Solar Channeling";
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

                        type = Mod.Find<ModProjectile>("PenthSwing").Type;
                        // Main.PlaySound(SoundLoader.customSoundType, vector8, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EscapeIsNotSoEasilyGranted"));


                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
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

                        type = Mod.Find<ModProjectile>("PenthSwing2").Type;
                        // Main.PlaySound(SoundLoader.customSoundType, vector8, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EscapeIsNotSoEasilyGranted"));


                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1),type,damage,0f,Main.myPlayer);}
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
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/AThousandBolts"));
                        castDelay = 0;
                        nextAttack = "Crack The Sky";
                        castTime = 0;
                        castTimeMax = 300;
                        isCasting = true;
                    }
                    if (nextCast == "Aegis of Frost")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MyDefenses"));
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
                        if (!ColorblindEnabled)
                        {
                            for (int d = 0; d < 5040; d += 420)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                if (nextType == 1)
                                {
                                    type = Mod.Find<ModProjectile>("RedSplatter").Type;
                                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 2500, placement.Y, 0, 0, type, 0, 0f, Main.myPlayer); }
                                }
                                if (nextType == 2)
                                {
                                    type = Mod.Find<ModProjectile>("BlueSplatter").Type;
                                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 2500, placement.Y, 0, 0, type, 0, 0f, Main.myPlayer); }
                                }
                                if (nextType == 3)
                                {
                                    type = Mod.Find<ModProjectile>("YellowSplatter").Type;
                                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 2500, placement.Y, 0, 0, type, 0, 0f, Main.myPlayer); }
                                    nextType = 0;
                                }

                                nextType++;
                            }
                        }
                        else
                        {
                            for (int d = 0; d < 3500; d += 500)
                            {

                                Vector2 placement = new Vector2((NPC.Center.X) + d, NPC.position.Y);
                                int type;
                                type = Mod.Find<ModProjectile>("RendHeaven").Type;
                                if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 1500, placement.Y, 0, 0, type, 0, 0f, Main.myPlayer); }
                            }
                        }
                        
                    }
                    if (!isCasting)
                    {
                        isSwinging = true;
                        swingAnimation = 120;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fools"));
                        castDelay = 0;
                        nextAttack = "Heavensfall";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                        NPC.ai[2] = 0;

                    }
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0, 0, Mod.Find<ModProjectile>("PenthCastingPage").Type, 0, 0f, Main.myPlayer); }
                    NPC.netUpdate = true;





                    // Special attacks /////////////////////////////////////////////////////////////////////////////////////////////



                    NPC.ai[2]++;



                }


                NPC.ai[1] = 0;


            }
            NPC.netUpdate = true;

        }


    }
}