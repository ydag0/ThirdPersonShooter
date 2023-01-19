using System.Collections.Generic;
using UnityEngine;
public static class Extensions 
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        if(list.Count == 0)
            throw new System.IndexOutOfRangeException("List is Empty");
        return list[Random.Range(0, list.Count)];
    }
}
