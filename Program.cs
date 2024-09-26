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
            Dictionary<int, float> dictOddBalls = new Dictionary<int, float>();
            dictOddBalls.Add(2399, 6995);
            dictOddBalls.Add(3055, 6111);
            dictOddBalls.Add(2401, 6997);
            dictOddBalls.Add(1681, 9987);
            dictOddBalls.Add(1682, 9987);

            CalculateOddBalls(dictOddBalls);

            int maxValue = 10000;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out maxValue);
            }

            //CalculateRangeMultiplication(maxValue);

            return 0;
        }

        // Calculates a given list of known numbers that cause the error calculation.
        private static void CalculateOddBalls(Dictionary<int, float> dictOddBalls)
        {
            List<string> lines = new List<string>();
            foreach (KeyValuePair<int, float> eachKVP in dictOddBalls)
            {
                Multiply(lines, eachKVP.Key, eachKVP.Value);
            }

            Console.WriteLine(_header_line);
            foreach (string eachLine in lines)
            {
                Console.WriteLine(eachLine);
            }
        }

        private static void CalculateRangeMultiplication(int maxValue)
        {

            Console.WriteLine($"Number to calculate to: {maxValue}");
            List<string> lines = new List<string>();

            lines.Add($"Max Number Calculated to: {maxValue}");
            lines.Add("Forumla used: result = A * B");
            lines.Add(_header_line);
            int numHeaderLines = lines.Count;

            for (int a = 1; a <= maxValue; a++)
            {
                for (int b = 1; b <= maxValue; b++)
                {
                    Multiply(lines, a, b);
                }
            }

            Console.WriteLine($"Number of lines: {lines.Count - numHeaderLines}");

            // Save to a file
            string fileName = "IntFloatError.csv";

            File.WriteAllLines(fileName, lines);

            Console.WriteLine($"File written: {fileName}");
        }

        private const string _header_line = "A~I,B~F,A~I * B~F,A~F * B~F,A~I * B~I,A~I * B~Dbl,A~I * B~Lng,A~I * B~Dec,A is Odd,B is Odd";

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
                bool isAOdd = !(a % 2 == 0);
                bool isBOdd = !(b % 2 == 0);

                // // Get the binary representation of the integer A.
                // string intBinaryRepresentation = Convert.ToString(a, 2);

                // // Get the binary represenation of the float B.
                // string floatBinary = Convert.ToString(Convert.ToInt32(b), 2);

                // // Get the binary representation of the interger A * float B.
                // string binaryRepresentation = Convert.ToString(Convert.ToInt32(fResult), 2);

                lines.Add($"{a}, {b}, {fResult}, {ffResult}, {iiResult}, {dResult}, {lResult}, {decResult}, {isAOdd}, {isBOdd}");
            }
        }
    }
}