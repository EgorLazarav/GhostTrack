using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PatrolState : State
{
    private Rect _patrolArea;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Patroling());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private void Start()
    {
        _patrolArea = new Rect(transform.position.x, transform.position.y, Enemy.PatrolRangeX, Enemy.PatrolRangeY);
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, Enemy.MaxStopTime));

            Vector3 newDestination = GetRandomPointInArea(_patrolArea);
            Vector3 lookDirection = newDestination - transform.position;
            float turnRate = 0.1f;
            float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            transform.DORotate(new Vector3(0, 0, lookAngle), turnRate);
            Enemy.Agent.SetDestination(newDestination);
        }
    }

    private Vector3 GetRandomPointInArea(Rect area)
    {
        float randomX = Random.Range(area.xMin, area.xMax);
        float randomY = Random.Range(area.yMin, area.yMax);

        return new Vector3(randomX, randomY, 0);
    }
}
