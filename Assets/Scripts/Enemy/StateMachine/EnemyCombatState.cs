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
    }
}
