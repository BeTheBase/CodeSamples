using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AninEventTrigger : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(OnCompletedAnimation());
    }

    private IEnumerator OnCompletedAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        //Fire Event! 

        EventManager.TriggerEvent("CompletedIntroCutscene");
    }


}
