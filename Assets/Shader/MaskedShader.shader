Shader "Custom/MaskedShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" { }
        _StencilComp("Stencil Comparison", Float) = 3

        // Z座標の閾値
        _ZThreshold("Z Threshold", Float) = 0.0

        // クリッピングモード選択 (Disabled, GreaterThan, LessThan)
        [KeywordEnum(Disabled, GreaterThan, LessThan)]
        _CLIPPING_MODE("CLIPPING Mode", Float) = 0.0

        // アルファクリッピングのオプション(表示オブジェクトの透明度)
        _AlphaCutoff("Alpha Cutoff", Range(0, 1)) = 1.0
    }
        SubShader
            {
                Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
                Pass
                {
                    Name "ForwardLit"
                    Tags { "LightMode" = "UniversalForward" }

                    Stencil
                    {
                        Ref 1
                        Comp[_StencilComp]
                        Pass Keep
                    }

                    HLSLPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag

                // クリッピングモードの定義(enum)
                #pragma multi_compile _CLIPPING_MODE_DISABLED _CLIPPING_MODE_GREATERTHAN _CLIPPING_MODE_LESSTHAN

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 worldPos : TEXCOORD1; // ワールド座標を渡すための変数
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);

                float _ZThreshold;   // Z座標のしきい値
                float _AlphaCutoff;  // アルファクリッピングのしきい値

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = TransformObjectToHClip(v.vertex.xyz); // オブジェクト空間からクリップ空間への変換
                    o.uv = v.uv;
                    o.worldPos = TransformObjectToWorld(v.vertex.xyz); // ワールド座標への変換
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // テクスチャからカラーをサンプリング
                    half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                    // CLIPPINGの処理 (KeywordEnumによる切り替え)
                    #if defined(_CLIPPING_MODE_GREATERTHAN)
                        // Z座標がしきい値より大きい場合は非表示にする処理
                        if (i.worldPos.z > _ZThreshold)
                        {
                            clip(-1); // ピクセルを破棄（非表示）
                        }
                    #elif defined(_CLIPPING_MODE_LESSTHAN)
                        // Z座標がしきい値より小さい場合は非表示にする処理
                        if (i.worldPos.z < _ZThreshold)
                        {
                            clip(-1); // ピクセルを破棄（非表示）
                        }
                    #endif

                        // アルファクリッピング（デフォルトでは無効）
                        clip(col.a - _AlphaCutoff);

                        return col;
                    }
                    ENDHLSL
                }
            }
                FallBack "Diffuse"
}