using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitVFX;
    [SerializeField] private AudioClip _deathSFX;

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
            var hitVFX = Instantiate(_hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFX.gameObject, hitVFX.main.duration);
            AudioManager.Instance.PlaySound(_deathSFX);
            Over?.Invoke();
        }
    }
}
