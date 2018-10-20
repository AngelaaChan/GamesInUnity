Shader "Custom/BumpMapping" 
{
    Properties {
        _backgorund ("Texture", 2D) = "white" {}
        _normalMap ("Normal Map", 2D) = "bump" {}
        _height ("Bump Amount", Range(0,10)) = 1
    }
    SubShader {

      CGPROGRAM

        #pragma surface surf Lambert
        
        sampler2D _normalMap;
        sampler2D _backgorund;
        half _height;
        

        struct Input {
            float2 _backgorund;
            float2 uv_normalMap;
        };
        
        void surf (Input IN, inout SurfaceOutput o) {
            // set background color
            o.Albedo = tex2D(_backgorund, IN._backgorund).rgb;
            // set the normal depending on the the normalMap
            o.Normal = UnpackNormal(tex2D(_normalMap, IN.uv_normalMap));
            // increase the protrusion
            o.Normal *= float3(_height,_height,1);
        }

      ENDCG
    }
    Fallback "Diffuse"
  }
