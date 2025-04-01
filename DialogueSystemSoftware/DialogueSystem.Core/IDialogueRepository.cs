namespace DialogueSystem.Core
{
    public interface IDialogueRepository
    {
        DialogueEntryData Get(DialogueID id);
    }
}