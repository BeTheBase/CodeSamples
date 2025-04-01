using DialogueSystem.Core;
using DialogueSystem.Infra;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Xunit;

namespace DialogueSystem.Tests
{
    public class JsonDialogueRepositoryTests
    {
        [Fact]
        public void Repository_Should_Load_Dialogues_From_Json()
        {
            var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            var dialogue = new DialogueEntryData
            {
                dialogueID = DialogueID.Intro,
                dialogueText = "Hello!",
                hasChoices = false,
                nextDialogue = DialogueID.None
            };

            var json = JsonSerializer.Serialize(new List<DialogueEntryData> { dialogue });
            File.WriteAllText(Path.Combine(tempDir, "intro.json"), json);

            var repo = new JsonDialogueRepository(tempDir);
            var loaded = repo.Get(DialogueID.Intro);

            Assert.NotNull(loaded);
            Assert.Equal("Hello!", loaded.dialogueText);

            Directory.Delete(tempDir, true);
        }
    }
}