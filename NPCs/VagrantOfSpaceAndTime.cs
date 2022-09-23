using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;

using StarsAbove.Dusts;

using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;
using StarsAbove.Items.BossBags;

namespace StarsAbove.NPCs
{
    [AutoloadBossHead]
    public class VagrantOfSpaceAndTime : ModNPC
    {//Code from Example Mod.
        public static readonly int arenaWidth = (int)(1.2f * 960);
        public static readonly int arenaHeight = (int)(1.2f * 600);
        
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.aiStyle = 0;
            NPC.lifeMax = 50000;

            NPC.damage = 0;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.width = 220;
            NPC.height = 270;
            NPC.scale = 1f;
            Main.npcFrameCount[NPC.type] = 8;
            NPC.value = Item.buyPrice(0, 1, 75, 45);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.buffImmune[24] = true;

            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/CosmicWill");

            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };

            //Music =  mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CosmicWill");
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
        private int portalFrame
        {
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Vagrant of Space and Time");
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                CustomTexturePath = "StarsAbove/Bestiary/PerseusPortrait", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
                Position = new Vector2(142f, 74f),
                PortraitScale = 0.8f,
                PortraitPositionXOverride = -136f,
                PortraitPositionYOverride = 58f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);

        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            int associatedNPCType = ModContent.NPCType<AstralCell>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                new FlavorTextBestiaryInfoElement("The enigmatic Starfarer Perseus. His outward appearance is that of utmost confidence... but he's just kind of a dork trying to look cool. His sisters agree on this.")
                });

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
            
            //Because you can not hit this boss, loot will be dropped seperately.
            if(Main.expertMode)
            {
                int k = Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, 0,0, Mod.Find<ModItem>("VagrantBossBag").Type, 1, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, k, 1f);
                }
            }

            NPC.SetEventFlagCleared(ref DownedBossSystem.downedVagrant, -1);
            DownedBossSystem.downedVagrant = true;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }

        }
        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = 0;
            NPC.spriteDirection = 0;



            NPC.frameCounter++;

            if (NPC.frameCounter >= 4)
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

            // if (isSwinging)

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<VagrantBossBag>()));

            // Trophies are spawned with 1/10 chance
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.VagrantTrophyItem>(), 10));

            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.VagrantBossRelicItem>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            // Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
            // Boss masks are spawned with 1/7 chance
            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.SpatialPrism>(), 4));

            // This part is not required for a boss and is just showcasing some advanced stuff you can do with drop rules to control how items spawn
            // We make 12-15 ExampleItems spawn randomly in all directions, like the lunar pillar fragments. Hereby we need the DropOneByOne rule,
            // which requires these parameters to be defined
            int itemType = ModContent.ItemType<Items.Materials.EnigmaticDust>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 12,
                MaximumItemDropsCount = 15,
            };

            notExpertRule.OnSuccess(new DropOneByOne(itemType, parameters));
            
            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
        }

        bool phaseTransition;

        public bool isInvincible;

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }

       

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int portalWidth = 48;
            int portalDepth = 18;
            Color color = new Color(58, 139, 243);
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
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/silence");
                NPC.dontTakeDamage = true;
                NPC.ai[3] += 1f; // increase our death timer.
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
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/VagrantDeathQuote"));
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/VagrantDefeated"));
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.

                    DownedBossSystem.downedVagrant = true;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                    }

                    modPlayer.VagrantActive = false;
                    modPlayer.VagrantBarActive = false;
                    modPlayer.undertaleActive = false;
                }
                return;
            }

            if (NPC.ai[3] == 0f)
            {
                NPC.dontTakeDamage = true;
            }


            if (!Main.dedServ)
            {
                portalFrame++;
                portalFrame %= 6 * Main.projFrames[ProjectileID.PortalGunGate];
            }

            //PreCast effects

            if (NPC.active)
            {
                modPlayer.VagrantActive = true;
                modPlayer.VagrantCastTime = castTime;
                modPlayer.VagrantCastTimeMax = castTimeMax;
                modPlayer.VagrantNextAttack = nextAttack;
                NPC.netUpdate = true;
            }
            else
            {
                modPlayer.VagrantActive = false;
                modPlayer.VagrantBarActive = false;
            }
            if (solar)
            {
                //Main.monolithType = 3;
            }
            if (vortex)
            {
                //Main.monolithType = 0;
            }
            if (stardust)
            {
                //Main.monolithType = 2;
            }
            if (nebula)
            {
                //Main.monolithType = 1;
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
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PityDisplay"));
                    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You fall to the Vagrant of Space and Time..."), 210, 60, 60);}
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
                modPlayer.VagrantActive = false;
                modPlayer.VagrantBarActive = false;
                NPC.velocity.Y -= -2f;
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
                    
                    if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}

                    
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
                desperadoShots = 4;
                eyeProjectile = 3;
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

                modPlayer.VagrantBarActive = true;
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
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("SaberDamage").Type, 80, 0f, Main.myPlayer);}
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
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("SaberDamage").Type, 140, 0f, Main.myPlayer);}
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

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
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

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}


                    }
                    if (nextAttack == "Synaptic Static")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Vector2 vector8 = SolemnConfiteorSaved;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("SolemnConfiteorDamage").Type, 400, 0f, Main.myPlayer);}
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

                       if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);}*/


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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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
                    if (nextAttack == "Gravitational Anomaly")
                    {
                        isCasting = false;
                        lastAttack = nextAttack;
                        nextAttack = "";
                        nframe = 0;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        NPC.ai[1] = 400;
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
                    if (nextAttack == "The Sword of Flames")
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

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y - 200), Vector2.Zero, Mod.Find<ModProjectile>("BossLaevateinn").Type, 0, 4, Main.myPlayer, 0, 1);}//The 1 here means that ai1 will be set to 1. this is good for the first cast.

                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Ars Laevateinn prepares to strike!"), 210, 60, 60);}


                    }
                    if (nextAttack == "The Test Concludes")
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
                        DownedBossSystem.downedVagrant = true;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                        }
                        NPC.ai[3] = 1f;


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
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Vagrant is protected by a shield of frost!"), 210, 60, 60);}
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
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel2 = new Vector2(1, 1);
                            vel2 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel2.X, vel2.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel3 = new Vector2(1, -1);
                            vel3 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel3.X, vel3.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel4 = new Vector2(-1, 1);
                            vel4 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel4.X, vel4.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel5 = new Vector2(0, -1);
                            vel5 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel5.X, vel5.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel6 = new Vector2(0, 1);
                            vel6 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel6.X, vel6.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel7 = new Vector2(1, 0);
                            vel7 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel7.X, vel7.Y, 348, 60, 0, Main.myPlayer);}
                            Vector2 vel8 = new Vector2(-1, 0);
                            vel8 *= 8f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel8.X, vel8.Y, 348, 60, 0, Main.myPlayer);}
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
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel.X, vel.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel2 = new Vector2(1, 1);
                            vel2 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel2.X, vel2.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel3 = new Vector2(1, -1);
                            vel3 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel3.X, vel3.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel4 = new Vector2(-1, 1);
                            vel4 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel4.X, vel4.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel5 = new Vector2(0, -1);
                            vel5 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel5.X, vel5.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel6 = new Vector2(0, 1);
                            vel6 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel6.X, vel6.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel7 = new Vector2(1, 0);
                            vel7 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel7.X, vel7.Y, 465, 60, 0, Main.myPlayer);}
                            Vector2 vel8 = new Vector2(-1, 0);
                            vel8 *= 3f;
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vel8.X, vel8.Y, 465, 60, 0, Main.myPlayer);}

                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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


                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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


                            //if (player.active && modPlayer.inVagrantFightTimer > 0)


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-1, 1));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2, ProjectileID.CultistBossIceMist, 40, 0, Main.myPlayer, NPC.whoAmI, 1);}
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-18, 18), Main.rand.NextFloat(-18, 18));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2, ProjectileID.FrostShard, 40, 0, Main.myPlayer, NPC.whoAmI, 1);}
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2, ProjectileID.FrostWave, 40, 0, Main.myPlayer, NPC.whoAmI, 1);}
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            //
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-8, 8));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2, ProjectileID.CultistBossFireBall, 40, 0, Main.myPlayer, NPC.whoAmI, 1);}
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }


                        for (int i = 0; i < 20; i++)
                        {
                            // Random upward vector.
                            Vector2 vector2 = new Vector2(Main.rand.NextFloat(-16, 16), Main.rand.NextFloat(-9, -20));
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2, ProjectileID.DD2BetsyFireball, 40, 0, Main.myPlayer, NPC.whoAmI, 1);}
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
                        }

                    }
                    if (nextAttack == "Unlimited Blade Works")
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_PhaseChange, NPC.Center);
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }
                        blazingSkies = 100;



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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;

                        }

                        float Speed = 24f;  //projectile speed
                                            //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                        int damage = 60;  //projectile damage
                        int type = Mod.Find<ModProjectile>("BossTheofania").Type;

                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, NPC.Center);

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}


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
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}

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
                    if (nextAttack == "Nebula Channeling")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, Main.myPlayer);}
                        nebula = true;
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
                    if (nextAttack == "Vorpal Channeling")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, Main.myPlayer);}
                        vortex = true;
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
                    if (nextAttack == "Solar Channeling")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, Main.myPlayer);}
                        solar = true;
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
                    if (nextAttack == "Starlit Channeling")
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, NPC.Center);
                        Vector2 placement2 = new Vector2((NPC.Center.X), NPC.Center.Y);
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, Main.myPlayer);}
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
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-365, 365), (int)NPC.Center.Y + Main.rand.Next(-365, 365), NPCType<NPCs.AstralCell>(), NPC.whoAmI);
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
                                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = 0;


                        }
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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

                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 1, 1, Main.myPlayer);}
                        }
                        modPlayer.LostToVagrant = true;
                        for (int d = 0; d < 305; d++)
                        {
                            Dust.NewDust(NPC.position, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                        }

                    }

                }
            }
            else
            {
                modPlayer.VagrantBarActive = false;
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
            if (blazingSkiesTimer >= 2)
            {



                float Speed = 8f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type;

                type = ProjectileID.FrostBeam;


                vector8 = new Vector2(P.position.X + Main.rand.Next(-900, 900), P.position.Y - 800);



                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}

                blazingSkiesTimer = 0;
                blazingSkies--;


            }
           
            if (eyeProjectile > 0)
            {
                NPC.ai[1] = 300;
                eyeProjectileTimer++;

            }
            if (eyeProjectileTimer >= 90)
            {
                float Speed = Main.rand.NextFloat(4, 13);  //projectile speed
                                                           //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-100, 100), P.position.Y - 400);
                int damage = 30;  //projectile damage
                int type = Mod.Find<ModProjectile>("SpaceGeometry").Type;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                //Main.PlaySound(SoundID.Roar, vector8, 0);
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
                eyeProjectileTimer = 0;
                eyeProjectile--;
            }

            if (QuintuplecastSkies > 0)
            {
                NPC.ai[1] = 300;
                QuintuplecastTimer++;

            }
            if (QuintuplecastTimer >= 6)
            {
                float Speed = Main.rand.NextFloat(8, 12);  //projectile speed
                                                           //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X + Main.rand.Next(-300, 300), P.position.Y - 800);
                int damage = 30;  //projectile damage
                int type = Mod.Find<ModProjectile>("BladeWorksProjectile").Type;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}


                float Speed2 = Main.rand.NextFloat(4, 7);  //projectile speed
                                                           //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector82 = new Vector2(P.position.X + Main.rand.Next(-1200, 1200), P.position.Y - 800);
                int damage2 = 30;  //projectile damage
                int type2 = Mod.Find<ModProjectile>("BladeWorksProjectile").Type;

                float rotation2 = (float)Math.Atan2(-70, 0);

                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector82.X, vector82.Y, (float)((Math.Cos(rotation2) * Speed2) * -1), (float)((Math.Sin(rotation2) * Speed2) * -1), type2, damage2, 0f, Main.myPlayer);}

                QuintuplecastTimer = 0;
                QuintuplecastSkies--;
            }
            if (desperadoShots > 0)
            {
                //npc.ai[1] = 300;
                desperadoTimer++;

            }
            if (desperadoTimer >= 600)
            {
                float Speed = 0f;  //projectile speed
                                   //Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                int damage = 0;  //projectile damage
                int type = Mod.Find<ModProjectile>("RendHeaven").Type;

                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
                desperadoTimer = 0;
                desperadoShots--;
            }

            for (int i = 0; i < 30; i++)
            {//Circle
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * 40);
                offset.Y += (float)(Math.Cos(angle) * 40);

                Dust d = Dust.NewDustPerfect(NPC.Center + offset + new Vector2(0, 20), 45, NPC.velocity, 200, default(Color), 0.7f);
                d.fadeIn = 1f;
                d.noGravity = true;
            }

            NPC.ai[1] += 4;
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
                            //Vector2 moveTo = P.Center + new Vector2(Main.rand.Next(-400, 400), Main.rand.Next(-600, 0)); //This is 200 pixels above the center of the player.
                            //npc.position = moveTo;
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
                                    int goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                                    goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                                    goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                                    Main.gore[goreIndex].scale = 1.5f;
                                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                                    goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
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
                    modPlayer.vagrantTimeLeft = 7320;
                    Vector2 initialMoveTo = P.Center;
                    NPC.Center = initialMoveTo;

                    if (!Main.dedServ)
                    {

                        for (int d = 0; d < 305; d++)
                        {
                            Dust.NewDust(NPC.Center, 0, 0, 20, 0f + Main.rand.Next(-35, 35), 0f + Main.rand.Next(-35, 35), 150, default(Color), 1.5f);
                        }

                    }
                    //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/VagrantIntroQuote"));
                    NPC.ai[1] = 200;



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
            if (NPC.ai[1] >= 350)
            {

                NPC.netUpdate = true;

                if (!isCasting)
                {


                    // Phase 1 /////////////////////////////////////////////////////////////////////////////////////////////


                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{npc.ai[2]}"), 210, 60, 60);}
                    //Boss Rotation
                    if (NPC.ai[2] == 0)
                    {
                        nextCast = "Theofania Inanis";//
                    }
                    if (NPC.ai[2] == 1)
                    {
                        nextCast = "Gravitational Anomaly";
                    }
                    if (NPC.ai[2] == 2)
                    {
                        nextCast = "Spatial Rip";
                    }
                    if (NPC.ai[2] == 3)
                    {
                        nextCast = "Nebula Channeling";//Change the BG to a Celestial Pillar's BG (Nebula)
                    }
                    if (NPC.ai[2] == 4)
                    {
                        nextCast = "Quasar Forthcoming";//Spam of magic stuff from the sky
                    }
                    if (NPC.ai[2] == 5)
                    {
                        nextCast = "Relinquish";//Return the BG to default
                    }
                    if (NPC.ai[2] == 6)
                    {
                        nextCast = "Theofania Inanis";//just shoot the sword like a bullet from the sky
                    }
                    if (NPC.ai[2] == 7)
                    {
                        nextCast = "Spatial Rip";
                    }
                    if (NPC.ai[2] == 8)
                    {
                        nextCast = "Vorpal Channeling";
                    }
                    if (NPC.ai[2] == 9)
                    {
                        nextCast = "Plutonic Barrage";//
                    }
                    if (NPC.ai[2] == 10)
                    {
                        nextCast = "Relinquish";
                    }
                    if (NPC.ai[2] == 11)
                    {
                        nextCast = "Meteor Shower";
                    }
                    if (NPC.ai[2] == 12)
                    {
                        nextCast = "Gravitational Anomaly";//Inflict Distorted debuff
                    }
                    if (NPC.ai[2] == 13)
                    {
                        nextCast = "Theofania Inanis";
                    }
                    if (NPC.ai[2] == 14)
                    {
                        nextCast = "Starlit Channeling";
                    }
                    if (NPC.ai[2] == 15)
                    {
                        nextCast = "Galactic Swarm";
                    }
                    if (NPC.ai[2] == 16)
                    {
                        nextCast = "Relinquish";
                    }
                    if (NPC.ai[2] == 17)
                    {
                        nextCast = "Meteor Shower";
                    }
                    if (NPC.ai[2] == 18)
                    {
                        nextCast = "Solar Channeling";
                    }
                    if (NPC.ai[2] == 19)
                    {
                        nextCast = "Unlimited Blade Works";
                    }
                    if (NPC.ai[2] == 20)
                    {
                        nextCast = "Relinquish";
                    }
                    if (NPC.ai[2] == 21)
                    {
                        nextCast = "The Test Concludes";
                    }

                    if (NPC.ai[2] == 22)
                    {
                        nextCast = "The Test Concludes";
                        NPC.ai[2] = 0;
                    }
                    NPC.netUpdate = true;
                    //End of Rotation
                    if (nextCast == "Heavensfall")
                    {

                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fools"));
                        castDelay = 0;
                        nextAttack = "Heavensfall";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "The Test Concludes")
                    {

                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fools"));
                        castDelay = 0;
                        nextAttack = "The Test Concludes";
                        castTime = 0;
                        castTimeMax = 50;
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
                    if (nextCast == "Plutonic Barrage")
                    {
                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WereYouExpectingRust"));
                        castDelay = 0;
                        nextAttack = "Plutonic Barrage";
                        castTime = 0;
                        castTimeMax = 30;
                        isCasting = true;
                    }
                    if (nextCast == "Coruscant Saber II")
                    {
                        castAnimation = 70;
                        Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ComeShowMeMore"));
                        castDelay = 0;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("SaberAOE2").Type, 0, 0f, Main.myPlayer);}
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
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("ShadowbladeAOE").Type, 0, 0f, Main.myPlayer);}
                        SolemnConfiteorSaved = vector8;
                        nextAttack = "Synaptic Static";
                        castTime = 0;
                        castTimeMax = 80;
                        isCasting = true;
                    }

                    if (nextCast == "Brain Drain")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                        castDelay = 0;
                        nextAttack = "Brain Drain";
                        castTime = 0;
                        castTimeMax = 150;
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
                    if (nextCast == "Unlimited Blade Works")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                        castDelay = 0;
                        nextAttack = "Unlimited Blade Works";
                        castTime = 0;
                        castTimeMax = 80;
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
                        castTimeMax = 30;
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
                    if (nextCast == "Sanctified Slaughter")
                    {
                        castAnimation = 70;
                        //Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You feel an evil presence watching you..."), 210, 60, 60);}
                        castDelay = 0;
                        nextAttack = "Sanctified Slaughter";
                        castTime = 0;
                        castTimeMax = 150;
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

                        type = Mod.Find<ModProjectile>("VagrantSwing").Type;
                        // Main.PlaySound(SoundLoader.customSoundType, vector8, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EscapeIsNotSoEasilyGranted"));


                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
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

                        type = Mod.Find<ModProjectile>("VagrantSwing2").Type;
                        // Main.PlaySound(SoundLoader.customSoundType, vector8, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EscapeIsNotSoEasilyGranted"));


                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
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
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 1500, placement.Y, 0, 0, type, 0, 0f, Main.myPlayer);}
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
                    


                    if (nextCast == "Blazing Skies II")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RefulgentEther"));
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
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fall"));
                        castDelay = 0;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("SaberAOE2").Type, 0, 0f, Main.myPlayer);}
                        CoruscantSaberSaved = vector8;
                        nextAttack = "Coruscant Saber II";
                        castTime = 0;
                        castTimeMax = 30;
                        isCasting = true;
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (player.active && modPlayer.inVagrantFightTimer > 0)
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

                        type = Mod.Find<ModProjectile>("VagrantSwing2").Type;
                        //  Main.PlaySound(SoundLoader.customSoundType, vector8, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EscapeIsNotSoEasilyGranted"));


                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
                    }
                    if (nextCast == "Absolute Fire III")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGameIsUp"));
                        castDelay = 0;
                        nextAttack = "Absolute Fire III";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "Absolute Blizzard III")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGameIsUp"));
                        castDelay = 0;
                        nextAttack = "Absolute Blizzard III";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "Absolute Thunder IV")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ComeShowMeYourStrength"));
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
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ItsTimeWeSettledThis"));
                        castDelay = 0;
                        nextAttack = "Quintuplecast";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "Absolute Holy")
                    {
                        castAnimation = 70;
                        castDelay = 0;
                        nextAttack = "Absolute Holy";
                        castTime = 0;
                        castTimeMax = 30;
                        isCasting = true;
                    }
                    if (nextCast == "Absolute Summoning")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/AnswerMyCall"));
                        castDelay = 0;
                        nextAttack = "Absolute Summoning";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "To The Limit")
                    {
                        if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The Vagrant of Light is transcending his limits!"), 210, 60, 60);}

                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheLightWillCleanseYourSins"));
                        castDelay = 0;
                        nextAttack = "To The Limit";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "Radiant Braver")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/YoureNotGoingAnywhere"));
                        castDelay = 0;
                        nextAttack = "Radiant Braver";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "Radiant Desperado")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/YoureNoMatchForMe"));
                        castDelay = 0;
                        nextAttack = "Radiant Desperado";
                        castTime = 0;
                        castTimeMax = 150;
                        isCasting = true;
                    }
                    if (nextCast == "Radiant Meteor")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/YourLifeIsMineForTheTaking"));
                        castDelay = 0;
                        nextAttack = "Radiant Meteor";
                        castTime = 0;
                        castTimeMax = 200;
                        isCasting = true;
                    }
                    if (nextCast == "Terror Unleashed")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/AFeebleShieldProtectsNothing"));
                        castDelay = 0;
                        nextAttack = "Terror Unleashed";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "SOUL Extraction")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LetsTrySomethingElse"));
                        castDelay = 0;
                        nextAttack = "SOUL Extraction";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "Light Rampant")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IveToyedWithYouLongEnough"));
                        castDelay = 0;
                        nextAttack = "Light Rampant";
                        castTime = 0;
                        castTimeMax = 100;
                        isCasting = true;
                    }
                    if (nextCast == "Shadowblast")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WereYouExpectingRust"));
                        castDelay = 0;
                        nextAttack = "Shadowblast";
                        castTime = 0;
                        castTimeMax = 40;
                        isCasting = true;
                    }
                    if (nextCast == "The Sword of Flames")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/UponMyHolyBlade"));

                        castDelay = 0;
                        nextAttack = "The Sword of Flames";
                        castTime = 0;
                        castTimeMax = 120;
                        isCasting = true;
                    }
                    if (nextCast == "Heavensfall")
                    {

                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Fools"));
                        castDelay = 0;
                        nextAttack = "Heavensfall";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "Blazing Skies II")
                    {
                        castAnimation = 70;
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WereYouExpectingRust"));
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
                        //  Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ComeShowMeMore"));
                        castDelay = 0;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("SaberAOE2").Type, 0, 0f, Main.myPlayer);}
                        CoruscantSaberSaved = vector8;
                        nextAttack = "Coruscant Saber II";
                        castTime = 0;
                        castTimeMax = 50;
                        isCasting = true;
                    }
                    if (nextCast == "Synaptic Static")
                    {
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheHeartsOfMen"));
                        castAnimation = 70;
                        Vector2 vector8 = new Vector2(P.position.X, P.position.Y);
                        castDelay = 0;
                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, 0, 0, Mod.Find<ModProjectile>("ShadowbladeAOE").Type, 0, 0f, Main.myPlayer);}
                        SolemnConfiteorSaved = vector8;
                        nextAttack = "Synaptic Static";
                        castTime = 0;
                        castTimeMax = 80;
                        isCasting = true;
                    }

                    if (nextCast == "Brain Drain")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EvenTheStrongestShields"));
                        castDelay = 0;
                        nextAttack = "Brain Drain";
                        castTime = 0;
                        castTimeMax = 150;
                        isCasting = true;
                    }
                    if (nextCast == "Sanctified Slaughter")
                    {
                        castAnimation = 70;
                        // Main.PlaySound(SoundLoader.customSoundType, (int)npc.Center.X, (int)npc.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TheGodsWillNotBeWatching"));
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

                        type = Mod.Find<ModProjectile>("VagrantSwing").Type;
                        // Main.PlaySound(SoundLoader.customSoundType, vector8, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EscapeIsNotSoEasilyGranted"));


                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));

                        if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);}
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
                            if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(NPC.GetSource_FromAI(), placement.X - 1500, placement.Y, 0, 0, type, 0, 0f, Main.myPlayer);}
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




                    // Special attacks /////////////////////////////////////////////////////////////////////////////////////////////



                    NPC.ai[2]++;



                }


                NPC.ai[1] = 0;


            }
            NPC.netUpdate = true;

        }



    }
}