
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using StarsAbove.NPCs.OffworldNPCs;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using SubworldLibrary;
using StarsAbove.Subworlds;
using StarsAbove.Items.Prisms;
using StarsAbove.Items.Consumables;
using StarsAbove.Items.Accessories;
using StarsAbove.Buffs.Farewells;
using System;
using StarsAbove.Buffs.IrminsulDream;
using StarsAbove.Biomes;
using StarsAbove.Items.Materials;
using StarsAbove.NPCs.OffworldNPCs.Caelum;

namespace StarsAbove
{
    public class StarsAboveSubworldNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool NanitePlague;
		public bool OceanCulling;
		public bool Petrified;
		public bool Riptide;
		public bool Starblight;
		public bool RyukenStun;
		public bool voidAtrophy1;
		public bool voidAtrophy2;
		public bool ruination;
		public bool MortalWounds;
		public bool Glitterglue;
		public bool InfernalBleed;
		public bool Hyperburn;
		public bool VerdantEmbrace;
		public int NanitePlagueLevel = 0;


        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
			if (spawnInfo.Player.InModBiome<SeaOfStarsBiome>())
			{
				pool.Clear();
				pool.Add(ModContent.NPCType<NPCs.PrismLoot>(), 0.05f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.AsteroidWormHead>(), 1f);
				pool.Add(NPCID.BlackSlime, 1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.AstralCell>(), 1f);
				if(!NPC.AnyNPCs(ModContent.NPCType<NPCs.TownNPCs.Yojimbo>()))
                {
					pool.Add(ModContent.NPCType<NPCs.TownNPCs.Yojimbo>(), 0.1f);

				}
				/*
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardSelenian>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardPredictor>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardVortexian>(), 0.1f);*/
			}

			if (spawnInfo.Player.InModBiome<LyraBiome>()) // Anomaly planet stuff
			{
				pool.Clear();

				pool.Add(NPCID.Pinky, 1f);

				pool.Add(NPCID.Crawdad, 0.4f);
				pool.Add(NPCID.Salamander, 0.4f);
				pool.Add(NPCID.GiantShelly, 0.4f);

				pool.Add(NPCID.GoldDragonfly, 0.1f);
				pool.Add(NPCID.EnchantedNightcrawler, 0.5f);

				pool.Add(ModContent.NPCType<NPCs.PrismLoot>(), 0.3f);

				pool.Add(NPCID.GemBunnyAmethyst, 0.4f);

				pool.Add(NPCID.GemBunnyAmber, 0.4f);

				pool.Add(NPCID.GemBunnyDiamond, 0.4f);

				pool.Add(NPCID.GemBunnyEmerald, 0.4f);

				pool.Add(NPCID.GemBunnyRuby, 0.4f);

				pool.Add(NPCID.GemBunnySapphire, 0.4f);

				pool.Add(NPCID.GemBunnyTopaz, 0.4f);


				if(spawnInfo.Player.GetModPlayer<SubworldPlayer>().anomalyTimer >= 7200)
                {
					if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.Arbitration>()))
					{
						pool.Add(ModContent.NPCType<NPCs.Arbitration>(), 0.5f);

					}


                }



            }
            if (spawnInfo.Player.InModBiome<CorvusBiome>())
            {
                pool.Clear();
                pool.Add(ModContent.NPCType<NPCs.PrismLoot>(), 0.05f);
                pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.AmethystHeadpiercer>(), 1f);
                pool.Add(NPCID.BlackSlime, 1f);
                pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.AmethystSwordsinner>(), 1f);
                pool.Add(NPCID.DemonEye, 0.4f);

                if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.TownNPCs.Yojimbo>()))
                {
                    pool.Add(ModContent.NPCType<NPCs.TownNPCs.Yojimbo>(), 0.1f);

                }

            }
			if (SubworldSystem.IsActive<Tucana>())
			{
				pool.Remove(NPCID.BoundGoblin);
				pool.Remove(NPCID.BoundWizard);
				pool.Remove(NPCID.DD2Bartender);

			}
			if (SubworldSystem.IsActive<Pyxis>())
			{
				pool.Clear();
				
			}
			if (SubworldSystem.IsActive<Serpens>())
			{
				pool.Clear();
				pool.Add(NPCID.EaterofSouls, 1f);
				pool.Add(NPCID.DevourerHead, 1f);
				if(Main.hardMode)
                {
					pool.Add(NPCID.Corruptor, 0.3f);
					pool.Add(NPCID.CorruptSlime, 0.3f);
					pool.Add(NPCID.Slimer, 0.3f);
					pool.Add(NPCID.DarkMummy, 0.1f);
				}
			}
			if (SubworldSystem.IsActive<Scorpius>())
			{
				pool.Clear();
				pool.Add(NPCID.BloodCrawler, 1f);
				pool.Add(NPCID.FaceMonster, 1f);
				pool.Add(NPCID.Crimera, 1f);
				if (Main.hardMode)
				{
					pool.Add(NPCID.Herpling, 0.3f);
					pool.Add(NPCID.Crimslime, 0.3f);
					pool.Add(NPCID.BloodJelly, 0.3f);
					pool.Add(NPCID.BloodMummy, 0.1f);
				}
			}
			if (spawnInfo.Player.InModBiome<BleachedWorldBiome>())
			{
				pool.Clear();
				pool.Add(NPCID.SpikeBall, 0.4f);
				pool.Add(NPCType<Soulless>(), 1f);
				//pool.Add(ModContent.NPCType<NPCs.WaywardStarcell>(), 0.1f);
				//pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardPaladin>(), 0.1f);

			}
			if (SubworldSystem.IsActive<Observatory>())
			{
				pool.Clear();
				pool.Add(NPCID.Bird, 0.3f);
				pool.Add(NPCID.BirdBlue, 0.3f);
				pool.Add(NPCID.BirdRed, 0.3f);
				pool.Add(NPCID.GoldBird, 0.01f);
			}

			base.EditSpawnPool(pool, spawnInfo);
			

			
        }

        
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
			
		}

		
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {

			
		}

		

	}
}
