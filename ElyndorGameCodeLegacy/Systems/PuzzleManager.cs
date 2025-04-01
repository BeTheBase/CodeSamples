using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int TotalArtifacts = 3;
    private int collectedArtifacts = 0;

    private void Start()
    {
        collectedArtifacts = 0;

        EventManager.StartListening("Collect Artifact", CollectArtifact);
    }

    public void CollectArtifact()
    {
        collectedArtifacts++;
        Debug.Log($"Collected Artifacts: {collectedArtifacts}/{TotalArtifacts}");

        // Check if all artifacts are collected
        if (collectedArtifacts >= TotalArtifacts)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle Completed! All artifacts collected.");
        EventManager.TriggerEvent("CollectArtifactCompleted");
        EventManager.StopListening("Collect Artifact", CollectArtifact );
    }
}
