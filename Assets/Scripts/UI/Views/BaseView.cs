using FishNet.Object;
using System;
using System.Collections;
using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    public bool IsInitialized { get; private set; }
    [property: SerializeField] public string Tag { get; private set; }
    virtual public void Init()
    {
        IsInitialized = true;   
    }
    public void Show(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    // Создание,Уничтожение
    // Show
    // Action OnCreate
}
