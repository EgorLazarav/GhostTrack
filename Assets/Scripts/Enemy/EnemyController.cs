using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyDetectionSystem))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _deadBody;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed = 3;
    [SerializeField] private float _maxStopTime = 6;
    [SerializeField] private Vector2 _patrolAreaSize;
    [SerializeField] private Vector2 _patrolAreaOffset;

    [Header("Combat Settings")]
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField][Range(0, 360)] private int _viewAngle = 180;
    [SerializeField] private float _viewRange = 6;
    [SerializeField] private float _maxReactionTime = 1.2f;
    [SerializeField] private float _hearingRange = 2;

    private Health _health;
    private Weapon _weapon;
    private NavMeshAgent _agent;
    private EnemyDetectionSystem _detectionSystem;
    private PlayerController _player;

    public NavMeshAgent Agent => _agent;
    public EnemyDetectionSystem DetectionSystem => _detectionSystem;
    public PlayerController Player => _player;
    public Weapon Weapon => _weapon;
    public float MaxStopTime => _maxStopTime;
    public Vector2 PatrolAreaSize => _patrolAreaSize;
    public Vector2 PatrolAreaOffset => _patrolAreaOffset;
    public float MaxReactionTime => _maxReactionTime;
    public float ViewRange => _viewRange;

    public static event UnityAction<EnemyController> Died;

    public void Init(PlayerController player)
    {
        _player = player;
        GetComponent<BoxCollider2D>().enabled = false;
        _health = GetComponent<Health>();
        _health.Over += OnHealthOver;
        _weapon = transform.GetComponentInChildren<Weapon>();

        _detectionSystem = GetComponent<EnemyDetectionSystem>();
        _detectionSystem.Init(_playerMask, _obstacleMask, _viewAngle, _viewRange, _hearingRange);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
    }

    private void OnDisable()
    {
        _health.Over -= OnHealthOver;
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider2D>().size = _patrolAreaSize;
        GetComponent<BoxCollider2D>().offset = _patrolAreaOffset;
    }

    private void OnHealthOver()
    {
        _weapon.Throw();
        Died?.Invoke(this);
        gameObject.SetActive(false);
        _deadBody.gameObject.SetActive(true);
        _deadBody.transform.parent = null;
    }

    public void TurnToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float turnRate = Mathf.Abs(lookAngle - transform.eulerAngles.z) / 10; 

        transform.DORotate(new Vector3(0, 0, lookAngle), turnRate * Time.deltaTime);
    }
}
