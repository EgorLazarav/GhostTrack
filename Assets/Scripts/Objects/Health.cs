using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private bool _isInvulnerable = false;
    [SerializeField] private ParticleSystem _hitVFX;
    [SerializeField] private AudioClip _hitSFX;

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
        if (_isInvulnerable)
            return;

        if (_damageReducer != null && ignoreArmor == false)
            amount = _damageReducer.Reduce(amount);

        _currentPercent = Mathf.Clamp(_currentPercent - amount, 0, _currentPercent);

        AudioManager.Instance.PlayHitSFX();
        var hitVFX = Instantiate(_hitVFX, transform.position, Quaternion.identity);
        Destroy(hitVFX, hitVFX.main.duration);

        if (_currentPercent <= 0)
            Over?.Invoke();
    }
}
