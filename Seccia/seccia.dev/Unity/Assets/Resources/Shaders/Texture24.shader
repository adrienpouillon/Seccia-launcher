Shader "AGE/Texture24"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend Off

		Pass
		{
			SetTexture [_MainTex]
			{
				combine texture
			}
		}
	}

	Fallback Off
}
