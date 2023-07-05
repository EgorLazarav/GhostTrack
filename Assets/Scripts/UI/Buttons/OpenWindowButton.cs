using UnityEngine;

public class OpenWindowButton : ButtonClickHandler
{
    [SerializeField] private GameObject _openningWindow;

    protected override void OnButtonClicked()
    {
        _openningWindow.SetActive(true);
    }
}
