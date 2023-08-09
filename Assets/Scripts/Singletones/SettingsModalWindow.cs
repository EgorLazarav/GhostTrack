using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsModalWindow : MonoBehaviour
{
    [SerializeField] private Image _settingsPanel;
    [SerializeField] private Image _audioPanel;
    [SerializeField] private Image _keyBindings;

    private bool _isActive = false;

    public static SettingsModalWindow Instance { get; private set; }

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isActive = !_isActive;

            if (_isActive)
                Show();
            else
                Close();

            if (GamePauseManager.Instance != null)
                GamePauseManager.Instance.Unpause(!_isActive);
        }
    }

    public void Show()
    {
        _settingsPanel.gameObject.SetActive(true);
        _isActive = true;
    }

    public void Close()
    {
        _settingsPanel.gameObject.SetActive(false);
        _audioPanel.gameObject.SetActive(false);
        _keyBindings.gameObject.SetActive(false);
        _isActive = false;
    }
}
