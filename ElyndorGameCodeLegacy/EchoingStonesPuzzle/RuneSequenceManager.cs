using UnityEngine;
using System.Collections.Generic;

public class RuneSequenceManager : MonoBehaviour
{
    [SerializeField] private AncientRune[] runes;
    [SerializeField] private List<int> correctSequence; // Define in inspector
    [SerializeField] private GameObject shardOfResonance;
    private List<int> playerSequence = new List<int>();
    private bool puzzleSolved = false;

    private void Start()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Initialize(i, OnRuneTouched, GetComponent<AudioSource>()??null);
        }
    }

    private void OnRuneTouched(AncientRune rune)
    {
        if (puzzleSolved) return;

        playerSequence.Add(rune.RuneIndex);

        if (playerSequence.Count == correctSequence.Count)
        {
            if (CheckSequence())
            {
                SolvePuzzle();
            }
            else
            {
                ResetSequence();
            }
        }
    }

    private bool CheckSequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    private void SolvePuzzle()
    {
        puzzleSolved = true;
        Debug.Log("✅ Correct sequence entered! Unlocking Shard of Resonance...");
        // Trigger Shard of Resonance
        // Add narrator glitch effect
    }

    private void ResetSequence()
    {
        Debug.Log("❌ Incorrect sequence. Try again!");
        playerSequence.Clear();
    }
}
