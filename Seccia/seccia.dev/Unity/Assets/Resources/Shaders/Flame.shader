Shader "AGE/Flame"
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
				float4 screenPos : TEXCOORD1;
			};
	
			uniform sampler2D _MainTex;		// texture
			uniform sampler2D _FlameTex;	// texture
			uniform half4 _Color;			// constant color
			uniform float _speed;
			uniform float _scale;

			v2f vs(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
				o.screenPos = ComputeScreenPos(o.pos);
				return o;
			}

			fixed4 ps(v2f i) : COLOR
			{
				float4 color = tex2D(_MainTex, i.uv);
				if ( color.a==0.0f )
					return 0.0f;

				if ( color.a!=1.0f )
				{
					float2 uv = i.screenPos.xy; // i.screenPos.w is always 1
					uv *= _scale;

					float2 distUV;
					distUV.x = 7.2f * (uv.x + sin(uv.y * 12.127f) * 0.2f * cos(uv.x * 5.21f) * 0.2f);
					distUV.y = uv.y * 2.3f - _Time.y * _speed;
					float patternBack = min(1.0f, 2.21f * tex2D(_FlameTex, distUV).r);
					distUV.x = 6.5f * (uv.x + sin(uv.y * 32.127f) * 0.0053f);
					distUV.y = uv.y * 2.0f - _Time.y * _speed * 3.527f;
					float patternFront = 1.0f - min(1.0f, 2.6f * tex2D(_FlameTex, distUV).r);

					color.a *= patternFront*0.6f + patternBack*0.4f;
				}

				color.a *= _Color.a;	// opacity
				color.rgb *= color.a;	// premul
				return color;
			}

			ENDCG
		}
	}
	
	FallBack Off
}
