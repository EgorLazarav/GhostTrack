using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Car : MonoBehaviour
{
    [SerializeField] private LevelCompleteHandler _levelCompleteHandler;

    private Collider2D _collider;

    public static event UnityAction PlayerEntered;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _levelCompleteHandler.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _levelCompleteHandler.LevelCompleted -= OnLevelCompleted;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            PlayerEntered?.Invoke();
            PlayerInput.Instance.enabled = false;
        }
    }

    private void OnLevelCompleted()
    {
        _collider.isTrigger = true;
    }
}
