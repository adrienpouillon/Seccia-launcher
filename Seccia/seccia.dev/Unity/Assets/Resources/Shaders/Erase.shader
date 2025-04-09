Shader "AGE/Erase"
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
				constantColor[_Color]
				combine constant
			}
		}
	}

	FallBack Off
}
