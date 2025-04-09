Shader "AGE/Parallax"
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

			uniform sampler2D _depthTex;
			uniform float _x;
			uniform float _y;
			uniform float _scale;
			uniform float _focus;
			uniform float _ratioX;
			uniform float _ratioY;

			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float depth = tex2D(_depthTex, i.uv).r - _focus;
				float scaleX = depth * _scale;
				float scaleY = scaleX * -_ratioY;
				scaleX *= _ratioX;
				float2 displacement = float2(_x*scaleX, _y*scaleY);

				float4 trg = tex2D(_MainTex, i.uv + displacement);
				trg.a *= _Color.a;
				trg.rgb *= trg.a;
				return trg;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
