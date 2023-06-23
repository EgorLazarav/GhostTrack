using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class Extends
{
    public static Color SetAlpha(this Color color, float newAlpha)
    {
        if (newAlpha < 0 || newAlpha > 1)
            throw new System.ArgumentOutOfRangeException("alpha can only be in range from 0 to 1");

        return new Color(color.r, color.g, color.b, newAlpha);
    }

    public static void ShiftArrayToRight(this System.Array array, int step = 1)
    {
        if (array == null)
            throw new System.ArgumentNullException("array is null");

        if (array.Length == 0)
            throw new System.ArgumentOutOfRangeException("array is empty");

        var temp = array.GetValue(array.Length - step);
        System.Array.Copy(array, 0, array, step, array.Length - step);
        array.SetValue(temp, 0);
    }

    public static void SetRandomPositionXY(this Transform transform)
    {
        float randomXOnScreen = Random.Range(0, Screen.width);
        float randomYOnScreen = Random.Range(0, Screen.height);
        Vector3 randomPointOnScreen = new Vector3(randomXOnScreen, randomYOnScreen, Camera.main.farClipPlane);

        transform.position = Camera.main.ScreenToWorldPoint(randomPointOnScreen);
    }

    public static void FollowTargetXY(this Transform transform, Transform target)
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    public static void DORotateZ(this Transform transform, float value, float time)
    {
        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + value), time);
    }
}
