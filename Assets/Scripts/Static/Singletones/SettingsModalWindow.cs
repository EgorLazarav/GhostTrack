using UnityEngine;
using UnityEngine.UI;

public class SettingsModalWindow : MonoBehaviour
{
    [SerializeField] private Image _settingsPanel;
    [SerializeField] private Image _audioPanel;
    [SerializeField] private Image _keyBindings;

    public static SettingsModalWindow Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Show()
    {
        _settingsPanel.gameObject.SetActive(true);
    }

    public void Close()
    {
        _settingsPanel.gameObject.SetActive(false);
        _audioPanel.gameObject.SetActive(false);
        _keyBindings.gameObject.SetActive(false);
    }
}
