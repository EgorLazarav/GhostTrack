using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyState
{
    private void OnEnable()
    {
        print("COMBAT!");
    }

    private void Update()
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
    }

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(EnemyController.Weapon.ShootPoint.position, EnemyController.Weapon.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        return (hit.collider != null && hit.collider.TryGetComponent(out PlayerController player));
    }
}
