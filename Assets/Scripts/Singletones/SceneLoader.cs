using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames
{
    MainMenu,
    Tutorial,
    NightClub
}

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; } = null;

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
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        var operation = SceneManager.LoadSceneAsync(SceneNames.MainMenu.ToString());
        StartCoroutine(LoadingMainMenu(operation));
    }

    public void LoadNewGame()
    {
        var operation = SceneManager.LoadSceneAsync(SceneNames.Tutorial.ToString());
        StartCoroutine(LoadingNewLevel(operation)); 
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TryLoadNextLevel()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            var operation = SceneManager.LoadSceneAsync(SceneNames.MainMenu.ToString());
            StartCoroutine(LoadingMainMenu(operation));
        }
        else
        {
            var operation = SceneManager.LoadSceneAsync(nextSceneIndex);
            StartCoroutine(LoadingNewLevel(operation));
        }
    }

    private IEnumerator LoadingNewLevel(AsyncOperation operation)
    {
        while (operation.isDone == false)
        {
            yield return null;
        }

        AudioManager.Instance.PlayRandomInGameTheme();
    }

    private IEnumerator LoadingMainMenu(AsyncOperation operation)
    {
        while (operation.isDone == false)
        {
            yield return null;
        }

        AudioManager.Instance.PlayRandomMenuTheme();
    }
}
