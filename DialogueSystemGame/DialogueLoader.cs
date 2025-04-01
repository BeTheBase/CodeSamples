using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DialogueLoader : MonoBehaviour
{
    public static DialogueLoader Instance;
    private Dictionary<string, DialogueEntryData> dialogues = new Dictionary<string, DialogueEntryData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadDialogueFiles();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadDialogueFiles()
    {
        string path = Application.dataPath + "/Dialogues/";
        if (!Directory.Exists(path)) return;

        foreach (string file in Directory.GetFiles(path, "*.json"))
        {
            string json = File.ReadAllText(file);
            List<DialogueEntryData> loadedDialogues = JsonConvert.DeserializeObject<List<DialogueEntryData>>(json);

            foreach (var entry in loadedDialogues)
            {
                dialogues[entry.dialogueID] = entry;
            }
        }

        Debug.Log($"Loaded {dialogues.Count} dialogues.");
    }

    public DialogueEntryData GetDialogue(string id)
    {
        return dialogues.TryGetValue(id, out var entry) ? entry : null;
    }
}
