Shader "VideoPoker/CardShimmer"
{
    Properties
    {
        _MainTex          ("Sprite Texture",    2D)           = "white" {}
        _ShimmerSpeed     ("Shimmer Speed",     Float)        = 1.5
        _ShimmerWidth     ("Shimmer Width",     Range(0.01, 0.5)) = 0.15
        _ShimmerIntensity ("Shimmer Intensity", Range(0, 1))  = 0.6
        _RainbowIntensity ("Rainbow Intensity", Range(0, 1))  = 0.35
    }

    SubShader
    {
        Tags
        {
            "Queue"             = "Transparent"
            "IgnoreProjector"   = "True"
            "RenderType"        = "Transparent"
            "PreviewType"       = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull     Off
        Lighting Off
        ZWrite   Off
        Blend    SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float  _ShimmerSpeed;
                float  _ShimmerWidth;
                float  _ShimmerIntensity;
                float  _RainbowIntensity;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float4 color       : COLOR;
            };

            // Hue (0-1) -> RGB
            float3 HueToRgb(float hue)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(float3(hue, hue, hue) + K.xyz) * 6.0 - K.www);
                return saturate(p - K.xxx);
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv          = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color       = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * IN.color;

                // Diyagonal ışık süpürmesi (köşeden köşeye geçiyor)
                float diag  = (IN.uv.x + IN.uv.y) * 0.5;
                float sweep = frac(diag - _Time.y * _ShimmerSpeed);
                float mask  = smoothstep(0.0, _ShimmerWidth, sweep) *
                              smoothstep(_ShimmerWidth * 2.0, _ShimmerWidth, sweep);

                // UV konumuna + zamana göre gökkuşağı rengi
                float hue      = frac(IN.uv.x * 0.5 + IN.uv.y * 0.3 + _Time.y * _ShimmerSpeed * 0.15);
                float3 rainbow = HueToRgb(hue);

                // Beyaz parıltı ile gökkuşağını harmanlıyoruz
                float3 shimmerColor = lerp(float3(1, 1, 1), rainbow, _RainbowIntensity);
                float3 finalRgb     = lerp(tex.rgb, shimmerColor, mask * _ShimmerIntensity * tex.a);

                return half4(finalRgb, tex.a);
            }
            ENDHLSL
        }
    }
}
