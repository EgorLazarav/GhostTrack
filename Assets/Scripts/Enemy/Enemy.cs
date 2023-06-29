using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
}
