Shader "AGE/Beacon"
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
			
			uniform float _width;
            uniform float _speed;
            uniform float _boost;
            uniform float _softness;
            uniform float _angle;
            uniform float _space;

			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				return o;
			}
	
			float smoothSweep(float offset, float center)
            {
                float halfWidth = _width * 0.5;
				float edgeStart = center - halfWidth - _softness;
                float edgeEnd = center + halfWidth + _softness;
				return smoothstep(edgeStart, center-halfWidth, offset) *  (1.0 - smoothstep(center+halfWidth, edgeEnd, offset));
            }

			fixed4 ps(v2f i) : COLOR
			{
                fixed4 color = tex2D(_MainTex, i.uv);

				float diagonal = i.uv.y * _angle;
				float sweepWidth = _width + _softness*2.0 + _space;
                float sweepCenter = fmod(_Time.y * _speed, 1.0+sweepWidth) - sweepWidth*0.5;
                float sweepMask = smoothSweep(i.uv.x+diagonal, sweepCenter);

                float luminance = dot(color.rgb, float3(0.2126, 0.7152, 0.0722));
                float boostedLuminance = luminance + (luminance * _boost * sweepMask);
                if ( luminance>0.0 )
                    color.rgb *= boostedLuminance / luminance;

				//color.rgb = sweepMask;
				color.a *= _Color.a;
				color.rgb *= color.a;
				return color;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
