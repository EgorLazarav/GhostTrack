using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private float _patrolAreaSizeX = 10;
    [SerializeField] private float _patrolAreaSizeY = 2;
    [SerializeField] private float _minTimeBetweenChangeDestination = 1;
    [SerializeField] private float _maxTimeBetweenChangeDestination = 5;

    private Rect _area;

    protected override void Start()
    {
        base.Start();

        CreateArea();
        StartCoroutine(Patroling());
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            Vector3 newDestination = GetRandomPointInArea();
            TurnToTarget(newDestination);
            Agent.SetDestination(newDestination);
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenChangeDestination, _maxTimeBetweenChangeDestination));
        }
    }

    private void CreateArea()
    {
        Vector2 areaCenter = new Vector2(transform.position.x - _patrolAreaSizeX / 2, transform.position.y - _patrolAreaSizeY / 2);
        Vector2 areaRadius = new Vector2(_patrolAreaSizeX, _patrolAreaSizeY);
        _area = new Rect(areaCenter, areaRadius);
    }

    private Vector2 GetRandomPointInArea()
    {
        return new Vector2(Random.Range(_area.xMin, _area.xMax), Random.Range(_area.yMin, _area.yMax));
    }
}
