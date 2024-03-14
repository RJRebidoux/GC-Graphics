Shader "Unlit/VertexTransformations"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f {
                half3 normal : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 color : TEXCOORD1;
            };

            float4x4 _Model;

            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL)
            {
                // Default model matrix. This comes from our Transform component!
                //float4x4 model = unity_ObjectToWorld;

                // Custom model matrix (sent from C# script as a uniform variable)!
                float4x4 model = _Model;
                float4x4 view = UNITY_MATRIX_V;
                float4x4 proj = UNITY_MATRIX_P;

                // Matrix multiplication happens right-to-left.
                // First we do model * view, then model-view * projection
                float4x4 mvp = mul( mul(proj, view), model);

                // This is the same as UnityObjectToClipPos(vertex);
                v2f o;
                o.pos = mul(mvp, vertex);
                o.normal = normal;
                o.color = UnityObjectToWorldNormal(normal) * 0.5 + 0.5;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color, 1.0); 
            }
            ENDCG
        }
    }
}
