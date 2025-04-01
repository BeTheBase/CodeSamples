// Unity Engine integratie

//using DialogueSystem.Core;
//using UnityEngine;
//using System.Collections.Generic;

//namespace DialogueSystem.UnityAdapter
//{
//    public class UnityDialogueUI : MonoBehaviour, IDialogueUI
//    {
//        public void ShowSubtitle(string text) => Debug.Log($"[Subtitle] {text}");
//        public void ShowChoices(List<DialogueChoiceData> choices, DialogueID current)
//        {
//            foreach (var choice in choices)
//                Debug.Log($"Choice: {choice.choiceText}");
//        }
//        public void HideChoices() => Debug.Log("Choices hidden");
//    }

//    public class UnityPlayerState : MonoBehaviour, IPlayerState
//    {
//        private readonly Dictionary<string, object> state = new();

//        public T Get<T>(string key) => (T)state[key];
//        public void Set<T>(string key, T value) => state[key] = value;
//        public bool Check(string key, string op, object value)
//        {
//            if (!state.ContainsKey(key)) return false;
//            var stored = state[key];
//            return new IntConditionEvaluator().Evaluate(stored, op, value);
//        }
//    }

//    public class JsonDialogueRepository : MonoBehaviour, IDialogueRepository
//    {
//        public DialogueEntryData Get(DialogueID id)
//        {
//            return new DialogueEntryData
//            {
//                dialogueID = id,
//                dialogueText = "Welcome!",
//                nextDialogue = DialogueID.QuestStart
//            };
//        }
//    }

//    public class DialogueBootstrap : MonoBehaviour
//    {
//        private void Start()
//        {
//            var repo = GetComponent<JsonDialogueRepository>();
//            var state = GetComponent<UnityPlayerState>();
//            var ui = GetComponent<UnityDialogueUI>();

//            var dialogueService = new DialogueService(
//                repo, state, ui,
//                new IConditionEvaluator[] { new IntConditionEvaluator() });

//            dialogueService.Play(DialogueID.Intro);
//        }
//    }
//}