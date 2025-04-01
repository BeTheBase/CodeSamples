using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanMovement : StickmanBehaviourBase
{

    private void Start()
    {
        OnGroundStatusChanged += GetGroundedStatus;
    }

    public void GetGroundedStatus(bool s)
    {

    }
}
