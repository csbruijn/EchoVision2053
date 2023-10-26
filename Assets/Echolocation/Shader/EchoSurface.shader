Shader"Custom/EchoSurface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FadingFactor ("Fading Factor", Range(0, 1)) = 1
    }
 
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 vertex : COLOR;
            };
 
            float _FadingFactor;
            sampler2D _MainTex;
 
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = lerp(col.rgb, float3(0, 0, 0), _FadingFactor);
                return col;
            }
            ENDCG
        }
    }
}