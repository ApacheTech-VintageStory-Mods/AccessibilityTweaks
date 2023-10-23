#version 330 core

// Inputs from ShaderProgram.
uniform sampler2D	iChannel0;				// Primary Scene Texture
uniform int			iColourVisionType;		// The type of colour vision profile to simulate.

uniform float		iRedBalance;			// The multiplier to add to the frag colour's Red channel.
uniform float		iGreenBalance;			// The multiplier to add to the frag colour's Green channel.
uniform float		iBlueBalance;			// The multiplier to add to the frag colour's Blue channel.
uniform float		iSaturation;			// The multiplier to add to the frag colour's Satuation.

// Inputs from Vertex Shader.
in vec2				fragCoord;				// The centred, clip-space uv coords of the fragment.

// Outputs to GPU.
out vec4			outColour;				// The final outputted colour of the fragment.

mat3 Daltonise[9] = mat3[]
(
    mat3(1.0  , 0.0  , 0.0  ,  0.0  , 1.0  , 0.0  ,  0.0  , 0.0  , 1.0  ), // Trichromacy
    mat3(0.567, 0.433, 0.0  ,  0.558, 0.442, 0.0  ,  0.0  , 0.242, 0.758), // Protanopia
    mat3(0.817, 0.183, 0.0  ,  0.333, 0.667, 0.0  ,  0.0  , 0.125 ,0.875), // Protanomaly
    mat3(0.625, 0.375, 0.0  ,  0.7  , 0.3  , 0.0  ,  0.0  , 0.3   ,0.7  ), // Deuteranopia
    mat3(0.8  , 0.2  , 0.0  ,  0.258, 0.742, 0.0  ,  0.0  , 0.142 ,0.858), // Deuteranomaly
    mat3(0.95 , 0.05 , 0.0  ,  0.0  , 0.433, 0.567,  0.0  , 0.475 ,0.525), // Tritanopia
    mat3(0.967, 0.033, 0.0  ,  0.0  , 0.733, 0.267,  0.0  , 0.183 ,0.817), // Tritanomaly
    mat3(0.299, 0.587, 0.114,  0.299, 0.587, 0.114,  0.299, 0.587 ,0.114), // Achromatopsia
    mat3(0.618, 0.320, 0.062,  0.163, 0.775, 0.062,  0.163, 0.320 ,0.516)  // Achromatomaly
);

float hueToRGB(float v1, float v2, float vH)
{
	if (vH < 0.0) vH+= 1.0;
	if (vH > 1.0) vH -= 1.0;
	if ((6.0 * vH) < 1.0) return (v1 + (v2 - v1) * 6.0 * vH);
	if ((2.0 * vH) < 1.0) return (v2);
	if ((3.0 * vH) < 2.0) return (v1 + (v2 - v1) * ((2.0 / 3.0) - vH) * 6.0);
	return v1;
}

vec4 RGBtoHSL(vec4 rgb)
{
	vec4 hsl = vec4(0.0, 0.0, 0.0, rgb.w);
	
	float vMin = min(min(rgb.x, rgb.y), rgb.z);
	float vMax = max(max(rgb.x, rgb.y), rgb.z);
	float vDelta = vMax - vMin;
	
	hsl.z = (vMax + vMin) / 2.0;
	
	if (vDelta == 0.0)
	{
		hsl.x = hsl.y = 0.0;
	}
	else
	{
		if (hsl.z < 0.5) hsl.y = vDelta / (vMax + vMin);
		else hsl.y = vDelta / (2.0 - vMax - vMin);
		
		float rDelta = (((vMax - rgb.x) / 6.0) + (vDelta / 2.0)) / vDelta;
		float gDelta = (((vMax - rgb.y) / 6.0) + (vDelta / 2.0)) / vDelta;
		float bDelta = (((vMax - rgb.z) / 6.0) + (vDelta / 2.0)) / vDelta;
		
		if (rgb.x == vMax) hsl.x = bDelta - gDelta;
		else if (rgb.y == vMax) hsl.x = (1.0 / 3.0) + rDelta - bDelta;
		else if (rgb.z == vMax) hsl.x = (2.0 / 3.0) + gDelta - rDelta;
		
		if (hsl.x < 0.0) hsl.x += 1.0;
		if (hsl.x > 1.0) hsl.x -= 1.0;
	}
	
	return hsl;
}

vec4 HSLtoRGB(vec4 hsl)
{
	vec4 rgb = vec4(0.0, 0.0, 0.0, hsl.w);
	
	if (hsl.y == 0)
	{
		rgb.xyz = hsl.zzz;
	}
	else
	{
		float v1;
		float v2;
		
		if (hsl.z < 0.5) v2 = hsl.z * (1 + hsl.y);
		else v2 = (hsl.z + hsl.y) - (hsl.y * hsl.z);
		
		v1 = 2.0 * hsl.z - v2;
		
		rgb.x = hueToRGB(v1, v2, hsl.x + (1.0 / 3.0));
		rgb.y = hueToRGB(v1, v2, hsl.x);
		rgb.z = hueToRGB(v1, v2, hsl.x - (1.0 / 3.0));
	}
	
	return rgb;
}

void main()
{
	vec4 screenColour = texture(iChannel0, fragCoord);
	screenColour.rgb *= Daltonise[iColourVisionType];

	vec4 hsl = RGBtoHSL(vec4(
		screenColour.r * iRedBalance,
		screenColour.g * iGreenBalance,
		screenColour.b * iBlueBalance,
		screenColour.a));
		
	hsl.y = hsl.y * iSaturation;

	outColour = HSLtoRGB(hsl);
}