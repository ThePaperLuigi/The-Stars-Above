
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
				pool.Add(ModContent.NPCType<NPCs.PrismLoot>(), 0.01f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.AsteroidWormHead>(), 1f);
				pool.Add(NPCID.BlackSlime, 1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.AstralCell>(), 1f);
				/*
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardSelenian>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardPredictor>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardVortexian>(), 0.1f);*/
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
