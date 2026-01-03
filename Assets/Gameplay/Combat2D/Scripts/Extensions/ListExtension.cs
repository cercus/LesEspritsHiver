using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static T Draw<T>(this List<T> list)
    {
        if(list.Count == 0) return default;
        int r = Random.Range(0, list.Count);
        T t = list[r];
        list.Remove(t);
        return t;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
