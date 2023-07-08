using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [Header("Combat SFX")]
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private AudioClip _punchSFX;
    [SerializeField] private AudioClip _emptyClipSFX;

    private Coroutine _emptyClipSoundPlayingCoroutine;

    public static AudioManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayPunchSFX()
    {
        _effectsSource.PlayOneShot(_punchSFX);
    }

    public void PlayHitSFX()
    {
        _effectsSource.PlayOneShot(_hitSFX);
    }

    public void PlayShotSFX(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void TryPlayEmptyClipSFX()
    {
        if (_emptyClipSoundPlayingCoroutine != null)
            return;

        _effectsSource.PlayOneShot(_emptyClipSFX);
        _emptyClipSoundPlayingCoroutine = StartCoroutine(EmptyClipSoundPlaying());
    }

    private IEnumerator EmptyClipSoundPlaying()
    {
        float delay = _emptyClipSFX.length;

        yield return new WaitForSeconds(delay);

        _emptyClipSoundPlayingCoroutine = null;
    }
}
