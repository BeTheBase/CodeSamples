using System.Collections;
using TMPro;
using UnityEngine;

public class IntroNarrativeManager : MonoBehaviour
{
    public TextMeshProUGUI NarrativeTextDialog; // The text display for the narrative

    public NarrativeData narrativeData; // Reference to the ScriptableObject holding texts

    public int NarrativeWaitTime = 3;

    private int currentTextIndex = 0; // Track the current text index

    private void Start()
    {
        InitText();
    }

    public void BeginNarrative()
    {
        UpdateNarrative();
    }

    // Initialize the first narrative text or fallback message
    public void InitText()
    {
        if (narrativeData != null && narrativeData.narrativeTexts.Count > 0)
        {
            NarrativeTextDialog.text = narrativeData.narrativeTexts[0];
        }
        else
        {
            NarrativeTextDialog.text = "No narrative text available!";
        }
    }

    // Updates the narrative text every 'waitTime' seconds
    public void UpdateNarrative()
    {
  
        if (narrativeData != null && currentTextIndex < narrativeData.narrativeTexts.Count)
        {
            StartCoroutine(WaitUpdateNarrativeText(NarrativeWaitTime));
        }
    }

    // Coroutine to wait and update the narrative text
    private IEnumerator WaitUpdateNarrativeText(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (currentTextIndex == narrativeData.narrativeTexts.Count)
        {
            EventManager.TriggerEvent("IntroSceneDone");
        }
        if (currentTextIndex < narrativeData.narrativeTexts.Count)
        {
            NarrativeTextDialog.text = narrativeData.narrativeTexts[currentTextIndex];
            currentTextIndex++; // Move to the next text
            StartCoroutine(WaitUpdateNarrativeText(waitTime));
        }
    }

    // Reset the narrative index
    public void ResetNarrative()
    {
        currentTextIndex = 0;
        InitText();
    }
}
