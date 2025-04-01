using DialogueSystem.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DialogueSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = InfraStartup.Configure();
            var dialogueService = provider.GetRequiredService<IDialogueService>();

            dialogueService.Play(DialogueID.Intro);
        }
    }
}