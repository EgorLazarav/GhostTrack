using UnityEngine;
using UnityEngine.Events;

public class EnemyDetectionSystem : MonoBehaviour
{
    private LayerMask _playerMask;
    private LayerMask _obstacleMask;
    private float _viewAngle;
    private float _viewRange;

    private bool _isPlayerDetected;

    public event UnityAction PlayerDetected;

    public void Init(LayerMask playerMask, LayerMask obstacleMask, float viewAngle, float viewRange)
    {
        _playerMask = playerMask;
        _obstacleMask = obstacleMask;
        _viewAngle = viewAngle;
        _viewRange = viewRange;
    }

    private void OnBecameVisible()
    {
        if (_isPlayerDetected)
            return;

        enabled = true;
        PlayerCombat.Shooted += OnPlayerShooted;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        PlayerCombat.Shooted -= OnPlayerShooted;
    }

    private void OnDisable()
    {
        PlayerCombat.Shooted -= OnPlayerShooted;
    }

    private void Update()
    {
        return;

        DetectPlayer();
    }

    private void DetectPlayer()
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

    private void OnPlayerShooted()
    {
        PlayerDetected?.Invoke();
    }
}