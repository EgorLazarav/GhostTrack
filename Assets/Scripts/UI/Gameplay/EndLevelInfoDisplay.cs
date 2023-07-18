using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class EndLevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerScoreHandler _playerScoreHandler;
    [SerializeField] private Image _backgroundWrapper;

    [Header("Texts")]
    [SerializeField] private TMP_Text _killScoreText;
    [SerializeField] private TMP_Text _killStreakScoreText;
    [SerializeField] private TMP_Text _timeScoreText;
    [SerializeField] private TMP_Text _accuracyScoreText;
    [SerializeField] private TMP_Text _totalScoreText;
    [SerializeField] private TMP_Text _rangText;
    [SerializeField] private TMP_Text _pressAnyButtonText;

    private Dictionary<TMP_Text, int> _scoresMap;
    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        Car.PlayerEntered += OnPlayerEnteredCar;
    }

    private void OnDisable()
    {
        Car.PlayerEntered += OnPlayerEnteredCar;
    }

    private void Start()
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
        _backgroundWrapper.gameObject.SetActive(true);

        StartCoroutine(Animating());
    }

    private IEnumerator Animating()
    {
        foreach (var item in _scoresMap)
        {
            _currentCoroutine = StartCoroutine(ScoreAnimating(item.Key, item.Value, 1));

            while (_currentCoroutine != null)
            {
                yield return null;
            }
        }

        _rangText.text += "KILLER";
        _pressAnyButtonText.gameObject.SetActive(true);
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
        _currentCoroutine = null;
    }
}
