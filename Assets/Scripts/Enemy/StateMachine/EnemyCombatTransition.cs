public class EnemyCombatTransition : EnemyTransition
{
    private void OnEnable()
    {
        EnemyController.DetectionSystem.PlayerDetected += OnPlayerDetected;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EnemyController.DetectionSystem.PlayerDetected -= OnPlayerDetected;
    }

    private void OnPlayerDetected()
    {
        NeedTransit = true;
    }
}
