using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    private DialogueMemory _dialogueMemory;
    private PlayerStateManager _playerState;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        DialogueEvents.OnChoiceSelected += HandleChoiceSelected;
        DialogueEvents.OnVariableChanged += HandleVariableChanged;
    }

    private void OnDisable()
    {
        DialogueEvents.OnChoiceSelected -= HandleChoiceSelected;
        DialogueEvents.OnVariableChanged -= HandleVariableChanged;
    }

    private void Start()
    {
        _dialogueMemory = DIContainer.Resolve<DialogueMemory>();
        _playerState = DIContainer.Resolve<PlayerStateManager>();
    }

    /// <summary>
    /// Toont de keuzes voor de gegeven dialoog, nadat de keuzes gefilterd zijn op basis van spelercondities.
    /// </summary>
    /// <param name="dialogueID">De huidige dialoog-ID.</param>
    public void DisplayChoices(DialogueID dialogueID)
    {
        if (!_dialogueMemory.Dialogues.TryGetValue(dialogueID, out DialogueEntryData entry) ||
            !entry.hasChoices || entry.choices == null || entry.choices.Count == 0)
        {
            Debug.LogWarning($"⚠ Geen geldige keuzes voor {dialogueID}");
            DialogueEvents.InvokeChoicesHidden();
            return;
        }

        List<DialogueChoiceData> validChoices = new List<DialogueChoiceData>();

        foreach (var choice in entry.choices)
        {
            // Controleer of alle condities zijn voldaan.
            if (choice.conditions != null && choice.conditions.Count > 0)
            {
                bool conditionsMet = true;
                foreach (var condition in choice.conditions)
                {
                    if (!_playerState.CheckCondition(condition.variable, condition.operatorType, condition.value))
                    {
                        conditionsMet = false;
                        break;
                    }
                }
                if (!conditionsMet) continue;
            }
            validChoices.Add(choice);
        }

        if (validChoices.Count > 0)
        {
            Debug.Log($"✅ {validChoices.Count} geldige keuzes gevonden, tonen...");
            DialogueEvents.InvokeChoicesDisplay(validChoices.ToArray(), dialogueID);
        }
        else
        {
            Debug.LogWarning($"⚠ Geen keuzes voldeden aan de condities voor {dialogueID}");
            DialogueEvents.InvokeChoicesHidden();
        }
    }
    /// <summary>
    /// Fires when a variable changes
    /// </summary>
    private void HandleVariableChanged(string variableName, object value)
    {
        Debug.Log($"⚡ Variable '{variableName}' changed to {value}");

        if (variableName == "SilentMode" && value is bool silent)
        {
            HandleSilenceMode(silent);
        }
        else if (variableName == "riddleWindAnsweredCorrectly" && value is bool opened)
        {
            //OPEN DIE DEUR!
            WorldEvents.InvokeWoodenHomeDoorOpened();
        }
    }

    private void HandleFirstDoorOpened(bool opened)
    {

    }

    /// <summary>
    /// Handles logic when Silence Mode is activated or deactivated.
    /// </summary>
    private void HandleSilenceMode(bool isSilent)
    {
        /*HACK:: finding mainvamera and dis/enabling the main AudioListener of the game ( REALLY SILENT )*/
        var mainCamera = FindObjectsByType<Camera>(FindObjectsSortMode.None).First(_ => _.CompareTag("MainCamera"));

        if (isSilent)
        {
            Debug.Log("🔇 Silence Mode Activated: Muting audio and disabling certain dialogues.");
            // Add logic to disable narrator or mute game sounds
            mainCamera.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            Debug.Log("🔊 Silence Mode Deactivated: Restoring audio.");
            mainCamera.GetComponent<AudioListener>().enabled = true;
        }
    }
    /// <summary>
    /// Event-handler voor de gekozen optie.
    /// Past de variabele-updates toe en start de volgende dialoog.
    /// </summary>
    /// <param name="chosenChoice">De volledige gegevens van de gekozen optie.</param>
    /// <param name="currentDialogueID">De huidige dialoog-ID.</param>
    private void HandleChoiceSelected(DialogueChoiceData chosenChoice, DialogueID currentDialogueID)
    {
        if (chosenChoice == null)
        {
            Debug.LogWarning("⚠ Ongeldige keuze ontvangen.");
            return;
        }

        // Pas variabele-updates toe.
        if (chosenChoice.updates != null && chosenChoice.updates.Count > 0)
        {
            foreach (var update in chosenChoice.updates)
            {
                _playerState.SetVariable(update.variable, update.value);
                Debug.Log($"Variable {update.variable} updated to: {_playerState.GetVariable(update.variable)}");
            }
        }

        // Start de volgende dialoog.
        if (Enum.TryParse(chosenChoice.nextDialogue, true, out DialogueID nextDialogueID))
        {
            DialogueEvents.InvokeDialogueStart(nextDialogueID);
        }
        else
        {
            Debug.LogWarning($"⚠ Ongeldige volgende dialoog-ID: {chosenChoice.nextDialogue}");
        }
    }
}
