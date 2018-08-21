using System;
using System.Collections.Generic;
using System.Text;

namespace NASA.MarsRover.VicRoads.Main.processors
{
    public class FieldAreaProcessor : IFieldAreaProcessor
    {
        private int _x;
        private int _y;

        public int Xcoordinate
        {
            get => _x;
            set => _x = value;
        }
        public int Ycoordinate
        {
            get => _y;
            set => _y = value;
        }

        public FieldAreaProcessor(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                Xcoordinate = x;
                Ycoordinate = y;
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid co-ordinate value: {0}, {1}", x, y));
            }
        }
    }
}
