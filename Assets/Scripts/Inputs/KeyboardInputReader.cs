using System.Collections;
using UnityEngine;


public class KeyboardInputReader : IInputReader
{
    #region MovementProperties

    public float Horizontal { get; private set; }

    public float Vertical { get; private set; }

    #endregion

    private bool isShowingScoreboard = false;

    public void ReadMovementInputs()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }
    public bool IsShowScoreboardButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            isShowingScoreboard = true;
        if (Input.GetKeyUp(KeyCode.Tab))
            isShowingScoreboard = false;

        return isShowingScoreboard;
    }
}
