Shader "Unlit/NormalsShader"
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
                float3 normal : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // vertex shader: takes object space normal as input too
            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);

                // "object-space" normals -- normals as authored in blender
                //o.normal = normal;

                // "world-space" normals -- normals that rotate with our object. Necessary for lighting calculations
                o.normal = UnityObjectToWorldNormal(normal);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.normal * 0.5 + 0.5, 1.0);
            }
            ENDCG
        }
    }
}
