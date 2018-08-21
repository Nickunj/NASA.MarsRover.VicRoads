using System;
using System.Collections.Generic;
using System.Text;

namespace NASA.MarsRover.VicRoads.Main.processors
{
    public interface IRoverProcessor
    {
        int RoverPositionX { get; set; }
        int RoverPositionY { get; set; }
        String Direction { get; set; }

        String ReadRoverCommands(String commands);
        void MoveRoverForward();
        void TurnRoverToRight();
        void TurnRoverToLeft();
        bool CheckRoverPositionOutsideField();
        string ToPositionString();
    }
}
