using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ContinueButton : ButtonClickHandler
{
    private void Start()
    {
        // Button.interactable = PlayerData.CurrentLevel != 0;

        var components = GetComponents<AnimatedUI>();
        components.ToList().ForEach(c => c.enabled = Button.interactable);
    }

    protected override void OnButtonClicked()
    {
        print("continue");
    }
}
