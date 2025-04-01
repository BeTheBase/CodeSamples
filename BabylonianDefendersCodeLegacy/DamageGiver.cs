using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    public string TagName = "Player";

    public float DamageAmount = 10f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<IHealth>() != null && collision.gameObject.tag == TagName)
        {
            collision.GetComponent<IHealth>().TakeDamage(DamageAmount);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IHealth>() != null && collision.gameObject.tag == TagName)
        {
            collision.GetComponent<IHealth>().TakeDamage(DamageAmount);
        }
    }
}
