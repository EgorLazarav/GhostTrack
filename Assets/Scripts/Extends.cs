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

    public static void ShiftArray(this Array array)
    {
        var temp = array.GetValue(array.Length - 1);
        Array.Copy(array, 0, array, 1, array.Length - 1);
        array.SetValue(temp, 0);
    }

    public static Vector3 RandomTranslate(this Vector3 position, float maxDistance)
    {
        float randomX = UnityEngine.Random.Range(-maxDistance, maxDistance);
        float randomY = UnityEngine.Random.Range(-maxDistance, maxDistance);
        Vector3 translateVector = new Vector3(randomX, randomY, 0);

        return position + translateVector;
    }
}
