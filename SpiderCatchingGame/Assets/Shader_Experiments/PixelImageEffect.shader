// following https://www.youtube.com/watch?v=9bTFVaKGIIQ guide

Shader "Hidden/PixelImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Columns ("Sprite Height", Float) = 50
		_Rows ("Sprite Width", Float) = 100
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

			float _Columns;
			float _Rows;
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv;
				// scale up and down and use round to loss information
				uv.x = round(uv.x * _Columns) / _Columns;
				uv.y = round(uv.y * _Rows) / _Rows;
				fixed4 col = tex2D(_MainTex, uv);
				return col;
			}
			ENDCG
		}
	}
}
