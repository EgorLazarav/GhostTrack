using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
public class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    protected EnemyController EnemyController;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        EnemyController = GetComponent<EnemyController>();
    }

    protected virtual void OnDisable()
    {
        NeedTransit = false;
    }
}
