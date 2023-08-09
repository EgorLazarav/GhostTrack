using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private bool _soundEnabled = false;

    [Header("Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [Header("Music")]
    [SerializeField] private AudioClip[] _mainMenuThemes;
    [SerializeField] private AudioClip[] _inGameThemes;
    [SerializeField] private AudioClip[] _endLevelThemes;

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

        if (!_soundEnabled)
            AudioListener.volume = 0;
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

    public void PlayRandomEndLevelTheme()
    {
        _musicSource.clip = _endLevelThemes[Random.Range(0, _endLevelThemes.Length)];
        _musicSource.Play();
    }

    public void StopPlaybackAll()
    {
        _musicSource.Pause();
        _effectsSource.Pause();
    }

    public void ContinuePlaybackAll()
    {
        _musicSource.UnPause();
        _effectsSource.UnPause();
    }

    public void StopPlaybackEffect()
    {
        _effectsSource.Stop();
    }
}
