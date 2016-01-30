Shader "Hidden/Darken" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Brightness("Substraction Amount", Range(-0.5, 0.5)) = 0
		_Contrast("Multiplication Amount", Range(0, 2)) = 0
	}
		SubShader
		{
			Pass
			{
				CGPROGRAM
				#pragma	vertex vert_img
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform float _Brightness;
				uniform float _Contrast;

				float4 frag(v2f_img i) : COLOR
				{
					float4 c = tex2D(_MainTex, i.uv);

					
					

					// Apply contrast.
					c.rgb = ((c.rgb - 0.5f) * max(_Contrast, 0)) + 0.5f;

					// Apply brightness.
					c.rgb += _Brightness;

					// Return final pixel color.
					c.rgb *= c.a;
					return c;
				}
				ENDCG
			}
		}
}