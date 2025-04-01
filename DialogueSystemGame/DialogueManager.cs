using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour, IDialogueProvider
{
    #region Inspector Fields

    [Header("Audio & UI")]
    [SerializeField] private AudioSource narratorAudioSource;

    [Header("Preloaded Dialogue Data (Debug View)")]
    [SerializeField] private List<DialogueEntryData> preloadedDialogues = new List<DialogueEntryData>();
    [SerializeField] private List<AudioClip> loadedAudioClips = new List<AudioClip>();

    #endregion

    #region Private Fields

    private Dictionary<DialogueID, DialogueEntryData> dialogueLookup = new Dictionary<DialogueID, DialogueEntryData>();
    private Dictionary<DialogueID, AudioClip> audioClips = new Dictionary<DialogueID, AudioClip>();
    private DialogueMemory dialogueMemory;
    private PlayerStateManager playerState; // Reference to PlayerStateManager

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        dialogueMemory = DIContainer.Resolve<DialogueMemory>();
        playerState = DIContainer.Resolve<PlayerStateManager>(); 
        StartCoroutine(WaitForDialogueMemory());
    }
    private void OnEnable()
    {
        DialogueEvents.OnDialogueStart += HandleDialoguePlayRequested;
    }

    private void OnDisable()
    {
        DialogueEvents.OnDialogueStart -= HandleDialoguePlayRequested;
    }
    private IEnumerator WaitForDialogueMemory()
    {
        yield return new WaitUntil(() => dialogueMemory != null && dialogueMemory.Dialogues.Count > 0);
        PreloadDialogues();
    }

    #endregion

    #region Dialogue Preloading

    private void PreloadDialogues()
    {
        dialogueLookup.Clear();
        audioClips.Clear();
        preloadedDialogues.Clear();
        loadedAudioClips.Clear();

        foreach (var kvp in dialogueMemory.Dialogues)
        {
            DialogueID dialogueID = kvp.Key;
            DialogueEntryData dialogueData = kvp.Value;

            dialogueLookup[dialogueID] = dialogueData;
            preloadedDialogues.Add(dialogueData);

            if (!string.IsNullOrEmpty(dialogueData.audioClipName))
            {
                string clipName = System.IO.Path.GetFileNameWithoutExtension(dialogueData.audioClipName);
                AudioClip clip = Resources.Load<AudioClip>($"Audio/{clipName}");
                if (clip != null)
                {
                    audioClips[dialogueID] = clip;
                    loadedAudioClips.Add(clip);
                }
                else
                {
                    Debug.LogWarning($"Missing audio clip: {clipName} for dialogue {dialogueID}");
                }
            }
        }
    }

    #endregion

    #region Public Methods

    public void PlayDialogue(DialogueID dialogueID)
    {
        if (!dialogueLookup.TryGetValue(dialogueID, out DialogueEntryData entry))
        {
            DialogueEvents.InvokeChoicesHidden();
            Debug.LogWarning($"Dialogue ID {dialogueID} not found.");
            return;
        }
        // **STEP 1: CHECK CONDITIONS BEFORE PLAYING**
        if (entry.conditions != null && entry.conditions.Count > 0)
        {
            foreach (var condition in entry.conditions)
            {
                if (!playerState.CheckCondition(condition.variable, condition.operatorType, condition.value))
                {
                    Debug.Log($"Skipping {dialogueID} due to unmet condition: {condition.variable} {condition.operatorType} {condition.value}");
                    DialogueEvents.InvokeChoicesHidden();
                    return;
                }
            }
        }

        // **STEP 2: PLAY DIALOGUE & AUDIO**
        DialogueEvents.InvokeSubtitleDisplay(entry.dialogueText);

        if (audioClips.TryGetValue(dialogueID, out AudioClip clip))
        {
            if (narratorAudioSource != null)
            {
                narratorAudioSource.clip = clip;
                narratorAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("Narrator AudioSource is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip not found for dialogue {dialogueID}");
        }

        // **STEP 3: UPDATE PLAYER VARIABLES**
        if (entry.updates != null && entry.updates.Count > 0)
        {
            foreach (var update in entry.updates)
            {
                playerState.SetVariable(update.variable, update.value);

                
            }
        }

        // **STEP 4: HANDLE CHOICES**
        if (entry.hasChoices && entry.choices != null && entry.choices.Count > 0)
        {
            DialogueEvents.InvokeChoicesDisplay(entry.choices.ToArray(), dialogueID);
            return;
        }

        // **STEP 5: AUTO-PROCEED IF NO CHOICES EXIST**
        if (!entry.hasChoices && !string.IsNullOrEmpty(entry.nextDialogue.ToString()))
        {
            PlayDialogue(entry.nextDialogue);
        }
    }
    private void HandleDialoguePlayRequested(DialogueID dialogueID)
    {
        PlayDialogue(dialogueID);
    }

    #endregion
}
