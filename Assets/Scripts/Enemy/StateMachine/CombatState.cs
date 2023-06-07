using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CombatState : State
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _reactionTime = 0.1f;
    [SerializeField] private float _accuracy = 1;

    private void OnEnable()
    {
        StartCoroutine(Fighting());
    }

    private IEnumerator Fighting()
    {
        yield return new WaitForSeconds(_reactionTime);

        while (Player != null)
        {
            yield return null;

            TurnToTarget(Player.transform.position);

            if (CheckShootingPossibility())
            {
                Agent.SetDestination(transform.position);
                Vector3 randomSpread = new Vector3(Random.Range(-_accuracy, _accuracy), Random.Range(-_accuracy, _accuracy), 0);
                _weapon.TryShoot(Player.transform.position + randomSpread - transform.position);
            }
            else
            {
                Agent.SetDestination(Player.transform.position);
            }
        }
    }

    private bool CheckShootingPossibility()
    {
        float rangeOffset = 0.5f;
        Ray2D ray = new Ray2D(_weapon.ShootPoint.position, _weapon.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _weapon.Data.FireRange - rangeOffset);
        Debug.DrawRay(ray.origin, ray.direction * _weapon.Data.FireRange, Color.red);
        return (hit.collider != null && hit.collider.TryGetComponent(out Player player));
    }
}
