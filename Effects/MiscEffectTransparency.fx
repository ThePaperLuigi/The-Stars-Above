sampler uImage0 : register(s0); // spritefont texture
sampler uImage1 : register(s1); // overlay texture 
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

float4 uShaderSpecificData;

float uRotation;
float2 uWorldPosition;
float3 uLightSource;
float2 uImageSize0;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;

float2 uUIPosition; // custom parameter
bool uDrawWithColor; // custom parameter
bool uDrawInTooptipCoords; // custom parameter

// ps_3_0 is needed for the "vPos" intrinsic. Modders Toolkit can compile ps_3_0 shaders.
float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 screenPosition : vPos ) : COLOR0
{
	float4 color = tex2D(uImage0, coords);

	if (color.a == 0.0)
		return float4(0.0,0.0,0.0,0.0);

	if (!any(color))
		return color; 
	
	// TODO: still need to account for zoom to be consistent.
	
	// This draws the texture consistent with screen space.
	float4 overlayColor = tex2D(uImage1, screenPosition / uImageSize1 );

	// uncomment this line to instead draw texture consistent with position within each individual tooltip line
	if(uDrawInTooptipCoords){
		overlayColor = tex2D(uImage1, (screenPosition - uUIPosition) / uImageSize1 ); 
	}

	// uncomment this code for some of the greyscale textures
	if(uDrawWithColor){
		float greyscaleAverage = (overlayColor.r + overlayColor.g + overlayColor.b) / 3 + 0.5;
		overlayColor.rgb = uColor.rgb * greyscaleAverage; 
	}
	
	return overlayColor;
}

technique Technique1
{
	pass CyclePass
	{
		// ps_3_0 is needed for the "vPos" intrinsic. Modders Toolkit can compile ps_3_0 shaders.
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}