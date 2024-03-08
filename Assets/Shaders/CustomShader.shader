Shader "Unlit/CustomShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}       // Inspector texture input
        _Color ("Main Color", Color) = (1,1,1,1)    // Inspector color input
    }
    SubShader
    {
        // Render category (either opaque or transparent) 
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            // Specifies entry points 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Input data from CPU to GPU
            struct vertexdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Fragment shader input / vertex shader output
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Shader inspector variables
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            // Vertex shader (transforms vertex data like positions & normals)
            v2f vert (appdata v)
            {
                v2f o;

                // Applies translation/rotation/scale to vertices
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Output uvs same as input uvs
                o.uv = v.uv;
                return o;
            }
            
            // Fragment shader (colours pixels)
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}
