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
using StarsAbove.Projectiles.Bosses.Vagrant;
using StarsAbove.NPCs.Vagrant;
using StarsAbove.Buffs.Boss;
using StarsAbove.Projectiles;
using StarsAbove.Utilities;
using StarsAbove.Projectiles.Bosses.Nalhaun;

namespace StarsAbove.NPCs.AttackLibrary
{
	public class AttackLibrary
	{
		// Here we define an enum we will use with the State slot. Using an ai slot as a means to store "state" can simplify things greatly. Think flowchart.
		// All bosses will look at this ActionState and go from there. Remember this takes the role of NPC.ai[0] so nothing else can go there.
		public enum ActionState
		{
			Spawning,
			Casting,
			PersistentCast,
			Idle,
			Dying,

			//Temporary from ExampleMod
			Notice,
			Jump,
			Hover,
			Fall,
			Asleep
		}

		

		//Vagrant attack. Charges forward, dealing contact damage and firing projectiles from starting position.
		public static void VorpalAssault(Player target, NPC npc)//This is the attack template for future attacks.
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Vorpal Assault";//The name of the attack.
				npc.ai[3] = 50;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);

				SoundEngine.PlaySound(SoundID.Item1, npc.Center);
				//Charge towards the target player.
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
				{
					float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
					npc.velocity.X = (float)(Math.Cos(rotation) * 32) * -1;
					npc.velocity.Y = (float)(Math.Sin(rotation) * 32) * -1;
				}

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					

				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);
				
				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
			




		}

		//Vorpal Barrage (Vagrant's bow attack.)
		public static void VorpalBarrage(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				
				modPlayer.NextAttack = "Vorpal Barrage";//The name of the attack.
				npc.ai[3] = 120;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBowSprite>(), 0, 0, Main.myPlayer);

					//Portal before teleporting.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

					if (npc.ai[2] % 2 == 0)
					{
						Vector2 moveTo = new Vector2(target.Center.X + 400, target.Center.Y - 80);
						npc.Center = moveTo;
					}
					else
					{
						Vector2 moveTo = new Vector2(target.Center.X - 400, target.Center.Y - 80);
						npc.Center = moveTo;
					}

					//Portal after teleporting
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_bowstring, npc.Center);

					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				
				float Speed = 6f;  //projectile speed
				Vector2 StartPosition = new Vector2(npc.Center.X, npc.Center.Y);
				int damage = 30;  //projectile damage
				int type = ProjectileType<VagrantStar>(); //Type of projectile

				float rotation = (float)Math.Atan2(StartPosition.Y - (target.position.Y + (target.height * 0.5f)), StartPosition.X - (target.position.X + (target.width * 0.5f)));
				Vector2 velocity = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
				
				SoundEngine.PlaySound(SoundID.Item124, npc.Center);
				for (int d = 0; d < 30; d++)
				{
					Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					float numberProjectiles = 3;
					float adjustedRotation = MathHelper.ToRadians(15);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition.X, StartPosition.Y, perturbedSpeed.X * 5, perturbedSpeed.Y * 5, type, damage, 0, Main.myPlayer);
					}


					/*Projectile.NewProjectile(npc.GetSource_FromAI(), 
						StartPosition.X, 
						StartPosition.Y, 
						(float)((Math.Cos(rotation) * Speed) * -1), 
						(float)((Math.Sin(rotation) * Speed) * -1), 
						type, 
						damage, 
						0f, 
						Main.myPlayer);*/
				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void VorpalSnipe(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{


				modPlayer.NextAttack = "Vorpal Snipe";//The name of the attack.
				npc.ai[3] = 120;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBowSprite>(), 0, 0, Main.myPlayer);

					//Portal before teleporting.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

					if (npc.ai[2] % 2 == 0)
					{
						Vector2 moveTo = new Vector2(target.Center.X + 400, target.Center.Y - 80);
						npc.Center = moveTo;
					}
					else
					{
						Vector2 moveTo = new Vector2(target.Center.X - 400, target.Center.Y - 80);
						npc.Center = moveTo;
					}

					//Portal after teleporting
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_bowstring, npc.Center);

					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				float Speed = 30f;  //projectile speed
				Vector2 StartPosition = new Vector2(npc.Center.X, npc.Center.Y);
				int damage = 10;  //projectile damage
				int type = ProjectileType<VagrantStar>(); //Type of projectile

				float rotation = (float)Math.Atan2(StartPosition.Y - (target.position.Y + (target.height * 0.5f)), StartPosition.X - (target.position.X + (target.width * 0.5f)));
				Vector2 velocity = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
				for (int d = 0; d < 30; d++)
				{
					Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
				}
				SoundEngine.PlaySound(SoundID.Item124, npc.Center);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					float numberProjectiles = 3;
					float adjustedRotation = MathHelper.ToRadians(3);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition.X, StartPosition.Y, perturbedSpeed.X * 5, perturbedSpeed.Y * 5, type, damage, 0, Main.myPlayer);
					}


					/*Projectile.NewProjectile(npc.GetSource_FromAI(), 
						StartPosition.X, 
						StartPosition.Y, 
						(float)((Math.Cos(rotation) * Speed) * -1), 
						(float)((Math.Sin(rotation) * Speed) * -1), 
						type, 
						damage, 
						0f, 
						Main.myPlayer);*/
				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}

		//"Overconfidence equals death."
		public static void VorpalSiege(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{
				//Boss dialogue
				 
				//CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{LangHelper.GetTextValue($"BossDialogue.Vagrant.VorpalSiege")}", false, false);
				modPlayer.NextAttack = "Vorpal Siege";//The name of the attack.

				npc.ai[3] = 120;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBowSprite>(), 0, 0, Main.myPlayer);

					//Portal before teleporting.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

					if (npc.ai[2] % 2 == 0)
					{
						Vector2 moveTo = new Vector2(target.Center.X + 400, target.Center.Y - 80);
						npc.Center = moveTo;
					}
					else
					{
						Vector2 moveTo = new Vector2(target.Center.X - 400, target.Center.Y - 80);
						npc.Center = moveTo;
					}

					//Portal after teleporting
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_bowstring, npc.Center);

					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				SoundEngine.PlaySound(SoundID.Item124, npc.Center);
				float Speed = 5f;  //projectile speed
				Vector2 StartPosition = new Vector2(npc.Center.X, npc.Center.Y);
				int damage = 30;  //projectile damage
				int type = ProjectileType<VagrantStar>(); //Type of projectile

				float rotation = (float)Math.Atan2(StartPosition.Y - (target.position.Y + (target.height * 0.5f)), StartPosition.X - (target.position.X + (target.width * 0.5f)));
				Vector2 velocity = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
				for (int d = 0; d < 30; d++)
				{
					Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					float numberProjectiles = 5;
					float adjustedRotation = MathHelper.ToRadians(15);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition.X, StartPosition.Y, perturbedSpeed.X * 5, perturbedSpeed.Y * 5, type, damage, 0, Main.myPlayer);
					}


					/*Projectile.NewProjectile(npc.GetSource_FromAI(), 
						StartPosition.X, 
						StartPosition.Y, 
						(float)((Math.Cos(rotation) * Speed) * -1), 
						(float)((Math.Sin(rotation) * Speed) * -1), 
						type, 
						damage, 
						0f, 
						Main.myPlayer);*/
				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 100;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void InverseStarfall(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);
				modPlayer.NextAttack = "Inverse Starfall";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, 500);

					int type = ProjectileType<VagrantStar>();
					int damage = 20;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 5; d += 1)
					{
						SoundEngine.PlaySound(SoundID.Item29, new Vector2(position.X - 200 + (d * 100), position.Y));
						Projectile.NewProjectile(entitySource, new Vector2(position.X - 200 + (d * 100), position.Y), -Vector2.UnitY*4, type, damage, 0f, Main.myPlayer);
						for (int ir = 0; ir < 30; ir++)
						{
							Vector2 positionNew = Vector2.Lerp(npc.Center, new Vector2(position.X - 200 + (d * 100), position.Y), (float)ir / 30);

							Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Green, null, 240, default(Color), 0.7f);
							da.fadeIn = 0.3f;
							da.noLight = true;
							da.noGravity = true;

						}
					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void Starfall(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);
				modPlayer.NextAttack = "Starfall";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, -500);

					int type = ProjectileType<VagrantStar>();
					int damage = 20;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 5; d += 1)
					{
						SoundEngine.PlaySound(SoundID.Item29, new Vector2(position.X - 200 + (d * 100), position.Y));

						Projectile.NewProjectile(entitySource, new Vector2(position.X - 200 + (d * 100), position.Y), Vector2.UnitY*4, type, damage, 0f, Main.myPlayer);
						for (int ir = 0; ir < 30; ir++)
						{
							Vector2 positionNew = Vector2.Lerp(npc.Center, new Vector2(position.X - 200 + (d * 100), position.Y), (float)ir / 30);
							
							Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Green, null, 240, default(Color), 0.7f);
							da.fadeIn = 0.3f;
							da.noLight = true;
							da.noGravity = true;

						}
					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}

		public static void HypertunedInverseStarfall(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);
				modPlayer.NextAttack = "Hypertuned Inverse Starfall";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, 500);

					int type = ProjectileType<VagrantStar>();
					int damage = 20;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 8; d += 1)
					{
						SoundEngine.PlaySound(SoundID.Item29, new Vector2(position.X - 400 + (d * 100), position.Y));
						Projectile.NewProjectile(entitySource, new Vector2(position.X - 400 + (d * 100), position.Y), -Vector2.UnitY * 4, type, damage, 0f, Main.myPlayer);
						for (int ir = 0; ir < 30; ir++)
						{
							Vector2 positionNew = Vector2.Lerp(npc.Center, new Vector2(position.X - 200 + (d * 100), position.Y), (float)ir / 30);

							Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Green, null, 240, default(Color), 0.7f);
							da.fadeIn = 0.3f;
							da.noLight = true;
							da.noGravity = true;

						}
					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void HypertunedStarfall(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);
				modPlayer.NextAttack = "Hypertuned Starfall";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, -500);

					int type = ProjectileType<VagrantStar>();
					int damage = 20;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 8; d += 1)
					{
						SoundEngine.PlaySound(SoundID.Item29, new Vector2(position.X - 400 + (d * 100), position.Y));

						Projectile.NewProjectile(entitySource, new Vector2(position.X - 400 + (d * 100), position.Y), Vector2.UnitY * 4, type, damage, 0f, Main.myPlayer);
						for (int ir = 0; ir < 30; ir++)
						{
							Vector2 positionNew = Vector2.Lerp(npc.Center, new Vector2(position.X - 200 + (d * 100), position.Y), (float)ir / 30);

							Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Green, null, 240, default(Color), 0.7f);
							da.fadeIn = 0.3f;
							da.noLight = true;
							da.noGravity = true;

						}
					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void MeteorShower(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);

				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, npc.Center);
				
				modPlayer.NextAttack = "Meteor Shower";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					for (int i = 0; i < 7; i++)
					{
						// Random upward vector.
						Vector2 vector2 = new Vector2(Main.rand.NextFloat(-16, 16), Main.rand.NextFloat(-9, -20));

						Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vector2, ProjectileID.DD2BetsyFireball, 15, 0, Main.myPlayer, npc.whoAmI, 1);
					}
					

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void TheofaniaInanis(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{
				//Boss dialogue
				 
				//CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{LangHelper.GetTextValue($"BossDialogue.Vagrant.Theofania")}", false, false);

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);

				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, npc.Center);

				modPlayer.NextAttack = "Theofania Inanis";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, -500);
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}
					//Play a sound effect.
					SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, npc.Center);

					int type = ProjectileType<BossTheofania>();
					int damage = 120;
					var entitySource = npc.GetSource_FromAI();
					Projectile.NewProjectile(entitySource, new Vector2(position.X, position.Y - 700), Vector2.UnitY * 14, type, damage, 0f, Main.myPlayer);
					for (int ir = 0; ir < 100; ir++)
					{
							Vector2 positionNew = Vector2.Lerp(npc.Center, new Vector2(position.X, position.Y - 600), (float)ir / 100);

							Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Green, null, 240, default(Color), 2f);
							da.fadeIn = 0.3f;
							da.noLight = true;
							da.noGravity = true;

					}
					
				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		//Keep your wits about you.
		public static void StarSundering(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{


				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)


					int type = ProjectileType<VagrantPlanetslash>();
					int damage = 10;
					var entitySource = npc.GetSource_FromAI();

					Projectile.NewProjectile(entitySource, new Vector2(npc.Center.X, npc.Center.Y - 150), Vector2.Zero, ProjectileID.PrincessWeapon, 0, 0f, Main.myPlayer);
					Projectile.NewProjectile(entitySource, new Vector2(npc.Center.X, npc.Center.Y - 150), Vector2.Zero, type, damage, 0f, Main.myPlayer);
					SoundEngine.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);

				}


				//CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{LangHelper.GetTextValue($"BossDialogue.Vagrant.UmbralUpsurge")}", false, false);

				modPlayer.NextAttack = "Star Sundering";//The name of the attack.
				npc.ai[3] = 130;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."

				

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSwordSprite>(), 0, 0, Main.myPlayer);


				#region attack
				for (int d = 0; d < 30; d++)
				{
					Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
				}
				


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		
		//"Backs against the wall- how will you respond?"

		//A: Looks like he's trapped you in... a gravitational loop!
		//E: What's happening? Did he ensnare you in a gravitational loop..?
		public static void GeneralRelativity(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				 
 
				modPlayer.NextAttack = "General Relativity";//The name of the attack.
				npc.ai[3] = 100;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				//Portal before teleporting.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

				Vector2 moveTo = new Vector2(target.Center.X, target.Center.Y - 80);
				npc.Center = moveTo;

				//Portal after teleporting
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);


				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);

				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, npc.Center);
				#region attack

				int index = NPC.NewNPC(null, (int)Main.player[npc.target].Center.X, (int)Main.player[npc.target].Center.Y, ModContent.NPCType<VagrantWalls>());
				NetMessage.SendData(MessageID.SyncNPC, number: index);


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void AdvancedRelativityVertical(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				
				modPlayer.NextAttack = "Advanced Relativity";//The name of the attack.
				npc.ai[3] = 100;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				//Portal before teleporting.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

				Vector2 moveTo = new Vector2(target.Center.X, target.Center.Y - 80);
				npc.Center = moveTo;

				//Portal after teleporting
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);


				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);


				#region attack
				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, npc.Center);
				int index = NPC.NewNPC(null, (int)Main.player[npc.target].Center.X, (int)Main.player[npc.target].Center.Y, ModContent.NPCType<VagrantWallsVertical>());
				NetMessage.SendData(MessageID.SyncNPC, number: index);


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void AdvancedRelativityHorizontal(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				 
				//CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{LangHelper.GetTextValue($"BossDialogue.Vagrant.AdvancedRelativity")}", false, false);
				modPlayer.NextAttack = "Advanced Relativity";//The name of the attack.
				npc.ai[3] = 100;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				//Portal before teleporting.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

				Vector2 moveTo = new Vector2(target.Center.X, target.Center.Y - 80);
				npc.Center = moveTo;

				//Portal after teleporting
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);


				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);

				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, npc.Center);
				#region attack

				int index = NPC.NewNPC(null, (int)Main.player[npc.target].Center.X, (int)Main.player[npc.target].Center.Y, ModContent.NPCType<VagrantWallsHorizontal>());
				NetMessage.SendData(MessageID.SyncNPC, number: index);

				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void Microcosmos(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantBurstSprite>(), 0, 0, Main.myPlayer);
				 
				//CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{LangHelper.GetTextValue($"BossDialogue.Vagrant.Microcosmos")}", false, false);

				modPlayer.NextAttack = "Microcosmos";//The name of the attack.
				npc.ai[3] = 100;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				//Portal before teleporting.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);

				Vector2 moveTo = new Vector2(target.Center.X, target.Center.Y - 80);
				npc.Center = moveTo;

				//Portal after teleporting
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantWormhole>(), 0, 0, Main.myPlayer);


				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);

				SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, npc.Center);

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)


					int type = ProjectileType<VagrantBulletSwarm>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();
					
					Projectile.NewProjectile(entitySource, new Vector2(npc.Center.X, npc.Center.Y - 100), Vector2.Zero, type, damage, 0f, Main.myPlayer);
					for (int ir = 0; ir < 50; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(npc.Center, new Vector2(npc.Center.X, npc.Center.Y - 100), (float)ir / 50);

						Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Green, null, 240, default(Color), 0.7f);
						da.fadeIn = 0.3f;
						da.noLight = true;
						da.noGravity = true;

					}

				}
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}

		//Purely visual. Use before using Bladework attacks.
		public static void ManifestBlade(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Manifest Laevateinn";//The name of the attack.
				npc.ai[3] = 60;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"CombatText.Nalhaun.ManifestBlade"), 241, 255, 180); }

					SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, npc.Center);
					npc.AddBuff(BuffType<NalhaunSword>(), 7200);
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordSprite>(), 0, 0, Main.myPlayer);
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_RuinationIsCome, npc.Center);

				

				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
		public static void RelinquishBlade(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Relinquish Laevateinn";//The name of the attack.
				npc.ai[3] = 60;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int index = npc.FindBuffIndex(BuffType<NalhaunSword>());
					if (index >= 0)
						npc.DelBuff(index);

					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunLoseSwordSprite>(), 0, 0, Main.myPlayer);
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.


				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
	
		//4 consecutive rotating slashes.
		public static void Bladework1(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			
			

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{
				//Global attack-specific variables
				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 20;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."

				

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);

				//Uses castTime so he holds the sword ready right before striking.
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 0 + 20);
				}

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 20;
					var entitySource = npc.GetSource_FromAI();

					//4 rotating slashes at the player's position.

					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 45f, 25f);
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 90f, 50f);
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 135f, 75f);

				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
		//Triangle centered on player.
		public static void Bladework2(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 10;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 0 + 30);
				}

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();

					//3 rotating slashes forming a triangle

					Projectile.NewProjectile(entitySource, new Vector2(target.Center.X - 70, target.Center.Y - 70), Vector2.Zero, type, damage, 0f, Main.myPlayer, 135f, 0f);
					Projectile.NewProjectile(entitySource, new Vector2(target.Center.X + 70, target.Center.Y - 70), Vector2.Zero, type, damage, 0f, Main.myPlayer, 45f, 5f);
					Projectile.NewProjectile(entitySource, new Vector2(target.Center.X, target.Center.Y + 70), Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 10f);

				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 60;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
		//Slashes in a + formation.
		public static void Bladework3(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 20;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 0 + 30);
				}
				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();

					//2 rotating slashes forming a +

					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 90f, 0f);

					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 120f);
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 90f, 120f);


				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}

		//Columns of vertical slashes and one horizontal slash on player.
		public static void BladeworkStrong1(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 40;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 0 + 30);
				}

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();


					for (int ir = 0; ir < 10; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 600, target.Center.Y), new Vector2(target.Center.X + 600, target.Center.Y), (float)ir / 10);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 90f, ir*2);
						

					}
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 0f);





				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
		//2 grids of slashes originating from left and right.
		public static void BladeworkStrong2(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 40;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{
				SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_Fools, npc.Center);

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 20 + 30);
				}

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();


					for (int ir = 0; ir < 10; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 600, target.Center.Y + 600), new Vector2(target.Center.X + 600, target.Center.Y - 600), (float)ir / 10);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 90f * ir, ir * 2 + 30);


					}
					for (int ir = 0; ir < 10; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X + 600, target.Center.Y + 600), new Vector2(target.Center.X - 600, target.Center.Y - 600), (float)ir / 10);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 90f * ir, ir * 2 + 120);


					}
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 45f, 30f);
					Projectile.NewProjectile(entitySource, target.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, 135f, 30f);





				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
		//Rapidly spinning slashes.
		public static void BladeworkStrong3(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 40;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 0 + 30);
				}

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();


					for (int ir = 0; ir < 10; ir++)
					{
						Vector2 positionNew = target.Center;

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f + ir*30, ir * 10);


					}
					





				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}

		//Chaotic slashes in "random" directions
		public static void BladeworkStrong4(Player target, NPC npc)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				modPlayer.NextAttack = "Burnished Bladework";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."


				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				//Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 30, 0, Main.myPlayer);
				SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_ComeShowMeMore, npc.Center);

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//Projectile.ai[0] corresponds to the delay before the swing attack.
					Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<NalhaunSwordAttackSprite>(), 0, 0, Main.myPlayer, 60 + 30);
				}

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int type = ProjectileType<BladeworkSlash>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();

					//Wave 1
					for (int ir = 0; ir < 10; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 700, target.Center.Y - 100), new Vector2(target.Center.X + 700, target.Center.Y + 100), (float)ir / 10);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f + ir * 29, ir * 10 + 60);


					}
					for (int ir = 0; ir < 4; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X + 600, target.Center.Y), new Vector2(target.Center.X - 600, target.Center.Y), (float)ir / 5);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f + ir * 32, ir * 10 + 60);


					}


					//Wave 2
					for (int ir = 0; ir < 10; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 600, target.Center.Y), new Vector2(target.Center.X + 600, target.Center.Y), (float)ir / 10);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f + ir * 69, ir * 10 + 220);


					}
					for (int ir = 0; ir < 4; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 600, target.Center.Y - 200), new Vector2(target.Center.X + 600, target.Center.Y + 200), (float)ir / 5);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f + ir * 32, ir * 10 + 220);


					}
					for (int ir = 0; ir < 5; ir++)
					{
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X + 600, target.Center.Y + 200), new Vector2(target.Center.X - 600, target.Center.Y - 200), (float)ir / 5);

						Projectile.NewProjectile(entitySource, positionNew, Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f + ir * 74, ir * 10 + 220);


					}



				}
				npc.netUpdate = true;
				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = -240;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}





		}
		public static void IvoryStake1(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<NalhaunCastSprite>(), 0, 0, Main.myPlayer, 80);
				SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);

				modPlayer.NextAttack = "Ivory Admonishment";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, -500);

					int type = ProjectileType<IvoryStake>();
					int damage = 50;
					var entitySource = npc.GetSource_FromAI();

					float Speed = 30f;  //projectile speed
					Vector2 StartPosition1 = new Vector2(target.Center.X - 1000, target.Center.Y - 400);
					Vector2 StartPosition2 = new Vector2(target.Center.X + 1000, target.Center.Y - 400);




					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}

					for (int i = 0; i < 4; i++)
					{
						float rotation1 = (float)Math.Atan2(StartPosition1.Y - (target.position.Y + (target.height * 0.5f)), StartPosition1.X - (target.position.X + (target.width * 0.5f)));
						float rotation2 = (float)Math.Atan2(StartPosition2.Y - (target.position.Y + (target.height * 0.5f)), StartPosition2.X - (target.position.X + (target.width * 0.5f)));

						Vector2 velocity1 = new Vector2((float)((Math.Cos(rotation1) * Speed) * -1 - i), (float)((Math.Sin(rotation1) * Speed) * -1 - i));
						Vector2 velocity2 = new Vector2((float)((Math.Cos(rotation2) * Speed) * -1 - i), (float)((Math.Sin(rotation2) * Speed) * -1 - i));

						float numberProjectiles = 4;
						float adjustedRotation = MathHelper.ToRadians(15);

						Vector2 perturbedSpeed1 = new Vector2(velocity1.X, velocity1.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f * (i+1); // Watch out for dividing by 0 if there is only 1 projectile.
						
						Vector2 perturbedSpeed2 = new Vector2(velocity2.X, velocity2.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f * (i+1); // Watch out for dividing by 0 if there is only 1 projectile.

						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition1.X, StartPosition1.Y, perturbedSpeed1.X, perturbedSpeed1.Y, type, damage, 0, Main.myPlayer);
						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition2.X, StartPosition2.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, 0, Main.myPlayer);

						
						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 600, target.Center.Y - 500), new Vector2(target.Center.X + 600, target.Center.Y - 500), (float)i / 10);
						
						//SoundEngine.PlaySound(SoundID.Item29, positionNew);

						
						

					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void IvoryStake2(Player target, NPC npc)//
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//Each attack in the Library has 3 important segments.
			//Part 1: Name of the attack and cast time. (ActionState.Idle)
			//Part 2: The actual execution of the attack. (ActionState.Casting)
			//Part 3: If the attack lasts longer than the initial attack, execute the active code. (ActionState.PersistentCast)

			//Global attack-specific variables

			if (npc.ai[0] == (float)ActionState.Idle && npc.ai[1] > 0)//If this is the first time the attack is being called.
			{

				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<NalhaunCastSprite>(), 0, 0, Main.myPlayer, 80);
				SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);

				modPlayer.NextAttack = "Ivory Admonishment";//The name of the attack.
				npc.ai[3] = 80;//This is the time it takes for the cast to finish.
				npc.localAI[3] = 0;//This resets the cast time.
				npc.ai[0] = (float)ActionState.Casting;//The boss is now in a "casting" state, and can run different animations, etc.
				npc.netUpdate = true;//NetUpdate for good measure.
									 //The NPC will recieve the message when this code is run: "Oh, I'm casting."
									 //Then it will think "I'm going to wait the cast time, then ask the Library what to do next."



				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.localAI[3] >= npc.ai[3])//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, -500);

					int type = ProjectileType<IvoryStake>();
					int damage = 50;
					var entitySource = npc.GetSource_FromAI();

					float Speed = 30f;  //projectile speed
					Vector2 StartPosition1 = new Vector2(target.Center.X - 1000, target.Center.Y - 400);
					Vector2 StartPosition2 = new Vector2(target.Center.X + 1000, target.Center.Y - 400);




					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}

					for (int i = 0; i < 3; i++)
					{
						float rotation1 = (float)Math.Atan2(StartPosition1.Y - (target.position.Y + (target.height * 0.5f)), StartPosition1.X - (target.position.X + (target.width * 0.5f)));
						float rotation2 = (float)Math.Atan2(StartPosition2.Y - (target.position.Y + (target.height * 0.5f)), StartPosition2.X - (target.position.X + (target.width * 0.5f)));

						Vector2 velocity1 = new Vector2((float)((Math.Cos(rotation1) * Speed) * -1 - i), (float)((Math.Sin(rotation1) * Speed) * -1 - i));
						Vector2 velocity2 = new Vector2((float)((Math.Cos(rotation2) * Speed) * -1 - i), (float)((Math.Sin(rotation2) * Speed) * -1 - i));

						float numberProjectiles = 3;
						float adjustedRotation = MathHelper.ToRadians(15);

						Vector2 perturbedSpeed1 = new Vector2(velocity1.X, velocity1.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f * (i + 1); // Watch out for dividing by 0 if there is only 1 projectile.

						Vector2 perturbedSpeed2 = new Vector2(velocity2.X, velocity2.Y).RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f * (i + 1); // Watch out for dividing by 0 if there is only 1 projectile.

						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition1.X, StartPosition1.Y, perturbedSpeed1.X, perturbedSpeed1.Y, type, damage, 0, Main.myPlayer);
						Projectile.NewProjectile(npc.GetSource_FromAI(), StartPosition2.X, StartPosition2.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, 0, Main.myPlayer);


						Vector2 positionNew = Vector2.Lerp(new Vector2(target.Center.X - 600, target.Center.Y - 500), new Vector2(target.Center.X + 600, target.Center.Y - 500), (float)i / 10);

						//SoundEngine.PlaySound(SoundID.Item29, positionNew);




					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.localAI[3] = 0;//Reset the cast.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void ResetAttack(Player target, NPC npc)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			
			
			modPlayer.CastTime = 0;
			modPlayer.NextAttack = "";
		}
	}
}