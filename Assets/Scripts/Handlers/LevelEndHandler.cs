using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class LevelEndHandler : MonoBehaviour
{
    [SerializeField] private EndLevelInfoDisplay _endLevelInfoDisplay;
    [SerializeField] private LevelCompleteHandler _levelCompleteHandler;
    [SerializeField] private Sprite _playerInCarSprite;
    [SerializeField] private AudioClip _carTurnOnSFX;

    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private Coroutine _coroutine;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _levelCompleteHandler.LevelCompleted += OnLevelCompleted;
        _endLevelInfoDisplay.AnimationEnded += OnAnimationEnded;
    }

    private void OnDisable()
    {
        _levelCompleteHandler.LevelCompleted -= OnLevelCompleted;
        _endLevelInfoDisplay.AnimationEnded -= OnAnimationEnded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.gameObject.SetActive(false);
            _coroutine = StartCoroutine(Animating());
        }
    }

    private void OnBecameInvisible()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _endLevelInfoDisplay.StartAnimation();
    }

    private void OnAnimationEnded()
    {
        SceneLoader.Instance.LoadMainMenu();
    }

    private void OnLevelCompleted()
    {
        _collider.isTrigger = true;
    }

    private IEnumerator Animating()
    {
        float delay = _carTurnOnSFX.length / 2;
        float speed = 3;
        float angle = 180;
        float turnTime = 0.5f;

        _renderer.sprite = _playerInCarSprite;
        AudioManager.Instance.PlayRandomEndLevelTheme();
        AudioManager.Instance.PlaySound(_carTurnOnSFX);

        yield return new WaitForSeconds(delay);

        transform.DORotate(new Vector3(0, 0, angle), turnTime);

        while (true)
        {
            transform.position -= Vector3.up * Time.deltaTime * speed;
            speed += speed * Time.deltaTime;

            yield return null;
        }
    }
}
