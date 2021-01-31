using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class UiManager : MonoBehaviour
    {
        public Image fadeImage;

        public Color lightColor = new Color(1, 1, 1, 0);
        public Color darkColor = new Color(0, 0, 0, 0);
        public float fadeTime = 2f;

        private void Awake()
        {
            if (fadeImage == null) throw new ArgumentException("Image Not Set");

            GameManager.Instance.OnTransition += FadeOut;
            GameManager.Instance.AfterTransition += FadeIn;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnTransition -= FadeOut;
            GameManager.Instance.AfterTransition -= FadeIn;
        }

        public void FadeOut(bool isLight)
        {
            Debug.Log($"Fade out. IsLight:{isLight}");

            // https://stackoverflow.com/questions/42330509/crossfadealpha-not-working
            var color = (isLight) ? lightColor : darkColor;
            color.a = 1;

            fadeImage.color = color;
            fadeImage.CrossFadeAlpha(0f, 0f, true);
            fadeImage.CrossFadeAlpha(1, fadeTime, false);
        }

        public void FadeIn(bool isLight)
        {
            Debug.Log($"Fade in. IsLight:{isLight}");

            // https://stackoverflow.com/questions/42330509/crossfadealpha-not-working
            var color = (isLight) ? lightColor : darkColor;
            color.a = 0;

            fadeImage.color = color;
            fadeImage.CrossFadeAlpha(0f, 0f, true);
            fadeImage.CrossFadeAlpha(1, fadeTime, false);
        }
    }
}