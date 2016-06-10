// Upgrade NOTE: replaced 'SeperateSpecular' with 'SeparateSpecular'

Shader "Vertex Color Lit" {
	Properties { _LightColor ("Light Color", Color) = (0,0,0,1)}

		SubShader{
		Pass{
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM

				#pragma vertex vert             
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform half4 _Color;
				uniform float4 _LightColor;

				struct vertInput
				{
					float4 pos : POSITION;
					half4 color : COLOR;
					float3 nor : NORMAL;
				};

				struct vertOutput
				{
					float4 pos : SV_POSITION;
					half4 color : COLOR;
				};

				vertOutput vert(vertInput input)
				{
					vertOutput o;

					float4 normal = float4(input.nor, 0.0);
					float3 n = normalize(mul(normal, _World2Object));
					float3 l = normalize(_WorldSpaceLightPos0);

					float3 NdotL = max(0.0, dot(n, l));
					float3 a = UNITY_LIGHTMODEL_AMBIENT * input.color;

					float3 d = NdotL * _LightColor * input.color;
					float4 c = float4(d + a, input.color.a);

					o.pos = mul(UNITY_MATRIX_MVP, input.pos);
					o.color = c;

					return o;
				}

				struct fragOut {
					half4 color : COLOR;
				};

				fragOut frag(float4 color: COLOR)
				{
					fragOut output;
					output.color = color;
					if (output.color.a <= 0.9)
						discard;
					return output;
				}

			ENDCG
		}

		Pass{ 
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM

				#pragma vertex vert             
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform half4 _Color;
				uniform float4 _LightColor0;

				struct vertInput
				{
					float4 pos : POSITION;
					half4 color : COLOR;
					float3 nor : NORMAL;
				};

				struct vertOutput
				{
					float4 pos : SV_POSITION;
					half4 color : COLOR;
				};

				vertOutput vert(vertInput input)
				{
					vertOutput o;

					o.pos = mul(UNITY_MATRIX_MVP, input.pos);
					o.color =input.color;


					return o;
				}

				struct fragOut {
					half4 color : COLOR;
				};

				fragOut frag(float4 color: COLOR)
				{
					fragOut output;
					output.color = color;
					if (output.color.a >= 0.9)
						discard;
					return output;
				}

			ENDCG
			Alphatest Less 0.9
			Blend SrcAlpha OneMinusSrcAlpha
		}
	}
}