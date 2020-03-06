#ifndef DICE_INCLUDED
#define DICE_INCLUDED

static const float dynamicRange = 2.7;

sampler2D _LUT;
float4 _LUT_TexelSize;
float _DiceShowAmount, _DiceStrength;
int _SwapEyes;

float3 ApplyCurve(float3 RGBlin, int eye) {
	float  Lin = (0.2126 * RGBlin.x + 0.7152 * RGBlin.y + 0.0722 * RGBlin.y);
	float LogLin = log(Lin) / log(10.0);

	//Apply Tonemapping curve from LUT here
	float INDEX = (LogLin + dynamicRange) * (_LUT_TexelSize.z / dynamicRange);
	float2 lookupPos = float2(INDEX / _LUT_TexelSize.z, eye);
	float LogLout = tex2D(_LUT, lookupPos).x;

	//Tonemapping curve ends
	float Lout = pow(10, LogLout);
	float3 RGBlout = (RGBlin / Lin) * Lout;

	// This shouldn't be the case after tonemapping, but when used in fragment shader can run into problems with this so clamp value
	if (LogLin < -dynamicRange)
		return float3(0,0,0);
	else
		return RGBlout;
}

float3 SmoothMix(float3 c1, float3 c2, float startX, float transition, float x) {
	return lerp(c1, c2, smoothstep(startX, startX + transition, x));
}

half3 GetDiceCol(half3 col, half xPos) {
	if (_DiceShowAmount < 0.001 || _DiceStrength < 0.001) {
		return col;
	}

	// Requires UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i); to be run before calling this!
	half3 tonemapped = ApplyCurve(col.xyz, unity_StereoEyeIndex != _SwapEyes);

	float intensity = xPos + (1 - _DiceShowAmount);
	intensity = unity_StereoEyeIndex ? intensity : 1 - intensity;
#ifndef DICE_POST_EFFECT
	intensity = intensity * 2 - 1.0;
#endif
	float3 final = SmoothMix(tonemapped, col.xyz, 0.6, 0.15, intensity);

	final = lerp(col.rgb, final, _DiceStrength);
	return final;
}

#endif // DICE_INCLUDED
