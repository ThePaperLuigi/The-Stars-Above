using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
using Terraria.Graphics.Shaders;
using Terraria;

namespace StarsAbove.Effects
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct PurpleTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			PurpleTrail._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			PurpleTrail._vertexStrip.DrawTrail();
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

	public struct RedTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Red;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 1f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct YellowTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Yellow;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct SmallYellowTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Yellow;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.2f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct SmallPurpleTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.MediumPurple;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.2f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct SmallOrangeTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Orange;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.2f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct UltimaPurpleTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Indigo;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct UltimaBlueTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.RoyalBlue;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct VeraeroVFX
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.LightGreen;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.6f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct VerfireVFX
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.OrangeRed;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.6f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct BlueTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.RoyalBlue;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 1f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct SmallBlueTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.RoyalBlue;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.3f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct MedBlueTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.LightBlue;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 1.6f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct LargeRedTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Red;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 2.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct LargeBlueTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.LightBlue;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 2.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct OrangeTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Orange;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct PinkTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.Pink;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 1f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}

	public struct GreenTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.DarkGreen;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct WhiteTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Color.White;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public struct RainbowTrail
	{
		private static VertexStrip _vertexStrip = new VertexStrip();

		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply();
			_vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		private Color StripColors(float progressOnStrip)
		{
			Color value = Main.hslToRgb(.225f, .39f, 0.7f);
			//Color result = Color.Lerp(Color.Black, value, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, clamped: true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
			Color result = Main.DiscoColor;
			result.A = 0;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			float num = 0.5f;
			float lerpValue = Utils.GetLerpValue(-0f, 0.2f, progressOnStrip, clamped: true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}
	}
	public class VertexStrip
	{
		public delegate Color StripColorFunction(float progressOnStrip);

		public delegate float StripHalfWidthFunction(float progressOnStrip);

		private struct CustomVertexInfo : IVertexType
		{
			public Vector2 Position;

			public Color Color;

			public Vector2 TexCoord;

			private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0), new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0), new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0));

			public VertexDeclaration VertexDeclaration => CustomVertexInfo._vertexDeclaration;

			public CustomVertexInfo(Vector2 position, Color color, Vector2 texCoord)
			{
				this.Position = position;
				this.Color = color;
				this.TexCoord = texCoord;
			}
		}

		private CustomVertexInfo[] _vertices = new CustomVertexInfo[1];

		private int _vertexAmountCurrentlyMaintained;

		private short[] _indices = new short[1];

		private int _indicesAmountCurrentlyMaintained;

		private List<Vector2> _temporaryPositionsCache = new List<Vector2>();

		private List<float> _temporaryRotationsCache = new List<float>();

		public void PrepareStrip(Vector2[] positions, float[] rotations, StripColorFunction colorFunction, StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), int? expectedVertexPairsAmount = null, bool includeBacksides = false)
		{
			int num = positions.Length;
			int num2 = (this._vertexAmountCurrentlyMaintained = num * 2);
			if (this._vertices.Length < num2)
			{
				Array.Resize(ref this._vertices, num2);
			}
			int num3 = num;
			if (expectedVertexPairsAmount.HasValue)
			{
				num3 = expectedVertexPairsAmount.Value;
			}
			for (int i = 0; i < num; i++)
			{
				if (positions[i] == Vector2.Zero)
				{
					num = i - 1;
					this._vertexAmountCurrentlyMaintained = num * 2;
					break;
				}
				Vector2 pos = positions[i] + offsetForAllPositions;
				float rot = MathHelper.WrapAngle(rotations[i]);
				int indexOnVertexArray = i * 2;
				float progressOnStrip = (float)i / (float)(num3 - 1);
				this.AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
			}
			this.PrepareIndices(num, includeBacksides);
		}

		public void PrepareStripWithProceduralPadding(Vector2[] positions, float[] rotations, StripColorFunction colorFunction, StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), bool includeBacksides = false, bool tryStoppingOddBug = true)
		{
			int num = positions.Length;
			this._temporaryPositionsCache.Clear();
			this._temporaryRotationsCache.Clear();
			for (int i = 0; i < num && !(positions[i] == Vector2.Zero); i++)
			{
				Vector2 vector = positions[i];
				float num2 = MathHelper.WrapAngle(rotations[i]);
				this._temporaryPositionsCache.Add(vector);
				this._temporaryRotationsCache.Add(num2);
				if (i + 1 >= num || !(positions[i + 1] != Vector2.Zero))
				{
					continue;
				}
				Vector2 vector2 = positions[i + 1];
				float num3 = MathHelper.WrapAngle(rotations[i + 1]);
				int num4 = (int)(Math.Abs(MathHelper.WrapAngle(num3 - num2)) / ((float)Math.PI / 12f));
				if (num4 != 0)
				{
					float num5 = vector.Distance(vector2);
					Vector2 value = vector + num2.ToRotationVector2() * num5;
					Vector2 value2 = vector2 + num3.ToRotationVector2() * (0f - num5);
					int num6 = num4 + 2;
					float num7 = 1f / (float)num6;
					Vector2 target = vector;
					for (float num8 = num7; num8 < 1f; num8 += num7)
					{
						Vector2 vector3 = Vector2.CatmullRom(value, vector, vector2, value2, num8);
						float item = MathHelper.WrapAngle(vector3.DirectionTo(target).ToRotation());
						this._temporaryPositionsCache.Add(vector3);
						this._temporaryRotationsCache.Add(item);
						target = vector3;
					}
				}
			}
			int count = this._temporaryPositionsCache.Count;
			Vector2 zero = Vector2.Zero;
			for (int j = 0; j < count && (!tryStoppingOddBug || !(this._temporaryPositionsCache[j] == zero)); j++)
			{
				Vector2 pos = this._temporaryPositionsCache[j] + offsetForAllPositions;
				float rot = this._temporaryRotationsCache[j];
				int indexOnVertexArray = j * 2;
				float progressOnStrip = (float)j / (float)(count - 1);
				this.AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
			}
			this._vertexAmountCurrentlyMaintained = count * 2;
			this.PrepareIndices(count, includeBacksides);
		}

		private void PrepareIndices(int vertexPaidsAdded, bool includeBacksides)
		{
			int num = vertexPaidsAdded - 1;
			int num2 = 6 + includeBacksides.ToInt() * 6;
			int num3 = (this._indicesAmountCurrentlyMaintained = num * num2);
			if (this._indices.Length < num3)
			{
				Array.Resize(ref this._indices, num3);
			}
			for (short num4 = 0; num4 < num; num4 = (short)(num4 + 1))
			{
				short num5 = (short)(num4 * num2);
				int num6 = num4 * 2;
				this._indices[num5] = (short)num6;
				this._indices[num5 + 1] = (short)(num6 + 1);
				this._indices[num5 + 2] = (short)(num6 + 2);
				this._indices[num5 + 3] = (short)(num6 + 2);
				this._indices[num5 + 4] = (short)(num6 + 1);
				this._indices[num5 + 5] = (short)(num6 + 3);
				if (includeBacksides)
				{
					this._indices[num5 + 6] = (short)(num6 + 2);
					this._indices[num5 + 7] = (short)(num6 + 1);
					this._indices[num5 + 8] = (short)num6;
					this._indices[num5 + 9] = (short)(num6 + 2);
					this._indices[num5 + 10] = (short)(num6 + 3);
					this._indices[num5 + 11] = (short)(num6 + 1);
				}
			}
		}

		private void AddVertex(StripColorFunction colorFunction, StripHalfWidthFunction widthFunction, Vector2 pos, float rot, int indexOnVertexArray, float progressOnStrip)
		{
			while (indexOnVertexArray + 1 >= this._vertices.Length)
			{
				Array.Resize(ref this._vertices, this._vertices.Length * 2);
			}
			Color color = colorFunction(progressOnStrip);
			float scaleFactor = widthFunction(progressOnStrip);
			Vector2 value = MathHelper.WrapAngle(rot - (float)Math.PI / 2f).ToRotationVector2() * scaleFactor;
			this._vertices[indexOnVertexArray].Position = pos + value;
			this._vertices[indexOnVertexArray + 1].Position = pos - value;
			this._vertices[indexOnVertexArray].TexCoord = new Vector2(progressOnStrip, 1f);
			this._vertices[indexOnVertexArray + 1].TexCoord = new Vector2(progressOnStrip, 0f);
			this._vertices[indexOnVertexArray].Color = color;
			this._vertices[indexOnVertexArray + 1].Color = color;
		}

		public void DrawTrail()
		{
			if (this._vertexAmountCurrentlyMaintained >= 3)
			{
				Main.instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, this._vertices, 0, this._vertexAmountCurrentlyMaintained, this._indices, 0, this._indicesAmountCurrentlyMaintained / 3);
			}
		}
	}
}