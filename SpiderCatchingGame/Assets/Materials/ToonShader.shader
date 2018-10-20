// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Based on https://en.wikibooks.org/wiki/Cg_Programming/Unity/Toon_Shading


// A toon shader that discreetizes phong illumination model
// also adds an outline
Shader "Custom/ToonShader" {
    // we don't use any colors or any texture
    // this is because texture gives too much detail 
    // sadly our texture conficts with Non-photorealistic effect
   Properties {
      _Color ("Diffuse Color", Color) = (1,1,1,1) 
      _UnlitColor ("Unlit Diffuse Color", Color) = (0.5,0.5,0.5,1) 
      _OutlineColor ("Outline Color", Color) = (0,0,0,1)
      _LitOutlineThickness ("Lit Outline Thickness", Range(0,1)) = 0.1
      _UnlitOutlineThickness ("Unlit Outline Thickness", Range(0,1))  = 0.4
      _SpecColor ("Specular Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 10
        _CutOff ("Threshold CutOff", Range(0,1))  = 0.8

   }
   SubShader {
      // Cel Shading
      Pass {      
         // get _LightColor0 and UNITY_LIGHTMODEL_AMBIENT light
         Tags { "LightMode" = "ForwardBase" } 

        Blend SrcAlpha OneMinusSrcAlpha 

 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 

         // get unity defined structures
         #include "UnityCG.cginc"
        // color of light source (from "Lighting.cginc")
         uniform float4 _LightColor0; 

 
         // User-specified properties
         uniform float4 _Color; 
         uniform float4 _UnlitColor;
         uniform float _DiffuseThreshold;
         uniform float4 _OutlineColor;
        uniform float4 _SpecColor;
         uniform float _LitOutlineThickness;
         uniform float _UnlitOutlineThickness;
         uniform float _Shininess;
        uniform float _CutOff;

        struct vertexShaderInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };

         struct vertexShaderOutput {
            float4 viewPosition : POSITION;
            float4 worldPosition : TEXCOORD0;
            float3 normalDirection : TEXCOORD1;
         };

         // calculate everything reletive to world position world 
         vertexShaderOutput vert(vertexShaderInput input) 
         {
            vertexShaderOutput output;

            // position of the vertex in world position
            output.worldPosition = mul (unity_ObjectToWorld, input.vertex);
            // position of the normal in world position
            output.normalDirection = mul (
                transpose ((float3x3) unity_WorldToObject), input.normal);
            output.normalDirection = normalize(output.normalDirection);
            // position of the vertex in projection position
            output.viewPosition = UnityObjectToClipPos(input.vertex);
 
            return output;
         }

        float4 frag(vertexShaderOutput input) : SV_Target
        {   

            float3 viewDirection = normalize(
                _WorldSpaceCameraPos - input.worldPosition.xyz);

            // check if the light source is point or directional
            float3 lightDirection;
            float attenuation;
            // directional light no attenuation 
            if (0.0 == _WorldSpaceLightPos0.w) 
            {
                attenuation = 1.0; 
                // directional light is independent on object's position
                lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            } 
            // point light has linear attenuation
            else 
            {
                float3 vertexToLightSource = 
                    _WorldSpaceLightPos0.xyz - input.worldPosition.xyz;
                attenuation = 1.0 / length(vertexToLightSource); 
                // point light is dependent on object's position
                lightDirection = normalize(vertexToLightSource);
            }

            // Discreetize the normal and reflection
            // cell shading
            float LdotN_Continous = dot (lightDirection, input.normalDirection);
            float LdotN;
            float3 reflection = normalize(
                (2.0 * LdotN_Continous * input.normalDirection) - lightDirection);
            if (LdotN_Continous > _CutOff)
                LdotN = _CutOff;
            else if (LdotN_Continous > 0.5)
                LdotN = 0.5;
            else if (LdotN_Continous > 0.25)
                LdotN = 0.25;
            else
                LdotN = 0.05;
            float RdotV = dot(reflection, viewDirection);
            if (RdotV > 0.95)
                RdotV = 0.95;
            else if (RdotV > 0.5)
                RdotV = 0.5;
            else if (RdotV > 0.25)
                RdotV = 0.25;
            else
                RdotV = 0.05;

            
            // Phong Shading
            // ambiant
            float3 ambinant = _UnlitColor * UNITY_LIGHTMODEL_AMBIENT;
            // diffusion
            float3 diffusion = attenuation * _LightColor0 * _Color * max(0, LdotN);
            // specular
            float4 returnColor = float4 (0.0f, 0.0f, 0.0f, 1.0f);
            if (_Shininess == 0) {
                returnColor.rgb = ambinant + diffusion; 
            } else {
                float3 specular = attenuation * _SpecColor * _LightColor0 * pow(max(0, RdotV), _Shininess);
                // Combine Phong illumination model components
                returnColor.rgb = ambinant + diffusion + specular;
            }

             // overlay with outline
             // use Sihouette Enhancement
            // find the normal that is considered shadow
            float shadowNormal = lerp(_UnlitOutlineThickness, _LitOutlineThickness, 
                max(0.0, LdotN_Continous));
            // if viewing angle is less than the shadow normal angle
            // give them shadow
            if (dot(viewDirection, input.normalDirection) < shadowNormal)
            {
               returnColor.rgb = _LightColor0.rgb * _OutlineColor.rgb; 
            }

            return returnColor;
        }
        ENDCG
    }
}
}