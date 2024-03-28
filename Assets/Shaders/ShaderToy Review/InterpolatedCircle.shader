Shader "Hidden/InterpolatedCircle"
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

            fixed4 frag (v2f i) : SV_Target
            {
                float aspect = _ScreenParams.x / _ScreenParams.y;

                // [0, 1] --> [-1, 1]
                float2 uv11 = i.uv * 2.0 - 1.0;
                i.uv.x *= aspect;
                uv11.x *= aspect;

                float d = length(uv11);
                float a = step(d, 1.0);
                float b = 1.0 - a;

                float nsin = _SinTime.w * 0.5 + 0.5;
                float tCircle = lerp(a, b, nsin);
                fixed3 col1 = fixed3(i.uv, 0.0);
                fixed3 col2 = fixed3(i.uv, 1.0);

                return fixed4(lerp(col1, col2, tCircle), 1.0);
            }
            ENDCG
        }
    }
}