using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [Header("Combat SFX")]
    [SerializeField] private AudioClip _punchHittedSFX;
    [SerializeField] private AudioClip _punchNotHittedSFX;

    public static AudioManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayPunchSFX(bool isPunchHitted)
    {
        if (isPunchHitted)
            _effectsSource.PlayOneShot(_punchHittedSFX);
        else
            _effectsSource.PlayOneShot(_punchNotHittedSFX);
    }
}
