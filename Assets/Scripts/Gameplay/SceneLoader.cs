using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int _lastSavedSceneIndex = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType<SceneLoader>().Length != 1)
        {
            Destroy(this);
        }

        if (PlayerPrefs.HasKey(Constants.LastSavedSceneIndex))
            _lastSavedSceneIndex = PlayerPrefs.GetInt(Constants.LastSavedSceneIndex);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(Constants.LastSavedSceneIndex, _lastSavedSceneIndex);
    }

    public static void LoadNewGameScene()
    {
        _lastSavedSceneIndex = 1;
        PlayerPrefs.SetInt(Constants.LastSavedSceneIndex, _lastSavedSceneIndex);
        SceneManager.LoadScene(_lastSavedSceneIndex);
    }

    public static void LoadLastSavedScene()
    {
        SceneManager.LoadScene(_lastSavedSceneIndex);
    }

    public static void LoadNextScene()
    {
        _lastSavedSceneIndex++;

        if (_lastSavedSceneIndex == SceneManager.sceneCountInBuildSettings)
            _lastSavedSceneIndex = 0;

        PlayerPrefs.SetInt(Constants.LastSavedSceneIndex, _lastSavedSceneIndex);
        SceneManager.LoadScene(_lastSavedSceneIndex);
    }

    public static void LoadMainMenuScene()
    {
        PlayerPrefs.SetInt(Constants.LastSavedSceneIndex, _lastSavedSceneIndex);
        SceneManager.LoadScene(0);
    }
}
