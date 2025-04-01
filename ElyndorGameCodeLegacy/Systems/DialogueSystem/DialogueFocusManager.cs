using UnityEngine;

public class DialogueFocusManager : MonoBehaviour
{
    [Header("Player Movement Reference")]
    [Tooltip("Assign the component that handles player movement (it will be disabled during dialogue).")]
    [SerializeField] private MonoBehaviour playerMovementComponent;

    private void OnEnable()
    {
        // Subscribe to dialogue events.
        DialogueEvents.OnChoicesDisplay += OnDialogueChoicesDisplayed;
        DialogueEvents.OnChoicesHidden += OnDialogueChoicesHidden;
    }

    private void OnDisable()
    {
        // Unsubscribe from events.
        DialogueEvents.OnChoicesDisplay -= OnDialogueChoicesDisplayed;
        DialogueEvents.OnChoicesHidden -= OnDialogueChoicesHidden;
    }

    /// <summary>
    /// Disables player movement and unlocks the cursor when dialogue choices appear.
    /// </summary>
    private void OnDialogueChoicesDisplayed(DialogueChoiceData[] choices, DialogueID dialogueID)
    {
        if (playerMovementComponent != null)
            playerMovementComponent.enabled = false;

        // Unlock and show the cursor for UI interaction.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Re-enables player movement and locks the cursor when dialogue choices are hidden.
    /// </summary>
    private void OnDialogueChoicesHidden()
    {
        if (playerMovementComponent != null)
            playerMovementComponent.enabled = true;

        // Optionally, lock and hide the cursor if that's your normal gameplay state.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
