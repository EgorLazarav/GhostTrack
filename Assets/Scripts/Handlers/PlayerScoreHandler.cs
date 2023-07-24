using System;
using System.Collections;
using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    public int KillScore => _killScore;
    public int KillStreakScore => _maxKillStreak * ScoreForEachStreak;
    public int TimeScore => (int)(BaseTimeScoreBonus - _timePassed * TimeScoreMultiplier);
    public int AccuracyScore => (int)((_enemiesCountOnLevel / _playerAttacksCount) * AccuracyMultiplier);
    public int TotalScore => GetTotalScore();

    private int _killScore;
    private float _timePassed;

    private float _enemiesCountOnLevel;
    private float _currentEnemiesCountOnLevel;
    private float _playerAttacksCount;

    private int _maxKillStreak = 0;
    private int _currentKillStreak = 0;
    private Coroutine _killStreakCoroutine;

    private const int ScoreForKill = 100;
    private const int ScoreForEachStreak = 100;
    private const int SecondsToContinueStreak = 3;
    private const int AccuracyMultiplier = 1000;
    private const int TimeScoreMultiplier = 5;
    private const int BaseTimeScoreBonus = 1000;

    public void Init(int enemiesCountOnLevel)
    {
        _enemiesCountOnLevel = enemiesCountOnLevel;
        _currentEnemiesCountOnLevel = _enemiesCountOnLevel;
    }

    private void OnEnable()
    {
        PlayerCombat.Attacked += OnPlayerAttacked;
        EnemyController.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        PlayerCombat.Attacked -= OnPlayerAttacked;
        EnemyController.Died -= OnEnemyDied;
    }

    private void Update()
    {
        if (_currentEnemiesCountOnLevel == 0)
            return;

        _timePassed += Time.deltaTime;
    }

    private void OnPlayerAttacked()
    {
        _playerAttacksCount++;
    }

    private void OnEnemyDied(EnemyController enemy)
    {
        _killScore += ScoreForKill;
        print(AccuracyScore);

        _currentKillStreak++;
        _currentEnemiesCountOnLevel--;

        if (_currentKillStreak > _maxKillStreak)
            _maxKillStreak = _currentKillStreak;

        if (_killStreakCoroutine != null)
            StopCoroutine(_killStreakCoroutine);

        _killStreakCoroutine = StartCoroutine(CountingKillStreakTimer());
    }

    private IEnumerator CountingKillStreakTimer()
    {
        yield return new WaitForSeconds(SecondsToContinueStreak);

        _currentKillStreak = 0;
        _killStreakCoroutine = null;
    }

    private int GetTotalScore()
    {
        return KillScore + KillStreakScore + TimeScore + AccuracyScore;
    }

}
