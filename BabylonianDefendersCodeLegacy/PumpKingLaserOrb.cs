using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpKingLaserOrb : MonoBehaviour
{
    public List<Transform> Spawns;
    public List<Transform> Targets;
    public float TravelTime = 3f;
    public float DamageAmount = 60f;
    private void Start()
    {
        InitLaser();
    }

    public void InitLaser()
    {
        StartCoroutine(LerpToTarget());
    }

    private IEnumerator LerpToTarget()
    {
        float eslapsedTime = 0;
        var tmpSpawn = Spawns[Random.Range(0, Spawns.Count - 1)].position;
        var tmpTarget = Targets[Random.Range(0, Targets.Count - 1)].position;
        while (eslapsedTime < TravelTime)
        {
            transform.position = Vector3.Lerp(tmpSpawn, tmpTarget, (eslapsedTime / TravelTime));
            eslapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        transform.position = tmpSpawn;
        StartCoroutine(LerpToTarget());
        yield return new WaitForEndOfFrame();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<IHealth>()?.TakeDamage(DamageAmount);
        }
    }
}
