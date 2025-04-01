using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawner : EnemySpawner
{
    private void Start()
    {
        InitSequence();
    }

    public void InitSequence()
    {
        foreach (EnemySpawnData data in SpawnPointDatas)
        {
            ShadowMan shadowMan = ObjectPool.Instance.SpawnFromPool<ShadowMan>(data.PublicSpawnPoint.position, data.PublicSpawnPoint.rotation) as ShadowMan;

        }
    }
}
