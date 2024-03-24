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

            // Vertex inputs (vertices & texture coordinates uploaded from blender to GPU)
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex shader output / fragment shader input
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Output vertices in clip-space so they're correctly rasterized into pixels
            // Output texture coordinates without any changes since they're min [0, 0] max [1, 1]
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // 1. fragCoord in ShaderToy goes from [0, 0] in the bottom-left to [width, height] in the top-right.
            // _ScreenParams in Unity goes from [0, 0] in the top-left to [width, height] in the bottom-right.
            // Hence, you'll need to invert _ScreenParams.y if you want it to match fragCoord.
            // Alternatively, derive your uvs from i.uv as its within the default range mentioned above.
            fixed4 uvComparison(v2f i)
            {
                // Unity uv vertical component must be inverted to match ShaderToy
                float2 screenCoordinateUV = i.vertex.xy / _ScreenParams.xy;
                screenCoordinateUV.y = 1.0 - screenCoordinateUV.y;

                // Easier solution -- use per-vertex texture coordinates
                float2 textureCoordinateUV = i.uv;

                // We can see that the output is identical by interpolating between the two!
                float nsin = _SinTime.w * 0.5 + 0.5;
                float2 col = lerp(screenCoordinateUV, textureCoordinateUV, nsin);
                return fixed4(col, 0.0, 1.0);
            }

            // 2. Remember the range of uv. By default, its [0, 0] bottom-left, [1, 1] top-right.
            // Sometimes its more desirable to have [0, 0] in the centre of our screen,
            // so we modify uv to have a minimum of [-1, 1] and maximum of [1, 1].
            fixed4 uvRange(v2f i)
            {
                // Again, I recommend you use texture coordinates (i.uv) instead of screen coordinates (i.vertex / _ScreenParams):
                // Simply multiply by 2 then subtract 1 to transform from [0, 1] to [-1, 1]!
                float2 uv = i.uv * 2.0 - 1.0;

                // Furthermore, remember to apply the aspect ratio correction!!
                float aspect = _ScreenParams.x / _ScreenParams.y;
                uv.x *= aspect;

                float d = length(uv);

                // step(left, right) returns 0 if left is less than right, otherwise 1 (aka return left < right ? 0 : 1)
                // Hence, everything within a radius of 1.0 will be black, forming a circle!
                float circle = step(d, 1.0);

                // Black circle cutout from white background
                float3 col = float3(1.0, 1.0, 1.0) * circle;

                return fixed4(col, 1.0);

                // (You won't see this output unless you comment the above line)
                // We can get even fancier if we colour the circle based on our uv coordinates!
                i.uv.x *= aspect;   // Color using our original uv within normalized range, apply aspect correction.
                return fixed4(float3(i.uv, _SinTime.w * 0.5 + 0.5) * circle, 1.0);
            }

            // Same concept of above with less comments:
            fixed4 ring(v2f i)
            {
                float2 uv = i.uv * 2.0 - 1.0;
                uv.x *= _ScreenParams.x / _ScreenParams.y;;
                float d = length(uv);
                float radius = 0.75;
                float thickness = 0.25;
                d -= radius;
                d = abs(d);
                d = step(thickness, d);
                float3 col = float3(1.0, 1.0, 1.0);
                return fixed4(col * d, 1.0);
            }

            // *Insert colour math here*
            fixed4 frag (v2f i) : SV_Target
            {
                //return uvComparison(i);
                //return uvRange(i);
                return ring(i);
            }
            ENDCG
        }
    }
}
