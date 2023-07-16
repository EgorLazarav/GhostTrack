using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private Image _backgroundWrapper;

    [Header("Texts")]
    [SerializeField] private Text _killScoreText;
    [SerializeField] private Text _killStreakScoreText;
    [SerializeField] private Text _timeScoreText;
    [SerializeField] private Text _accuracyScoreText;
    [SerializeField] private Text _totalScoreText;
    [SerializeField] private Text _rangText;

    private void OnEnable()
    {
        Car.PlayerEntered += OnPlayerEnteredCar;
    }

    private void OnDisable()
    {
        Car.PlayerEntered += OnPlayerEnteredCar;
    }

    private void OnPlayerEnteredCar()
    {
        _backgroundWrapper.gameObject.SetActive(true);
    }
}
