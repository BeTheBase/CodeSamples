using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericPooler
{
    MonoBehaviour GetObjectFromPool<T>() where T : MonoBehaviour;
    MonoBehaviour SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T : MonoBehaviour;
}
