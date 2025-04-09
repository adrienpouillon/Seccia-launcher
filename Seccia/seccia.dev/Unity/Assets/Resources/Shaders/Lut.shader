Shader "AGE/LUT"
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
			uniform sampler2D _lutTex;
			uniform float _row;
			
			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float4 c = tex2D(_MainTex, i.uv);

				c.r = tex2D(_lutTex, half2(c.r*0.996, _row)).r;
				c.g = tex2D(_lutTex, half2(c.g*0.996, _row)).g;
				c.b = tex2D(_lutTex, half2(c.b*0.996, _row)).b;

				c.a *= _Color.a;
				c.rgb *= c.a;
				return c;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
