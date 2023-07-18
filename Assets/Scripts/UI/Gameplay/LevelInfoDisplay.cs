using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private LevelCompleteHandler _levelHandler;

    private CanvasGroup _canvasGroup;

    private const string PlayerDiedText = "PRESS 'R' TO RESTART";
    private const string LevelCompletedText = "GO TO CAR";

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        PlayerController.Died += OnPlayerDied;
        _levelHandler.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        PlayerController.Died -= OnPlayerDied;
        _levelHandler.LevelCompleted -= OnLevelCompleted;
    }

    private void OnPlayerDied()
    {
        _canvasGroup.alpha = 1;
        _text.text = PlayerDiedText;
    }

    private void OnLevelCompleted()
    {
        _canvasGroup.alpha = 1;
        _text.text = LevelCompletedText;
    }
}