using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class PlayerHUDCanvas : MonoBehaviour
{
    public static PlayerHUDCanvas Instance { get; private set; }
    public Canvas Canvas { get; private set; }
    void Awake()
    {
        Instance = this;
        Canvas = GetComponent<Canvas>();    
    }
}
