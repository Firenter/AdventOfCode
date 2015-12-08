using System;
using System.Collections.Generic;
using AdventOfCode.Classes;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
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

            List<House> visitedHouses = new List<House>(new House[] { firstHouse });

            Console.WriteLine("Visiting houses");

            for (int i = 0; i < input.Length; i++)
            {
                Santa currentSanta;

                if (i % 2 == 0)
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

                if (currentHouse == null)
                {
                    currentHouse = new House(currentSanta.North, currentSanta.East);

                    visitedHouses.Add(currentHouse);
                }

                currentSanta.DeliverPresent(currentHouse);
            }

            Console.WriteLine("Visited houses: " + visitedHouses.Count);
            Console.ReadLine();

        }

        public static void DayFour()
        {
            MD5 md5 = MD5.Create();

            string input = File.ReadAllText("input.txt");

            int counter = 0;

            bool found = false;

            while (!found)
            {
                Console.WriteLine("Counter is: " + counter.ToString());
                string newInput = input + counter;

                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(newInput));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                if (sb.ToString().Substring(0, 6) == "000000")
                {
                    found = true;
                }
                else
                {
                    counter++;
                }

            }

            Console.WriteLine("You had to count to: " + counter.ToString());
            Console.ReadLine();
        }

        public static void DayFive()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");

            Console.WriteLine("Checking who's naughty and nice");

            string line;

            int amountNice = 0;

            while ((line = file.ReadLine()) != null)
            {
                /*int vowelCount = 0;
                bool doubleLetter = false;
                bool hasDisallowed = false;*/

                bool doubleNoOverlap = false;
                bool repeating = false;
                
                List<string> comboList = new List<string>();

                string previousCombo = "";
                bool overlap = false;

                //create all combos
                for (int i = 0; i < line.Length - 1; i++)
                {
                    string combo = line[i].ToString() + line[i + 1].ToString();

                    //check for overlap
                    if (combo.Equals(previousCombo))
                    {
                        overlap = true;
                    }

                    comboList.Add(combo);
                    previousCombo = combo;
                }

                bool multiple = false;

                //check if combo apears more than once
                comboList.ForEach(delegate (string combi) {
                    if(comboList.IndexOf(combi) != comboList.LastIndexOf(combi))
                    {
                        multiple = true;
                    }
                });

                doubleNoOverlap = multiple && !overlap;

                //check for repeat with skip one letter
                for (int j = 0; j < line.Length - 2; j++)
                {
                    if(line[j] == line[j + 2])
                    {
                        repeating = true;
                    }
                }

                if(doubleNoOverlap && repeating)
                {
                    Console.WriteLine("Nice");
                    amountNice++;
                }
                else
                {
                    Console.WriteLine("Naughty");
                }

                /*if (vowelCount >= 3 && doubleLetter && !hasDisallowed)
                {
                    Console.WriteLine("Nice");
                    amountNice++;
                }
                else
                {
                    Console.WriteLine("Naughty");
                }*/

            }

            Console.WriteLine("So many nice words: " + amountNice);
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            DayFive();
        }
    }
}
