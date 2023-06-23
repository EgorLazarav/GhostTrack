using UnityEngine;

public class CloseWindowButton : ButtonClickHandler
{
    [SerializeField] private GameObject _closableWindow;

    protected override void OnButtonClicked()
    {
        _closableWindow.SetActive(false);
    }
}
