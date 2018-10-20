// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Based on https://en.wikibooks.org/wiki/Cg_Programming/Unity/Cutaways

Shader "Custom/Displacement" {
    Properties {
      // _CutOff is defined for each different call to shader
        _CutOff ("Threshold CutOff", Range(-3,2))  = 0
        _Color ("Color", Color) = (1,1,1,1) 
       }
   SubShader {
    Pass {
         Cull Off 
         // so I can see the inside
 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 

         float _CutOff;
         uniform float4 _Color; 
 
         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posInObjectCoords : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.pos =  UnityObjectToClipPos(input.vertex);
            output.posInObjectCoords = input.vertex; 
 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
           // discard from top to bottom
            if (input.posInObjectCoords.x < _CutOff) 
            {
               discard; 
            }
            return _Color ; 
         }
 
         ENDCG  
      }
   }
}