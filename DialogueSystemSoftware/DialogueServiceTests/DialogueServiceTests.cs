using DialogueSystem.Core;
using Moq;
using Xunit;

namespace DialogueSystem.Tests
{
    public class DialogueServiceTests
    {
        [Fact]
        public void Play_Should_ShowSubtitle_WhenConditionsMet()
        {
            var context = new DialogueTestContext();
            var dialogue = new DialogueEntryData
            {
                dialogueID = DialogueID.Intro,
                dialogueText = "Hello!",
                hasChoices = false,
                nextDialogue = DialogueID.None
            };

            var service = context.CreateService(dialogue);
            service.Play(DialogueID.Intro);

            context.UiMock.Verify(ui => ui.ShowSubtitle("Hello!"), Times.Once);
        }

        [Fact]
        public void Play_Should_ShowChoices_WhenChoicesExist()
        {
            var context = new DialogueTestContext();
            var dialogue = new DialogueEntryData
            {
                dialogueID = DialogueID.Intro,
                dialogueText = "Choose wisely.",
                hasChoices = true,
                choices = new()
                {
                    new DialogueChoiceData { choiceText = "Yes", nextDialogue = DialogueID.QuestAccepted },
                    new DialogueChoiceData { choiceText = "No", nextDialogue = DialogueID.QuestDeclined }
                }
            };

            var service = context.CreateService(dialogue);
            service.Play(DialogueID.Intro);

            context.UiMock.Verify(ui => ui.ShowChoices(It.IsAny<List<DialogueChoiceData>>(), DialogueID.Intro), Times.Once);
        }

        [Fact]
        public void Play_Should_SkipDialogue_WhenConditionFails()
        {
            var context = new DialogueTestContext();
            context.StateMock.Setup(s => s.Check(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>())).Returns(false);

            var dialogue = new DialogueEntryData
            {
                dialogueID = DialogueID.Intro,
                dialogueText = "Blocked.",
                conditions = new() { new DialogueCondition { variable = "hasKey", operatorType = "==", value = true } }
            };

            var service = context.CreateService(dialogue);
            service.Play(DialogueID.Intro);

            context.UiMock.Verify(ui => ui.ShowSubtitle(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Play_Should_ChainToNextDialogue_WhenDefined()
        {
            var context = new DialogueTestContext();
            var intro = new DialogueEntryData
            {
                dialogueID = DialogueID.Intro,
                dialogueText = "Start",
                hasChoices = false,
                nextDialogue = DialogueID.QuestStart
            };

            var next = new DialogueEntryData
            {
                dialogueID = DialogueID.QuestStart,
                dialogueText = "Next"
            };

            context.RepoMock.Setup(r => r.Get(DialogueID.Intro)).Returns(intro);
            context.RepoMock.Setup(r => r.Get(DialogueID.QuestStart)).Returns(next);
            context.StateMock.Setup(s => s.Check(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>())).Returns(true);

            var service = new DialogueSystem.Application.DialogueService(
                context.RepoMock.Object,
                context.StateMock.Object,
                context.UiMock.Object,
                new List<IConditionEvaluator>());

            service.Play(DialogueID.Intro);

            context.UiMock.Verify(ui => ui.ShowSubtitle("Start"), Times.Once);
            context.UiMock.Verify(ui => ui.ShowSubtitle("Next"), Times.Once);
        }
    }
}