using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewGameButton : ButtonClickHandler
{
    protected override void OnButtonClicked()
    {
        print("New Game");
    }
}
