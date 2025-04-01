using DialogueSystem.Core;
using System.Collections.Generic;
using System.Linq;

namespace DialogueSystem.Application
{
    public class DialogueService : IDialogueService
    {
        private readonly IDialogueRepository _repository;
        private readonly IPlayerState _playerState;
        private readonly IDialogueUI _ui;
        private readonly IEnumerable<IConditionEvaluator> _evaluators;

        public DialogueService(
            IDialogueRepository repository,
            IPlayerState playerState,
            IDialogueUI ui,
            IEnumerable<IConditionEvaluator> evaluators)
        {
            _repository = repository;
            _playerState = playerState;
            _ui = ui;
            _evaluators = evaluators;
        }

        public void Play(DialogueID id)
        {
            var entry = _repository.Get(id);
            if (entry == null) return;

            if (!AreConditionsMet(entry.conditions)) return;

            _ui.ShowSubtitle(entry.dialogueText);

            foreach (var update in entry.updates)
                _playerState.Set(update.variable, update.value);

            if (entry.hasChoices && entry.choices.Any())
            {
                var validChoices = entry.choices
                    .Where(choice => AreConditionsMet(choice.conditions))
                    .ToList();

                _ui.ShowChoices(validChoices, id);
            }
            else if (entry.nextDialogue != DialogueID.None)
            {
                Play(entry.nextDialogue);
            }
        }

        private bool AreConditionsMet(IEnumerable<DialogueCondition> conditions)
        {
            if (conditions == null || !conditions.Any())
                return true;

            foreach (var condition in conditions)
            {
                if (!_playerState.Check(condition.variable, condition.operatorType, condition.value))
                    return false;
            }

            return true;
        }
    }
}