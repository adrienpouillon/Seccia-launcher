Shader "AGE/Brush"
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
	
			uniform half4 _Color;
			
			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				half4 c = _Color;
				c.rgb *= c.a;
				return c;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
