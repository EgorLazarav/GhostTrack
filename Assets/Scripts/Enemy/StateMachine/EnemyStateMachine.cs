using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _startState;

    private EnemyState _currentState;

    private void Start()
    {
        _currentState = _startState;

        if (_currentState != null)
            _currentState.Enter();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.TryGetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}