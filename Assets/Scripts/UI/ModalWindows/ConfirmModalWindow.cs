using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmModalWindow : ModalWindow
{
    [SerializeField] private Image _blurPanel;
    [SerializeField] private Text _headerText;
    [SerializeField] private Button _acceptButton;

    public override void Show()
    {
        _blurPanel.gameObject.SetActive(true);
    }

    public override void Close()
    {
        _blurPanel.gameObject.SetActive(true);
    }
}
