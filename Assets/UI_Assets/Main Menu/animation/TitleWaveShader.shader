Shader "UI/WaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Float) = 2.0
        _WaveStrength ("Wave Strength", Float) = 0.02
        _Frequency ("Wave Frequency", Float) = 10.0
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        Cull Off Lighting Off ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "WaveEffect"
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _WaveSpeed;
            float _WaveStrength;
            float _Frequency;

            v2f vert (appdata_t v)
            {
                v2f o;

                // Apply the wave effect to both X and Y coordinates
                float waveX = sin(_Time.y * _WaveSpeed + v.uv.y * _Frequency) * _WaveStrength;
                float waveY = cos(_Time.y * _WaveSpeed * 0.8 + v.uv.x * _Frequency) * _WaveStrength * 0.5;

                // Distort the UVs based on the calculated wave
                v.uv.x += waveX;
                v.uv.y += waveY;
                
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
