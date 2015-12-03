using System;
using System.Collections.Generic;
using AdventOfCode.Classes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {

        private static void DayOne()
        {

            Console.WriteLine("Counting floors...");
            string input = System.IO.File.ReadAllText("input.txt");

            Console.WriteLine("Input length: " + input.Length);

            int floor = 0;

            int firstbasement = -1;

            for (int x = 0; x < input.Length; x++)
            {
                char stairs = input[x];

                if (stairs == '(')
                {
                    floor += 1;
                }
                else if (stairs == ')')
                {
                    floor -= 1;
                }

                if (floor == -1 && firstbasement == -1)
                {
                    firstbasement = x + 1;
                }

            }

            Console.WriteLine("Your final floor is: " + floor);
            Console.WriteLine("The first time you hit the basement was: " + firstbasement);

            Console.ReadLine();
        }

        public static void DayTwo()
        {
            int length = 0;
            int width = 0;
            int height = 0;

            double face1 = 0;
            double face2 = 0;
            double face3 = 0;

            double totalSurface = 0;
            double totalRibbon = 0;

            string line;

            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");

            Console.WriteLine("Calculating total surface");

            while ((line = file.ReadLine()) != null)
            {
                string[] numbers = line.Split('x');

                length = Convert.ToInt32(numbers[0]);
                width = Convert.ToInt32(numbers[1]);
                height = Convert.ToInt32(numbers[2]);

                List<int> dimensions = new List<int>(new int[] { length, width, height });

                face1 = length * width;
                face2 = width * height;
                face3 = height * length;

                List<double> faces = new List<double>();

                faces.Add(face1);
                faces.Add(face2);
                faces.Add(face3);

                faces.Sort();

                double lowest = faces[0];

                double surface = 2 * face1 + 2 * face2 + 2 * face3 + lowest;

                int shortest, secondShortest;

                dimensions.Sort();

                shortest = dimensions[0];
                secondShortest = dimensions[1];

                double ribbon = 2 * shortest + 2 * secondShortest;

                ribbon += (length * width * height);

                totalSurface += surface;

                totalRibbon += ribbon;

            }

            Console.WriteLine("Total surface is: " + totalSurface);
            Console.WriteLine("Total ribbon is: " + totalRibbon);
            Console.ReadLine();

        }

        public static void DayThree()
        {
            string input = System.IO.File.ReadAllText("input.txt");

            int north = 0;
            int east = 0;

            House firstHouse = new House(north, east);
            Santa oldSanta = new Santa();
            Santa roboSanta = new Santa();

            oldSanta.DeliverPresent(firstHouse);
            roboSanta.DeliverPresent(firstHouse);

            List<House> visitedHouses = new List<House>(new House[]{ firstHouse });

            Console.WriteLine("Visiting houses");

            for(int i = 0; i < input.Length; i++)
            {
                Santa currentSanta;

                if(i % 2 == 0)
                {
                    currentSanta = oldSanta;
                }
                else
                {
                    currentSanta = roboSanta;
                }

                switch (input[i])
                {
                    case '^':
                        currentSanta.North += 1;
                        break;
                    case 'v':
                        currentSanta.North -= 1;
                        break;
                    case '<':
                        currentSanta.East -= 1;
                        break;
                    case '>':
                        currentSanta.East += 1;
                        break;
                }

                House currentHouse = visitedHouses.Where(h => h.North == currentSanta.North && h.East == currentSanta.East).FirstOrDefault();

                if(currentHouse == null)
                {
                    currentHouse = new House(currentSanta.North, currentSanta.East);

                    visitedHouses.Add(currentHouse);
                }

                currentSanta.DeliverPresent(currentHouse);
            }

            Console.WriteLine("Visited houses: " + visitedHouses.Count);
            Console.ReadLine();

        }

        static void Main(string[] args)
        {
            DayThree();
        }
    }
}
