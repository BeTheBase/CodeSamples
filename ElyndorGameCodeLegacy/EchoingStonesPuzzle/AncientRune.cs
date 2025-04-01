using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class AncientRune : MonoBehaviour
{
    public event Action<AncientRune> OnRuneTouched; // Event for interaction

    [SerializeField] private AudioClip runeSound;
    private AudioSource _audioSource;

    public int RuneIndex { get; private set; }

    public void Initialize(int index, Action<AncientRune> callback, AudioSource audioSource)
    {
        _audioSource = audioSource;
        RuneIndex = index;
        OnRuneTouched += callback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnRuneTouched?.Invoke(this);
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (runeSound != null)
        {
            _audioSource.PlayOneShot(runeSound);
        }
    }
}
