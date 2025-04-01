using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleCollisions : MonoBehaviour
{
    StickmanBehaviourBase parentBaseClass;

    private void Start()
    {
        parentBaseClass = GetComponentInParent<StickmanBehaviourBase>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Ground")
        {
           parentBaseClass.BroadCastDelegate(true);
        }
    }
}
