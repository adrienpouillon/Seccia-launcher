Shader "AGE/Monochrome"
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
	
			uniform sampler2D _MainTex;		// texture
			uniform half4 _Color;			// constant color
			uniform half4 _tint;			// tint
			uniform half _layer;			// layer/8 (byte position : r, g, b or a)
			uniform half _layer2;			// pow(2, layer%8)
			uniform half _width;			// texture width - 1
			uniform half _height;			// texture height - 1

			half4 GetColor(float2 uv)
			{
				half byte = round(tex2D(_MainTex, uv)[_layer]*255.0);
				if ( round(byte/_layer2)==0.0 )
					return 0.0;
				return _tint;
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
				float4 color = GetColor(i.uv);
				color.a *= _Color.a;
				color.rgb *= color.a;
				return color;

				//float u = i.uv.x * _width;
				//float v = i.uv.y * _height;
				//
				//float4 color = 0.0f;
				//for ( float y=-1.0 ; y<1.5 ; y++ )
				//{
				//	for ( float x=-1.0 ; x<1.5 ; x++ )
				//	{
				//		color += GetColor(float2((u+x)/_width, (v+y)/_height));
				//	}
				//}
				//color /= 9.0;
				//
				//color.a *= _Color.a;
				//color.rgb *= color.a;
				//return color;
			}

			ENDCG
		}
	}
	
	FallBack Off
}
