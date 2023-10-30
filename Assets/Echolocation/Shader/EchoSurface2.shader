Shader "Unlit/EchoSurface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FadingFactor ("Fading Factor", Range(0, 1)) = 1
        _distFade ("Distance fading", Range(0, 1)) = 1

        _Center ("Echo Origin", vector) = (0, 0, 0)
        _Radius ("Radius", float) = 0
        _ShadeSize ("Shade Size", float) = 0




    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float3 worldPos : TEXCOORD1;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;


            float _FadingFactor;

            float _distFade;

            float3 _Center;
            float _Radius;
            float _ShadeSize;



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); 

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dist = distance(_Center, i.worldPos);

    
                if (dist <= _Radius)
                {
                _distFade = 0;
                }
                else if (dist >= _Radius + _ShadeSize)
                {
                _distFade = 1;
                }
                else if (_Radius > 0 )
                {
                _distFade = (dist - _Radius) / _ShadeSize;
                }
    
        fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = lerp(col.rgb, float3(0, 0, 0), _FadingFactor * _distFade);
                col.a = 1.0;
                return col;
            }
            ENDCG
        }
    }
}
