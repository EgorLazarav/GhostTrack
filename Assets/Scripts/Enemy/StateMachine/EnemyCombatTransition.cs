public class EnemyCombatTransition : EnemyTransition
{
    private void OnEnable()
    {
        EnemyController.DetectionSystem.PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        EnemyController.DetectionSystem.PlayerDetected -= OnPlayerDetected;
    }

    private void OnPlayerDetected()
    {
        NeedTransit = true;
    }
}
