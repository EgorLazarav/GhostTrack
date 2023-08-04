using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shotVFX;

    public static VFXManager Instance { get; private set; }

    private List<ParticleSystem> _shotsVFX = new List<ParticleSystem>();

    private void Awake()
    {
        Instance = this;
    }

    public ParticleSystem GetShotVFX()
    {
        var shotVFX = _shotsVFX.FirstOrDefault(v => v.gameObject.activeSelf == false);

        if (shotVFX == null)
        {
            shotVFX = Instantiate(_shotVFX);
            _shotsVFX.Add(shotVFX);
        }

        StartCoroutine(Deactivating(shotVFX));
        shotVFX.gameObject.SetActive(true);

        return shotVFX;
    }

    private IEnumerator Deactivating(ParticleSystem particleSystem)
    {
        float duration = particleSystem.main.duration;

        yield return new WaitForSeconds(duration);

        particleSystem.gameObject.SetActive(false);
    }
}
