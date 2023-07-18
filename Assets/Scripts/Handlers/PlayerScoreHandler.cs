using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    public int KillScore { get; private set; } = 1000;
    public int KillStreakScore { get; private set; } = 1000;
    public int TimeScore { get; private set; } = 1000;
    public int AccuracyScore { get; private set; } = 1000;
    public int TotalScore { get; private set; } = 1000;

    private int _enemiesCountOnLevel;
    private int _playerAttacksCount;

    public void Init(int enemiesCountOnLevel)
    {
        _enemiesCountOnLevel = enemiesCountOnLevel;
    }

    private void OnEnable()
    {
        PlayerCombat.Attacked += OnPlayerAttacked;
    }

    private void OnDisable()
    {
        PlayerCombat.Attacked -= OnPlayerAttacked;
    }

    private void OnPlayerAttacked()
    {
        _playerAttacksCount++;
    }
}
