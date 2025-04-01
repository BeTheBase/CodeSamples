using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchView : MonoBehaviour
{
    public bool Switch = false;
    public GameObject Map1, Map2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Switch = true;
            Map2.SetActive(true);

            Map1.SetActive(false);
        }
    }
}
