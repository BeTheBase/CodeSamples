using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Narrator/Dialogue Entry")]
public class DialogueEntry : ScriptableObject
{
    public DialogueID dialogueID;
    public string dialogueText;
    public AudioClip audioClip;

    [Header("Conditional Branching")]
    public ConditionType conditionType;
    public DialogueID trueBranch;
    public DialogueID falseBranch;

    [Header("Dialogue Choices")]
    public bool hasChoices;
    public DialogueChoice[] choices;
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueID nextDialogue;
}


public enum ConditionType
{
    None,
    HasItem,
    SolvedPuzzle,
    HasItemAndSolvedPuzzle, // NEW: Multiple conditions
    CustomCondition // NEW: Custom scriptable conditions
}
