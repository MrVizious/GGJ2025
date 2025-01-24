using System.Collections.Generic;
using DesignPatterns;
using Sirenix.OdinInspector;
using UnityEngine;

public class VarietyPool : SerializedMonoBehaviour
{
    [ReadOnly]
    [ShowInInspector]
    Dictionary<string, Pool<Poolable>> pools = new Dictionary<string, Pool<Poolable>>();

    [Button]
    public void AddPrefab(Poolable newPoolable)
    {
        pools.Add(newPoolable.GetType().ToString(), new Pool<Poolable>(prefab: newPoolable));
    }

    public void AddPrefab<T>(T newPoolable) where T : Poolable
    {

    }

}
