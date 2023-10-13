using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using Terraria.Graphics.Shaders;
using Terraria;

namespace StarsAbove.Effects
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct SwordSwing
	{
		//No, this doesn't work.
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["Zenith"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			SwordSwing._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			SwordSwing._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Purple;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 2f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}


}