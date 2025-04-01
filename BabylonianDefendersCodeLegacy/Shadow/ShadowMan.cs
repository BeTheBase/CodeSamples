using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMan : MonoBehaviour
{
    public List<Transform> Places;

    private void Start()
    {
        this.transform.position = Places[Random.Range(0, Places.Count-1)].position;

        InvokeRepeating("SwitchPlaces", 1f, 3f);
    }

    private void SwitchPlaces()
    {
        this.transform.position = Places[Random.Range(0, Places.Count - 1)].position;
    }
}
