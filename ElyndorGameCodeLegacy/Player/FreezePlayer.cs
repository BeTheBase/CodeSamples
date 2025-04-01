using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{
    public float TimeFreezed = 10f;
    private FirstPersonController myPlayerController;
    private float moveSpeedBeforeLock = 4f;
    private void OnEnable()
    {
        myPlayerController = GetComponent<FirstPersonController>();
    }

    private void Start()
    {
        moveSpeedBeforeLock = myPlayerController.MoveSpeed;
        ActiveLock();
        EventManager.StartListening("FreezePlayer", ActiveLock);
    }

    public void ActiveLock()
    {
        StartCoroutine(LockPlayerForTime(TimeFreezed));
    }

    private IEnumerator LockPlayerForTime(float time)
    {
        LockPlayer();

        yield return new WaitForSeconds(time);

        UnlockPlayer();
    }

    public void LockPlayer(bool zAxis = true)
    {
        myPlayerController.MoveSpeed = 0;
    }

    public void UnlockPlayer()
    {
        myPlayerController.MoveSpeed = moveSpeedBeforeLock;
    }

    private void OnDisable()
    {
        EventManager.StopListening("FreezePlayer", ActiveLock);
    }
}
