using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyState
{
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Fighting());
    }

    private void OnDisable()
    {
        if (_coroutine != null )
            StopCoroutine(_coroutine);
    }

    private IEnumerator Fighting()
    {
        yield return new WaitForSeconds(Random.Range(0, EnemyController.MaxReactionTime));

        while (true)
        {
            EnemyController.TurnToTarget(EnemyController.Player.transform.position);

            if (CheckShootingPossibility())
            {
                EnemyController.Agent.SetDestination(transform.position);
                EnemyController.Weapon.TryShoot();
            }
            else
            {
                EnemyController.Agent.SetDestination(EnemyController.Player.transform.position);
            }

            yield return new WaitForSeconds(Time.deltaTime * EnemyController.MaxReactionTime);
        }
    }

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(EnemyController.Weapon.ShootPoint.position, EnemyController.Weapon.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, EnemyController.ViewRange);

        Debug.DrawRay(ray.origin, ray.direction * EnemyController.ViewRange, Color.red);

        return (hit.collider != null && hit.collider.TryGetComponent(out PlayerController player));
    }
}
