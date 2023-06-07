using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private float _animationSpeed;

    private void Start()
    {
        StartCoroutine(Animating(GetComponent<Image>()));
    }

    private IEnumerator Animating(Image image)
    {
        while (true)
        {
            foreach (var frame in _frames)
            {
                image.sprite = frame;
                yield return new WaitForSeconds(1/_animationSpeed);
            }
        }
    }
}
