using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames
{
    MainMenu,
    Tutorial
}

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneNames.MainMenu.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadSceneAsync(SceneNames.Tutorial.ToString());
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
