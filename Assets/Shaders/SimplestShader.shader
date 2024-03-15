Shader "Unlit/SimplestShader"
{
    Properties
    {
        // Color property for material inspector, default to white
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Graphics pipeline:
            // 1. Upload data from CPU to GPU
            // 2. Vertex processing (convert vertex positions from object-space to clip-space and forward information to fragment shader)
            // 3. Rasterization (GPU converts triangles into pixels)
            // 4. Fragment shader (colour pixels with math)
            // 5. Presentation (display object on screen)
            
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }
            
            // color from the material
            fixed4 _Color;

            // pixel shader, no inputs needed
            fixed4 frag () : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
