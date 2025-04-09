Shader "AGE/BCS"
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
			uniform half _brightness;
			uniform half _contrast;
			uniform half _saturation;			

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

				// Brightness and Contrast
				color.rgb = saturate((color.rgb-0.5f)*_contrast + 0.5f + _brightness);

				// Saturation
				float lum = color.r*0.2126f + color.g*0.7152f + color.b*0.0722f;
				color.rgb = saturate(lum+(color.rgb-lum)*_saturation);
				
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
