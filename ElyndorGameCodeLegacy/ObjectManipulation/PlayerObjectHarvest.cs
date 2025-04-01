using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectHarvest : MonoBehaviour
{
    public GameObject TargetObjectToHarvest;
    public Transform ActiveObjectPosition;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "HarvestableObject")
        {
            TargetObjectToHarvest = other.gameObject;
            HarvestObjectAndContain();
        }
    }

    public void HarvestObjectAndContain()
    {
        TargetObjectToHarvest.transform.SetParent(null);

        TargetObjectToHarvest.transform.position = ActiveObjectPosition.position;
        

    }
}
