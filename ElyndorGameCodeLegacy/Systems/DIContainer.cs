using System;
using System.Collections.Generic;
using UnityEngine;

public enum Lifetime
{
    Singleton,
    Transient
}

public static class DIContainer
{
    private static readonly Dictionary<Type, object> _singletonInstances = new Dictionary<Type, object>();
    private static readonly Dictionary<Type, Func<object>> _factories = new Dictionary<Type, Func<object>>();
    private static readonly Dictionary<Type, Lifetime> _lifetimes = new Dictionary<Type, Lifetime>();

    public static void Register<T>(T instance) where T : class
    {
        var type = typeof(T);
        if (_singletonInstances.ContainsKey(type))
        {
            Debug.LogWarning($"Service {type} is already registered as singleton.");
            return;
        }
        _singletonInstances[type] = instance;
        _lifetimes[type] = Lifetime.Singleton;
    }

    public static void Register<T>(Func<T> factory, Lifetime lifetime = Lifetime.Singleton) where T : class
    {
        var type = typeof(T);
        if (_factories.ContainsKey(type))
        {
            Debug.LogWarning($"Service {type} is already registered.");
            return;
        }
        _factories[type] = () => factory();
        _lifetimes[type] = lifetime;
    }

    public static T Resolve<T>() where T : class
    {
        var type = typeof(T);

        if (_singletonInstances.ContainsKey(type))
        {
            return (T)_singletonInstances[type];
        }

        if (_factories.ContainsKey(type))
        {
            if (_lifetimes[type] == Lifetime.Singleton)
            {
                T instance = (T)_factories[type]();
                _singletonInstances[type] = instance;
                return instance;
            }
            else
            {
                return (T)_factories[type]();
            }
        }

        throw new Exception($"Service of type {type} is not registered. Did you forget to register it?");
    }

    public static void Clear()
    {
        _singletonInstances.Clear();
        _factories.Clear();
        _lifetimes.Clear();
    }
}
