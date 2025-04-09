Shader "AGE/Fire"
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

			uniform half _fusion;
			uniform half _ratio;			// screen ratio
			uniform half _speed;			// factor
			
			float3 rgb2hsv(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
				float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

				float d = q.x - min(q.w, q.y);
				float e = 1.0e-10;
				return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}

			float3 hsv2rgb(float3 c)
			{
				float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
				float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
				return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
			}

			float rand(float2 n)
			{
				return frac(sin(cos(dot(n, float2(12.9898,12.1414)))) * 83758.5453);
			}

			float noise(float2 n)
			{
				const float2 d = float2(0.0, 1.0);
				float2 b = floor(n);
				float2 f = smoothstep(float2(0.0, 0.0), float2(1.0, 1.0), frac(n));
				return lerp(lerp(rand(b), rand(b + d.yx), f.x), lerp(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
			}

			float fbm(float2 n)
			{
				float total = 0.0, amplitude = 1.0;
				for ( int i=0 ; i<5 ; i++ )
				{
					total += noise(n) * amplitude;
					n += n*1.7;
					amplitude *= 0.47;
				}
				return total;
			}

			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float time = _Time.y * _speed;
				//float2 uv = float2(i.uv.x*_width, i.uv.y*_height);
				float2 uv = float2(i.uv.x*_ratio, i.uv.y);

				const float3 c1 = float3(0.5, 0.0, 0.1);
				const float3 c2 = float3(0.9, 0.1, 0.0);
				const float3 c3 = float3(0.2, 0.1, 0.7);
				const float3 c4 = float3(1.0, 0.9, 0.1);
				const float3 c5 = float3(0.1, 0.1, 0.1);
				const float3 c6 = float3(0.9, 0.9, 0.9);

				float2 speed = float2(1.2, 0.1);
				float shift = 1.327 + sin(time*2.0)/2.4;
    
				//change the constant term for all kinds of cool distance versions,
				//make plus/minus to switch between 
				//ground fire and fire rain!
				float dist = 3.5-sin(time*0.4)/1.89;
    
				float2 p = uv * dist / _ratio;
				p.x -= time/1.1;
				float q = fbm(p - time * 0.01+1.0*sin(time)/10.0);
				float qb = fbm(p - time * 0.002+0.1*cos(time)/5.0);
				float q2 = fbm(p - time * 0.44 - 5.0*cos(time)/7.0) - 6.0;
				float q3 = fbm(p - time * 0.9 - 10.0*cos(time)/30.0) - 4.0;
				float q4 = fbm(p - time * 2.0 - 20.0*sin(time)/20.0) + 2.0;
				q = (q + qb - .4 * q2 -2.0*q3  + .6*q4)/3.8;
				float2 r = float2(fbm(p + q /2.0 + time * speed.x - p.x - p.y), fbm(p + q - time * speed.y));
				float3 c = lerp(c1, c2, fbm(p + r)) + lerp(c3, c4, r.x) - lerp(c5, c6, r.y);
				float3 color = float3(c * cos(shift * uv.y));
				//float3 color = float3(c * cos(shift * uv.y / _height));
				color += 0.05;
				color.r *= 0.8;
				float3 hsv = rgb2hsv(color);
				hsv.y *= hsv.z  * 1.1;
				hsv.z *= hsv.y * 1.13;
				hsv.y = (2.2-hsv.z*.9)*1.20;
				color = hsv2rgb(hsv);

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
