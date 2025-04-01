using System.Collections.Generic;

namespace DialogueSystem.Core
{
    public class DialogueEntryData
    {
        public DialogueID dialogueID { get; set; }
        public string dialogueText { get; set; }
        public string audioClipName { get; set; }
        public bool hasChoices { get; set; }
        public DialogueID nextDialogue { get; set; }
        public List<DialogueChoiceData> choices { get; set; } = new();
        public List<DialogueCondition> conditions { get; set; } = new();
        public List<DialogueVariableUpdate> updates { get; set; } = new();
    }

    public class DialogueChoiceData
    {
        public string choiceText { get; set; }
        public DialogueID nextDialogue { get; set; }
        public List<DialogueCondition> conditions { get; set; } = new();
        public List<DialogueVariableUpdate> updates { get; set; } = new();
    }

    public class DialogueCondition
    {
        public string variable { get; set; }
        public string operatorType { get; set; }
        public object value { get; set; }
    }

    public class DialogueVariableUpdate
    {
        public string variable { get; set; }
        public object value { get; set; }
    }
}