Shader "AGE/Text"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend [_SrcBlend] [_DstBlend] // SrcAlpha OneMinusSrcAlpha

		Pass
		{
			SetTexture[_MainTex]
			{
				constantColor[_Color]
				combine constant lerp(texture) previous
			}
			SetTexture[_MainTex]
			{
				combine previous * texture
			}
		}		 
	}

	FallBack Off
}
