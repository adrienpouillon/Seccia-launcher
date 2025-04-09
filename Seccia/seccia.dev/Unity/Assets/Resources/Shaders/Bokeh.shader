Shader "AGE/Bokeh"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend One Zero
	
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
