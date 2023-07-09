using System.Collections;
using UnityEngine;

public class EnemyPatrolState : EnemyState
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
        _patrolArea = new Rect(
            transform.position.x - EnemyController.PatrolAreaSize.x / 2 + EnemyController.PatrolAreaOffset.x, 
            transform.position.y - EnemyController.PatrolAreaSize.y / 2 + EnemyController.PatrolAreaOffset.y, 
            EnemyController.PatrolAreaSize.x, 
            EnemyController.PatrolAreaSize.y
            );
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            Vector3 newDestination = GetRandomPointInArea(_patrolArea);

            while (Vector2.Distance(transform.position, newDestination) > EnemyController.Agent.radius)
            {
                EnemyController.TurnToTarget(EnemyController.Agent.steeringTarget);
                EnemyController.Agent.SetDestination(newDestination);

                yield return null;
            }

            float stopTime = Random.Range(0, EnemyController.MaxStopTime);

            while (stopTime > 0)
            {
                stopTime -= Time.deltaTime;
                yield return null;
            }
        }
    }

    private Vector3 GetRandomPointInArea(Rect area)
    {
        float randomX = Random.Range(area.xMin, area.xMax);
        float randomY = Random.Range(area.yMin, area.yMax);

        return new Vector3(randomX, randomY, 0);
    }
}
