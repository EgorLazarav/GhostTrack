using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    public int KillScore { get; private set; }
    public int KillStreakScore { get; private set; }
    public int TimeScore { get; private set; }
    public int AccuracyScore { get; private set; }
    public int TotalScore { get; private set; }

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
