using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonsMenuAnimator : AnimatedUI
{
    protected override IEnumerator Animating()
    {
        Button[] buttons = transform.GetComponentsInChildren<Button>(includeInactive: true);
        buttons.ToList().ForEach(button => button.gameObject.SetActive(false));

        foreach (var button in buttons)
        {
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            button.gameObject.SetActive(true);
            button.transform.localScale = Vector3.zero;
            button.transform.DOScale(1, 1 / AnimationSpeed);
        }
    }
}
