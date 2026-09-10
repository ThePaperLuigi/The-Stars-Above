sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float2 uViewportSize;
float3 uMaskTransformX;
float3 uMaskTransformY;
float4 uMaskFrame;
float uMaskEnabled;
float2 uScrollSpeed;
float4 uBorderColor;
float2 uBorderDirection;
float uBorderScale;

float4 DrawBorder(float4 color, float2 offset, float scale, float opacity)
{
	float2 borderCoords = offset / (scale * uBorderScale) + 0.5;
	float insideBorder = step(0.0, borderCoords.x) * step(borderCoords.x, 1.0) * step(0.0, borderCoords.y) * step(borderCoords.y, 1.0);
	float2 borderHalfPixel = 0.5 / uImageSize3;
	float4 borderColor = tex2D(uImage3, clamp(borderCoords, borderHalfPixel, 1.0 - borderHalfPixel)) * uBorderColor * opacity * insideBorder;

	return saturate(borderColor + color * (1.0 - borderColor.a));
}

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
	float4 sceneColor = tex2D(uImage0, coords);
	float3 screenPosition = float3(coords * uViewportSize, 1.0);
	float2 maskCoords = float2(dot(screenPosition, uMaskTransformX), dot(screenPosition, uMaskTransformY));
	float insideMask = step(0.0, maskCoords.x) * step(maskCoords.x, 1.0) * step(0.0, maskCoords.y) * step(maskCoords.y, 1.0);
	float2 maskHalfPixel = 0.5 / (uImageSize2 * uMaskFrame.zw);
	float2 maskUV = uMaskFrame.xy + clamp(maskCoords, maskHalfPixel, 1.0 - maskHalfPixel) * uMaskFrame.zw;
	float maskAlpha = tex2D(uImage2, maskUV).a * insideMask * uMaskEnabled;
	float backgroundScale = max(uViewportSize.x / uImageSize1.x, uViewportSize.y / uImageSize1.y);
	float2 backgroundUV = (coords - 0.5) * uViewportSize / (uImageSize1 * backgroundScale) + 0.5 + frac(uScrollSpeed * uTime);
	float4 backgroundColor = tex2D(uImage1, backgroundUV);
	float4 color = lerp(sceneColor, backgroundColor, (1.0 - maskAlpha) * uOpacity);
	float2 borderOffset = (maskCoords - 0.5) * uImageSize2 * uMaskFrame.zw / uImageSize3 * uBorderDirection;
	float phase = frac(uTime * 0.5);
	float secondPhase = frac(phase + 0.5);
	float borderOpacity = uOpacity * uMaskEnabled;

	color = DrawBorder(color, borderOffset, 1.041, borderOpacity);
	color = DrawBorder(color, borderOffset, 1.041, borderOpacity);
	color = DrawBorder(color, borderOffset, 1.0 + phase * 0.35, min(phase, 1.0 - phase) * borderOpacity);
	color = DrawBorder(color, borderOffset, 1.0 + secondPhase * 0.35, min(secondPhase, 1.0 - secondPhase) * borderOpacity);

	return color;
}

technique Technique1
{
	pass ReversePass
	{
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}
