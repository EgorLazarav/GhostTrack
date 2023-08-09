using System;
using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    public static GamePauseManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Unpause(bool state)
    {
        Time.timeScale = Convert.ToInt32(state);
    }
}