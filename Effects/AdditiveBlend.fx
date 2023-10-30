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

float2 uScrollSpeed = float2(0.01, 0.01); // New variable for the scrolling effect. Adjust these values as needed.

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
float2x2 RotationMatrix(float angle)
{
    return float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
}

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
     float4 color = tex2D(uImage0, coords);

    // Rotate and then translate (scroll) the coordinates
    float2 rotatedCoords = mul(coords - 0.5, RotationMatrix(uRotation)) + 0.5; // Assuming pivot is at the center
    rotatedCoords += uScrollSpeed * uTime; // Scrolling effect
    rotatedCoords = frac(rotatedCoords); // Wrap around the texture coordinates to remain in bounds

    float4 overlayColor = tex2D(uImage1, rotatedCoords);

    // Additive blending
    float4 resultColor;
    resultColor.rgb = color.rgb + overlayColor.rgb;
    resultColor.a = color.a; // Maintain original alpha, or you can manipulate as required

    return resultColor;
}

technique Technique1
{
    pass BlendPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}