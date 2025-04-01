using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    Rigidbody2D Body2D;

    float firePower = 0;
    float damage = 50;

    private void OnEnable()
    {
        Body2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * firePower*Time.deltaTime);
    }

    public void MoveProjectile(float power, Vector3 dir, float lifeTime)
    {
        firePower = power;
       // Body2D.AddForce(power * dir);
        StartCoroutine(DeactivateAfterTime(lifeTime));
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        this.gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.name);
        if(collision.GetComponent<IHealth>()!= null)
        {
           collision.GetComponent<IHealth>().TakeDamage(damage);

        }
    }

}
