using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JakobController : MonoBehaviour
{
    public Animator JakobAnimator;

    public Transform PointA, PointB;
    public float elapsedTime, waitTime;

    private void Start()
    {
        
        StartCoroutine(WalkTo(PointA.position, PointB.position));
    }

    private IEnumerator WalkTo(Vector3 currPos, Vector3 nextPos)
    {
        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(currPos, nextPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
    
            yield return null;
        }
        //StartCoroutine(WalkTo(nextPos, currPos));
        elapsedTime = 0;
        PointB.position = currPos;
        PointA.position = nextPos;
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(WalkTo(PointA.position, PointB.position));

    }

    private void SwitchAnimation(string animation)
    {
        JakobAnimator.SetBool(animation, true);
    }

    private void ResetPrevAnimation(string prevAnimation)
    {
        JakobAnimator.SetBool(prevAnimation, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SwitchAnimation("Attacking");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SwitchAnimation("Walking");
            ResetPrevAnimation("Attacking");
        }
    }
}
