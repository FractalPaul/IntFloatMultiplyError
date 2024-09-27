using System.Net.Sockets;

namespace IntFloatMultiplyError
{
    public class Program
    {
        /*
        *** This program calculates the multiplication of an integer by a float.  Both variables are iterated from 1 to 5000 each.
        *** The multiplication result is compared to a double version to see if there is a discrepancy in the calculated values vs 
        *** the int * int or int * double values.  
        *** If there is a discrepancy then the value is output to a CSV file.
        */
        public static int Main(string[] args)
        {
            Console.WriteLine("Integer multiplied to Float error calculation...");
            if (args.Length == 0)
            {
                Console.WriteLine("input two whole numbers to multiply and see if the produce correct result.");
                Console.WriteLine("Usage: dotnet run <A> <B>");
                Console.WriteLine("A * B = result.");
                Console.WriteLine("Usage: dotnet run <maxVal>");
                Console.WriteLine("If maxVal (int) is provided then both A and B will iterate from 1 to the maxVal (10,000)");
                Console.WriteLine("If A and B are not provided then a predetermined Odd ball list will be calculated.");
            }

            int maxValue = 0;
            int a = 0;
            float b = 0;

            if (args.Length == 2)
            {
                if (!int.TryParse(args[0], out a))
                {
                    Console.WriteLine("Please provide a valid integer value for A the first argument.");
                    return 1;
                }
                if (!float.TryParse(args[1], out b))
                {
                    Console.WriteLine("Please provide a valid float value for B the second argument.");
                    return 1;
                }

                DoCombinations(a, b);
            }
            else if (args.Length == 0)
            {
                DoOddBallCalculations();
            }
            else if (args.Length == 1)
            {
                if (int.TryParse(args[0], out maxValue))
                {
                    CalculateRangeMultiplication(maxValue);
                }
                else
                {
                    Console.WriteLine("Please, provide a valid integer value for max Value to calculate range.");
                }
            }
            return 0;
        }

        private static List<string> CalcCombinations(List<string> lines, int a, float b)
        {
            Multiply(lines, a, b);
            Multiply(lines, (int)b, a);
            Multiply(lines, -a, -b);
            Multiply(lines, -a, b);
            Multiply(lines, a, -b);

            return lines;
        }
        private static void DoCombinations(int a, float b)
        {
            List<String> lines = new List<string>();
            lines = CalcCombinations(lines, a, b);

            DisplayLines(lines);
        }

        private static void DoOddBallCalculations()
        {
            List<KeyValuePair<int, float>> dictOddBalls = new List<KeyValuePair<int, float>>();

            dictOddBalls.Add(new KeyValuePair<int, float>(1681, 9987));
            dictOddBalls.Add(new KeyValuePair<int, float>(2399, 6995));
            dictOddBalls.Add(new KeyValuePair<int, float>(2401, 6997));
            dictOddBalls.Add(new KeyValuePair<int, float>(2797, 5999));
            dictOddBalls.Add(new KeyValuePair<int, float>(3055, 6995));
            dictOddBalls.Add(new KeyValuePair<int, float>(3055, 6111));

            CalculateOddBalls(dictOddBalls);
        }

        // Calculates a given list of known numbers that cause the error calculation.
        private static void CalculateOddBalls(List<KeyValuePair<int, float>> dictOddBalls)
        {
            Console.WriteLine("Calculating Odd Ball numbers...");

            foreach (KeyValuePair<int, float> eachKVP in dictOddBalls)
            {
                DoCombinations(eachKVP.Key, eachKVP.Value);
            }
        }

        private static void DisplayLines(List<string> lines)
        {
            Console.WriteLine(_header_line);
            foreach (string eachLine in lines)
            {
                Console.WriteLine(eachLine);
            }
        }

        private static void CalculateRangeMultiplication(int maxValue)
        {
            Console.WriteLine($"Number to calculate to: {maxValue}");

            int numLines = 0;

            // Save to a file
            string fileName = "IntFloatError.csv";
            // Open file to write to...
            using (StreamWriter fileWriter = new StreamWriter(fileName))
            {
                List<string> lines = new List<string>();

                fileWriter.WriteLine($"Max Number Calculated to: {maxValue}");
                fileWriter.WriteLine("Forumla used: result = A * B");
                fileWriter.WriteLine(_header_line);

                for (int a = 1; a <= maxValue; a++)
                {
                    for (int b = 1; b <= maxValue; b++)
                    {
                        CalcCombinations(lines, a, b);
                        numLines += lines.Count;
                        foreach (string eachLine in lines)
                        {
                            fileWriter.WriteLine(eachLine);
                        }
                        lines.Clear();
                    }
                }
            }
            
            Console.WriteLine($"Number of lines: {numLines}");

            Console.WriteLine($"File written: {fileName}");
        }

        private const string _header_line = "A~I,B~F,A~I * B~F,A~F * B~F,A~I * B~I,A~I * B~Dbl,A~I * B~Lng,A~I * B~Dec,A Odd/Even,B Odd/Even,Is Error";

        private static void Multiply(List<string> lines, int a, float b)
        {
            float fResult = a * b;
            int iiResult = a * Convert.ToInt32(b);
            double dResult = a * Convert.ToDouble(b);

            if (fResult - dResult != 0.0 || fResult - iiResult != 0)
            {
                // Only perform these additional calculations if
                // there is a difference in the calculation.
                float ffResult = Convert.ToSingle(a) * b;
                decimal decResult = a * Convert.ToDecimal(b);
                long lResult = Convert.ToInt64(a) * Convert.ToInt64(b);
                string isAOdd = (a % 2 == 0) ? "Even" : "Odd";
                string isBOdd = (b % 2 == 0) ? "Even" : "Odd";
                bool isError = fResult.ToString() != iiResult.ToString();

                // // Get the binary representation of the integer A.
                // string intBinaryRepresentation = Convert.ToString(a, 2);

                // // Get the binary represenation of the float B.
                // string floatBinary = Convert.ToString(Convert.ToInt32(b), 2);

                // // Get the binary representation of the interger A * float B.
                // string binaryRepresentation = Convert.ToString(Convert.ToInt32(fResult), 2);

                lines.Add($"{a}, {b}, {fResult}, {ffResult}, {iiResult}, {dResult}, {lResult}, {decResult}, {isAOdd}, {isBOdd}, {isError}");
            }
        }
    }
}