Shader "Custom/PortalGateShader" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)  // �J���[�̃v���p�e�B
    }
        SubShader{
        // Render Queue Override: �����I�u�W�F�N�g�p�̃����_�����O�L���[
        Tags { "RenderType" = "Fade" "Queue" = "Geometry-1" "RenderPipeline" = "UniversalPipeline"}  // Queue�^�O�Ń����_�����O�������w��
        LOD 200

        // �t�F�[�h�p�̃u�����h���[�h (�A���t�@�u�����f�B���O)
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off  // Z�o�b�t�@�ւ̏������݂𖳌���
        Cull Off    // �J�����O�𖳌����i���ʂ��`��j

        Pass {
                // �X�e���V���o�b�t�@�̐ݒ�
                Stencil {
                    Ref 1           // �X�e���V���Q�ƒl��1�ɐݒ�
                    Comp Always      // ��ɃX�e���V���e�X�g����������悤�ɐݒ�
                    Pass Replace     // �����ɍ��v�����ꍇ�A�X�e���V���o�b�t�@�̒l��u��������
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

                // ���_�V�F�[�_�[
                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);  // �I�u�W�F�N�g���W���N���b�v��Ԃɕϊ�
                    o.uv = v.uv;  // UV���W�����̂܂ܓn��
                    return o;
                }

                // �t���O�����g�V�F�[�_�[
                fixed4 frag(v2f i) : SV_Target {
                    return _Color;  // �ݒ肳�ꂽ�J���[�����̂܂ܕԂ�
                }
                ENDCG
            }
    }
}