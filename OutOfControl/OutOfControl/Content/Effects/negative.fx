#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float1 amount;
sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;

	
	float1 full_r = color.a - color.r;
	float1 full_g = color.a - color.g;
	float1 full_b = color.a - color.b;

	float r = color.r + (full_r - color.r) * amount;
	float g = color.g + (full_g - color.g) * amount;
	float b = color.b + (full_b - color.b) * amount;

	float4 invertedColor = float4(r,g,b, color.a);
	return invertedColor;
}
technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};