using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubertController : MonoBehaviour
{
    public GameObject RubertUpper;
    public GameObject RubertDowner;

    private void Start()
    {
        StartCoroutine(LoopRubertUpDown(3));
    }

    private IEnumerator LoopRubertUpDown(float time)
    {
        float t = 0;
        Vector3 rubertUpp = RubertUpper.transform.position;
        Vector3 rubertDown = RubertDowner.transform.position;
        while(true)
        {
            t += Time.deltaTime;
            RubertUpper.transform.position = Vector3.Lerp(rubertUpp + Vector3.up, rubertUpp -Vector3.up, Mathf.PingPong(t, 1));
            RubertDowner.transform.position = Vector3.Lerp(rubertDown - Vector3.up, rubertDown + Vector3.up, Mathf.PingPong(t, 1));
            yield return new WaitForEndOfFrame();
        }
    }
}
