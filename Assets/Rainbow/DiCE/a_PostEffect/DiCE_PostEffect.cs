using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rainbow.DiCE {
    /// <summary>
    /// Attach this to the main VR camera
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class DiCE_PostEffect : MonoBehaviour {
        private static DiCE_PostEffect _effect;
        private Material _diceMat;

        private static void CheckExists() {
            if (_effect == null) {
                Debug.LogError("Could not find script in Scene. You must add this script to the main Camera! \nAdding temporarily for now.");
                Camera.main.gameObject.AddComponent<DiCE_PostEffect>().Initialise();
            }
        }

        /// <summary>
        /// Enables DiCE as a post effect. This is generally slower than using the fragment shader version of the effect, especially on mobile VR.
        /// </summary>
        public static void EnableDiCE() {
            CheckExists();
            _effect.enabled = true;
        }
        /// <summary>
        /// Disables DiCE as a post effect. 
        /// </summary>
        public static void DisableDiCE() {
            CheckExists();
            _effect.enabled = false;
        }

        /// <summary>
        /// Checks if _effect has been set and so there is an active post effect in the scene
        /// </summary>
        /// <returns>True if there is an active post effect in the scene</returns>
        public static bool UsingPostEffect() {
            return _effect != null;
        }


        private void Awake() {
            if (_initialised || !enabled)
                return;

            Initialise();
        }

        private bool _initialised = false;
        private void Initialise() {
            _effect = this;
            _diceMat = new Material(Shader.Find("PostEffect/DiCE_Slow"));
            _initialised = true;
        }

        private void OnEnable() {
            DiCE_Settings.Enable();
        }

        private void OnDisable() {
            DiCE_Settings.Disable();
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest) {
            Graphics.Blit(src, dest, _diceMat);
        }
    }
}
