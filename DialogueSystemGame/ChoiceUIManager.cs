using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoiceUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _choiceButtonPrefab;
    [SerializeField] private Transform _choiceContainer;
    private GenericObjectPool<ChoiceButton> _buttonPool; 
    private readonly List<ChoiceButton> _activeButtons = new List<ChoiceButton>();
    private int currentSelection = 0; 

    private void Awake()
    {
        _buttonPool = new GenericObjectPool<ChoiceButton>(_choiceButtonPrefab.GetComponent<ChoiceButton>(), 500, _choiceContainer);
        HideChoices();
    }

    private void OnEnable()
    {
        DialogueEvents.OnChoicesDisplay += ShowChoices;
        DialogueEvents.OnChoicesHidden += HideChoices;
    }

    private void OnDestroy()
    {
        DialogueEvents.OnChoicesDisplay -= ShowChoices;
        DialogueEvents.OnChoicesHidden -= HideChoices;
    }


    public void ShowChoices(DialogueChoiceData[] choices, DialogueID currentDialogueID)
    {
        HideChoices(); 
        StartCoroutine(ShowChoicesCoroutine(choices, currentDialogueID));
    }

    private IEnumerator ShowChoicesCoroutine(DialogueChoiceData[] choices, DialogueID currentDialogueID)
    {
        // Wait one frame to ensure HideChoices fully finishes.
        yield return null;
        _choiceContainer.gameObject.SetActive(true);

        for (int i = 0; i < choices.Length; i++)
        {
            DialogueChoiceData choice = choices[i];
            ChoiceButton button = _buttonPool.GetObject();
            Debug.Log("Getting button from pool: " + _buttonPool + " with object: " + button);
            // Ensure the button is reparented
            button.transform.SetParent(_choiceContainer, false);
            button.gameObject.SetActive(true);
            Debug.Log($"Button for '{choice.choiceText}' active? {button.gameObject.activeSelf} - Parent active? {button.transform.parent.gameObject.activeSelf}");
            button.SetText(choice.choiceText);
            button.Configure(() => OnChoiceSelected(choice, currentDialogueID));
            _activeButtons.Add(button);
        }
        StartCoroutine(ResetSelection());
    }

    private IEnumerator ResetSelection()
    {
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
        yield return null;
    }

    private void OnChoiceSelected(DialogueChoiceData choice, DialogueID currentDialogueID)
    {
        DialogueEvents.InvokeChoiceSelected(choice, currentDialogueID);
        HideChoices();
    }

    public void HideChoices()
    {
        foreach (ChoiceButton button in _activeButtons)
        {
            _buttonPool.ReturnObject(button);
        }
        _activeButtons.Clear();
        _choiceContainer.gameObject.SetActive(false);
    }
}
