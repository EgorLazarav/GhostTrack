using UnityEngine;
using UnityEngine.Events;

public class EnemyDetectionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField][Range(0, 360)] private int _viewAngle = 180;
    [SerializeField] private float _viewRange = 10;

    public event UnityAction PlayerDetected;

    private void Update()
    {
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
                }
            }
        }
    }
}