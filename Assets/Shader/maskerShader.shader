Shader "Custom/PortalGateShader" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)  // カラーのプロパティ
    }
        SubShader{
        // Render Queue Override: 透明オブジェクト用のレンダリングキュー
        Tags { "RenderType" = "Fade" "Queue" = "Geometry-1" "RenderPipeline" = "UniversalPipeline"}  // Queueタグでレンダリング順序を指定
        LOD 200

        // フェード用のブレンドモード (アルファブレンディング)
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off  // Zバッファへの書き込みを無効化
        Cull Off    // カリングを無効化（裏面も描画）

        Pass {
                // ステンシルバッファの設定
                Stencil {
                    Ref 1           // ステンシル参照値を1に設定
                    Comp Always      // 常にステンシルテストが成功するように設定
                    Pass Replace     // 条件に合致した場合、ステンシルバッファの値を置き換える
                }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                fixed4 _Color;

                // 頂点シェーダー
                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);  // オブジェクト座標をクリップ空間に変換
                    o.uv = v.uv;  // UV座標をそのまま渡す
                    return o;
                }

                // フラグメントシェーダー
                fixed4 frag(v2f i) : SV_Target {
                    return _Color;  // 設定されたカラーをそのまま返す
                }
                ENDCG
            }
    }
}