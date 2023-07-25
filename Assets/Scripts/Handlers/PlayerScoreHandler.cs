using System;
using System.Collections;
using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    [SerializeField] private AudioClip[] _killStreakSFX;

    private float _enemiesCountOnLevel;
    private float _currentEnemiesCountOnLevel;
    private float _playerAttacksCount;

    private Coroutine _killStreakCoroutine;
    private float _killScore;
    private float _maxKillStreak;
    private float _currentReachedKillStreak;
    private float _maxReachedKillStreak;

    private float _timePassed;
    private float _timeToPerfectCompleteLevel;

    public float KillScore => _killScore;
    public float KillStreakScore => GetKillStreakScore();
    public float TimeScore => GetTimeScore();
    public float AccuracyScore => GetAccuracyScore();
    public float TotalScore => GetTotalScore();
    public float MaxScore => GetMaxScore();

    public void Init(int enemiesCountOnLevel)
    {
        _maxKillStreak = enemiesCountOnLevel;
        _enemiesCountOnLevel = enemiesCountOnLevel;
        _currentEnemiesCountOnLevel = enemiesCountOnLevel;
        _timeToPerfectCompleteLevel = enemiesCountOnLevel * Constants.SecondsToContinueStreak;
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
        _killScore += Constants.KillScoreMultiplier;

        _currentReachedKillStreak++;
        AnnounceKillStreak();

        _currentEnemiesCountOnLevel--;

        if (_currentReachedKillStreak > _maxReachedKillStreak)
            _maxReachedKillStreak = _currentReachedKillStreak;

        if (_killStreakCoroutine != null)
            StopCoroutine(_killStreakCoroutine);

        _killStreakCoroutine = StartCoroutine(CountingKillStreakTimer());
    }

    private void AnnounceKillStreak()
    {
        AudioManager.Instance.PlaySound(_killStreakSFX[(int)_currentReachedKillStreak - 1]);
    }

    private IEnumerator CountingKillStreakTimer()
    {
        yield return new WaitForSeconds(Constants.SecondsToContinueStreak);

        _currentReachedKillStreak = 0;
        _killStreakCoroutine = null;
    }

    private float GetTotalScore()
    {
        return KillScore + KillStreakScore + TimeScore + AccuracyScore;
    }

    private float GetMaxScore()
    { 
        return Constants.KillScoreMultiplier * _enemiesCountOnLevel
            + _maxKillStreak * Constants.KillStreakScoreMultiplier
            + Constants.TimeScoreMultiplier
            + Constants.AccuracyScoreMultiplier;
    }

    private float GetKillStreakScore()
    {
        return _maxReachedKillStreak * Constants.KillStreakScoreMultiplier;
    }

    private float GetTimeScore()
    {
        return Mathf.Clamp((_timeToPerfectCompleteLevel / _timePassed) * Constants.TimeScoreMultiplier, 0, Constants.TimeScoreMultiplier);
    }

    private float GetAccuracyScore()
    {
        return Mathf.Clamp((_enemiesCountOnLevel / _playerAttacksCount) * Constants.AccuracyScoreMultiplier, 0, Constants.AccuracyScoreMultiplier);
    }
}
