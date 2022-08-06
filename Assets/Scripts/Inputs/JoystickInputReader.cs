using System.Collections;
using UnityEngine;


public class JoystickInputReader : IInputReader
{
    #region ElementsHUD

    private Joystick joystick;

    #endregion

    #region MovementProperties

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    #endregion

    public JoystickInputReader(Joystick joystick)
    {
        this.joystick = joystick;
    }

    public void ReadMovementInputs()
    {
        Vector2 joystickDirection = joystick.Direction;
        Horizontal = joystickDirection.x;
        Vertical = joystickDirection.y;
    }

    public bool IsShowScoreboardButtonPressed()
    {
        throw new System.NotImplementedException();
    }
}
