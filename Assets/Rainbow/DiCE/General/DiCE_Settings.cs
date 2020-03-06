using System.Collections.Generic;
using Rainbow.SplineHandling;
using UnityEngine;

namespace Rainbow.DiCE {
    public static class DiCE_Settings {
        private static string[] _CSVs = { "1", "2", "3", "4", "5"};
        private static Dictionary<string, Texture2D> _LUTs = new Dictionary<string, Texture2D>();
        private static bool _lutLoaded = false;

        private static readonly int lutId = Shader.PropertyToID("_LUT"), showId = Shader.PropertyToID("_DiceShowAmount"), 
            strengthId = Shader.PropertyToID("_DiceStrength"), eyesId = Shader.PropertyToID("_SwapEyes");


        /// <summary>
        /// Fully enables the DiCE effect. If no LUT has been loaded, loads the default one
        /// </summary>
        public static void Enable() {
            if(!_lutLoaded)
                SetLookupTexture(0);

            SetShowAmount(1);
            SetStrengthAmount(1);
        }

        /// <summary>
        /// Fully disables the DiCE effect.
        /// </summary>
        public static void Disable() {
            SetShowAmount(0);
            SetStrengthAmount(0);
        }

        /// <summary>
        /// Sets how much the effect is visible.
        /// </summary>
        /// <param name="value">0 for disabled, 1 for enabled, interpolate in-between to slide effect in or out</param>
        public static void SetShowAmount(float value) {
            Shader.SetGlobalFloat(showId, value);
        }

        /// <summary>
        /// Sets how strong the effect is.
        /// </summary>
        /// <param name="value">0 for disabled, 1 for enabled, interpolate in-between to fade effect in or out</param>
        public static void SetStrengthAmount(float value) {
            Shader.SetGlobalFloat(strengthId, value);
        }

        private static bool _eyesSwapped = false;
        /// <summary>
        /// Swaps the effect between eyes.
        /// </summary>
        public static void SwapEyes() {
            _eyesSwapped = !_eyesSwapped;
            Shader.SetGlobalInt(eyesId, _eyesSwapped ? 1 : 0);
        }

        /// <summary>
        /// Changes the LUT loaded. Currently there are 5 levels of DiCE strength
        /// </summary>
        /// <param name="index">Strength of dice: 0 - weakest (default), 4 - strongest</param>
        public static void SetLookupTexture(int index) {
            if (index >= _CSVs.Length) {
                Debug.LogError("Trying to set lookup texture that doesn't exist! Temporarily using mod of index.");
                index %= _CSVs.Length;
            }

            string csv = _CSVs[index];
            if (!_LUTs.ContainsKey(csv)) {
                _LUTs.Add(csv, LoadLUT(csv));
            }

            Shader.SetGlobalTexture(lutId, _LUTs[csv]);
            _lutLoaded = true;
        }

        private static Texture2D LoadLUT(string name) {
            SplineDescription spline = SplineReader.GenSplineDescription(name);
            float[] lutLeft = spline.LeftEyeLUT, lutRight = spline.RightEyeLUT;
            if (lutLeft.Length != lutRight.Length) {
                Debug.LogError("LUT table provided is invalid: LUT lengths for each eye are different!");
                return null;
            }
            // If TextureFormat is RFloat then very slow performance on Quest (I guess floats are not supported natively?), so this setting is actually very important!
            // Hopefully this helps someone avoid such a pitfall in the future, I spent a week just debugging this problem.
            Texture2D lutTexture = new Texture2D(lutLeft.Length, 2, TextureFormat.RHalf, false, true); // Left eye y = 0, right y = 1
            for (int i = 0; i < lutLeft.Length; i++) {
                lutTexture.SetPixel(i, 0, new Color(lutLeft[i], 0, 0));
                lutTexture.SetPixel(i, 1, new Color(lutRight[i], 0, 0));
            }
            lutTexture.wrapMode = TextureWrapMode.Clamp;
            lutTexture.Apply(false, true);
            return lutTexture;
        }
    }
}
