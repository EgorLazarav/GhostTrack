using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ButtonClickHandler
{
    protected override void OnButtonClicked()
    {
        print("exit");
        ConfirmModalWindow.Instance.Show("Exit game?", Application.Quit);
    }
}
