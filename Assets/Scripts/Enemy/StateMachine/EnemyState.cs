using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
public class EnemyState : MonoBehaviour
{
    [SerializeField] private List<EnemyTransition> _transitions;

    protected EnemyController EnemyController;

    private void Awake()
    {
        EnemyController = GetComponent<EnemyController>();
    }

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;

            foreach (var transition in _transitions)
                transition.enabled = true;
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public EnemyState TryGetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}