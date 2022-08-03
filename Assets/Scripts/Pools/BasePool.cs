using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePool<T> :  NetworkBehaviour where T : NetworkBehaviour
{
    [SerializeField] private int capacity;
    [SerializeField] private T prefab;
    private ObjectPool<T> pool; 
    private void Awake()
    {
        pool = new ObjectPool<T>(CreatePoolAction, GetAction, ReturnAction, capacity);
    }
    private T CreatePoolAction()
    {
        T instance = Instantiate(prefab);
        instance.gameObject.SetActive(false);
        instance.transform.SetParent(gameObject.transform, false);
        return instance;
    }
    private void GetAction(T instance)
    {
        instance.gameObject.SetActive(true);
    }
    private void ReturnAction(T instance)
    {
        instance.gameObject.SetActive(false);
    }
    public T GetFromPool()
    {
        return pool.Get();
    }
    public void ReturnToPool(T instance)
    {
        pool.ReturnToPool(instance);
    }
}

