using System;

namespace AirForce
{
   [Flags]
   internal enum MoveMode
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }
}
