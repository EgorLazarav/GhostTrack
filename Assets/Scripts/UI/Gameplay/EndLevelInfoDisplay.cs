using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndLevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerScoreHandler _playerScoreHandler;
    [SerializeField] private Image _curtainPanel;
    [SerializeField] private Image _scorePanel;

    [Header("Texts")]
    [SerializeField] private TMP_Text _killScoreText;
    [SerializeField] private TMP_Text _killStreakScoreText;
    [SerializeField] private TMP_Text _timeScoreText;
    [SerializeField] private TMP_Text _accuracyScoreText;
    [SerializeField] private TMP_Text _totalScoreText;
    [SerializeField] private TMP_Text _rangText;
    [SerializeField] private TMP_Text _pressAnyButtonText;

    private Dictionary<TMP_Text, int> _scoresMap;
    private Coroutine _animatingScoreTextCoroutine;

    private void OnEnable()
    {
        LevelEndHandler.PlayerEntered += OnPlayerEnteredCar;
    }

    private void OnDisable()
    {
        LevelEndHandler.PlayerEntered -= OnPlayerEnteredCar;
    }

    private void CreateDict()
    {
        _scoresMap = new Dictionary<TMP_Text, int>();

        _scoresMap.Add(_killScoreText, _playerScoreHandler.KillScore);
        _scoresMap.Add(_killStreakScoreText, _playerScoreHandler.KillStreakScore);
        _scoresMap.Add(_timeScoreText, _playerScoreHandler.TimeScore);
        _scoresMap.Add(_accuracyScoreText, _playerScoreHandler.AccuracyScore);
        _scoresMap.Add(_totalScoreText, _playerScoreHandler.TotalScore);
    }

    private void OnPlayerEnteredCar()
    {
        StartCoroutine(Animating());
    }

    private IEnumerator Animating()
    {
        CreateDict();
        float currentAlpha = 0;
        float animationSpeed = 0.5f;

        while (currentAlpha < 1)
        {
            _curtainPanel.color = _curtainPanel.color.SetAlpha(currentAlpha);
            currentAlpha += Time.deltaTime * animationSpeed;

            yield return null;
        }

        _scorePanel.gameObject.SetActive(true);

        foreach (var item in _scoresMap)
        {
            _animatingScoreTextCoroutine = StartCoroutine(ScoreAnimating(item.Key, item.Value, 1));

            while (_animatingScoreTextCoroutine != null)
            {
                yield return null;
            }
        }

        // тут нужно определять уровень и анимировать ранг
        _rangText.gameObject.SetActive(true);
        _rangText.text += "KILLER";

        // после анимации ранга вылазит
        _pressAnyButtonText.gameObject.SetActive(true);

        // тут нужна ассихронная загрузка с экраном натса
        StartCoroutine(WaitingForRestart());
    }

    private IEnumerator WaitingForRestart()
    {
        while (true)
        {
            if (Input.anyKeyDown)
                break;

            yield return null;
        }

        SceneLoader.Instance.ReloadLevel();
    }

    private IEnumerator ScoreAnimating(TMP_Text text, int value, int speed)
    {
        int currentValue = 0;
        string defaultText = text.text;

        text.text = defaultText + currentValue;
        text.gameObject.SetActive(true);

        while (currentValue != value)
        {
            if (Input.anyKeyDown)
            {
                yield return new WaitForEndOfFrame();
                break;
            }

            currentValue = Mathf.Clamp(currentValue += speed, currentValue, value);
            text.text = defaultText + currentValue;

            yield return null;  
        }

        text.text = defaultText + value;
        _animatingScoreTextCoroutine = null;
    }
}
