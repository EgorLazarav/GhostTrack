using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class State : MonoBehaviour
{
    protected NavMeshAgent Agent;
    protected Player Player;

    private SpriteRenderer _spriteRenderer;
    private const float TurnRate = 0.1f;

    protected virtual void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    protected void TurnToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _spriteRenderer.transform.DORotate(new Vector3(0, 0, lookAngle), TurnRate);
    }

    public void Init(NavMeshAgent agent, SpriteRenderer spriteRenderer, Player player)
    {
        _spriteRenderer = spriteRenderer;
        Agent = agent;
        Player = player;
    } 
}