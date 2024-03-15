Shader "Unlit/UVShader"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Vertex shader inputs
            struct vertexdata
            {
                float4 vertex : POSITION;   // vertex position
                float2 uv : TEXCOORD0;      // vertex texture coordinates
            };

            // Vertex shader outputs / fragment shader inputs
            struct v2f
            {
                float2 uv : TEXCOORD0;      // texture coordinates
                float4 vertex : SV_POSITION;// clip-space position (not accessible in fragment shader)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (vertexdata v)
            {
                v2f o;

                // Model * View * Projection -- model = world position, view = camera position, proj = 3d effect
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Output the texture coordinates as inputted
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(i.uv, _SinTime.w * 0.5 + 0.5, 1.0);
                return col;
            }
            ENDCG
        }
    }
}
