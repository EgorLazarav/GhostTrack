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
                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
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
        float boxSize = 0.2f;
        float boxAngle = 90;

        var hit = Physics2D.BoxCast(EnemyController.Weapon.ShootPoint.position, new Vector2(boxSize, boxSize), boxAngle, EnemyController.Weapon.transform.right);

        return (hit.collider != null && hit.collider.TryGetComponent(out PlayerController player));
    }
}
