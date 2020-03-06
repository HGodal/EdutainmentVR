using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rainbow.DiCE {
    /// <summary>
    /// Example DiCE controller that can be placed anywhere in the scene
    /// </summary>
    public class DiCE_Manager : MonoBehaviour {
        public bool _usingPostEffect = false, _diceEnabled = true;
        private int _diceLevel = 0;
        private float _diceStrength = 1;

        private void Start() {
            Initialise();
        }

        private void Initialise() {
            _usingPostEffect = DiCE_PostEffect.UsingPostEffect();

#if UNITY_ANDROID
            if (_usingPostEffect)
                Debug.LogWarning("Post effects on mobile can have a significant performance hit, due to tiled rendering! Consider using the fragment shader version of DiCE.");
#endif
        }

        /// <summary>
        /// Use T to toggle DiCE, E to swap eyes, Up and Down to change DiCE levels, R and F to change effect strength.
        /// </summary>
        private void Update() {
            if (Input.GetKeyDown(KeyCode.T)) {
                Debug.Log("Test");
                _diceEnabled = !_diceEnabled;
                if (_usingPostEffect) {
                    if(_diceEnabled)
                        DiCE_PostEffect.EnableDiCE();
                    else
                        DiCE_PostEffect.DisableDiCE();
                } else {
                    if (_diceEnabled) {
                        DiCE_Settings.Enable();
                        _diceStrength = 1;
                    } else {
                        DiCE_Settings.Disable();
                        _diceStrength = 0;
                    }
                }
            }


            if (Input.GetKeyDown(KeyCode.E)) {
                DiCE_Settings.SwapEyes();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _diceLevel++;
                if (_diceLevel > 4) _diceLevel = 0;
                DiCE_Settings.SetLookupTexture(_diceLevel);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _diceLevel--;
                if (_diceLevel < 0) _diceLevel = 4;
                DiCE_Settings.SetLookupTexture(_diceLevel);
            }

            if (Input.GetKey(KeyCode.R)) {
                _diceStrength = Mathf.Min(1, _diceStrength + Time.deltaTime * 0.5f);
                DiCE_Settings.SetStrengthAmount(_diceStrength);
            }

            if (Input.GetKey(KeyCode.F)) {
                _diceStrength = Mathf.Max(0, _diceStrength - Time.deltaTime * 0.5f);
                DiCE_Settings.SetStrengthAmount(_diceStrength);
            }
        }

        private void OnEnable() {
            DiCE_Settings.Enable();
        }

        private void OnDisable() {
            DiCE_Settings.Disable();
        }
    }
}
