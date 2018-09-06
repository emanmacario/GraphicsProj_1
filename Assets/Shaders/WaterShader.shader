//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/WaterShader"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 300
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"


			struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
			};

			// Color from the material
			fixed4 _Color; 

			// Implementation of the vertex shader
            vertOut vert(vertIn v)
            {   
				// Displace the original vertex in model space,
				// using compound sine waves to simulate wave effects
				float phase1 = _Time.y * 1.5;
				float phase2 = _Time.y * 0.75;
				float offset1 = (v.vertex.x + (v.vertex.x * 0.2)) * 0.7;
				float offset2 = (v.vertex.z * 0.6 + (v.vertex.z * 0.4)) * 0.5;
				v.vertex.y += sin(phase1 + offset1) * 0.2 + sin(phase2 + offset2) * 0.3;

				// Transform vertex to clip space
                v.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

				vertOut o;
				o.vertex = v.vertex;
                
                return o;
            }

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				// Return the main water color
				return _Color;
			}
			ENDCG
		}
	}
}