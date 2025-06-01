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

float gauss[3][3] =
{
    0.075, 0.124, 0.075,
    0.124, 0.204, 0.124,
    0.075, 0.124, 0.075
};

float4 NeonVeil(float2 coords : TEXCOORD0) : COLOR0
{
	// Convert the pixel coords to a world position.
	// This is done by multiplying the UV by the screen resolution and adding the screen position.
	float2 worldCoords = coords * uScreenResolution + uScreenPosition;
	
	// Check if the current pixel is below the cut-off height
	if (worldCoords.y < uTargetPosition.y)
	{
		// Return original texture above cut-off without effects
		return tex2D(uImage0, coords); 
	}
	
	// Get the distance between the current pixel's world coords and the cut-off position
	float distance = worldCoords.y - uTargetPosition.y;
	
	// Create a new position and assign the world coordinates of the pixel
	float2 reflectedPosition = worldCoords;
	
	// Now if we remove the distance once we get to the cut-off height...
	// If we would do it twice instead, we get the correct mirrored Y position!
	reflectedPosition.y -= distance * 2.0;
	
	// Reverse the world coordinates back to UV coordinates
	float2 reflectedCoords = (reflectedPosition - uScreenPosition) / uScreenResolution;
	
	// Calculate the UV offset for getting the color from the texture,
	// this UV corresponds with 3 pixels on the screen. This is used for blur.
    float dx = 3.0 * 1.0 / uScreenResolution.x;
    float dy = 3.0 * 1.0 / uScreenResolution.y;
    
	// Prepare a variable to hold the color
    float4 color = float4(0, 0, 0, 0);
	
	// Apply Gaussian blur on the reflected and tinted image
    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            float2 sampleCoords = float2(reflectedCoords.x + dx * i, reflectedCoords.y + dy * j);
            float4 sampleColor = tex2D(uImage0, sampleCoords);

            // Apply dark blue tint to each sampled color
            sampleColor.rgb *= uColor;

            // Accumulate the color weighted by the Gaussian kernel
            color += gauss[i + 1][j + 1] * sampleColor;
        }
    }

    // Return a blended color based between original
	// and processed based on uIntensity
    return lerp(tex2D(uImage0, coords), color, uIntensity);
}

technique Technique1
{
    pass NeonVeil
    {
        PixelShader = compile ps_2_0 NeonVeil();
    }
}