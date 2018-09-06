/* Phong Shading heavily based on Lab 5 code */

//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/PhongShader"
{
	Properties
	{
        _SunColor ("Sun Color", Color) = (0, 0, 0)
        _SunDirec ("Sun Direc", Vector) = (-1, 0, 0)
	}
	SubShader
	{
		Pass
		{
			//TODO
			//implement lighting
			//fix colour interpolation so it's a clean break
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float3 _SunColor;
			uniform float3 _SunDirec;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float4 color : COLOR;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
                float4 worldVertex : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				vertOut o;

				// Convert Vertex position and corresponding normal into world coords.
				// Note that we have to multiply the normal by the transposed inverse of the world 
				// transformation matrix (for cases where we have non-uniform scaling; we also don't
				// care about the "fourth" dimension, because translations don't affect the normal) 
				float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
				float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));

				// AMBIENT LIGHT
				float Ka = 1;
				float3 amb = v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

                // DIFFUSE LIGHT
				float fAtt = 1;
				float Kd = 1;
                /* Directional => parallel, reverse direc as _SunDirec is OPPOSITE of light direc from vertex */
				float3 L = normalize(-1 * _SunDirec);
                // Save L.N for reuse, calculate reflected ray for specular
				float LdotN = dot(L, worldNormal.xyz);
				float3 dif = fAtt * _SunColor.rgb * Kd * v.color.rgb * saturate(LdotN);

				// SPECULAR LIGHT
				float Ks = 1;
				float specN = 5; // Values>>1 give tighter highlights
				float3 V = normalize(_WorldSpaceCameraPos - worldVertex.xyz);
                float3 R = 2.0f * LdotN * worldNormal - L;
				float3 spe = fAtt * _SunColor.rgb * Ks * pow(saturate(dot(V, R)), specN);

				// COMBINE PHONG
				o.color.rgb = amb.rgb + dif.rgb + spe.rgb;
				o.color.a = v.color.a;

				// Transform vertex in world coordinates to camera coordinates
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

                // PIXEL SHADER NEEDS THESE VECTORS
                o.worldVertex = worldVertex;
                o.worldNormal = worldNormal;

				return o;
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
                float3 wV = v.worldVertex;
                float3 wN = v.worldNormal;
                fixed4 col = v.color * dot(wV, wN);
				return col;
			}

			ENDCG

		}
	}
}
