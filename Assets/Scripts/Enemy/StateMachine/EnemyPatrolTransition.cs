using System.Collections;
using UnityEngine;

public class EnemyPatrolTransition : EnemyTransition
{
    private void OnEnable()
    {
        PlayerController.Died += OnPlayerDied;
        PlayerController.Shifted += OnPlayerShifted;
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerController.Died -= OnPlayerDied;
        PlayerController.Shifted -= OnPlayerShifted;
    }

    private void OnPlayerDied()
    {
        NeedTransit = true;
    }

    private void OnPlayerShifted()
    {
        NeedTransit = true;
    }
}
