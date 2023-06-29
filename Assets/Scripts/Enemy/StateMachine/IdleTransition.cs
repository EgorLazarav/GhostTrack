using System.Collections;
using UnityEngine;

public class IdleTransition : Transition
{
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Idling());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Idling()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            NeedTransit = true;
        }
    }
}
