using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountesOrb : MonoBehaviour
{
    public float DamageAmount = 5f;

    private float moveSpeed = 5f;

    Rigidbody2D rBody;
    Vector2 playerPosition;

    private void OnEnable()
    {
        rBody = GetComponent<Rigidbody2D>();

        StartCoroutine(Deactivate());
    }


    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(moveSpeed);
        this.gameObject.SetActive(false);
    }

    public void InitOrb(Vector2 incomingRange, GameObject _player)
    {
        playerPosition = _player.transform.position;
        transform.rotation = Quaternion.Euler(0, 0 ,Random.Range(incomingRange.x, incomingRange.y));

    }

    private void Update()
    {
        //transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        transform.position = Vector2.MoveTowards(this.transform.position, playerPosition, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<IHealth>().TakeDamage(DamageAmount);
            this.gameObject.SetActive(false);
        }
    }
}
