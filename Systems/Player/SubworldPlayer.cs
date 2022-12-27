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

namespace StarsAbove
{
    public class SubworldPlayer : ModPlayer
    {
        //This is for specific effects WITHIN subworlds
        public float gravityMod;

        public bool inYojimboRange; //So dialogue doesn't begin right after other dialogue ends.
        public bool inGarridineRange;

        public int anomalyTimer; //When this reaches a specific value, Arbitration will spawn.
       

        //When entering the range of a friendly NPC, use CombatText that says "Right click to initiate conversation!"


        public override void PreUpdate()
        {
            if (Main.LocalPlayer.isNearNPC(ModContent.NPCType<Yojimbo>(), 150) || Main.LocalPlayer.isNearNPC(ModContent.NPCType<Garridine>(), 150))
            {
                
            }

            DoYojimboDialogue();
            DoGarridineDialogue();

            if(SubworldSystem.Current == null)
            {
                //If not in a subworld
                
            }

        }
        private void DoYojimboDialogue()
        {
            if (Main.LocalPlayer.isNearNPC(ModContent.NPCType<Yojimbo>(), 150))
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
                        if(randomDialogue == 0)
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
            if (Main.LocalPlayer.isNearNPC(ModContent.NPCType<Garridine>(), 150))
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
                        

                    }
                    else
                    {

                        //Player.GetModPlayer<StarsAbovePlayer>().garridineIntroDialogue = 2;
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


        public override void PostUpdate()
        {

           
           
        }
        public override void OnRespawn(Player player)
        {
            if (SubworldSystem.Current != null)
            {
                player.AddBuff(BuffType<Invincibility>(), 240);
            }
            base.OnRespawn(player);
        }
        public override void PostUpdateBuffs()
        {
            

            if (Player.InModBiome(ModContent.GetInstance<LyraBiome>()))
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

                if(anomalyTimer > 7200)
                {
                    Player.AddBuff(BuffType<ApproachingEvilBuff>(), 10);

                }
            }
            else
            {
                anomalyTimer = 0;
            }
            if (Player.InModBiome(ModContent.GetInstance <FriendlySpaceBiome>()))
            {
                //Make sure the player can't do what's not allowed:
                Player.AddBuff(BuffType<Superimposed>(), 10);
                Player.noBuilding = true;

                //Will change if new friendly space places are added.
                if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.TownNPCs.Garridine>()) && Player.velocity.Y == 0)//Make sure Garridine isn't already there, and also check if you're standing.
                {
                    int index = NPC.NewNPC(null, (int)Player.Center.X + 1150, (int)Player.Center.Y, ModContent.NPCType<Garridine>());
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
            if (Player.InModBiome(ModContent.GetInstance<SeaOfStarsBiome>()))
            {
                //Make sure the player can't do what's not allowed:
                Player.AddBuff(BuffType<Superimposed>(), 10);
                Player.noBuilding = true;

                //Space gravity!
                Player.gravity -= 0.3f;
               
                //Fall too far into the void, and you'll be launched back up while taking heavy DoT.
                if ((int)(Player.Center.Y / 16) > 500)
                {
                    Player.AddBuff(BuffType<SpatialBurn>(), 120);

                    Player.velocity = new Vector2(Player.velocity.X, -17);
                }
            }
            if (Player.InModBiome(ModContent.GetInstance<CorvusBiome>()))
            {
                //Make sure the player can't do what's not allowed:

                //Add this later.
                Player.AddBuff(BuffType<Superimposed>(), 10);
                Player.noBuilding = true;

                Player.AddBuff(BuffType<ArdorInfluence>(), 10);



                
            }
            if (Player.InModBiome(ModContent.GetInstance<BleachedWorldBiome>()))
            {
                //Make sure the player can't do what's not allowed:
                Player.AddBuff(BuffType<Superimposed>(), 10);
                Player.noBuilding = true;

                //Space gravity!
                //Player.gravity -= 0.3f;

                if ((int)(Player.Center.Y / 16) < 100)
                {
                    //Player.AddBuff(BuffType<SpatialBurn>(), 120);

                    Player.velocity = new Vector2(Player.velocity.X, +17);
                }

            }
            if (SubworldSystem.IsActive<Tucana>())
            {
                //Make sure the player can't do what's not allowed:
                Player.AddBuff(BuffType<Superimposed>(), 10);
                Player.noBuilding = true;

                //Space gravity!
                Player.gravity -= 0.1f;


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

            base.PostUpdateBuffs();
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (SubworldSystem.Current != null) //Within subworlds...
            {
                if (proj.type == ProjectileID.VortexLaser)
                {
                    damage /= 6;
                }

            }


            base.ModifyHitByProjectile(proj, ref damage, ref crit);
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

};