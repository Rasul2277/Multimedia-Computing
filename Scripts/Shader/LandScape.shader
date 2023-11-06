Shader "Unlit/LandScape"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

             struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                float4 col = tex2Dlod(_MainTex, float4(o.uv, 0.0f, 0.0f));
                o.vertex.y -= (float) col.b * 256;
                
    if (col.r > 0.95)
    {
        o.vertex.y -= col.b * 64;
    }
    else if (col.r > 0.9)
    {
        o.vertex.y -= col.b * 24;
    }
    else if (col.r > 0.8)
    {
        o.vertex.y -= col.b * 16;
    }
    

                return o;
}

            fixed4 frag (v2f i) : SV_Target
            {
                
                
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
    if (col.r > 1 - 0.0625 * 1)
    {
        col.r = (float) 256 / 256 ;
        col.g = (float) 256 / 256 ;
        col.b = (float) 256 / 256 ;
    }
    else if (col.r > 1 - 0.0625 * 2)
    {
        col.r = (float) 222 / 256;
        col.g = (float) 223 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 3)
    {
        col.r = (float) 184 / 256;
        col.g = (float) 188 / 256;
        col.b = (float) 214 / 256;
    }
    else if (col.r > 1 - 0.0625 * 4)
    {
        col.r = (float) 0 / 256;
        col.g = (float) 186 / 256;
        col.b = (float) 34 / 256;
    }
    else if (col.r > 1 - 0.0625 * 5)
    {
        col.r = (float) 0 / 256;
        col.g = (float) 206 / 256;
        col.b = (float) 37 / 256;
    }
    else if (col.r > 1 - 0.0625 * 6)
    {
        col.r = (float) 131 / 256;
        col.g = (float) 204 / 256;
        col.b = (float) 89 / 256;
    }
    else if (col.r > 1 - 0.0625 * 7)
    {
        col.r = (float) 175 / 256;
        col.g = (float) 204 / 256;
        col.b = (float) 81 / 256;
    }
    else if (col.r > 1 - 0.0625 * 8)
    {
        col.r = (float) 229 / 256;
        col.g = (float) 223 / 256;
        col.b = (float) 162 / 256;
    }
    else if (col.r > 1 - 0.0625 * 9)
    {
        col.r = (float) 172 / 256;
        col.g = (float) 199 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 10)
    {
        col.r = (float) 137 / 256;
        col.g = (float) 171 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 11)
    {
        col.r = (float) 110 / 256;
        col.g = (float) 143 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 12)
    {
        col.r = (float) 78 / 256;
        col.g = (float) 110 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 13)
    {
        col.r = (float) 48 / 256;
        col.g = (float) 66 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 14)
    {
        col.r = (float) 2 / 256;
        col.g = (float) 6 / 256;
        col.b = (float) 229 / 256;
    }
    else if (col.r > 1 - 0.0625 * 15)
    {
        col.r = (float) 1 / 256;
        col.g = (float) 4 / 256;
        col.b = (float) 178 / 256;
    }
    else if (col.r <= 1 - 0.0625 * 15)
    {
        col.r = (float) 1 / 256;
        col.g = (float) 3 / 256;
        col.b = (float) 119 / 256;
    }
    
    
    
                // apply fog
        UNITY_APPLY_FOG(i.fogCoord, col);
                
                return col;
            }
            ENDCG
        }
    }
}
