using System.Collections;
using UnityEngine;

public class EnemyPatrolTransition : EnemyTransition
{
    private void OnEnable()
    {
        Player.Died += OnPlayerDied;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        NeedTransit = true;
    }
}
