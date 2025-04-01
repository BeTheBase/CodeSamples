using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Narrator/Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public List<DialogueEntry> dialogueEntries;

    public DialogueEntry GetDialogue(DialogueID id)
    {
        return dialogueEntries.Find(entry => entry.dialogueID == id);
    }
}
