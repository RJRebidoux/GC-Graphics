Shader "Unlit/UniformMatrix"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            float4x4 _Model;
            
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                
                //float4x4 model = unity_ObjectToWorld; // Unity's model matrix (Transform component)
                float4x4 model = _Model;                // Custom model matrix uploaded from CPU via C# script
                float4x4 view = UNITY_MATRIX_V;
                float4x4 proj = UNITY_MATRIX_P;
                float4x4 mvp = mul(mul(proj, view), model);
                return mul(mvp, vertex);
                //return UnityObjectToClipPos(vertex);
            }
            

            fixed4 frag () : SV_Target
            {
                return fixed4(1.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
