using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Rigidbody2D ProjectilePrefab;

    public Transform ProjectileSpawn;

    public Vector2 Direction;

    public float Power;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireProjectile(Power, Direction);
        }

    }

    private void FireProjectile(float power, Vector2 dir)
    {
        Rigidbody2D proj = Instantiate(ProjectilePrefab, ProjectileSpawn.position, transform.rotation);
        proj.AddForce(dir * power);
    }
}
