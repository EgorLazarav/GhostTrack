using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    [SerializeField] private NavMeshAgent _agent;

    private Coroutine _coroutine;
    private Vector3 _startPosition;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Patroling());
    }

    private void OnDisable()
    {
        _agent.isStopped = true;
        StopCoroutine(_coroutine);
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 6));

            Vector3 point = new Vector3(
                _startPosition.x + Random.Range(-5, 5),
                _startPosition.y + Random.Range(-5, 5),
                _startPosition.z
                );

            _agent.isStopped = false;
            _agent.SetDestination(point);
        }
    }
}
