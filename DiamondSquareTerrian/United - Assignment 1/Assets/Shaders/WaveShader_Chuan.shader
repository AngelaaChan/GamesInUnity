//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/WaveShader"
{
 Properties
 {
 // for unity interface
     _MainTex ("Texture", 2D) = "white" {}
 }
 SubShader
 {
     Pass
     {
         Cull Off

         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag

         #include "UnityCG.cginc"

         uniform sampler2D _MainTex; 

         struct vertIn
         {
             float4 vertex : POSITION;
             float2 uv : TEXCOORD0;
         };

         struct vertOut
         {
             float4 vertex : SV_POSITION;
             float2 uv : TEXCOORD0;
         };

         // Implementation of the vertex shader
         vertOut vert(vertIn v)
         {
             vertOut o;
             o.vertex = v.vertex;

             // // Displace the original vertex in model space
             static const float PI = 3.14159265f;
             float waveSpeed = 2*PI*_Time.y - PI*sin(_Time.y);
             float height = 0.5f;
             float dy = height*sin(o.vertex.x+waveSpeed);
             float4 displacement = float4(0.0f, dy, 0.0f, 0.0f);
             o.vertex += displacement;

             o.vertex = mul(UNITY_MATRIX_MVP, o.vertex);
             o.uv = v.uv;

             return o;
         }
         
         // Implementation of the fragment shader
         fixed4 frag(vertOut v) : SV_Target
         {
             fixed4 col = tex2D(_MainTex, v.uv);
             return col;
         }
         ENDCG
     }
 }
}