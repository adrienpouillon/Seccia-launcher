Shader "AGE/Grayscale"
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
	
			static const float3 _luminance = { 0.3, 0.59, 0.11 };
			uniform sampler2D _MainTex;
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
				float4 color = tex2D(_MainTex, i.uv);
				color.rgb = dot(color.rgb, _luminance);
				color.a *= _Color.a;
				color.rgb *= color.a;
				return color;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
