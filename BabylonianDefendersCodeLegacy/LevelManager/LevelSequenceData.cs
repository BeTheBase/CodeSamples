using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSequenceData : MonoBehaviour
{
    public int CurrentLevel;
    public List<InitData> InitDatas;

    public void Do()
    {
        if (InitDatas == null) return;
        foreach (InitData initData in InitDatas)
        {
            foreach (GameObject obj in initData.ObjsToActivate)
            {
                if (obj != null)
                    StartCoroutine(ActivateAfterTime(obj, initData.ActivationTime));
            }
        }
    }

    private IEnumerator ActivateAfterTime(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);
        obj.SetActive(true);
    }
}

[System.Serializable]
public class InitData
{
    public GameObject[] ObjsToActivate;
    public float ActivationTime;

}