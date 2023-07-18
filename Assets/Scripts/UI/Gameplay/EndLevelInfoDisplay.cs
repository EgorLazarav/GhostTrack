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

    private TMP_Text[] _scores;
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
        _scores = new TMP_Text[] { _killScoreText, _killStreakScoreText, _timeScoreText, _accuracyScoreText, _totalScoreText };
        _scores.ToList().ForEach(s => s.gameObject.SetActive(false));
        _rangText.gameObject.SetActive(false);
    }

    private void OnPlayerEnteredCar()
    {
        _backgroundWrapper.gameObject.SetActive(true);

        StartCoroutine(Animating());
    }

    private IEnumerator Animating()
    {
        foreach (var score in _scores)
        {
            int scoreValue = 1500;
            _currentCoroutine = StartCoroutine(ScoreAnimating(score, scoreValue, scoreValue.ToString().Length));

            while (_currentCoroutine != null)
            {
                yield return null;
            }
        }

        _rangText.text += "\nKILLER";
    }

    private IEnumerator ScoreAnimating(TMP_Text text, int value, int speed)
    {
        int currentValue = 0;
        string defaultText = text.text;

        text.text = defaultText + currentValue;
        text.gameObject.SetActive(true);

        while (currentValue != value)
        {
            currentValue = Mathf.Clamp(currentValue += speed, currentValue, value);
            text.text = defaultText + currentValue;

            yield return null;  
        }

        _currentCoroutine = null;
    }
}
