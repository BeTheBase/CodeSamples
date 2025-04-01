using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountesOrbPhase : MonoBehaviour
{
    public List<Transform> OrbSpawnes;

    [SerializeField]public Vector2 RotationRange;
    [SerializeField]public Vector2 RandomOrbRangeSpeed;

    public float OrbAmount;

    public GameObject PlayerRef;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(7f);
        StartCoroutine(InitOrbPhase(6f));
    }

    public IEnumerator InitOrbPhase(float time)
    {
        yield return new WaitForEndOfFrame();
        foreach(Transform t in OrbSpawnes)
        {
            CountesOrb orb = ObjectPool.Instance.SpawnFromPool<CountesOrb>(t.position, t.rotation) as CountesOrb;

            orb.InitOrb(RotationRange, PlayerRef);
        }

        yield return new WaitForSeconds(time);
        StartCoroutine(InitOrbPhase(5f));

    }
}
