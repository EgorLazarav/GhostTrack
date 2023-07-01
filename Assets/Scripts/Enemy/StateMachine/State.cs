using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Enemy Enemy;

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
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

    public State TryGetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}

public class CombatState : State
{

}