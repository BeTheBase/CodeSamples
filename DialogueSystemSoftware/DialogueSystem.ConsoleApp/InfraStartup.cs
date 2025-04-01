using DialogueSystem.Application;
using DialogueSystem.Core;
using DialogueSystem.Infra;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DialogueSystem.ConsoleApp
{
    public static class InfraStartup
    {
        public static ServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IDialogueRepository>(provider => new JsonDialogueRepository("Dialogues"));
            services.AddSingleton<IPlayerState, InMemoryPlayerState>();
            services.AddSingleton<IDialogueUI, ConsoleDialogueUI>();

            services.AddSingleton<IConditionEvaluator, IntConditionEvaluator>();
            services.AddSingleton<IEnumerable<IConditionEvaluator>>(sp => new[]
            {
                sp.GetRequiredService<IConditionEvaluator>()
            });

            services.AddSingleton<IDialogueService, DialogueService>();

            return services.BuildServiceProvider();
        }
    }
}