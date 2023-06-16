using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BloodVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _vfx;

    public void Play()
    {
        _vfx.Play();
    }

    private void Update()
    {
        gameObject.SetActive(_vfx.isPlaying);
    }
}
