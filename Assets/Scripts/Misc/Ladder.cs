using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform _changeStagePoint;
    [SerializeField] private Transform _startStagePoint;
    [SerializeField] private Ladder _mergedLadder;
    [SerializeField] private List<Enemy> _enemiesOnFloor;
    [SerializeField] private Canvas _canvas;

    public Transform StartStagePoint => _startStagePoint;

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (_enemiesOnFloor.Contains(enemy) == false)
            return;

        _enemiesOnFloor.Remove(enemy);

        if (_enemiesOnFloor.Count == 0)
            _canvas.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_enemiesOnFloor.Count != 0)
            return;

        if (collision.TryGetComponent(out Player player))
        {
            if (Vector2.Distance(_changeStagePoint.position, player.transform.position) < player.transform.localScale.x / 2)
            {
                player.transform.position = _mergedLadder.StartStagePoint.position;
                _canvas.gameObject.SetActive(false);
            }
        }
    }
}
