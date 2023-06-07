using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private float _minTimeBetweenTurn = 4;
    [SerializeField] private float _maxTimeBetweenTurn = 8;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(Idling());
    }

    private IEnumerator Idling()
    {
        while (true)
        {
            TurnToTarget(Vector2.right * Random.Range(-10, 10));
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenTurn, _maxTimeBetweenTurn));
        }
    }
}
