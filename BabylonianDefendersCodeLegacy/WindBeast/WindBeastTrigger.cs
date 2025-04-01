using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBeastTrigger : MonoBehaviour
{
    public GameObject ActivateWindBeastObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ActivateWindBeastObject.SetActive(true);
        }
    }
}

