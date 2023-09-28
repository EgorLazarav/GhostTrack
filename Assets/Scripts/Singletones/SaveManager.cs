using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static int CurrentLevel { get; private set; }
    public static float TotalScore { get; private set; }

    private const string Level = nameof(Level);
    private const string Score = nameof(Score);

    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        CurrentLevel = PlayerPrefs.GetInt(Level);
        TotalScore = PlayerPrefs.GetFloat(Score);
    }

    public static void ResetSaves()
    {
        CurrentLevel = 0;
        TotalScore = 0;

        PlayerPrefs.SetFloat(Score, TotalScore);
        PlayerPrefs.SetInt(Level, CurrentLevel);
    }

    public static void IncreaseTotalScore(float amount)
    {
        TotalScore += amount;

        PlayerPrefs.SetFloat(Score, TotalScore);
    }

    public static void TrySaveLevel(int level)
    {
        if (CurrentLevel < level)
            CurrentLevel = level;

        PlayerPrefs.SetInt(Level, CurrentLevel);
    }
}
