using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public static int CurrentLevel { get; private set; }
    public static int TotalScore { get; private set; }

    private const string Level = nameof(Level);

    public static void ResetSaves()
    {
        CurrentLevel = 0;
        TotalScore = 0;
    }

    public static void IncreaseTotalScore(int amount)
    {
        TotalScore += amount;
    }

    public static void TrySaveLevel(int level)
    {
        if (CurrentLevel < level)
            CurrentLevel = level;

        PlayerPrefs.SetInt(Level, CurrentLevel);
    }
}
