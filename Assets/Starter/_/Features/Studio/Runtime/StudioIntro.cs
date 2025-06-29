using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Studio.Runtime
{
    public class StudioIntro : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 1f;
        public float stayDuration = 2f;
        public string nextSceneName = "MainMenu"; // ou ta scène suivante

        private void Start()
        {
            _inputReader.Initialize();
            _inputReader.EnableMenuMap();
            _inputReader.AnyKeyEvent += OnAnyKey;
            StartCoroutine(FadeSequence());
        }

        private IEnumerator FadeSequence()
        {
            // Start transparent
            canvasGroup.alpha = 0f;

            // Fade in
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = t / fadeDuration;
                yield return null;
            }
            canvasGroup.alpha = 1f;

            // Stay
            yield return new WaitForSeconds(stayDuration);

            // Fade out
            t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = 1f - (t / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = 0f;
            // Ensuite tu peux charger une scène ou activer ton menu principal
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }

        private void OnAnyKey()
        {
            //StopCoroutine(FadeSequence());
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
        [SerializeField] private InputReader _inputReader;
    }
}
