using System.Collections;
using UnityEngine;

public class PatrolTransition : Transition
{
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Patroling());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            NeedTransit = true;
        }
    }
}
