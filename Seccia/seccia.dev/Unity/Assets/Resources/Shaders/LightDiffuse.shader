Shader "AGE/LightDiffuse"
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
				float2 alpha : TEXCOORD0;
			};
	
			uniform half4 _Color;
			uniform float _x;
			uniform float _y;
			uniform float _dist;
			uniform float _attn;
			
			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				float alpha = 1.0f;
				if ( _dist>0.0f && _attn>0.0f )
					alpha -= saturate(distance(v.vertex.xy, float2(_x, _y))/_dist);
				o.alpha.x = alpha;
				o.alpha.y = 0.0f;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float4 color = _Color;
				if ( _attn>0.0f )
					color.a *= pow(i.alpha.x, _attn);
				color.rgb *= color.a;
				return color;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
