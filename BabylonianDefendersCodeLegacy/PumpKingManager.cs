using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PumpKingLaserData
{
    public List<Transform> Spawns;
    public float Width = 10f;
    public float Damage = 10f;
}


public class PumpKingManager : MonoBehaviour
{
    public List<PumpKingLaserData> PumpKingLaserSpawns;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(7f);
        StartCoroutine(InitLaserPhase(6f));
    }

    public IEnumerator InitLaserPhase(float time)
    {
        yield return new WaitForEndOfFrame();
        foreach (PumpKingLaserData pld in PumpKingLaserSpawns)
        {
            foreach (Transform spawn in pld.Spawns)
            {
                PumpKingLaser orb = ObjectPool.Instance.SpawnFromPool<PumpKingLaser>(spawn.position, spawn.rotation) as PumpKingLaser;

                orb.InitLaser(pld.Width);
            }
        }

        yield return new WaitForSeconds(time);
        StartCoroutine(InitLaserPhase(5f));

    }
}
