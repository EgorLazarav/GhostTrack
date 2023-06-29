using System.Collections;
using UnityEngine;

public class IdleState : State
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
            yield return new WaitForSeconds(Random.Range(2, 5));
            transform.DORotateZ(Random.Range(0, 360), 0.5f);
        }
    }
}
