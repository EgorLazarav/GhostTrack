using System.Collections;
using UnityEngine;

public class PatrolTransition : Transition
{
    private void OnEnable()
    {
        Player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        Player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        NeedTransit = true;
    }
}
