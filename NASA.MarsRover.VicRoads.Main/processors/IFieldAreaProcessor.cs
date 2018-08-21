using System;
using System.Collections.Generic;
using System.Text;

namespace NASA.MarsRover.VicRoads.Main.processors
{
    public interface IFieldAreaProcessor
    {
        int Xcoordinate { get; set; }
        int Ycoordinate { get; set; }
    }
}
