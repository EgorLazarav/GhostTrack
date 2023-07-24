using System;
using System.Collections;
using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    public int KillScore => _killScore;
    public int KillStreakScore => _maxReachedKillStreak * ScoreForEachStreak;
    public int TimeScore => Mathf.Clamp((int)((BaseTimeScoreBonus + _timeScoreBonusOnLevel) - _timePassed * _timeScoreMultiplier), 0, BaseTimeScoreBonus);
    public int AccuracyScore => Mathf.Clamp((int)((_enemiesCountOnLevel / _playerAttacksCount) * BaseAccuracyScoreBonus), 0, BaseAccuracyScoreBonus);
    public int TotalScore => GetTotalScore();
    public int MaxScore => GetMaxScore();

    private Coroutine _killStreakCoroutine;
    private int _killScore;
    private float _timePassed;
    private float _enemiesCountOnLevel;
    private float _currentEnemiesCountOnLevel;
    private float _playerAttacksCount;
    private float _timeScoreMultiplier;
    private int _maxReachedKillStreak;
    private int _currentReachedKillStreak;
    private int _maxKillStreak;
    private float _timeScoreBonusOnLevel;

    private const int ScoreForKill = 100;
    private const int ScoreForEachStreak = 100;
    private const int SecondsToContinueStreak = 3;
    private const int BaseAccuracyScoreBonus = 1000;
    private const int BaseTimeScoreBonus = 1000;
    private const float BaseTimeScoreMultiplier = 100;

    public void Init(int enemiesCountOnLevel)
    {
        _maxKillStreak = enemiesCountOnLevel;
        _enemiesCountOnLevel = enemiesCountOnLevel;
        _currentEnemiesCountOnLevel = enemiesCountOnLevel;
        _timeScoreMultiplier = BaseTimeScoreMultiplier / enemiesCountOnLevel;
        _timeScoreBonusOnLevel = SecondsToContinueStreak * enemiesCountOnLevel * 10;
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
        _currentReachedKillStreak++;
        _currentEnemiesCountOnLevel--;

        if (_currentReachedKillStreak > _maxReachedKillStreak)
            _maxReachedKillStreak = _currentReachedKillStreak;

        if (_killStreakCoroutine != null)
            StopCoroutine(_killStreakCoroutine);

        _killStreakCoroutine = StartCoroutine(CountingKillStreakTimer());
    }

    private IEnumerator CountingKillStreakTimer()
    {
        yield return new WaitForSeconds(SecondsToContinueStreak);

        _currentReachedKillStreak = 0;
        _killStreakCoroutine = null;
    }

    private int GetTotalScore()
    {
        return KillScore + KillStreakScore + TimeScore + AccuracyScore;
    }
    private int GetMaxScore()
    { 
        return (int)(ScoreForKill * _enemiesCountOnLevel
            + _maxKillStreak * ScoreForEachStreak
            + BaseTimeScoreBonus
            + BaseAccuracyScoreBonus);
    }
}
