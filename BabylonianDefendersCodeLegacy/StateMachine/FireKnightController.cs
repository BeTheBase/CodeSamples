using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKnightController : MonoBehaviour
{
    public Transform PlayerReference;

    public Vector3 AttackRange1, AttackRange2;

    public float AttackRadius1, AttackRadius2;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackRange1, AttackRadius1);
        Gizmos.DrawWireSphere(AttackRange2, AttackRadius2);
    }

}