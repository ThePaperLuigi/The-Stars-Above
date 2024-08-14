
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using StarsAbove.Biomes;
using StarsAbove.Utilities;
using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.Melee.SkyStriker;

namespace StarsAbove.NPCs.TownNPCs
{
    /// <summary>
    /// The main focus of this NPC is to show how to make something similar to the vanilla bone merchant;
    /// which means that the NPC will act like any other town NPC but won't have a happiness button, won't appear on the minimap,
    /// and will spawn like an enemy NPC. If you want a traditional town NPC instead, see <see cref="ExamplePerson"/>.
    /// </summary>
    public class Yojimbo : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 25; // The amount of frames the NPC has

			NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
			NPCID.Sets.AttackFrameCount[Type] = 4;
			NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the npc that it tries to attack enemies.
			NPCID.Sets.AttackType[Type] = 0;
			NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
			NPCID.Sets.AttackAverageChance[Type] = 30;
			NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.

			//This sets entry is the most important part of this NPC. Since it is true, it tells the game that we want this NPC to act like a town NPC without ACTUALLY being one.
			//What that means is: the NPC will have the AI of a town NPC, will attack like a town NPC, and have a shop (or any other additional functionality if you wish) like a town NPC.
			//However, the NPC will not have their head displayed on the map, will de-spawn when no players are nearby or the world is closed, will spawn like any other NPC, and have no happiness button when chatting.
			NPCID.Sets.ActsLikeTownNPC[Type] = true;

			//To reiterate, since this NPC isn't technically a town NPC, we need to tell the game that we still want this NPC to have a custom/randomized name when they spawn.
			//In order to do this, we simply make this hook return true, which will make the game call the TownNPCName method when spawning the NPC to determine the NPC's name.
			NPCID.Sets.SpawnsWithCustomName[Type] = true;

			//The vanilla Bone Merchant cannot interact with doors (open or close them, specifically), but if you want your NPC to be able to interact with them despite this,
			//uncomment this line below.
			NPCID.Sets.AllowDoorInteraction[Type] = true;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new()
            {
                Hide = true // Hides this NPC from the bestiary
            };

		}

		public override void SetDefaults()
		{
			NPC.friendly = true; // NPC Will not attack player
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			

			AnimationType = NPCID.Guide;
		}

		//Make sure to allow your NPC to chat, since being "like a town NPC" doesn't automatically allow for chatting.
		public override bool CanChat()
		{
			return false;
		}

        
        public override bool PreKill()
        {
			NPC.life = 1;
			return false;

        }
        

        public override void HitEffect(NPC.HitInfo hit)
		{
			// Causes dust to spawn when the NPC takes damage.
			int num = 3;

			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric);
			}
			if(NPC.life < NPC.lifeMax/2)
            {
				NPC.active = false;
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(LangHelper.GetTextValue($"NPCDialogue.Yojimbo.Leaving"), 255, 126, 114); }

				for (int ka = 0; ka < 50; ka++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric);
				}
			}
			
		}

		

		public override List<string> SetNPCNameList()
		{
			return new List<string> {
				"Yojimbo"
			};
		}//Let's see... If I'm with the Empire today, my name is Loki. Actually, this far out from Empire turf, you can call me whatever you like- I don't care.

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			//If any player is underground and has an example item in their inventory, the example bone merchant will have a slight chance to spawn.
			if (spawnInfo.Player.InModBiome<SeaOfStarsBiome>())
			{
				//return 0.04f;
			}

			//Else, the example bone merchant will not spawn if the above conditions are not met.
			return 0f;
		}
		
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();

			
			chat.Add(LangHelper.GetTextValue("NPCDialogue.Yojimbo.1"));
			chat.Add(LangHelper.GetTextValue("NPCDialogue.Yojimbo.2"));
			chat.Add(LangHelper.GetTextValue("NPCDialogue.Yojimbo.3"));
			chat.Add(LangHelper.GetTextValue("NPCDialogue.Yojimbo.4"));
			chat.Add(LangHelper.GetTextValue("NPCDialogue.Yojimbo.5"));
			return chat; // chat is implicitly cast to a string.
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{ // What the chat buttons are when you open up the chat UI
			button = LangHelper.GetTextValue("NPCDialogue.Talk");
		}
        public override void AI()
        {
			Lighting.AddLight(NPC.Center, TorchID.Ice);
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player p = Main.player[i];
				if (p.active && p.Distance(NPC.Center) < 150)
				{

					NPC.velocity = Vector2.Zero;
				}



			}
			

			//If a player gets close to him, and he hasn't met them yet, play a special dialogue.


			base.AI();
        }
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
			{
				//Play the VN dialogue. Yojimbo will quip about random story relevant bits, and then give you bounty hunting pointers, granting a buff.

				//shop = true;
			}
		}

		

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 100;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 120;
			randExtraCooldown = 120;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 22f;
			randomOffset = 0f;
		}
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
			projType = ModContent.ProjectileType<SkyStrikerRailgunRound>();
			attackDelay = 1;
        }
    }
}