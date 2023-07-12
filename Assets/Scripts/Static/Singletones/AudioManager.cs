using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [Header("Music")]
    [SerializeField] private AudioClip[] _mainMenuThemes;
    [SerializeField] private AudioClip[] _inGameThemes;

    public static AudioManager Instance { get; private set; } = null;

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

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value)
    {
        _musicSource.volume = value;
    }

    public void SetEffectsVolume(float value)
    {
        _effectsSource.volume = value;
    }

    public void PlayRandomMenuTheme()
    {
        _musicSource.clip = _mainMenuThemes[Random.Range(0, _mainMenuThemes.Length)];
        _musicSource.Play();
    }

    public void PlayRandomInGameTheme()
    {
        _musicSource.clip = _inGameThemes[Random.Range(0, _inGameThemes.Length)];
        _musicSource.Play();
    }
}
