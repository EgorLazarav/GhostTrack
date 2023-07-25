using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Rangs
{
    Pussyboy,
    Amateur,
    Killer,
    Ghost
}

public class EndLevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerScoreHandler _playerScoreHandler;
    [SerializeField] private Image _curtainPanel;
    [SerializeField] private Image _scoresWrapper;
    [SerializeField] private AudioClip _scoreLoopSFX;

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

        _scoresMap.Add(_killScoreText, (int)_playerScoreHandler.KillScore);
        _scoresMap.Add(_killStreakScoreText, (int)_playerScoreHandler.KillStreakScore);
        _scoresMap.Add(_timeScoreText, (int)_playerScoreHandler.TimeScore);
        _scoresMap.Add(_accuracyScoreText, (int)_playerScoreHandler.AccuracyScore);
        _scoresMap.Add(_totalScoreText, (int)_playerScoreHandler.TotalScore);
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

        _scoresWrapper.gameObject.SetActive(true);

        foreach (var item in _scoresMap)
        {
            _animatingScoreTextCoroutine = StartCoroutine(ScoreAnimating(item.Key, item.Value));

            while (_animatingScoreTextCoroutine != null)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame();
        }

        float result = (float)_playerScoreHandler.TotalScore / (float)_playerScoreHandler.MaxScore;
        _rangText.gameObject.SetActive(true);
        _rangText.text += DetermineRang(result);

        // после анимации ранга вылазит
        _pressAnyButtonText.gameObject.SetActive(true);

        // тут нужна ассихронная загрузка с экраном натса
        StartCoroutine(WaitingForRestart());
    }

    private static string DetermineRang(float result)
    {
        if (result >= 0.9f)
            return Rangs.Ghost.ToString();
        else if (result >= 0.7f)
            return Rangs.Killer.ToString();
        else if (result >= 0.5f)
            return Rangs.Amateur.ToString();
        else
            return Rangs.Pussyboy.ToString();
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

    private IEnumerator ScoreAnimating(TMP_Text text, int value)
    {
        float currentValue = 0;
        string defaultText = text.text;

        text.text = defaultText + currentValue;
        text.gameObject.SetActive(true);
        AudioManager.Instance.StartPlaySoundLoop(_scoreLoopSFX);

        while (currentValue != value)
        {
            if (Input.anyKeyDown)
            {
                yield return new WaitForEndOfFrame();
                break;
            }

            currentValue = Mathf.Clamp(currentValue += value * Time.deltaTime, currentValue, value);
            text.text = defaultText + (int)currentValue;

            yield return null;  
        }

        AudioManager.Instance.StopPlaySoundLoop();
        text.text = defaultText + value;
        _animatingScoreTextCoroutine = null;
    }
}
