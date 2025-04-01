using System.Collections.Generic;

namespace DialogueSystem.Core
{
    public interface IDialogueUI
    {
        void ShowSubtitle(string text);
        void ShowChoices(List<DialogueChoiceData> choices, DialogueID current);
        void HideChoices();
    }
}