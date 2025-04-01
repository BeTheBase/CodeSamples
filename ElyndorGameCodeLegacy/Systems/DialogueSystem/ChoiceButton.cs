using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetText(string text)
    {
        if (_buttonText != null)
            _buttonText.text = text;
    }

    public void Configure(Action onClick)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => onClick.Invoke());
    }
}
