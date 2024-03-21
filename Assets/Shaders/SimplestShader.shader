// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SimplestShader"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            // pixel shader, no inputs needed
            fixed4 frag () : SV_Target
            {
                return fixed4(0.0, 1.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
