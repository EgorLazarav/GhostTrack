using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalScoreText;

    private void Start()
    {
        _totalScoreText.text = "Total Score:" + (int)SaveManager.TotalScore;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SaveManager.ResetSaves();
            SceneLoader.Instance.LoadMainMenu();
        }
    }
}
