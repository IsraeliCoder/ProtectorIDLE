using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static T GetRandom<T>(this T[] arr)
    {
        
        return arr[Random.Range(0, arr.GetLength(0))];

    }
}
