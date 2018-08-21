using System;
using Unity;
using NASA.MarsRover.VicRoads.Main.processors;
using Unity.Resolution;

namespace NASA.MarsRover.VicRoads.Main
{
    class Program
    {
        private static FieldAreaProcessor _fieldArea;

        static Program()
        {
            _fieldArea = null;
        }

        static void Main(string[] args)
        {
            try
            {
                IUnityContainer objectContainer = new UnityContainer();

                Console.WriteLine("Input:");

                int[] fieldcoordinates = Array.ConvertAll(Console.ReadLine().ToUpper()?.Split(" "), int.Parse) ??
                                         throw new ArgumentNullException("Console.ReadLine().Split(\" \")");
                objectContainer.RegisterType<IFieldAreaProcessor, FieldAreaProcessor>();
                _fieldArea =
                    objectContainer.Resolve<FieldAreaProcessor>(GenerateFieldAreaObject(fieldcoordinates));

                string[] rover1InitPosition = Console.ReadLine().ToUpper()?.Split(" ") ??
                                              throw new ArgumentNullException("Console.ReadLine().Split(\" \")");
                objectContainer.RegisterType<IRoverProcessor, RoverProcessor>();
                RoverProcessor rover =
                    objectContainer.Resolve<RoverProcessor>(GenerateRoverProcessorObject(rover1InitPosition));
                rover.ReadRoverCommands(Console.ReadLine().ToUpper());

                string[] rover2InitPosition = Console.ReadLine().ToUpper()?.Split(" ") ??
                                              throw new ArgumentNullException("Console.ReadLine().Split(\" \")");
                RoverProcessor rover2 = objectContainer.Resolve<RoverProcessor>(
                    GenerateRoverProcessorObject(rover2InitPosition));
                rover2.ReadRoverCommands(Console.ReadLine().ToUpper());
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("Output:");
                Console.WriteLine(rover.ToPositionString());
                Console.WriteLine(rover2.ToPositionString());
                Console.WriteLine(Environment.NewLine);
                Console.Write("Press <enter> to exit...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(Environment.NewLine);
                Console.Write("Press <enter> to exit...");
                Console.ReadKey();
            }
        }

        private static ParameterOverrides GenerateFieldAreaObject(int[] fieldcoordinates)
        {
            if (fieldcoordinates.Length == 2)
            {
                return new ParameterOverrides { { "x", fieldcoordinates[0] }, { "y", fieldcoordinates[1] } };
            }
            else
            {
                throw new Exception("Invalid co-ordinate value. Correct value should be in X Y format");
            }
        }

        private static ParameterOverrides GenerateRoverProcessorObject(string[] roverPosition)
        {
            if (roverPosition.Length == 3)
            {
                return new ParameterOverrides { { "x", Convert.ToInt32(roverPosition[0]) }, { "y", Convert.ToInt32(roverPosition[1]) }, { "direction", roverPosition[2] }, { "area", _fieldArea } };
            }
            else
            {
                throw new Exception("Invalid co-ordinate value. Correct value should be in X Y  format");
            }
        }
    }
}
