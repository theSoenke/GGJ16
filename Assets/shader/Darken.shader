Shader "Hidden/Darken" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_subAmount("Substraction Amount", Range(-1, 1)) = 0
		_mulAmount("Multiplication Amount", Range(1, 2)) = 1
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
				uniform float _subAmount;
				uniform float _mulAmount;

				float4 frag(v2f_img i) : COLOR
				{
					float4 c = tex2D(_MainTex, i.uv);

					
					

					float4 result;
					result.r = c.r * _mulAmount - (_subAmount + _mulAmount - 1);
					result.g = c.g * _mulAmount - (_subAmount + _mulAmount - 1);
					result.b = c.b * _mulAmount - (_subAmount + _mulAmount - 1);
					return result;
				}
				ENDCG
			}
		}
}