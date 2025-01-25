using System;
using System.Collections.Generic;
using DesignPatterns;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class VarietyPool : DesignPatterns.Singleton<VarietyPool>
{
    protected override bool dontDestroyOnLoad => false;
    [ReadOnly]
    [OdinSerialize]
    private Dictionary<Type, Poolable> prefabs = new Dictionary<Type, Poolable>();
    private Dictionary<Type, Pool<Poolable>> pools = new Dictionary<Type, Pool<Poolable>>();

    [Button]
    public void AddPrefab(Poolable newPrefab)
    {
        Type newType = newPrefab.GetType();
        if (prefabs.ContainsKey(newType)) return;
        prefabs.Add(newType, newPrefab);
    }


    public T Get<T>(bool needsPrefab = true) where T : Poolable
    {
        if (pools.ContainsKey(typeof(T)))
        {
            return (T)pools[typeof(T)].Get();
        }
        if (prefabs.ContainsKey(typeof(T)))
        {
            var prefab = prefabs[typeof(T)];
            Pool<Poolable> newPool = new Pool<Poolable>(defaultCapacity: 350, maxSize: 1000, newObjectName: typeof(T).ToString(), prefab: prefab, parent: transform);
            pools.Add(typeof(T), newPool);
            return (T)pools[typeof(T)].Get();
        }
        if (!needsPrefab)
        {
            Pool<Poolable> newPool = new Pool<Poolable>(newObjectName: typeof(T).ToString(), parent: transform);
            pools.Add(typeof(T), newPool);
            return (T)pools[typeof(T)].Get();
        }
        throw new ArgumentException("Can't get an instance of the given type because no prefab is provided to the factory" +
                                "Try adding one or setting \"needsPrefab\" to false so a simple instance can be created.");
    }
}
