shader "MyShaders/VertexAnimation"
{
	Properties
	{
		[MainColor] _BaseColor("Base Color", Color) = (1,1,1,1)
		_Speed("WaveSpeed", Range(0.1,80)) = 5
		_Frequency("Wave Freacuency", Range(0,5)) = 3.16
		_Amplitude("Wave Amplitude",  Range(-1,1)) = 1
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			struct Attributes
			{
                float4 positionOS : POSITION;
                half3 normal : NORMAL;
			};
			struct Varyings
			{
                float4 positionHCS : SV_POSITION;
                half3 normal : TEXCOORD0;
			};
			CBUFFER_START(UnityPerMaterial)
				float _Speed;
				float _Frequency;
                float _Amplitude;
                half4 _BaseColor;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normal = TransformObjectToWorldNormal(IN.normal);
                float time = _Time * _Speed;
                float waveValue = sin(time + OUT.positionHCS.x + _Frequency) * _Amplitude;
                OUT.positionHCS.xyz = float3(OUT.positionHCS.x, OUT.positionHCS.y + waveValue, OUT.positionHCS.z);
                OUT.normal = normalize(float3(OUT.normal.x, OUT.normal.y + waveValue, OUT.normal.z));
                return OUT;
            }
            half4 frag(Varyings IN) : SV_Target
            {
                return _BaseColor;
            }
            ENDHLSL
        }
    }
    Fallback "Diffuse"
}