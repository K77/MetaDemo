//一个简单美颜Shader
//功能：磨皮、美白
Shader "Custom/BeautyFaceShader"
{
Properties
{
_MainTex ("Texture", 2D) = "white" {}
//模糊大小 (值越大越模糊)
_Radius ("Radius", Range(0, 10)) = 5
//调节距离空间权重,值越大权重越大
_SigmaS("SigmaS", float) = 3
//调节颜色空间权重,值越大权重越大
_SigmaR("SigmaR", float) = 1
//亮度值
_Brightness("Brightness", float) = 20
//对比度
_Contrast("Contrast", float) = 1.18
//饱和度
_Saturation("Saturation", float) = 1
}
SubShader
{
Tags { "RenderType"="Opaque" }
LOD 100
 
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
 
sampler2D _MainTex;
float4 _MainTex_ST;
half4 _MainTex_TexelSize;
float _Radius;
float _SigmaS;
float _SigmaR;
float _Brightness;
float _Contrast;
float _Saturation;
 
//计算像素亮度值
float Luminance(float3 color)
{
return dot(color, float3(0.2125, 0.7154, 0.0721));
}
 
//提亮暗部区域
float3 Brightness(float3 col)
{
//此公式: 使图像暗部变化强，亮部变化弱
//col.r = log(col.r * _Brightness);
//col.g = log(col.g * _Brightness);
//col.b = log(col.b * _Brightness);
 
//原理: https://blog.csdn.net/weixin_33716557/article/details/86197248?utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7Edefault-7.control&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7Edefault-7.control
//此公式: 使图像两端变化弱，中间变化强
col.r = log(col.r * (_Brightness - 1) + 1) / log(_Brightness);
col.g = log(col.g * (_Brightness - 1) + 1) / log(_Brightness);
col.b = log(col.b * (_Brightness - 1) + 1) / log(_Brightness);
return col;
}
//原理: https://blog.csdn.net/u013066730/article/details/87919412
//转载: https://www.jianshu.com/p/90feece27a04?utm_campaign=maleskine&utm_content=note&utm_medium=seo_notes&utm_source=recommendation
//双边滤波算法-保边去噪
float3 BilateralFilter(float2 uv)
{
float i = uv.x;
float j = uv.y;
float sigmaSSquareMult2 = (2 * _SigmaS * _SigmaS);
float sigmaRSquareMult2 = (2 * _SigmaR * _SigmaR);
 
float3 centerCol = tex2D(_MainTex, uv).rgb; // 中心点像素的颜色 //
float centerLum = Luminance(centerCol); // 中心点像素的亮度 //
 
float3 sum_up; // 分子 //
float3 sum_down; // 分母 //
for (int k = -_Radius; k <= _Radius; k++)
{
for (int l = -_Radius; l <= _Radius; l++)
{
float2 uv_new = uv + _MainTex_TexelSize.xy * float2(k, l);
float3 curCol = tex2D(_MainTex, uv_new).rgb; // 当前像素的颜色 //
float curLum = Luminance(curCol); // 当前像素的亮度 //
float3 deltaColor = curCol - centerCol;
float len = dot(deltaColor, deltaColor);
// float exponent = -((i-k)*(i-k)+(j-l)*(j-l))/sigmaSSquareMult2 - (curLum-centerLum)*(curLum-centerLum)/sigmaRSquareMult2;
float exponent = -((i - k) * (i - k) + (j - l) * (j - l)) / sigmaSSquareMult2 - len / sigmaRSquareMult2;
float weight = exp(exponent);
sum_up += curCol * weight;
sum_down += weight;
}
}
 
float3 rgb = sum_up / sum_down;
return rgb;
}
 
v2f vert(appdata v)
{
v2f o;
o.vertex = UnityObjectToClipPos(v.vertex);
o.uv = TRANSFORM_TEX(v.uv, _MainTex);
return o;
}
 
float4 frag(v2f i) : SV_Target
{
//保边去噪
float3 rgb = BilateralFilter(i.uv);
//该像素对应的亮度值
fixed luminance = Luminance(rgb);
//使用该亮度值创建一个饱和度为0的颜色
fixed3 luminanceColor = fixed3(luminance, luminance, luminance);
//创建一个对比度度为0的颜色
fixed3 avgColor = fixed3(0.5, 0.5, 0.5);
 
//调整饱和度
rgb = lerp(luminanceColor, rgb, _Saturation);
//调整对比度
rgb = lerp(avgColor, rgb, _Contrast);
//调整亮度
rgb = Brightness(rgb);
 
return fixed4(rgb, 1);
}
ENDCG
}
}
}