using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Extends
{
    public static Color SetAlpha(this Color color, float newAlpha)
    {
        if (newAlpha < 0 || newAlpha > 1)
            throw new ArgumentOutOfRangeException();

        return new Color(color.r, color.g, color.b, newAlpha);
    }

    public static void ShiftArrayToRight(this Array array, int step = 1)
    {
        var temp = array.GetValue(array.Length - step);
        Array.Copy(array, 0, array, step, array.Length - step);
        array.SetValue(temp, 0);
    }

    public static Vector3 RandomTranslateXY(this Vector3 position, float maxDistance)
    {
        float randomX = UnityEngine.Random.Range(-maxDistance, maxDistance);
        float randomY = UnityEngine.Random.Range(-maxDistance, maxDistance);
        Vector3 translateVector = new Vector3(randomX, randomY, 0);

        return position + translateVector;
    }

    public static void SetRandomPosition(this Transform transform)
    {
        float randomXOnScreen = UnityEngine.Random.Range(0, Screen.width);
        float randomYOnScreen = UnityEngine.Random.Range(0, Screen.height);
        Vector3 randomPointOnScreen = new Vector3(randomXOnScreen, randomYOnScreen, Camera.main.depth);

        transform.position = Camera.main.ScreenToWorldPoint(randomPointOnScreen);
    }
}
