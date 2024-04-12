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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 position : TEXCOORD2;
                float3 normal : TEXCOORD1;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.position = mul(unity_ObjectToWorld, v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                return o;
            }

            float4 _LightColor;
            float _Ambient;
            float _Diffuse;

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
