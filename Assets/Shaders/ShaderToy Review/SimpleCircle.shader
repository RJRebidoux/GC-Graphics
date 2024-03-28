Shader "Hidden/SimpleCircle"
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

            fixed3 palette( float t ) {
                fixed3 a = fixed3(0.5, 0.5, 0.5);
                fixed3 b = fixed3(0.5, 0.5, 0.5);
                fixed3 c = fixed3(1.0, 1.0, 1.0);
                fixed3 d = fixed3(0.263, 0.416, 0.557);

                return a + b * cos( 6.28318 * (c * t + d) );
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // aspect-corrected uvs within [-1, 1]
                //vec2 uv = (fragCoord * 2.0 - iResolution.xy) / iResolution.y;
                fixed2 uv = i.uv * 2.0 - 1.0;
                uv.x *= _ScreenParams.x / _ScreenParams.y;
    
                // copy of original uv so we can sample global distance relative to centre (before space repetition)
                fixed2 uv0 = uv;
    
                // Accumulate colour onto a black background
                fixed3 finalColor = fixed3(0.0, 0.0, 0.0);

                float iTime = _Time.y;
    
                // Increase/decrease the iteration count to control number of repetitions
                for (float i = 0.0; i < 4.0; i++) {
                    // Repeat space then re-centre uvs
                    uv = frac(uv * 1.5) - 0.5;
        
                    // Combining x^2 and e^-x^2
                    float d = length(uv) * exp(-length(uv0));
        
                    // Animate colours based on distance, iteration count, and time
                    fixed3 col = palette(length(uv0) + i*.4 + iTime * 0.4);
        
                    // Create ripples
                    d = sin(d * 8.0 + iTime) / 8.0;
                    d = abs(d);
                    d = pow(0.01 / d, 1.2);
        
                    // Attenuate colour based on restricted logarithmic curve
                    finalColor += col * clamp(log(d), 0.01, 0.8);
                }
        
                return fixed4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}
