using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpKingLaser : MonoBehaviour
{
    public float width = 1.0f;
    public bool useCurve = true;
    private LineRenderer lr;

    void Start()
    {

        lr = GetComponent<LineRenderer>();
        InitLaser(width);

    }
    public void InitLaser(float _width)
    {
        width = _width;
        StartCoroutine(AddLaserLenghtInTime());
    }

    private IEnumerator AddLaserLenghtInTime()
    {
        float newY = 300;
        while(newY > 1)
        {
            newY-=0.5f;
            lr.SetPosition(0, new Vector3(lr.GetPosition(0).x, newY, lr.GetPosition(0).z));
            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForEndOfFrame();

        if (newY < 150)
            StartCoroutine(RemoveLaserLenghtInTime());
    }

    private IEnumerator RemoveLaserLenghtInTime()
    {
        float newY = 0;
        while(newY < 300)
        {
            newY+=0.5f;
            lr.SetPosition(0, new Vector3(lr.GetPosition(0).x, newY, lr.GetPosition(0).z));
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

        if (newY > 250)
            StartCoroutine(AddLaserLenghtInTime());

    }
}
