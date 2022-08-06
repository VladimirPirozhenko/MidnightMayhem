using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IInputReader
{
    public float Horizontal { get; }
    public float Vertical { get; }
    public void ReadMovementInputs();
    public bool IsShowScoreboardButtonPressed();
}

