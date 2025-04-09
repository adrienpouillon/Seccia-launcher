/*Shader "AGE/LightDiffuseFinal"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend [_SrcBlend] [_DstBlend]

		Pass
		{
			CGPROGRAM
			#pragma target 2.5
			#pragma vertex vs
			#pragma fragment ps
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
	
			uniform sampler2D _MainTex;
			
			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				return tex2D(_MainTex, i.uv);
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}*/

Shader "AGE/LightFinal"
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
				combine texture
			}
		}
	}

	Fallback Off
}
