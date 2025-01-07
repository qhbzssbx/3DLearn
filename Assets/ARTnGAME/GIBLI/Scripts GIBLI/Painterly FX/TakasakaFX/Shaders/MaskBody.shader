Shader "Hidden/PostEffectsMaskBody"
{
	CGINCLUDE
#include "UnityCG.cginc"
	ENDCG

	SubShader
	{
		Tags { "Queue" = "Overlay+1000" }

		// �}�X�N���̂̐F�͉�ʂɏo���Ȃ�
		Blend Zero One
		// �f�o�b�O�\���p
		//Blend One Zero

		ZTest Always

		Stencil
		{
			Ref 2
			Comp Always
			Pass Replace
			Fail Zero
			ZFail Zero
		}

        Pass
		{
            CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			float _MaskType;
            float4 frag(v2f_img i) : SV_Target{ return 1.0; }
            ENDCG
        }
    }
}