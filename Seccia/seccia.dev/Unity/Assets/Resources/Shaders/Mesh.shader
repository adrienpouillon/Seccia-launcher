Shader "AGE/Mesh"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend [_SrcBlend] [_DstBlend]
	
		Pass
		{
			SetTexture [_MainTex]
			{
				constantColor[_Color]
				combine primary * constant
			}
		}
	}
	
	FallBack Off
}
