//https://pastebin.com/t4fuCLmP 


Shader"Custom/Echolocation" {

    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Center ("CenterX", vector) = (0, 0, 0)
        _Center2 ("Center2", vector) = (0, 0, 0)
    
        _Radius ("Radius", float) = 0
        _Radius2 ("Radius2", float) = 0
        _FullShade ("FullShade",float) = .1
        _SemiShade ("SemiShade", float) = 1.5
        }

    SubShader {
           Pass {
            Tags { "RenderType"="Opaque" }
       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            float4 _Color;
            float3 _Center;
            float3 _Center2;
            float _Radius;
            float _Radius2;
            float _FullShade;
            float _SemiShade;

            float NormalizeValue(float value, float minValue, float maxValue)
            {
                // Ensure the value is within the specified range.
                value = clamp(value, minValue, maxValue);

                // Calculate the normalized value between 0 and 1.
                return (value - minValue) / (maxValue - minValue);
}
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };
 
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
 

            fixed4 frag(v2f i) : COLOR
            {
                float dist1 = distance(_Center, i.worldPos);
                float dist2 = distance(_Center2, i.worldPos);

                            // Calculate the color contributions for both centers
                float val1 = 1 - step(dist1, _Radius - _FullShade) * 0.5;
                val1 = step(_Radius - _SemiShade, dist1) * step(dist1, _Radius) * val1 * NormalizeValue(dist1, _Radius - _SemiShade, (_Radius - _FullShade));

                float val2 = 1 - step(dist2, _Radius2 - _FullShade) * 0.5;
                val2 = step(_Radius2 - _SemiShade, dist2) * step(dist2, _Radius2) * val2 * NormalizeValue(dist2, _Radius2 - _SemiShade, (_Radius2 - _FullShade));

                            // Combine the color contributions
                fixed4 finalColor = (_Color * val1) + (_Color * val2); // You can adjust this blending as needed

                return finalColor;
            }

            //fixed4 frag(v2f i) : COLOR
            //{
            //    float dist = distance(_Center, i.worldPos);
                    

 
            //    float val = 1 - step(dist, _Radius - _FullShade) * 0.5;
            //    val = step(_Radius - _SemiShade, dist) * step(dist, _Radius) * val * NormalizeValue(dist, _Radius- _SemiShade, (_Radius - _FullShade));
            //    return fixed4(val * _Color.r, val * _Color.g, val * _Color.b, 1.0);
            //}

            

 
            ENDCG
        }
    }
FallBack"Diffuse"
}