Shader "AGE/Grain"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend Off
	
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
			uniform half4 _Color;

			uniform sampler2D _grainTex;
			uniform half _gain;
			uniform half _delta;
			
			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				//return tex2D(_grainTex, i.uv);

				//float4 color = tex2D(_MainTex, i.uv);
				//color.rgb += tex2D(_grainTex, i.uv+_delta).r * _gain;

				return lerp(tex2D(_MainTex, i.uv), tex2D(_grainTex, i.uv+_delta).r, _gain);
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
