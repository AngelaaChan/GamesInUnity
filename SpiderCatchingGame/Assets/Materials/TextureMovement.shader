Shader "Custom/TextureMovement" 
{
	Properties 
	{
        _intensity ("Brightness", Range(0,10)) = 1
        _texture ("Texture", 2D) = "white" {}
    }
    SubShader 
	{
		CGPROGRAM	
		// use unity Lambertian shader for just diffused effect
		#pragma surface surf Lambert

		half _intensity;
		sampler2D _texture;

		struct Input 
		{
			// caculate the objects normal in world orientation
			float2 uv_texture;
		};
	
		// use unity Lambertian shader
		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = (tex2D(_texture, IN.uv_texture)).rgb * _intensity;
		}
	
		ENDCG
    }
    Fallback "Diffuse"
  }
