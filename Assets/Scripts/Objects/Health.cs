using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitVFX;
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] private SpriteRenderer[] _bloodSprites;

    private float _currentPercent = 100;
    private DamageReducer _damageReducer;

    public event UnityAction Over;

    private void Start()
    {
        if (TryGetComponent(out DamageReducer damageReducer))
            _damageReducer = damageReducer;
    }

    public void ApplyDamage(float amount = 100, bool ignoreArmor = false)
    {
        if (CheatCodeActivator.IsPlayerInvulnerable && gameObject.TryGetComponent(out PlayerController player))
            return;

        if (_damageReducer != null && ignoreArmor == false)
            amount = _damageReducer.Reduce(amount);

        _currentPercent = Mathf.Clamp(_currentPercent - amount, 0, _currentPercent);
      
        if (_currentPercent <= 0)
        {
            var blood = Instantiate(_bloodSprites[Random.Range(0, _bloodSprites.Length)], transform.position, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
            blood.transform.DOScale(Random.Range(0.6f, 1.2f), Random.Range(0.3f, 0.6f));

            var hitVFX = Instantiate(_hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFX.gameObject, hitVFX.main.duration);

            AudioManager.Instance.PlaySound(_deathSFX);
            Over?.Invoke();
        }
    }
}
