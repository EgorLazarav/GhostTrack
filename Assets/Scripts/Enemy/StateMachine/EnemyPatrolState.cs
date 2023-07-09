using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Rect _patrolArea;
    private Coroutine _coroutine;
    private Tween _rotateTween;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Patroling());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _rotateTween.Kill();
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
        yield return new WaitForSeconds(1);

        while (true)
        {
            Vector3 newDestination = GetRandomPointInArea(_patrolArea);
            float stopTime = Random.Range(0, EnemyController.MaxStopTime);

            while (Vector2.Distance(transform.position, newDestination) > EnemyController.Agent.radius / 2 && stopTime > 0)
            {
                stopTime -= Time.deltaTime;
                EnemyController.TurnToTarget(EnemyController.Agent.steeringTarget);
                EnemyController.Agent.SetDestination(newDestination);

                print("idy");

                yield return null;
            }

            stopTime = Random.Range(0, EnemyController.MaxStopTime);
            _rotateTween = transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + 120), stopTime);

            while (stopTime > 0)
            {
                print("stoyu");

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
