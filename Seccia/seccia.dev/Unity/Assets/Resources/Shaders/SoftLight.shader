Shader "AGE/SoftLight"
{
	SubShader
	{
		Lighting Off
		ZWrite Off
		Blend Off

        GrabPass
        {
        }

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
				float4 uvgrab : TEXCOORD1;
			};
	
			uniform sampler2D _MainTex;
			uniform sampler2D _GrabTexture;
			
			v2f vs(appdata_img v)
			{ 
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv =  v.texcoord.xy;
				o.uvgrab = ComputeGrabScreenPos(o.pos);
				return o;
			}
	
			fixed4 ps(v2f i) : COLOR
			{
				float4 a = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
				float4 b = tex2D(_MainTex, i.uv);
				float4 c;
				c.rgb = (1.0-2.0*b.rgb)*a.rgb*a.rgb + 2.0*b.rgb*a.rgb;
				c.rgb = c.rgb*b.a + a.rgb*(1.0-b.a);
				c.a = 1.0;
				return c;
			}
	
			ENDCG
		}
	}
	
	FallBack Off
}
