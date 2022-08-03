using System.Collections;
using UnityEngine;


public class KeyboardInputReader : IInputReader
{
    public float Horizontal { get; private set; }

    public float Vertical { get; private set; }

    public void ReadInputs()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }
}
