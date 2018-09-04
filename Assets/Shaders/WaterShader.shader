//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/WaterShader"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0.0,0.5)) = 0.25
        _Amplitude("Amplitude", Float) = 1
        _Speed ("Speed", Float) = 1

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
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _Transparency;
			fixed4 _Color; // Color from the material
			float4 _TintColor;


			// Implementation of the vertex shader
            vertOut vert(vertIn v)
            {   
				//float y = (sin(v.vertex.x * 1.0 + _Time.y * 1.0) + sin(v.vertex.z * 2.3 + _Time.y * 1.2) + sin(v.vertex.x * 2.9 + _Time.y * 0.7)) / 3.0;

				/*
				float y1 = (sin(v.vertex.x * 1.0 + _Time.y * 1.0) + sin(v.vertex.x * 1.0 + _Time.y * 1.5) + sin(v.vertex.z * 3.2 + _Time.y * 0.7)) / 3.0;
				float y2 = (sin(v.vertex.z * 0.65 + _Time.y * 1.0) + sin(v.vertex.z * 0.4 + _Time.y * 0.5) + sin(v.vertex.x * 1.5 + _Time.y * 1.5)) + sin(v.vertex.z * 3.2 + _Time.y * 0.4) / 4.0;

                float4 displacement = float4(0.0f, 0.35 * y1, 0.0f, 0.0f);
                v.vertex += displacement;

                vertOut o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
				*/
				
				// Displace the original vertex in model space
				float phase1 = _Time.y * 1.5;
				float phase2 = _Time.y * 0.75;
				float offset1 = (v.vertex.x + (v.vertex.x * 0.2)) * 0.7;
				float offset2 = (v.vertex.z * 0.6 + (v.vertex.z * 0.4)) * 0.5;
				v.vertex.y += sin(phase1 + offset1) * 0.2 + sin(phase2 + offset2) * 0.3;

				// Transform vertex to clip space
                v.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

				vertOut o;
				o.vertex = v.vertex;
				o.uv = v.uv;
                
                return o;
            }

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				//fixed4 color = tex2D(_MainTex, v.uv);
				//return color;

				// Just return the main color
				return _Color;
			}
			ENDCG
		}
	}
}