Shader "AGE/Curve"
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
			uniform fixed _lum[256];
			uniform fixed _sat[256];
			uniform half _hue[360];

			float HueToRgb(float v1, float v2, float vH)
			{
				if ( vH<0.0f )
					vH += 1.0f;
				if ( vH>1.0f )
					vH -= 1.0f;
				if ( 6.0f*vH<1.0f )
					return v1 + (v2 - v1) * 6.0f * vH;
				if ( 2.0f*vH<1.0f )
					return v2;
				if ( 3.0f*vH<2.0f )
					return v1 + (v2-v1) * ((2.0f/3.0f) - vH) * 6.0f;
				return v1;
			}

			float3 RgbToHsl(fixed3 rgb)
			{
				float minval = min(min(rgb.r, rgb.g), rgb.b);
				float maxval = max(max(rgb.r, rgb.g), rgb.b);
				float delta = maxval - minval;

				float hue = 0.0f;
				float sat = 0.0f;
				float lum = (minval+maxval) * 0.5f;

				if ( delta!=0.0f )
				{
					sat = lum<=0.5f ? delta/(maxval+minval) : delta/(2.0f-maxval-minval);

					if ( rgb.r==maxval )
						hue = ((rgb.g - rgb.b) / 6.0f) / delta;
					else if ( rgb.g==maxval )
						hue = (1.0f/3.0f) + ((rgb.b - rgb.r) / 6.0f) / delta;
					else
						hue = (2.0f/3.0f) + ((rgb.r - rgb.g) / 6.0f) / delta;

					if ( hue<0.0f )
						hue += 1.0f;
					if ( hue>1.0f )
						hue -= 1.0f;
				}

				return float3(hue, sat, lum);
			}

			float3 HslToRgb(float3 hsl)
			{
				float hue = hsl.x;
				float sat = hsl.y;
				float lum = hsl.z;

				float3 rgb = 0.0f;
			
				if ( sat==0.0f )
				{
					rgb = lum;
				}
				else
				{
					float v2 = lum<0.5f ? lum*(1.0f+sat) : (lum+sat)-(lum*sat);
					float v1 = 2.0f * lum-v2;
					rgb.r = HueToRgb(v1, v2, hue+(1.0f/3.0f));
					rgb.g = HueToRgb(v1, v2, hue);
					rgb.b = HueToRgb(v1, v2, hue-(1.0f/3.0f));
				}
			
				return rgb;
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
				// Source
				float4 color = tex2D(_MainTex, i.uv);

				// HSL
				float3 hsl = RgbToHsl(color.rgb);
				
				// Map
				hsl.x -= _hue[(int)(hsl.x*359.0f)] - 0.5f;
				hsl.y = _sat[(int)(hsl.y*255.0f)];
				hsl.z = _lum[(int)(hsl.z*255.0f)];

				// RGB
				color.rgb = HslToRgb(hsl);
			
				// Target
				color.a *= _Color.a;
				color.rgb *= color.a;
				return color;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
