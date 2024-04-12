Shader "Unlit/Phong"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

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

            float4 _LightColor;
            float _Ambient;

            float4 frag (v2f i) : SV_Target
            {
                float3 result = float3(0.0, 0.0, 0.0);
                result += _LightColor * _Ambient;
                return float4(result, 1.0);
            }
            ENDCG
        }
    }
}
