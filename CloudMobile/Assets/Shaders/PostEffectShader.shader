Shader "Custom/PostEffectShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }
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

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target      //(v2f i = inputInformation)
            {

                fixed4 col = tex2D(_MainTex, i.uv + float2(0, sin(i.vertex.x/250 + _Time[1])/25)); // taking the MainTexture, i.uv = current coordinate of the pixel from this Texture
                                                    //+float2(0,10) = offset
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                //col.r = 1;
                
                
                return col;
            }
            ENDCG
        }
    }
}
