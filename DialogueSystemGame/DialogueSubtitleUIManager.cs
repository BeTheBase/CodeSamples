using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueSubtitleUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text subtitleText; // Assign your TMP_Text component here.

    [Header("Animation Settings")]
    [Tooltip("Duration of the fade in/out animations.")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Tooltip("Delay between each character when typing out the text.")]
    [SerializeField] private float letterDelay = 0.05f;

    [Tooltip("Time to wait after the text has been fully displayed before fading out.")]
    [SerializeField] private float autoClearDelay = 2.0f;

    private CanvasGroup canvasGroup;
    private Coroutine currentSubtitleCoroutine;

    private void Awake()
    {
        // Ensure we have a CanvasGroup to control fade.
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        DialogueEvents.OnSubtitleDisplay += ShowSubtitle;
    }

    private void OnDisable()
    {
        DialogueEvents.OnSubtitleDisplay -= ShowSubtitle;
    }

    /// <summary>
    /// Public entry point to show a subtitle with advanced effects.
    /// </summary>
    /// <param name="text">The full subtitle text.</param>
    private void ShowSubtitle(string text)
    {
        // If a subtitle is already running, stop it to avoid overlapping.
        if (currentSubtitleCoroutine != null)
        {
            StopCoroutine(currentSubtitleCoroutine);
        }
        currentSubtitleCoroutine = StartCoroutine(DisplaySubtitleRoutine(text));
    }

    /// <summary>
    /// Coroutine that handles the full display lifecycle:
    /// fade in, type letter-by-letter, wait, and then fade out.
    /// </summary>
    /// <param name="fullText">The full text to display.</param>
    private IEnumerator DisplaySubtitleRoutine(string fullText)
    {
        // Ensure the text is initially empty and fully transparent.
        subtitleText.text = "";
        canvasGroup.alpha = 0f;

        // Fade in.
        yield return StartCoroutine(FadeCanvasGroupAlpha(0f, 1f, fadeDuration));

        // Letter-by-letter display.
        for (int i = 1; i <= fullText.Length; i++)
        { 
            subtitleText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(letterDelay);
        }

        // Wait for a given time after the text is fully displayed.
        yield return new WaitForSeconds(autoClearDelay);

        // Fade out.
        yield return StartCoroutine(FadeCanvasGroupAlpha(1f, 0f, fadeDuration));

        // Clear text after fading out.
        subtitleText.text = "";
        currentSubtitleCoroutine = null;
    }

    /// <summary>
    /// Fades the canvas group alpha from a start value to an end value over a specified duration.
    /// </summary>
    /// <param name="startAlpha">Starting alpha value.</param>
    /// <param name="endAlpha">Ending alpha value.</param>
    /// <param name="duration">Duration of the fade.</param>
    private IEnumerator FadeCanvasGroupAlpha(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        canvasGroup.alpha = startAlpha;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
