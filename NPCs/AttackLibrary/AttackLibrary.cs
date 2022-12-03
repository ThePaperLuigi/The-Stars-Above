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
				npc.ai[3] = 50;//This is the cast time- when this value reaches 0, the cast is finished.
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
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{

				#region attack
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSpearSprite>(), 70, 0, Main.myPlayer);


				//Charge towards the target player.
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
				{
					float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
					npc.velocity.X = (float)(Math.Cos(rotation) * 32) * -1;
					npc.velocity.Y = (float)(Math.Sin(rotation) * 32) * -1;
				}

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					

					

					

					npc.netUpdate = true;
				}
				#endregion

				
				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);
				
				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
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
				npc.ai[3] = 120;//This is the cast time- when this value reaches 0, the cast is finished.
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

					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{

				#region attack
				
				float Speed = 6f;  //projectile speed
				Vector2 StartPosition = new Vector2(npc.Center.X, npc.Center.Y);
				int damage = 30;  //projectile damage
				int type = ProjectileType<VagrantStar>(); //Type of projectile

				float rotation = (float)Math.Atan2(StartPosition.Y - (target.position.Y + (target.height * 0.5f)), StartPosition.X - (target.position.X + (target.width * 0.5f)));
				Vector2 velocity = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));

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
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
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


				modPlayer.NextAttack = "Vorpal Siege";//The name of the attack.
				npc.ai[3] = 120;//This is the cast time- when this value reaches 0, the cast is finished.
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

					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}

				return;
			}
			if (npc.ai[0] == (float)ActionState.PersistentCast)//If an attack lasts, it'll be moved to PersistentCast until the cast finishes.
			{

				return;
			}
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{

				#region attack

				float Speed = 12f;  //projectile speed
				Vector2 StartPosition = new Vector2(npc.Center.X, npc.Center.Y);
				int damage = 40;  //projectile damage
				int type = ProjectileType<VagrantStar>(); //Type of projectile

				float rotation = (float)Math.Atan2(StartPosition.Y - (target.position.Y + (target.height * 0.5f)), StartPosition.X - (target.position.X + (target.width * 0.5f)));
				Vector2 velocity = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));

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
				npc.ai[1] = 100;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
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

				modPlayer.NextAttack = "Starfall";//The name of the attack.
				npc.ai[3] = 60;//This is the cast time- when this value reaches 0, the cast is finished.
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
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, 400);

					int type = ProjectileType<VagrantStar>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 5; d += 1)
					{
						Projectile.NewProjectile(entitySource, new Vector2(position.X - 200 + (d * 100), position.Y), -Vector2.UnitY, type, damage, 0f, Main.myPlayer);

					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.ai[1] = 50;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
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

				modPlayer.NextAttack = "Inverse Starfall";//The name of the attack.
				npc.ai[3] = 60;//This is the cast time- when this value reaches 0, the cast is finished.
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
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, -400);

					int type = ProjectileType<VagrantStar>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 5; d += 1)
					{
						Projectile.NewProjectile(entitySource, new Vector2(position.X - 200 + (d * 100), position.Y), Vector2.UnitY, type, damage, 0f, Main.myPlayer);

					}

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
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

				modPlayer.NextAttack = "Meteor Shower";//The name of the attack.
				npc.ai[3] = 80;//This is the cast time- when this value reaches 0, the cast is finished.
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
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{

				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					for (int i = 0; i < 7; i++)
					{
						// Random upward vector.
						Vector2 vector2 = new Vector2(Main.rand.NextFloat(-16, 16), Main.rand.NextFloat(-9, -20));

						Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vector2, ProjectileID.DD2BetsyFireball, 20, 0, Main.myPlayer, npc.whoAmI, 1);
					}
					

				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}
		public static void UmbralUpsurge(Player target, NPC npc)//
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

				modPlayer.NextAttack = "Umbral Upsurge";//The name of the attack.
				npc.ai[3] = 100;//This is the cast time- when this value reaches 0, the cast is finished.
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
			if (npc.ai[0] == (float)ActionState.Casting && npc.ai[3] <= 0)//If this attack is called again (which means the cast finished)
			{
				//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
				Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);


				#region attack

				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					// Spawn projectile randomly below target, based on horizontal velocity to make kiting harder, starting velocity 1f upwards
					// (The projectiles accelerate from their initial velocity)

					float kitingOffsetX = Utils.Clamp(target.velocity.X * 16, -100, 100);
					Vector2 position = target.Bottom + new Vector2(kitingOffsetX, 400);

					int type = ProjectileType<VagrantStar>();
					int damage = 30;
					var entitySource = npc.GetSource_FromAI();
					for (int d = 0; d < 5; d += 1)
					{
						Projectile.NewProjectile(entitySource, new Vector2(position.X - 200 + (d * 100), position.Y), -Vector2.UnitY, type, damage, 0f, Main.myPlayer);

					}
					
				}


				#endregion


				//After the attack ends, we do some cleanup.
				ResetAttack(target, npc);

				npc.ai[0] = (float)ActionState.Idle;//If the attack continues, change ActionState to PersistentCast instead
				modPlayer.NextAttack = "";//Empty the UI text.
				npc.ai[1] = 0;//Reset the internal clock before the next attack. Higher values means less of a delay before the next attack.
				npc.ai[2] += 1;//Increment the rotation counter.
				npc.netUpdate = true;//NetUpdate for good measure.

				return;
			}
		}

		/*
		 */

		//Celestial Opposition

		//Solar Skipgate
		//(Beam of light that travels from the right side of the screen to the left- you have to use the teleport walls to avoid it.)

		//Leftward Starfall

		//Microcosmos

		//Macrocosmos

		//Cosmoturgy

		//Essence Conjuration


		//Uses pre-hardmode Essences?
		//Eldritch Blast

		//Upon the Dark Moon

		//Theater of Bullets


		public static void ResetAttack(Player target, NPC npc)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			
			
			modPlayer.CastTime = 0;
			modPlayer.NextAttack = "";
		}
	}
}