using System;
using Unity.VisualScripting;
using UnityEngine;

public static class WorldEvents
{
    public static event Action OnWoodenHomeDoorOpened;

    public static event Action<int> OnBioluminescentOrbsCollected;

    public static void InvokeBioluminescentOrbsCollected(int count)
    {
        Debug.Log($"BioluminescentOrbs Collected ");
        OnBioluminescentOrbsCollected?.Invoke(count);
    }

    public static void InvokeWoodenHomeDoorOpened()
    {
        Debug.Log($"Wooden home door opened!");
        OnWoodenHomeDoorOpened?.Invoke();
    }
}
