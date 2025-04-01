using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptablePoolObject : ScriptableObject
{
    [SerializeField] private MonoBehaviour prefab;
    public MonoBehaviour Prefab { get => prefab; set => prefab = value; }

    [SerializeField] private int size;
    public int Size { get => size; set => size = value; }
}
