
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using StarsAbove.Items;
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

namespace StarsAbove.NPCs
{
	public class StarsAboveGlobalNPC : GlobalNPC
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
		public int NanitePlagueLevel = 0;


        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
			if (spawnInfo.Player.GetModPlayer<StarsAbovePlayer>().SeaOfStars)
			{
				pool.Clear();
				//pool.Add(ModContent.NPCType<NPCs.WaywardStarcell>(), 0.1f);
				/*pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardStargazer>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardSelenian>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardPredictor>(), 0.1f);
				pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardVortexian>(), 0.1f);*/
			}
			if (spawnInfo.Player.GetModPlayer<StarsAbovePlayer>().BleachedWorld)
			{
				pool.Clear();
				//pool.Add(ModContent.NPCType<NPCs.WaywardStarcell>(), 0.1f);
				//pool.Add(ModContent.NPCType<NPCs.OffworldNPCs.WaywardPaladin>(), 0.1f);

			}
			if (SubworldSystem.IsActive<Observatory>() || SubworldSystem.IsActive<EternalConfluence>())
			{
				pool.Clear();
				pool.Add(NPCID.Bird, 0.3f);
				pool.Add(NPCID.BirdBlue, 0.3f);
				pool.Add(NPCID.BirdRed, 0.3f);
				pool.Add(NPCID.GoldBird, 0.01f);
			}

			base.EditSpawnPool(pool, spawnInfo);
			

			
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
			if(player.HasBuff(BuffType<BossEnemySpawnMod>()))
            {
				maxSpawns = 0;
            }

            base.EditSpawnRate(player, ref spawnRate, ref maxSpawns);
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (NanitePlagueLevel > 20)
			{
				NanitePlagueLevel = 20;
			}
			if (NanitePlague)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 16;
				if (damage < 2)
				{
					damage = 2;
				}
			}
			if (Starblight)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 30;
				
				damage = 30;
				
			}
			if (InfernalBleed)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 60;

				damage = 60;

			}
			if (ruination)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 2;

				damage = 2;

			}
			if (voidAtrophy2)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 100;

				damage = 400;

			}
			if (voidAtrophy1)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 100;

				damage = 200;

			}
			
		}
		
		public override void ResetEffects(NPC npc)
		{
			OceanCulling = false;
			MortalWounds = false;
			NanitePlague = false;
			Petrified = false;
			Riptide = false;
			Starblight = false;
			voidAtrophy1 = false;
			voidAtrophy2 = false;
			RyukenStun = false;
			Glitterglue = false;
			InfernalBleed = false;
		}
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior && !npc.boss && npc.damage > 0 && !npc.friendly)
			{


				npc.color = Color.LightGoldenrodYellow;
			}
		}
       
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
			

			return base.GetAlpha(npc, drawColor);
        }
		


        public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior && !npc.boss && npc.damage > 0 && !npc.friendly)
			{
				
				if (Main.rand.Next(2000) < 2)
				{
					for (int i = 0; i < 6; i++)
					{
						// Random upward vector.
						Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-1, -4));
						if(Main.netMode != NetmodeID.MultiplayerClient){Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vel, ProjectileID.GreekFire3, 40, 0, 0, 0, 1);}
					}
					for (int d = 0; d < 8; d++)
					{
						int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 222, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
						Main.dust[dustIndex].noGravity = true;
					}

					npc.life += npc.lifeMax / 10;
					if(npc.life > npc.lifeMax)
                    {
						npc.life = npc.lifeMax;
                    }
				}
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 158, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					
				}
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 91, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.32f;
					


				}
				
				Lighting.AddLight(npc.position, 0.1f, 0.1f, 0.1f);
				drawColor = drawColor.MultiplyRGB(Color.Yellow);
			}
			if (NanitePlague)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 235, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.2f;
					}
				}
				Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
			}
			if (MortalWounds)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 105, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.2f;
					}
				}
				//Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
			}
			if (InfernalBleed)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 105, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
					Main.dust[dust2].noGravity = true;
					Main.dust[dust2].velocity *= 0.8f;
					Main.dust[dust2].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust2].noGravity = false;
						Main.dust[dust2].scale *= 0.2f;
					}
				}
				
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						
						Main.dust[dust].scale *= 0.5f;
					}
				
				//Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
			}
			if (Petrified)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 0, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 2f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 1.5f;
					}
				}
			}
			if (Riptide)
			{
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 15, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.8f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}
				}
				if (Main.rand.NextBool(3))
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<Dusts.bubble>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.8f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}
				}
			}
			if (OceanCulling)
			{
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<Dusts.WaterShine>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.4f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 1f;
					}
				}
				
					int dust1 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<Dusts.WaterShine>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust1].noGravity = true;
					Main.dust[dust1].velocity *= 1.8f;
					Main.dust[dust1].velocity.Y -= 0.5f;
					Main.dust[dust1].scale = 0.4f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust1].noGravity = false;
						Main.dust[dust1].scale = 1f;
					}
				
			}
			if (ruination)
			{
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.8f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}
				}
				if (Main.rand.Next(12) < 2)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 109, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.8f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}
				}
			}
			if (Starblight)
			{
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 205, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.3f;
					if (Main.rand.NextBool(6))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}
					

				}
				int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 223, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Main.dust[dust2].velocity.Y -= 0.5f;
				Main.dust[dust2].scale = 0.2f;
				if (Main.rand.NextBool(4))
				{
					Main.dust[dust2].noGravity = false;
					Main.dust[dust2].scale = 1f;
				}
				
			}
			if (Glitterglue)
			{
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 86, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.3f;
					if (Main.rand.NextBool(6))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}


				}
				int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 97, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Main.dust[dust2].velocity.Y -= 0.5f;
				Main.dust[dust2].scale = 0.2f;
				if (Main.rand.NextBool(4))
				{
					Main.dust[dust2].noGravity = false;
					Main.dust[dust2].scale = 1f;
				}

			}
			if (voidAtrophy1 || voidAtrophy2)
			{
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 205, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.3f;
					if (Main.rand.NextBool(6))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}


				}
				int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 223, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Main.dust[dust2].velocity.Y -= 0.5f;
				Main.dust[dust2].scale = 0.2f;
				if (Main.rand.NextBool(4))
				{
					Main.dust[dust2].noGravity = false;
					Main.dust[dust2].scale = 1f;
				}

			}
			if (RyukenStun)
			{
				
				if (Main.rand.Next(12) < 5)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 205, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.dust[dust].scale = 0.3f;
					if (Main.rand.NextBool(6))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 2f;
					}


				}
				int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 223, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Main.dust[dust2].velocity.Y -= 0.5f;
				Main.dust[dust2].scale = 0.2f;
				if (Main.rand.NextBool(4))
				{
					Main.dust[dust2].noGravity = false;
					Main.dust[dust2].scale = 1f;
				}

			}
		}

		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
		}
        public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit)
        {


            base.OnHitNPC(npc, target, damage, knockback, crit);
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
			

            base.OnHitByItem(npc, player, item, damage, knockback, crit);
        }
		
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {

			
			
			if (npc.type == NPCID.WallofFlesh)
			{
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("ShatteredDisk").Type, 1));
				

			}

			if (npc.type == NPCID.QueenSlimeBoss)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<RoyalSlimePrism>(), 4));
			}
			if (npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer || npc.type == NPCID.TheDestroyer || npc.type == NPCID.SkeletronPrime)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<MechanicalPrism>(), 4));
			}
			if (npc.type == NPCID.Plantera)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<OvergrownPrism>(), 4));
			}
			if (npc.type == NPCID.Golem)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<LihzahrdPrism>(), 4));
			}
			if (npc.type == NPCID.HallowBoss)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<EmpressPrism>(), 4));
			}
			if (npc.type == NPCID.DukeFishron)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<TyphoonPrism>(), 4));
			}
			if (npc.type == NPCID.MoonLordCore)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<LuminitePrism>(), 4));
				npcLoot.Add(ItemDropRule.Common(ItemType<Items.Materials.CelestialPrincessGenesisPrecursor>(), 4));
			}
			if (npc.type == NPCType<VagrantOfSpaceAndTime>())
			{
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("SpatialPrism").Type, 2));
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EnigmaticDust").Type, 1,3,12));




			}
			if (npc.type == NPCType<Nalhaun>())
			{
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("BurnishedPrism").Type, 2));



			}
			if (npc.type == NPCType<Penthesilea>())
			{
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PaintedPrism").Type, 2));



			}
			if (npc.type == NPCType<Arbitration>())
			{
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("VoidsentPrism").Type, 2));



			}
			if (npc.type == NPCID.Demon)
			{
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("Luciferium").Type, 1000));


			}
			if (npc.type == NPCID.DemonTaxCollector)
			{

				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("Luciferium").Type, 1));

			}
			if (NPC.downedMoonlord)
			{
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("Glitterglue").Type, 10000));
				
			}
			if (npc.lifeMax <= 10 && npc.damage == 0)
			{
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("ToMurder").Type, 10000));
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PrismaticCore").Type, 10));
			}
			if (!npc.SpawnedFromStatue)
			{
				
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PrismaticCore").Type, 100));
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PrismaticCore").Type, 10));
			}
			if (DownedBossSystem.downedVagrant)
			{
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PrismaticCore").Type, 1));//Temp
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("Starlight").Type, 25));
				//npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PerfectlyGenericAccessory").Type, 10000));
				
				//Disabled normally
				/*
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("RadiantPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("RefulgentPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("VerdantPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EverflamePrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("CrystallinePrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("CastellicPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("AlchemicPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("ApocryphicPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("LucentPrism").Type, 10000));
				npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PhylacticPrism").Type, 10000));
				*/
			}


			VagrantDrops VagrantDropCondition = new VagrantDrops();
			IItemDropRule conditionalRule = new LeadingConditionRule(VagrantDropCondition);
			IItemDropRule rule = ItemDropRule.Common(Mod.Find<ModItem>("PrismaticCore").Type, chanceDenominator: 100);
			conditionalRule.OnSuccess(rule);
			npcLoot.Add(conditionalRule);

			IItemDropRule conditionalRule1 = new LeadingConditionRule(VagrantDropCondition);
			IItemDropRule rule1 = ItemDropRule.Common(Mod.Find<ModItem>("Starlight").Type, chanceDenominator: 25);
			conditionalRule.OnSuccess(rule1);
			npcLoot.Add(conditionalRule1);

			IItemDropRule conditionalRule2 = new LeadingConditionRule(VagrantDropCondition);
			IItemDropRule rule2 = ItemDropRule.Common(Mod.Find<ModItem>("PerfectlyGenericAccessory").Type, chanceDenominator: 10000);
			conditionalRule.OnSuccess(rule2);
			npcLoot.Add(conditionalRule2);


		}

		
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
			


			
        }

		

	}
}
