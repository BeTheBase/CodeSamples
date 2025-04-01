using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _container;
    private readonly Queue<T> _pool;
    private readonly bool _persistent;

    /// <summary>
    /// Initializes the pool with a prefab, an initial size, an optional container, and an optional persistent flag.
    /// If persistent is true, the container and all pooled objects are marked as DontDestroyOnLoad.
    /// </summary>
    /// <param name="prefab">The prefab to instantiate.</param>
    /// <param name="initialSize">The number of objects to pre-instantiate.</param>
    /// <param name="container">An optional parent transform for the objects.</param>
    /// <param name="persistent">If true, the container and objects are not destroyed on scene load.</param>
    public GenericObjectPool(T prefab, int initialSize, Transform container = null, bool persistent = false)
    {
        _prefab = prefab;
        _persistent = persistent;

        // Create a new container if none is provided.
        if (container == null)
        {
            GameObject containerGO = new GameObject($"{prefab.name}_PoolContainer");
            container = containerGO.transform;
            if (_persistent)
                Object.DontDestroyOnLoad(containerGO);
        }
        _container = container;

        _pool = new Queue<T>();
        for (int i = 0; i < initialSize; i++)
        {
            T instance = Object.Instantiate(_prefab, _container);
            if (_persistent)
                Object.DontDestroyOnLoad(instance.gameObject);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }

    /// <summary>
    /// Returns an object from the pool. If the pool is empty, a new object is instantiated.
    /// </summary>
    public T GetObject()
    {
        T instance;
        if (_pool.Count > 0)
        {
            instance = _pool.Dequeue();
        }
        else
        {
            instance = Object.Instantiate(_prefab, _container);
        }
        // Ensure the instance is parented to the container and activated.
        instance.transform.SetParent(_container, false);
        instance.gameObject.SetActive(true);
        return instance;
    }

    /// <summary>
    /// Returns an object back to the pool.
    /// </summary>
    /// <param name="instance">The object to return to the pool.</param>
    public void ReturnObject(T instance)
    {
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }
}
