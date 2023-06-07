using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Car : MonoBehaviour
{
    private BoxCollider2D _collider;

    public event UnityAction PlayerSatInCar;

    private void OnEnable()
    {
        LevelManager.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        LevelManager.GameOver -= OnGameOver;
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayerSatInCar?.Invoke();
            player.gameObject.SetActive(false);
            StartCoroutine(RidingOut());
        }
    }

    private IEnumerator RidingOut()
    {
        float speed = 3;

        while (true)
        {
            yield return null;
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
    }

    private void OnGameOver(LevelStates state)
    {
        if (state == LevelStates.Victory)
            _collider.isTrigger = true;
    }
}
