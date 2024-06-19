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
using StarsAbove.NPCs.Vagrant;
using SubworldLibrary;
using StarsAbove.Biomes;
using StarsAbove.Subworlds;
using StarsAbove.Buffs.Subworlds;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.NPCs.OffworldNPCs;
using StarsAbove.NPCs.TownNPCs;
using StarsAbove.Utilities;
using StarsAbove.Items.Loot;
using StarsAbove.Subworlds.ThirdRegion;
using Microsoft.Xna.Framework.Graphics;
using Terraria.WorldBuilding;
using StarsAbove.NPCs.Arbitration;
using StarsAbove.Buffs.Boss;

namespace StarsAbove.Systems
{
    public class SubworldPlayer : ModPlayer
    {
        //This is for specific effects WITHIN subworlds
        public float gravityMod;

        public bool inYojimboRange; //So dialogue doesn't begin right after other dialogue ends.
        public bool inGarridineRange;

        public int anomalyTimer; //When this reaches a specific value, Arbitration will spawn.

        public int GarridineQuest;
        public bool AcceptedGarridineQuest;//Turns to true once you've read the new quest objective. It doesn't save.
        public int GarridineQuestLimit = 1;//The amount of Garridine Quests available.

        public int GarridineQuestCooldown;

        //When entering the range of a friendly NPC, use CombatText that says "Right click to initiate conversation!"
        public override void SaveData(TagCompound tag)
        {
            tag["GQuest"] = GarridineQuest;

        }

        public override void LoadData(TagCompound tag)
        {
            GarridineQuest = tag.GetInt("GQuest");

        }


        public override void PreUpdate()
        {
            if (Main.LocalPlayer.isNearNPC(NPCType<Yojimbo>(), 150) || Main.LocalPlayer.isNearNPC(NPCType<Garridine>(), 150))
            {

            }

            GarridineQuestCooldown--;
            DoYojimboDialogue();
            DoGarridineDialogue();

            if (SubworldSystem.Current == null)
            {
                //If not in a subworld

            }

        }
        private void DoYojimboDialogue()
        {
            if (Main.LocalPlayer.isNearNPC(NPCType<Yojimbo>(), 150))
            {
                //Player.GetModPlayer<StarsAbovePlayer>().yojimboIntroDialogue = 0;
                if (!inYojimboRange)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(43, 255, 43, 240), LangHelper.GetTextValue("NPCDialogue.Talk"), false, false);
                    inYojimboRange = true;

                }
                if (Main.mouseRight && !Player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive)
                {

                    if (Player.GetModPlayer<StarsAbovePlayer>().yojimboIntroDialogue == 2)
                    {
                        Player.AddBuff(BuffType<YojimboBuff>(), 7200);

                        int randomDialogue = Main.rand.Next(0, 3);
                        if (randomDialogue == 0)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 100;
                            ActivateVNDialogue(Player);
                            return;
                        }
                        if (randomDialogue == 1)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 101;
                            ActivateVNDialogue(Player);
                            return;
                        }
                        if (randomDialogue == 2)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 102;
                            ActivateVNDialogue(Player);
                            return;
                        }

                    }
                    else
                    {

                        Player.GetModPlayer<StarsAbovePlayer>().yojimboIntroDialogue = 2;
                        if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 19;
                            ActivateVNDialogue(Player);
                            return;
                        }
                        if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 20;
                            ActivateVNDialogue(Player);
                            return;

                        }




                    }
                }


            }
            else
            {
                inYojimboRange = false;

            }
        }

        private void DoGarridineDialogue()
        {
            //Maybe if no quest is active, Garridine gives you a random objective, and you have to get her the item?
            if (Main.LocalPlayer.isNearNPC(NPCType<Garridine>(), 150))
            {
                //Player.GetModPlayer<StarsAbovePlayer>().garridineIntroDialogue = 0;
                if (!inGarridineRange)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(43, 255, 43, 240), LangHelper.GetTextValue("NPCDialogue.Talk"), false, false);
                    inGarridineRange = true;

                }
                if (Main.mouseRight && !Player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive)
                {

                    if (Player.GetModPlayer<StarsAbovePlayer>().garridineIntroDialogue == 2)
                    {
                        //After the intro.

                        //If a quest has been completed in the last 5 minutes...
                        if (GarridineQuestCooldown > 0)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 199;
                            ActivateVNDialogue(Player);
                            return;
                        }


                        //If there is no current quest, assign one at random. These are player specific so no multiplayer nonsense. (hopefully)
                        GarridineQuestLimit = 12;
                        //If no quest...
                        //GarridineQuest = 1;
                        if (GarridineQuest == 0)
                        {
                            GarridineQuest = Main.rand.Next(1, GarridineQuestLimit + 1);//Pick randomly from a list of 10 quests.

                            //debugging
                            //GarridineQuest = 2;
                        }
                        else
                        {

                        }
                        //Depending on the quest, check for the specified item, and then reward the player.
                        if (GarridineQuest == 1)
                        {
                            ActivateQuest(new int[] { ItemID.WaterBolt });
                        }
                        else if (GarridineQuest == 2)
                        {
                            ActivateQuest(new int[] { ItemType<AgnianFarewell>(), ItemType<KevesiFarewell>() });
                        }
                        else if (GarridineQuest == 3)
                        {
                            ActivateQuest(new int[] { ItemID.GoldButterfly });
                        }
                        else if (GarridineQuest == 4)
                        {
                            ActivateQuest(new int[] { ItemID.FlyingCarpet });
                        }
                        else if (GarridineQuest == 5)
                        {
                            ActivateQuest(new int[] { ItemID.LavaCharm });
                        }
                        else if (GarridineQuest == 6)
                        {
                            ActivateQuest(new int[] { ItemID.LifeformAnalyzer });
                        }
                        else if (GarridineQuest == 7)
                        {
                            ActivateQuest(new int[] { ItemID.MechanicalLens });
                        }
                        else if (GarridineQuest == 8)
                        {
                            ActivateQuest(new int[] { ItemID.ManaFlower, ItemID.NaturesGift });
                        }
                        else if (GarridineQuest == 9)
                        {
                            ActivateQuest(new int[] { 887 });
                        }
                        else if (GarridineQuest == 10)
                        {
                            ActivateQuest(new int[] { ItemID.Umbrella, ItemID.TragicUmbrella, ItemID.UmbrellaHat });
                        }
                        else if (GarridineQuest == 11)
                        {
                            ActivateQuest(new int[] { ItemID.Actuator });
                        }
                        else if (GarridineQuest == 12)
                        {
                            ActivateQuest(new int[] { ItemID.BoneTorch });
                        }
                    }
                    else
                    {

                        Player.GetModPlayer<StarsAbovePlayer>().garridineIntroDialogue = 2;
                        if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 21;
                            ActivateVNDialogue(Player);
                            return;
                        }
                        if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 22;
                            ActivateVNDialogue(Player);
                            return;

                        }




                    }
                }


            }
            else
            {
                inGarridineRange = false;

            }
        }

        public void ActivateQuest(int[] NeededItem)
        {
            if (!AcceptedGarridineQuest)
            {
                //Explain your objective.
                Player.GetModPlayer<StarsAbovePlayer>().sceneID = 200 + GarridineQuest;
                ActivateVNDialogue(Player);

                AcceptedGarridineQuest = true;
                return;
            }
            //Looking for "a magical book with the capability to spurt out water"
            for (int b = 0; b < NeededItem.Length; b++)
            {
                for (int i = 0; i < Player.inventory.Length; i++)
                {

                    if (Player.inventory[i].type == NeededItem[b])
                    {
                        //The correct item!

                        //Play the good job dialouge.
                        Player.GetModPlayer<StarsAbovePlayer>().sceneID = 200;
                        ActivateVNDialogue(Player);

                        //Give the player the reward, a tier 1 Bag
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ItemType<StellarFociGrabBagTier1>());

                        //Reset values.
                        AcceptedGarridineQuest = false;
                        GarridineQuest = 0;

                        //Garridine goes on cooldown!
                        GarridineQuestCooldown = 18000;//5 minute cooldown.
                        return;
                    }
                    else
                    {

                    }
                }
            }


            //If the item wasn't found...

            // If the item wasn't found, play the quest dialogue again. (This won't appear if the item is found because of the return statement.
            Player.GetModPlayer<StarsAbovePlayer>().sceneID = 200 + GarridineQuest;
            ActivateVNDialogue(Player);
        }

        public override void PostUpdate()
        {



        }
        public override void OnRespawn()
        {
            if (SubworldSystem.Current != null)
            {
                Player.AddBuff(BuffType<Invincibility>(), 240);
            }
        }
        public override void PostUpdateBuffs()
        {
            if (SubworldSystem.Current == null)
            {
                //If not in a subworld
                anomalyTimer = 0;
            }
            else
            {
                if (Player.InModBiome(GetInstance<LyraBiome>()))
                {
                    //Make sure the player can't do what's not allowed:
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.AddBuff(BuffType<AnomalyBuff>(), 10);

                    Player.noBuilding = true;
                    Lighting.AddLight(Player.Center, TorchID.Pink);

                    //Lyra has really low gravity.
                    Player.gravity -= 0.35f;

                    //The anomaly's evil approaches!
                    anomalyTimer++;

                    if (anomalyTimer > 7200)
                    {
                        Player.AddBuff(BuffType<ApproachingEvilBuff>(), 10);

                    }
                    if (anomalyTimer > 8000)
                    {
                        //Yoink the player into Katabasis for taking too long!
                        SubworldSystem.Enter<Katabasis>();

                    }
                }
                else
                {

                }
                if (Player.InModBiome(GetInstance<FriendlySpaceBiome>()))
                {
                    //Make sure the player can't do what's not allowed:
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;

                    //Will change if new friendly space places are added.
                    if (!NPC.AnyNPCs(NPCType<Garridine>()) && Player.velocity.Y == 0)//Make sure Garridine isn't already there, and also check if you're standing.
                    {
                        int index = NPC.NewNPC(null, (int)Player.Center.X + 1150, (int)Player.Center.Y, NPCType<Garridine>());
                        NPC GarridineNPC = Main.npc[index];

                        // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                        if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, number: index);
                        }

                    }


                    //Fall too far into the void, and you'll be launched back up while taking heavy DoT.
                    if ((int)(Player.Center.Y / 16) > 500)
                    {
                        Player.AddBuff(BuffType<SpatialBurn>(), 120);

                        Player.velocity = new Vector2(Player.velocity.X, -17);
                    }
                }
                if (Player.InModBiome(GetInstance<SeaOfStarsBiome>()))
                {
                    //Make sure the player can't do what's not allowed:
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;


                    //Fall too far into the void, and you'll be launched back up while taking heavy DoT.
                    if ((int)(Player.Center.Y / 16) > 520)
                    {
                        Player.AddBuff(BuffType<SpatialBurn>(), 120);

                        Player.velocity = new Vector2(Player.velocity.X, -17);
                    }

                    if (SubworldSystem.IsActive<DreamingCity>())
                    {
                        Player.AddBuff(BuffType<AnomalyBuff>(), 10);

                        //The anomaly's evil approaches!
                        anomalyTimer++;

                        //More time then Lyra
                        if (anomalyTimer > 20800)
                        {
                            Player.AddBuff(BuffType<ApproachingEvilBuff>(), 10);

                        }
                        if (anomalyTimer > 22000)
                        {
                            //Yoink the player into Katabasis for taking too long!
                            SubworldSystem.Enter<Katabasis>();
                        }
                    }
                    else
                    {
                        if (SubworldSystem.IsActive<Katabasis>() || SubworldSystem.IsActive<FaintArchives>())
                        {
                            if (SubworldSystem.IsActive<Katabasis>())
                            {
                                Player.shimmerMonolithShader = true;

                                if (Player.velocity.X == 0)
                                {
                                    Player.AddBuff(BuffType<VisionsBeyondBuff>(), 10);
                                }

                                if (!NPC.AnyNPCs(NPCType<ArbitrationBoss>()) && anomalyTimer > 0)
                                {
                                    anomalyTimer = 0;
                                    int index = NPC.NewNPC(null, (int)Player.Center.X + 1150, (int)Player.Center.Y, NPCType<ArbitrationBoss>());

                                    // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                                    if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                                    {
                                        NetMessage.SendData(MessageID.SyncNPC, number: index);
                                    }

                                }
                            }

                        }
                        else
                        {
                            Player.gravity -= 0.3f;

                        }
                        //Space gravity!
                    }
                }
                if (Player.InModBiome(GetInstance<DreamingCityBiome>()))
                {
                    //Make sure the player can't do what's not allowed:
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;




                    Player.AddBuff(BuffType<AnomalyBuff>(), 10);

                    //The anomaly's evil approaches!
                    anomalyTimer++;

                    //More time then Lyra
                    if (anomalyTimer > 20800)
                    {
                        Player.AddBuff(BuffType<ApproachingEvilBuff>(), 10);

                    }
                    if (anomalyTimer > 22000)
                    {
                        //Yoink the player into Katabasis for taking too long!
                        SubworldSystem.Enter<Katabasis>();
                    }
                }
                if (Player.InModBiome(GetInstance<CorvusBiome>()))
                {
                    //Make sure the player can't do what's not allowed:

                    //Add this later.
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;

                    Player.AddBuff(BuffType<ArdorInfluence>(), 10);


                    if ((int)(Player.Center.Y / 16) < 830)
                    {
                        //Main.NewText(Language.GetTextValue($"{Player.Center.Y / 16}"), 255, 255, 247);

                        Player.velocity = new Vector2(Player.velocity.X, 17);
                    }
                    else
                    {
                        //Main.NewText(Language.GetTextValue($"{Player.Center.Y / 16}"), 220, 100, 247);

                    }


                }
                if (Player.InModBiome(GetInstance<BleachedWorldBiome>()))
                {
                    //Make sure the player can't do what's not allowed:
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;

                    //Space gravity!
                    //Player.gravity -= 0.3f;

                    if ((int)(Player.Center.Y / 16) < 320)
                    {
                        //Main.NewText(Language.GetTextValue($"{Player.Center.Y / 16}"), 255, 255, 247);

                        Player.velocity = new Vector2(Player.velocity.X, 17);
                    }
                    else
                    {
                        //Main.NewText(Language.GetTextValue($"{Player.Center.Y / 16}"), 220, 100, 247);

                    }

                }
                if (SubworldSystem.IsActive<Tucana>())
                {
                    //Make sure the player can't do what's not allowed:
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;

                    //Space gravity!
                    Player.gravity -= 0.1f;

                    if ((int)(Player.Center.Y / 16) < 570)
                    {
                        //Main.NewText(Language.GetTextValue($"{Player.Center.Y / 16}"), 255, 255, 247);

                        Player.velocity = new Vector2(Player.velocity.X, 17);
                    }
                    else
                    {
                        //Main.NewText(Language.GetTextValue($"{Player.Center.Y / 16}"), 220, 100, 247);

                    }
                }
                //Observatory.
                if (SubworldSystem.IsActive<Observatory>())
                {
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;
                    Main.cloudBGActive = 1f;

                    if ((int)(Player.Center.Y / 16) > 415)
                    {
                        Player.velocity = new Vector2(Player.velocity.X, -21);

                    }

                    Player.gravity -= gravityMod;
                }
                if (SubworldSystem.IsActive<DreamingCity>())
                {
                    if (Player.GetModPlayer<StarsAbovePlayer>().inCombat > 0)
                    {
                        Player.shimmerMonolithShader = true;

                    }
                }
                //Final boss arena.
                if (SubworldSystem.IsActive<EternalConfluence>())
                {
                    Player.AddBuff(BuffType<Superimposed>(), 10);
                    Player.noBuilding = true;
                    Main.cloudBGActive = 1f;

                    if ((int)(Player.Center.Y / 16) < 385)
                    {
                        //player.AddBuff(BuffType<SpatialBurn>(), 120);

                        Player.velocity = new Vector2(Player.velocity.X, 6);

                    }
                }
            }



            base.PostUpdateBuffs();
        }
        public override void UpdateDead()
        {
            if (Player.whoAmI == Main.myPlayer && Player.respawnTimer <= 60 && SubworldSystem.AnyActive<StarsAbove>())
            {
                SubworldSystem.Exit();
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (SubworldSystem.Current != null) //Within subworlds...
            {
                if (proj.type == ProjectileID.VortexLaser)
                {
                    modifiers.FinalDamage /= 6;
                }

            }



        }

        public override void ResetEffects()
        {


        }


        private void ActivateVNDialogue(Player player)
        {

            player.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
            player.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;
            player.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;
            player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;

        }
    }

    public class SubworldTile : GlobalTile
    {
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (SubworldSystem.AnyActive())
            {
                if (type == TileID.FogMachine || type == TileID.Teleporter || type == TileID.LunarMonolith || type == TileID.MusicBoxes || type == TileID.ShimmerMonolith || type == TileID.LogicSensor)
                    Framing.GetTileSafely(i, j).IsTileInvisible = true;

                if (type == TileID.Torches)
                {
                    if (Framing.GetTileSafely(i, j).TileFrameX == 0)
                    {
                        Framing.GetTileSafely(i, j).IsTileInvisible = true;

                    }
                }
            }

            return base.PreDraw(i, j, type, spriteBatch);
        }
    }
};