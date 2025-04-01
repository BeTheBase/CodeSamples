using System;
using UnityEngine;

public static class DialogueEvents
{
    public static event Action<DialogueID> OnDialogueStart;
    public static event Action<DialogueChoiceData[], DialogueID> OnChoicesDisplay;
    public static event Action OnChoicesHidden;
    public static event Action<DialogueChoiceData, DialogueID> OnChoiceSelected; 
    public static event Action<string> OnSubtitleDisplay; // New subtitle event
    public static event Action<string, object> OnVariableChanged;

    public static void InvokeDialogueStart(DialogueID dialogueID)
    {
        Debug.Log($"🗣 Dialogue started: {dialogueID}");
        OnDialogueStart?.Invoke(dialogueID);
    }

    public static void InvokeChoicesDisplay(DialogueChoiceData[] choices, DialogueID dialogueID)
    {
        Debug.Log("📜 Choices displayed.");
        OnChoicesDisplay?.Invoke(choices, dialogueID);
    }
    public static void InvokeChoicesHidden()
    {
        Debug.Log("🧹 Choices hidden.");
        OnChoicesHidden?.Invoke();
    }

    public static void InvokeChoiceSelected(DialogueChoiceData choice, DialogueID dialogueID)
    {
        Debug.Log($"🎭 Choice selected: {choice.choiceText}");
        OnChoiceSelected?.Invoke(choice, dialogueID);
    }

    public static void InvokeVariableChanged(string key, object value)
    {
        Debug.Log($"{key} {value}");
        OnVariableChanged?.Invoke(key, value);
    }

    public static void InvokeSubtitleDisplay(string subtitleText)
    {
        Debug.Log($"📝 Subtitle: {subtitleText}");
        OnSubtitleDisplay?.Invoke(subtitleText);
    }
}
