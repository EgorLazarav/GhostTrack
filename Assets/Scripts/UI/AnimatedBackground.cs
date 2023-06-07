using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private Image _background;
    [SerializeField] private float _animationSpeed;

    private void Start()
    {
        StartCoroutine(Animating());
    }

    IEnumerator Animating()
    {
        while (true)
        {
            foreach(var frame in _frames)
            {
                _background.sprite = frame;
                yield return new WaitForSeconds(Time.deltaTime / _animationSpeed);
            }
        }
    }
}
