using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyDetectionSystem))]
public class EnemyController : MonoBehaviour
{
    [Header("Patrol State Settings")]
    [SerializeField] private float _movementSpeed = 3;
    [SerializeField] private float _patrolRangeX = 4;
    [SerializeField] private float _patrolRangeY = 4;
    [SerializeField] private float _maxStopTime = 6;

    [Header("Combat State Settings")]
    [SerializeField]private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField][Range(0, 360)] private int _viewAngle = 180;
    [SerializeField] private float _viewRange = 10;
    [SerializeField] private float _maxReactionTime = 1.2f;
    [SerializeField] private float _maxSpread = 0.2f;
    [SerializeField] private float _hearingRange = 2;

    private NavMeshAgent _agent;
    private EnemyDetectionSystem _detectionSystem;
    private PlayerController _player;

    public NavMeshAgent Agent => _agent;
    public EnemyDetectionSystem DetectionSystem => _detectionSystem;
    public PlayerController Player => _player;
    public Weapon Weapon => _weapon;
    public float MaxStopTime => _maxStopTime;
    public float PatrolRangeX => _patrolRangeX;
    public float PatrolRangeY => _patrolRangeY;
    public float MaxReactionTime => _maxReactionTime;
    public float MaxSpread => _maxSpread;
    public float ViewRange => _viewRange;

    public static event UnityAction<EnemyController> Died;

    public void Init(PlayerController player)
    {
        _player = player;

        _detectionSystem = GetComponent<EnemyDetectionSystem>();
        _detectionSystem.Init(_playerMask, _obstacleMask, _viewAngle, _viewRange, _hearingRange);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
    }

    private void OnEnable()
    {
        _health.Over += OnHealthOver;
    }

    private void OnDisable()
    {
        _health.Over -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void TurnToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float turnRate = Mathf.Abs(lookAngle - transform.eulerAngles.z) / 10; 

        transform.DORotate(new Vector3(0, 0, lookAngle), turnRate * Time.deltaTime);
    }
}
