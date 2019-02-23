Shader "Custom/EarthSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_WaveMaskTex("Albedo_mask (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_SecondTex("Albedo2nd (RGB)", 2D) = "white" {}
		_Range("Range", Range(0,1)) = 0.3
		_Center("Center", Vector) = (0.0,0.0,0.0,0.0)

    }
    SubShader
    {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        LOD 200

		Pass{
		  ZWrite ON
		  ColorMask 0
		}
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:fade
		#pragma target 3.0


		sampler2D _MainTex;
		sampler2D _WaveMaskTex;

		sampler2D _SecondTex;
		float _Range;
		float3 _Center;


		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos; 
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			// 地球と波のレンダリング

			float xuv = -0.01, yuv = -0.01;
			float anglex = 0.0;
			float angley = 0.0;
			fixed2 uv = IN.uv_MainTex;

			fixed4 cm = tex2D(_WaveMaskTex, uv) * _Color;
			if (dot(cm.rgb, fixed3(1, 1, 1)) < 0.01) {
				anglex = 80 * _Time + uv.x * 800;
				angley = 80 * _Time + uv.y * 800;

				xuv = (sin(anglex) + cos(angley*0.8)) * 0.005;
				uv.x += xuv;
				yuv = (sin(angley) + cos(anglex * 1.2)) * 0.005;
				uv.y += yuv;
			}



			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, uv) * _Color;
			o.Albedo = c.rgb * (1.0 + (0.02 + xuv + yuv) * 50.0);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			if (dot(c.rgb, fixed3(0, 0, 1)) > 0.4) {
				o.Alpha = c.a * 1;
			}


			// セカンドテクスチャとの合成

			if (length(_Center -IN.worldPos) < _Range) {
				o.Albedo = fixed3(1.0, 1.0, 1.0); 
			}
		}
		ENDCG


    }
    FallBack "Diffuse"
}
