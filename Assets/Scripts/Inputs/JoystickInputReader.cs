using System.Collections;
using UnityEngine;


public class JoystickInputReader : IInputReader
{
    private Joystick joystick;
    public float Horizontal { get; private set; }   
    public float Vertical { get; private set; }

    public JoystickInputReader(Joystick joystick)
    {
        this.joystick = joystick;
    }
    public void ReadInputs()
    {
        Vector2 joystickDirection = joystick.Direction;
        Horizontal = joystickDirection.x;
        Vertical = joystickDirection.y;       
    }
}
