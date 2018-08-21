using System;
using System.Collections.Generic;
using System.Text;

namespace NASA.MarsRover.VicRoads.Main.processors
{
    public class RoverProcessor: IRoverProcessor
    {
        private int _roverPositionX;
        private int _roverPositionY;
        private String _direction;
        private FieldAreaProcessor _area;

        public int RoverPositionX
        {
            get => _roverPositionX;
            set => _roverPositionX = value;
        }

        public int RoverPositionY
        {
            get => _roverPositionY;
            set => _roverPositionY = value;
        }

        public String Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public FieldAreaProcessor Area
        {
            get => _area;
            set => _area = value;
        }

        public RoverProcessor(int x, int y, String direction, FieldAreaProcessor area)
        {
            if (area != null)
            {
                Area = area;
            }
            else
            {
                throw new ArgumentException(string.Format("Field area cannot be null"));
            }

            if (x >= 0 && y >= 0 && x <= area.Xcoordinate && y <= area.Ycoordinate)
            {
                RoverPositionX = x;
                RoverPositionY = y;
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid co-ordinate value: {0}, {1}", x, y));
            }

            if (direction.Equals("N") || direction.Equals("S") ||
                direction.Equals("E") || direction.Equals("W"))
            {
                Direction = direction;
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid direction value: {0}", direction));
            }
        }


        public String ReadRoverCommands(String commands)
        {
            foreach (char character in commands)
            {
                switch (character)
                {
                    case 'L':
                        TurnRoverToLeft();
                        break;
                    case 'R':
                        TurnRoverToRight();
                        break;
                    case 'M':
                        MoveRoverForward();
                        break;
                }
            }
            return Direction;
        }

        public void MoveRoverForward()
        {
            switch (Direction)
            {
                case "N":
                    RoverPositionY = RoverPositionY + 1;
                    break;
                case "E":
                    RoverPositionX = RoverPositionX + 1;
                    break;
                case "S":
                    RoverPositionY = RoverPositionY - 1;
                    break;
                case "W":
                    RoverPositionX = RoverPositionX - 1;
                    break;
            }
        }

        public void TurnRoverToRight()
        {
            bool flag = false;

            switch (Direction)
            {
                case "S":
                    if (!flag)
                    {
                        Direction = "W";
                        flag = true;
                    }
                    break;
                case "W":
                    if (!flag)
                    {
                        Direction = "N";
                        flag = true;
                    }
                    break;
                case "N":
                    if (!flag)
                    {
                        Direction = "E";
                        flag = true;
                    }
                    break;
                case "E":
                    if (!flag)
                    {
                        Direction = "S";
                        flag = true;
                    }
                    break;
            }
        }

        public void TurnRoverToLeft()
        {
            bool flag = false;
            switch (Direction)
            {
                case "S":
                    if (!flag)
                    {
                        Direction = "E";
                        flag = true;
                    }

                    break;
                case "W":
                    if (!flag)
                    {
                        Direction = "S";
                        flag = true;
                    }
                    break;
                case "N":
                    if (!flag)
                    {
                        Direction = "W";
                        flag = true;
                    }
                    break;
                case "E":
                    if (!flag)
                    {
                        Direction = "N";
                        flag = true;
                    }
                    break;
            }
        }

        public bool CheckRoverPositionOutsideField()
        {
            bool isInsideField = RoverPositionX > Area.Xcoordinate || RoverPositionY > Area.Ycoordinate;
            return isInsideField;
        }

        public string ToPositionString()
        {
            string printRoverPosition = string.Format("{0} {1} {2}", RoverPositionX, RoverPositionY, Direction);
            if (CheckRoverPositionOutsideField())
                printRoverPosition = 
                    string.Format("Rover outside the field, Rover position: {0} , field limit {1}",
                        printRoverPosition, Area.Xcoordinate.ToString() + "," + Area.Ycoordinate.ToString() );

            return printRoverPosition;
        }
    }
}
