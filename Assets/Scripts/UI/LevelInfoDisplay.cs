using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private CanvasGroup _canvasGroup;  

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
        _canvasGroup.alpha = 0;
    }

    private void OnGameOver(LevelStates state)
    {
        LevelManager.GameOver -= OnGameOver;
        _canvasGroup.alpha = 1;

        switch (state)
        {
            case LevelStates.Victory:
                _text.text = "GO TO CAR";
                break;

            case LevelStates.Defeat:
                _text.text = "'R' TO RESTART";
                break;
        }
    }
}
