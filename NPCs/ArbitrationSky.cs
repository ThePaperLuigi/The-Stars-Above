using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace ExampleMod.NPCs.PuritySpirit
{
	public class ArbitrationSky : CustomSky
	{
		private bool isActive;
		private float intensity;
		private int arbitrationIndex;

		public override void Update(GameTime gameTime) {
			if (isActive && intensity < 1f) {
				intensity += 0.01f;
			}
			else if (!isActive && intensity > 0f) {
				intensity -= 0.01f;
			}
		}

		private bool UpdateArbitrationIndex() {
			int arbitrationType = ModLoader.GetMod("StarsAbove").Find<ModNPC>("Arbitration").Type;
			if (arbitrationIndex >= 0 && Main.npc[arbitrationIndex].active && Main.npc[arbitrationIndex].type == arbitrationType) {
				return true;
			}
			arbitrationIndex = -1;
			for (int i = 0; i < Main.maxNPCs; i++) {
				if (Main.npc[i].active && Main.npc[i].type == arbitrationType) {
					arbitrationIndex = i;
					break;
				}
			}
			return arbitrationIndex >= 0;
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth) {
			if (maxDepth >= 0 && minDepth < 0) {
				spriteBatch.Draw((Texture2D)Terraria.GameContent.TextureAssets.BlackTile, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(200, 200, 200) * intensity);
			}
		}

		public override float GetCloudAlpha() {
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args) {
			isActive = true;
		}

		public override void Deactivate(params object[] args) {
			isActive = false;
		}

		public override void Reset() {
			isActive = false;
		}

		public override bool IsActive() {
			return isActive || intensity > 0f;
		}
	}
}