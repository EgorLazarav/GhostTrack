using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private float _enemyKillBounty = 100;
    [SerializeField] private float _killStreakTimer = 10;
    [SerializeField] private float _secondsCountForBestTime;

    private float _killStreakCounter = 1;
    private float _killScore = 0;
    private float _killedEnemiesCount = 0;
    private float _playerShootsCount = 0;
    private float _headShotsCounter = 0;
    private float _timeElapsed = 0;
    private float _timeScoreBonus = 1000;

    private Coroutine _coroutine;

    public float KillScoreBonus => _killScore;
    public float HeadshotsScoreBonus => _headShotsCounter * HeadshotsScoreMultiplier;
    public float AccuracyScoreBonus => (_killedEnemiesCount / _playerShootsCount) * AccuracyScoreMultiplier;
    public float TimeScoreBonus => _timeScoreBonus;
    public float TotalScore => _timeScoreBonus + _killScore + AccuracyScoreBonus + HeadshotsScoreBonus;

    private const float AccuracyScoreMultiplier = 1000;
    private const float HeadshotsScoreMultiplier = 500;
    private const float MaxTimeScoreBonus = 1000;

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
        Enemy.HeadShoted += OnHeadShoted;
        _playerCombat.Shooted += OnPlayerShooted;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
        Enemy.HeadShoted -= OnHeadShoted;
        _playerCombat.Shooted -= OnPlayerShooted;
    }

    private void Update()
    {
        if (FindObjectsOfType<Enemy>().Length == 0)
            return;

        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _secondsCountForBestTime)
        {
            _timeScoreBonus -= MaxTimeScoreBonus / _secondsCountForBestTime * Time.deltaTime;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _killScore += _enemyKillBounty * _killStreakCounter;
        _killStreakCounter++;
        _killedEnemiesCount++;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CountingKillStreakTimer());
    }

    private IEnumerator CountingKillStreakTimer()
    {
        yield return new WaitForSeconds(_killStreakTimer);

        _killStreakCounter = 1;
        _coroutine = null;
    }

    private void OnPlayerShooted()
    {
        _playerShootsCount++;
    }

    private void OnHeadShoted()
    {
        _headShotsCounter++;
        print(_headShotsCounter);
    }
}
