using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // UI Image used for fading
    public float fadeDuration = 1f; // Time it takes to fade in or out

    // Fade out (black screen to transparent)
    public void FadeOut(float duration = -1)
    {
        StartCoroutine(Fade(255, 0, duration > 0 ? duration : fadeDuration));
    }

    // Fade in (transparent to black screen)
    public void FadeIn(float duration = -1)
    {
        StartCoroutine(Fade(0, 255, duration > 0 ? duration : fadeDuration));
    }

    // Fade from one alpha value to another over a duration, alpha in 0-255 range
    private IEnumerator Fade(byte startAlpha, byte endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color32 tempColor = fadeImage.color;

        // Set initial alpha directly using 0-255
        tempColor.a = startAlpha;
        fadeImage.color = tempColor;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Lerp alpha in the 0-255 range (as byte)
            byte newAlpha = (byte)Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            tempColor.a = newAlpha;
            fadeImage.color = tempColor;

            yield return null;
        }

        // Ensure the final alpha is set to avoid any precision issues
        tempColor.a = endAlpha;
        fadeImage.color = tempColor;
    }
}
