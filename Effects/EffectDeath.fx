sampler uImage0 : register(s0);
sampler uImage1 : register(s1); // Automatically Images/Misc/Perlin via Force Shader testing option
sampler uImage2 : register(s2); // Automatically Images/Misc/noise via Force Shader testing option
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

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
	if (!any(color))
		return color;
	float4 color1= tex2D( uImage1 , coords.xy);

	float readRed = uOpacity * 1.1;

	if(color1.r > readRed){
		color.rgba = 0;
	}else if(color1.r > uOpacity ){
		color =  float4(255.0/255, 238.0/255, 130.0/255, 1);
	}
	return color;
}

technique Technique1
{
    pass DeathAnimation
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}