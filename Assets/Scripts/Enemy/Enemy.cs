using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Player _player;
    [SerializeField] private float _patrolRangeX = 4;
    [SerializeField] private float _patrolRangeY = 4;
    [SerializeField] private float _maxStopTime = 6;

    public NavMeshAgent Agent => _agent;
    public float MaxStopTime => _maxStopTime;
    public float PatrolRangeX => _patrolRangeX;
    public float PatrolRangeY => _patrolRangeY;

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
}
