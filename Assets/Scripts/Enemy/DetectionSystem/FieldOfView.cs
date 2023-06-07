using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class FieldOfView : MonoBehaviour
{
    private BoxCollider2D _collider;
    private float _viewAngle = 180;
    private LayerMask _playerMask;
    private LayerMask _obstacleMask;
    private Vector2 _baseAreaSize;

    public event UnityAction PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            StartCoroutine(Viewing());
        }

        if (collision.TryGetComponent(out Bullet bullet))
        {
            PlayerDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            StopCoroutine(Viewing());
        }
    }

    public void Init(Vector2 areaSize, float viewAngle, LayerMask playerMask, LayerMask obstacleMask)
    {
        _collider = GetComponent<BoxCollider2D>();
        _baseAreaSize = areaSize;
        _collider.size = areaSize;
        _collider.isTrigger = true;

        _viewAngle = viewAngle;
        _playerMask = playerMask;
        _obstacleMask = obstacleMask;
    }

    private IEnumerator Viewing()
    {
        while (true)
        {
            DetectPlayer();
            yield return null;
        }
    }

    private void DetectPlayer()
    {
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _baseAreaSize.x, _playerMask);

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
