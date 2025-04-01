using System;
using System.Collections.Generic;

[Serializable]
public class DialogueEntryData
{
    public string dialogueID;
    public string dialogueText;
    public string audioClipName;
    public bool hasChoices;
    public List<DialogueChoiceData> choices = new List<DialogueChoiceData>();
    public DialogueID nextDialogue; // If no choices exist, automatically transition.

    public List<DialogueCondition> conditions = new List<DialogueCondition>(); // NEW: Conditions required to play this dialogue.
    public List<DialogueVariableUpdate> updates = new List<DialogueVariableUpdate>(); // NEW: Updates when dialogue is played.
}

[Serializable]
public class DialogueChoiceData
{
    public string choiceText;
    public string nextDialogue;
    public DialogueID conditionID = DialogueID.None; // Legacy condition (deprecated but kept for compatibility).

    public List<DialogueCondition> conditions = new List<DialogueCondition>(); // NEW: Conditions for this choice.
    public List<DialogueVariableUpdate> updates = new List<DialogueVariableUpdate>(); // NEW: Updates when choice is made.

    // Conversion property for type safety:
    public DialogueID nextDialogueID
    {
        get
        {
            if (Enum.TryParse(nextDialogue, true, out DialogueID id))
                return id;
            return DialogueID.None;
        }
    }
}

[Serializable]
public class DialogueCondition
{
    public string variable; // The variable name to check.
    public string operatorType; // Comparison: ==, !=, >, <, >=, <=.
    public object value; // The expected value for the condition to be met.
}

[Serializable]
public class DialogueVariableUpdate
{
    public string variable; // The variable name to update.
    public object value; // The new value OR increment.
}
