using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Babylonian;

public class SendPlayerPosition : PlayerBase
{
    public int Id = 400;
    private float lastSend;
    private bool isClient = false;
    private void Start()
    {
        StartCoroutine(LateToggle());

        if (ClientBehaviour.Instance != null) isClient = true;
    }

    private IEnumerator LateToggle()
    {
        yield return new WaitForSeconds(1f);
        //do something
    }

    private void Update()
    {
        //Send player data to syn player over server and client by tick
        if(Time.time - lastSend > 0.01f)
        {
            NetPlayerPosition ps = new NetPlayerPosition(Id, transform.position.x, transform.position.y, transform.position.z);
            if(ClientBehaviour.Instance != null) ClientBehaviour.Instance.SendToServer(ps);
            lastSend = Time.time;
        }

        //we need to update the current player his synchronized position on the other side

        //So, if we have initialized this player on this machine we are aloud to move it.
        //if not we can't move it so no control scripts addede

    }


}
