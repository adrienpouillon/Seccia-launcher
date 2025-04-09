Shader "AGE/Rain"
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
			uniform half4 _Color;

			uniform sampler2D _noiseTex;
			uniform float _fusion;
			uniform int _count;
			uniform half _distance;
			uniform half _vertical;
			uniform half4 _lightColor;

			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float dist = _distance;
				float time = _Time.y;
				float2 q = i.uv;
				float2 st0 =  q * float2(1.5, 0.05);
				if ( _vertical==0.0 )
					st0 += float2(-time*0.1+q.y*0.5, time*0.12);
				else
					st0 += float2(1.5, time*0.12);

				float3 color = 0.0;
				for ( int index=0 ; index<_count ; index++ )
				{
					float f = pow(dist, 0.45) + 0.25;

					//float2 st =  f * (q * float2(1.5, 0.05)+float2(-time*0.1+q.y*0.5, time*0.12));
					float2 st = st0 * f;
					f = tex2D(_noiseTex, st*0.5).x + tex2D(_noiseTex, st*0.284).y; // if wrap is repeat
					//f = tex2D(_noiseTex, frac(st*0.5)).x + tex2D(_noiseTex, frac(st*0.284)).y; // if wrap is clamp
					f = clamp(pow(abs(f)*0.5, 29.0) * 140.0, 0.00, q.y*0.4+0.05);

					float3 bri = 0.25;
					bri += _lightColor.rgb;
					bri *= f;
					color += bri;

					dist += _distance*3.5;
				}
				color = saturate(color);

				float4 trg = float4(color, 1.0);
				if ( _fusion==2.0 ) // mask
					trg.a = tex2D(_MainTex, i.uv).x;
				else if ( _fusion==1.0 ) // add
					trg.rgb += tex2D(_MainTex, i.uv).rgb;
				trg.a *= _Color.a;
				trg.rgb *= trg.a;
				return trg;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
