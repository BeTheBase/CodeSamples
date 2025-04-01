using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public Transform PublicSpawnPoint;
    public float AliveTime = 3f;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public List<EnemySpawnData> SpawnPointDatas;


}
