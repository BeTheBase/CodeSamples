using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueID dialogueID;
    public UnityEvent<DialogueID> OnDialogueTriggered;

    private void Awake()
    {
        // Ensure PlayDialogue is invoked by default if nothing else is assigned
        if (OnDialogueTriggered == null || OnDialogueTriggered.GetPersistentEventCount() == 0)
        {
            OnDialogueTriggered = new UnityEvent<DialogueID>();
            OnDialogueTriggered.AddListener(id => DialogueEvents.InvokeDialogueStart(id));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            OnDialogueTriggered.Invoke(dialogueID);

            GameObject.Destroy(gameObject, 1f);
        }
    }
}