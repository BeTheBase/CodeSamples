using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DialogueMemory : MonoBehaviour
{
    private Dictionary<DialogueID, DialogueEntryData> dialogues = new Dictionary<DialogueID, DialogueEntryData>();
    private Dictionary<DialogueID, bool> memory = new Dictionary<DialogueID, bool>(); // Tracks past choices

    public IReadOnlyDictionary<DialogueID, DialogueEntryData> Dialogues => dialogues;
    public IReadOnlyDictionary<DialogueID, bool> Memory => memory;

    private void Awake()
    {
         DIContainer.Register<DialogueMemory>(this);
         LoadDialogueFiles();

    }

    private void LoadDialogueFiles()
    {
        string path = Application.dataPath + "/_Dialogues/";
        if (!Directory.Exists(path)) return;

        foreach (string file in Directory.GetFiles(path, "*.json"))
        {
            try
            {
                string json = File.ReadAllText(file);
                List<DialogueEntryData> loadedDialogues = JsonConvert.DeserializeObject<List<DialogueEntryData>>(json);

                foreach (var entry in loadedDialogues)
                {
                    if (System.Enum.TryParse(entry.dialogueID, out DialogueID dialogueEnum))
                    {
                        dialogues[dialogueEnum] = entry;
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid DialogueID: {entry.dialogueID} in file {file}");
                    }
                }
            }
            catch (JsonException e)
            {
                Debug.LogError($"Error parsing dialogue file {file}: {e.Message}");
            }
        }

        Debug.Log($"Loaded {dialogues.Count} dialogues.");
    }

    public bool GetMemory(DialogueID id) => memory.ContainsKey(id) && memory[id];

    public void SetMemory(DialogueID dialogueID, bool value)
    {
        memory[dialogueID] = value;
    }
}
