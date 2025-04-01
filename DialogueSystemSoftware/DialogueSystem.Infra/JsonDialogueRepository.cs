using DialogueSystem.Core;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DialogueSystem.Infra
{
    public class JsonDialogueRepository : IDialogueRepository
    {
        private readonly Dictionary<DialogueID, DialogueEntryData> _dialogues = new();

        public JsonDialogueRepository(string path)
        {
            LoadFromPath(path);
        }

        public DialogueEntryData Get(DialogueID id)
        {
            return _dialogues.TryGetValue(id, out var entry) ? entry : null;
        }

        private void LoadFromPath(string path)
        {
            if (!Directory.Exists(path)) return;

            foreach (var file in Directory.GetFiles(path, "*.json"))
            {
                var json = File.ReadAllText(file);
                var entries = JsonSerializer.Deserialize<List<DialogueEntryData>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                foreach (var entry in entries)
                {
                    _dialogues[entry.dialogueID] = entry;
                }
            }
        }
    }
}