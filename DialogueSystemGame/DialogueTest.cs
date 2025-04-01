using System.Collections;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public GameObject testCanvas; // Assign in Inspector

    private void Start()
    {
        Debug.Log("🧪 Starting Dialogue System Test...");
        StartCoroutine(RunDialogueTest());
    }

    private IEnumerator RunDialogueTest()
    {
        Debug.Log("📜 Initializing test dialogue...");

        DialogueChoiceData[] testChoices = new DialogueChoiceData[]
        {
            new DialogueChoiceData { choiceText = "Enter the cave", nextDialogue = "CaveEntry" },
            new DialogueChoiceData { choiceText = "Walk away", nextDialogue = "ForestPath" }
        };

        DialogueID testDialogueID = DialogueID.Narrator_Welcome_02_Epic;

        // Ensure UI is active
        if (testCanvas != null)
        {
            testCanvas.SetActive(true);
            Debug.Log("✅ Test canvas enabled.");
        }
        else
        {
            Debug.LogError("❌ Test canvas is missing! Assign it in the inspector.");
        }

        yield return new WaitForSeconds(1f);

        Debug.Log("🚀 Triggering dialogue start event...");
        DialogueEvents.InvokeDialogueStart(testDialogueID);

        yield return new WaitForSeconds(1f);

        Debug.Log("🎭 Displaying choices...");
        DialogueEvents.InvokeChoicesDisplay(testChoices, testDialogueID);

        yield return new WaitForSeconds(3f); // Observe UI

        // Simulate player choosing first option
        Debug.Log($"🖱 Player selects choice 0: {testChoices[0].choiceText}");
        DialogueEvents.InvokeChoiceSelected(testChoices[0], testDialogueID);

        yield return new WaitForSeconds(2f);

        Debug.Log("🧹 Hiding choices...");
        DialogueEvents.InvokeChoicesHidden();

        yield return new WaitForSeconds(1f);

        Debug.Log("✅ Dialogue System Test Complete.");
    }
}
