using DialogueSystem.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DialogueSystem.ConsoleApp
{
    public class ConsoleDialogueUI : IDialogueUI
    {
        public void ShowSubtitle(string text)
        {
            Console.WriteLine($"> {text}");
        }

        public void ShowChoices(List<DialogueChoiceData> choices, DialogueID current)
        {
            Console.WriteLine("Choices:");
            for (int i = 0; i < choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {choices[i].choiceText}");
            }

            Console.Write("Choose (1 - N): ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int selected) && selected > 0 && selected <= choices.Count)
            {
                Console.WriteLine($"You chose: {choices[selected - 1].choiceText}");
                var next = choices[selected - 1].nextDialogue;

                var provider = InfraStartup.Configure();
                var dialogueService = provider.GetRequiredService<IDialogueService>();
                dialogueService.Play(next);
            }
        }

        public void HideChoices()
        {
            // No-op for console
        }
    }
}