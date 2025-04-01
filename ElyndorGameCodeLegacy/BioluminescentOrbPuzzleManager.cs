using System;
using UnityEngine;

public class BioluminescentOrbPuzzleManager : MonoBehaviour 
{
    public int OrbsCollected = 0;
    public int OrbsTotal = 3;
    public GameObject PathBlocker;
    public event Action PuzzleCompleted = delegate { };

    public void OnEnable()
    {
        PathBlocker.SetActive(true);
        WorldEvents.OnBioluminescentOrbsCollected += CollectOrb;
    }

    public void OnDissable()
    {
        WorldEvents.OnBioluminescentOrbsCollected -= CollectOrb;
    }

    public void CollectOrb(int count)
    {
        OrbsCollected += count;
        if(OrbsCollected >= OrbsTotal)
        {
            //DONE!
            Debug.Log("BioluminescentOrbPuzzle Completed, Opening path");
            PuzzleCompleted.Invoke();
            PathBlocker.SetActive(false);
        }
    }
}
