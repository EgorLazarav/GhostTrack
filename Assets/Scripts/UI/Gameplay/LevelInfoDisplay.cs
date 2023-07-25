using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private LevelCompleteHandler _levelHandler;

    private const string PlayerDiedText = "'R' TO RESTART";
    private const string LevelCompletedText = "GO TO CAR";
    private const string NoAmmoText = "NO AMMO";
    private const string BulletsText = "|||";

    public void Init(int startWeaponBulletsCount)
    {
        SetBulletsText(startWeaponBulletsCount);
    }

    private void OnEnable()
    {
        PlayerController.Died += OnPlayerDied;
        PlayerCombat.BulletsChanged += OnPlayerBulletsChanged;
        _levelHandler.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        PlayerController.Died -= OnPlayerDied;
        PlayerCombat.BulletsChanged -= OnPlayerBulletsChanged;
        _levelHandler.LevelCompleted -= OnLevelCompleted;
    }

    private void OnPlayerBulletsChanged(int bulletsCount)
    {
        SetBulletsText(bulletsCount);
    }

    private void SetBulletsText(int bulletsCount)
    {
        if (bulletsCount == 0)
            _text.text = NoAmmoText;
        else
            _text.text = BulletsText + bulletsCount;
    }

    private void OnPlayerDied()
    {
        PlayerCombat.BulletsChanged -= OnPlayerBulletsChanged;
        _text.text = PlayerDiedText;
    }

    private void OnLevelCompleted()
    {
        PlayerCombat.BulletsChanged -= OnPlayerBulletsChanged;
        _text.text = LevelCompletedText;
    }
}