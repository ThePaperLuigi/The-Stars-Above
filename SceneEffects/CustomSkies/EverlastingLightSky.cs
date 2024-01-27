using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.WarriorOfLight;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.SceneEffects.CustomSkies
{
	public class EverlastingLightSky : CustomSky
	{
		private readonly Random _random = new Random();
		private bool _isActive;

		public override void OnLoad()
		{
		}

		public override void Update(GameTime gameTime)
		{
		}

		private float GetIntensity()
		{
			return 1f - Utils.SmoothStep(3000f, 6000f, 200f);
		}

		public override Color OnTileColor(Color inColor)
		{
			float intensity = GetIntensity();
			return new Color(255,243,198,240);
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)//Draw behind everything.
			{
				float intensity = GetIntensity();
                if (NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBoss>()) || NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBossFinalPhase>()))
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/SceneEffects/CustomSkies/EverlastingLightBGBoss"), new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);

                }
                else
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/SceneEffects/CustomSkies/EverlastingLightBG"), new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);

                }
            }
		}

		public override float GetCloudAlpha()
		{
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			_isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			_isActive = false;
		}

		public override void Reset()
		{
			_isActive = false;
		}

		public override bool IsActive()
		{
			return _isActive;
		}
	}
}
