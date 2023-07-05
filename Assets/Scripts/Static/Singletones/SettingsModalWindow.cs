using UnityEngine;
using UnityEngine.UI;

public class SettingsModalWindow : MonoBehaviour
{
    [SerializeField] private Image _blurPanel;

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
        _blurPanel.gameObject.SetActive(true);
    }

    public void Close()
    {
        _blurPanel.gameObject.SetActive(false);
    }
}
