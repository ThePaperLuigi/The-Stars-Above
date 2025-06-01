sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float uOpacity;
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uEffectPos;
float2 uImageSize0;
float2 uImageSize1;

// Dark blue tint color
float3 darkBlueTint = float3(0.2, 0.3, 0.6); 

float gauss[3][3] =
{
    0.075, 0.124, 0.075,
    0.124, 0.204, 0.124,
    0.075, 0.124, 0.075
};

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    // Increase the blur intensity by sampling further from each pixel
    float dx = 3.0 * 1.0 / uScreenResolution.x; // Increased from 1.0 to 3.0
    float dy = 3.0 * 1.0 / uScreenResolution.y; // Increased from 1.0 to 3.0

    float2 targetScreenCoords = (uTargetPosition - uScreenPosition) / uScreenResolution;
    float targetYTextureSpace = targetScreenCoords.y;

    float cutoffYNormalized = (uTargetPosition.y - uScreenPosition.y) / uScreenResolution.y;
    if (coords.y < cutoffYNormalized)
    {
        return tex2D(uImage0, coords); // Return original texture above cutoff without effects
    }

    float aspectRatio = uScreenResolution.x / uScreenResolution.y;
    float2 adjustedCoords = (coords - 0.5) * float2(1.0, aspectRatio) + 0.5 - targetScreenCoords;
    adjustedCoords = max(0.0, min(adjustedCoords, 1.0));

    float distanceYFromTarget = coords.y - targetYTextureSpace;
    float reflectedY = targetYTextureSpace - distanceYFromTarget;
    float2 reflectedCoords = float2(adjustedCoords.x, reflectedY);

    // Apply Gaussian blur on the reflected and tinted image
    float4 color = float4(0, 0, 0, 0);

    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            float2 sampleCoords = float2(reflectedCoords.x + dx * i, reflectedCoords.y + dy * j);
            float4 sampleColor = tex2D(uImage0, sampleCoords);

            // Apply dark blue tint to each sampled color
            sampleColor.rgb *= darkBlueTint;

            // Accumulate the color weighted by the Gaussian kernel
            color += gauss[i + 1][j + 1] * sampleColor;
        }
    }

    // Retrieve the original texture color for blending
    float4 originalColor = tex2D(uImage0, coords);

    // Blend the original color with the processed color based on uIntensity
    float4 finalColor = lerp(originalColor, color, uIntensity);

    return finalColor;
}

technique Technique1
{
    pass Test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}