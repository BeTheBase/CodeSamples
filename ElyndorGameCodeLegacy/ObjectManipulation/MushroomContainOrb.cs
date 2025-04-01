using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomContainOrb : MonoBehaviour
{
    public Transform OrbSuperPosition;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HarvestableObjects")
        {
            WorldEvents.InvokeBioluminescentOrbsCollected(1);
            print(other.gameObject);
            other.gameObject.transform.position = OrbSuperPosition.position;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
