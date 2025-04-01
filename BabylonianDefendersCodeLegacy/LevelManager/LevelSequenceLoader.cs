using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelSequenceLoader : MonoBehaviour
{
    public UnityEvent OnLevelLoaded;
    public UnityEvent OnLevelDone;

    public float ActivationTime = 3f;

    public int LevelQuestCompletionNumber = 3;

    private void Start()
    {
        InitLevelSequenceStart();
    }

    private void InitLevelSequenceStart()
    {
        OnLevelLoaded.Invoke();
    }

    public void UpdateBossStatus()
    {
        Debug.Log(LevelQuestCompletionNumber);
        LevelQuestCompletionNumber -= 1;
        if (LevelQuestCompletionNumber < 1)
        {
            InitLevelCompleted();
        }
        
    }

    private void InitLevelCompleted()
    {
        OnLevelDone.Invoke();
    }

    public void ActivateAfterTime(GameObject objToActivate)
    {
        StartCoroutine(ActivateAfterTimeIE(objToActivate));
    }

    public void ActivateAfterTime(GameObject[] objsToActivate)
    {
        StartCoroutine(ActivateAfterTimeIE(objsToActivate));
    }

    private IEnumerator ActivateAfterTimeIE(GameObject obj)
    {
        yield return new WaitForSeconds(ActivationTime);
        obj.SetActive(true);
    }

    private IEnumerator ActivateAfterTimeIE(GameObject[] objs)
    {
        yield return new WaitForSeconds(ActivationTime);
        foreach(GameObject ob in objs)
        {
            ob.SetActive(true);
        }
    }

}
