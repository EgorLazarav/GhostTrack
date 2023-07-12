using UnityEngine;
using UnityEngine.Events;

public class EnemyDetectionSystem : MonoBehaviour
{
    private LayerMask _playerMask;
    private LayerMask _obstacleMask;
    private float _viewAngle;
    private float _viewRange;

    private bool _isPlayerDetected;
    private float _hearingRange;

    public event UnityAction PlayerDetected;

    public void Init(LayerMask playerMask, LayerMask obstacleMask, float viewAngle, float viewRange, float hearingRange)
    {
        _playerMask = playerMask;
        _obstacleMask = obstacleMask;
        _viewAngle = viewAngle;
        _viewRange = viewRange;
        _hearingRange = hearingRange;
    }

    private void OnBecameVisible()
    {
        if (_isPlayerDetected)
            return;

        enabled = true;
        PlayerCombat.Attacked += OnPlayerAttacked;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        PlayerCombat.Attacked -= OnPlayerAttacked;
    }

    private void OnDisable()
    {
        PlayerCombat.Attacked -= OnPlayerAttacked;
    }

    private void Update()
    {
        if (CheatCodeActivator.IsPlayerInvisible)
            return;

        CheckPlayerInViewRange();
        CheckPlayerInHearingRange();
    }

    private void CheckPlayerInHearingRange()
    {
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _hearingRange, _playerMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            float distantionToTarget = Vector2.Distance(transform.position, target.transform.position);

            if (Physics2D.Raycast(transform.position, directionToTarget, distantionToTarget, _obstacleMask) == false)
            {
                PlayerDetected?.Invoke();
                _isPlayerDetected = true;
                enabled = false;
            }
        }
    }

    private void CheckPlayerInViewRange()
    {
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _viewRange, _playerMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.right, directionToTarget) < _viewAngle / 2)
            {
                float distantionToTarget = Vector2.Distance(transform.position, target.transform.position);

                if (Physics2D.Raycast(transform.position, directionToTarget, distantionToTarget, _obstacleMask) == false)
                {
                    PlayerDetected?.Invoke();
                    _isPlayerDetected = true;
                    enabled = false;
                }
            }
        }
    }

    private void OnPlayerAttacked()
    {
        if (CheatCodeActivator.IsPlayerInvisible)
            return;

        PlayerDetected?.Invoke();
    }
}