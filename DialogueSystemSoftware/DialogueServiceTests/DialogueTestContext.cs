using DialogueSystem.Application;
using DialogueSystem.Core;
using Moq;
using System.Collections.Generic;

namespace DialogueSystem.Tests
{
    public class DialogueTestContext
    {
        public Mock<IDialogueRepository> RepoMock { get; } = new();
        public Mock<IPlayerState> StateMock { get; } = new();
        public Mock<IDialogueUI> UiMock { get; } = new();

        public DialogueService CreateService(DialogueEntryData dialogue)
        {
            RepoMock.Setup(r => r.Get(dialogue.dialogueID)).Returns(dialogue);
            StateMock.Setup(s => s.Check(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>())).Returns(true);

            return new DialogueSystem.Application.DialogueService(
                RepoMock.Object,
                StateMock.Object,
                UiMock.Object,
                new List<IConditionEvaluator>());
        }
    }
}