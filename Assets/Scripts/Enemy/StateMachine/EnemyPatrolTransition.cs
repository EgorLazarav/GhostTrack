using System.Collections;
using UnityEngine;

public class EnemyPatrolTransition : EnemyTransition
{
    private void OnEnable()
    {
        PlayerController.Died += OnPlayerDied;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerController.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        NeedTransit = true;
    }
}
