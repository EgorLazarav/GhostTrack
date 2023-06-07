using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private Health[] _bodyParts;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Other objects")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player _player;
    [SerializeField] private DetectionSystem _detectionSystem;

    public static event UnityAction<Enemy> Died;
    public static event UnityAction HeadShoted;

    private void OnEnable()
    {
        foreach (var part in _bodyParts)
        {
            part.HeadShoted += OnHeadShoted;
            part.Overed += OnHealthOvered;
        }

        _detectionSystem.PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        foreach (var part in _bodyParts)
        {
            part.HeadShoted -= OnHeadShoted;
            part.Overed -= OnHealthOvered;
        }

        _detectionSystem.PlayerDetected -= OnPlayerDetected;
    }

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _stateMachine.Init(_agent, _spriteRenderer, _player);
    }

    private void OnHealthOvered()
    {
        _weapon.Drop();
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnPlayerDetected()
    {
        _detectionSystem.gameObject.SetActive(false);
        _stateMachine.SwitchState(States.Combat);
    }

    private void OnHeadShoted()
    {
        HeadShoted?.Invoke();
    }
}
