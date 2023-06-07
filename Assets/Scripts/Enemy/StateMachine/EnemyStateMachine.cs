using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Combat,
    Idle,
    Patrol
}

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _currentState;

    [Header("States")]
    [SerializeField] private CombatState _combat;
    [SerializeField] private IdleState _idle;
    [SerializeField] private PatrolState _patrol;

    private State[] _states;

    public void Init(NavMeshAgent agent, SpriteRenderer spriteRenderer, Player player)
    {
        _states = new State[] { _combat, _idle, _patrol};

        foreach (var state in _states)
        {
            state.Init(agent, spriteRenderer, player);
            state.enabled = false;
        }

        _currentState.enabled = true;
    }

    public void SwitchState(States newState)
    {
        _currentState.enabled = false;

        switch (newState)
        {
            case States.Combat:
                _currentState = _combat;
                break;
        }

        _currentState.enabled = true;
    }
}
