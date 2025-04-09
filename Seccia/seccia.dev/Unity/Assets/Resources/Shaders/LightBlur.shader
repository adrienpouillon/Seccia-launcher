// not used anymore
Shader "AGE/LightBlur"
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
	
			#define DIR			16.0
			#define DIRCOUNT	16
			#define QUALITY		8.0
			#define QUALITYINV	0.125 // 1.0/QUALITY

			struct v2f
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
	
			uniform sampler2D _MainTex;
			uniform float _size;
			uniform float _aspect;
			
			static float2 _db[DIRCOUNT] =
			{
				float2(1.0, 0.0),
				float2(0.92388, 0.382683),
				float2(0.707107, 0.707107),
				float2(0.382683, 0.92388),
				float2(0.0, 1.0),
				float2(-0.382684, 0.92388),
				float2(-0.707107, 0.707107),
				float2(-0.92388, 0.382683),
				float2(-1.0, 0.0),
				float2(-0.92388, -0.382683),
				float2(-0.707107, -0.707107),
				float2(-0.382684, -0.92388),
				float2(0.0, -1.0),
				float2(0.382684, -0.923879),
				float2(0.707107, -0.707107),
				float2(0.92388, -0.382683),
			};

			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float r;
				float c;
				float s;

				float4 color = tex2D(_MainTex, i.uv);
				for ( int index=0 ; index<DIRCOUNT ; index++ )
				{
					c = _db[index].x * _aspect;
					s = _db[index].y;
					for ( float q=QUALITYINV ; q<1.0 ; q+=QUALITYINV )
					{
						r = _size * q;
						color += tex2D(_MainTex, i.uv+float2(c*r, s*r));
					}
				}
    
				color /= QUALITY * DIR - (DIR-1.0);
				//color.rgb *= color.a; pas utile ?
				return color;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
