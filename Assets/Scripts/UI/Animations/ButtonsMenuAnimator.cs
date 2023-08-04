using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonsMenuAnimator : AnimatedUI
{
    private bool _isEnded = false;

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

        _isEnded = true;
    }

    private void Update()
    {
        if (_isEnded)
            return;

        if (Input.GetKeyDown(PlayerInput.Instance.KeysMap[Keys.Skip]))
        {
            StopAllCoroutines();
            _isEnded = true;

            Button[] buttons = transform.GetComponentsInChildren<Button>(includeInactive: true);
            buttons.ToList().ForEach(button => button.gameObject.SetActive(true));
            buttons.ToList().ForEach(button => button.transform.localScale = Vector3.one);
        }
    }
}
