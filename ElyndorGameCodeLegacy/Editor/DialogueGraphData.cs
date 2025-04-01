using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueGraphData
{
    public List<DialogueNodeData> nodes = new List<DialogueNodeData>();
}

[Serializable]
public class DialogueNodeData
{
    public string GUID;
    public string dialogueText;
    public float posX;
    public float posY;
    public bool isEntryPoint;
    public List<DialogueGraphChoiceData> choices = new(); // Store choices
}

[Serializable]
public class DialogueGraphChoiceData
{
    public string choiceText;
    public string nextDialogue; // Stores GUID of linked node
}
