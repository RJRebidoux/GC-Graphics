Shader "Hidden/ShaderToy"
{
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // We're rendering a giant quad (rectangle) that covers our screen (FSQ = "full screen quad").
            // The FSQ needs vertex positions & texture coordiantes just like every other piece of geometry.
            // We simply upload it directly from our CPU to GPU.
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // We output our vertex positions in clip-space (so our GPU can turn the vertices into pixels).
            // We forward our texture coordinates (uvs) to the fragment shader.
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // You can probably get away with converting your ShaderToy shader with nothing but built-in variables
            // (https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html)
            // Still, you now know how to upload anything you like from C# if need be!
            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.uv, _SinTime.w, 1.0);
            }
            ENDCG
        }
    }
}
