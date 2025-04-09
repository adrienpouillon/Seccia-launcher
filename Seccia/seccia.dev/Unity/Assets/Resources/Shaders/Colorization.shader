Shader "AGE/Colorization"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		//Blend SrcAlpha OneMinusSrcAlpha 
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

			uniform half4 _shadowColor;
			uniform half4 _highlightColor;
			uniform half _smoothMin;			// 0 <-> 1
			uniform half _smoothLen;			// 0 <-> 1

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

				float lum = color.r*0.2126 + color.g*0.7152 + color.b*0.0722;
				float w = saturate(saturate(lum-_smoothMin)/_smoothLen);
				float v = 1.0 - w;
				color.rgb = (_shadowColor.rgb*v + _highlightColor.rgb*w) * lum;

				color.a *= _Color.a;
				color.rgb *= color.a;
				return color;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
