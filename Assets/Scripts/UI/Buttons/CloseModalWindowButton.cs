using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseModalWindowButton : ButtonClickHandler
{
    [SerializeField] private ModalWindow _window;

    protected override void OnButtonClicked()
    {
        _window.Close();
    }
}
