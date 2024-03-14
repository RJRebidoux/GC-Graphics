Shader "Unlit/UniformColor"
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

            // "Uniform" variable uploaded from CPU to GPU by setting the properties of our material in C#
            // Called a uniform variable because its constant for each run of the shader
            // (unlike vertices and fragments which generally change between invocations)
            fixed4 _Color;

            fixed4 frag () : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
