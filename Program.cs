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

            int maxValue = 5000;

            if (args.Length > 0)
            {
                int.TryParse(args[0], out maxValue);
            }

            Console.WriteLine($"Number to calculate to: {maxValue}");
            List<string> lines = new List<string>();

            lines.Add($"Max Number Calculated to: {maxValue}");
            lines.Add("Forumla used: result = A * B");
            lines.Add("A(Int),A(Int)(Binary), B(Int), B(Float), B(Float)(Binary), A(Int) * B(Float), A(Float) * B(Float), A(Int)*B(Float)(Binary), A(Int) * B(Double), A(Int) * B(Long), A(Int) * B(Decimal), A is Odd, B is Odd");
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
            return 0;
        }

        private static void Multiply(List<string> lines, int a, int b)
        {
            float fB = Convert.ToSingle(b);
            float fResult = a * fB;
            int iiResult = a * b;

            double dResult = a * Convert.ToDouble(b);

            if (fResult - dResult != 0.0 || fResult - iiResult != 0)
            {
                // Only perform these additional calculations if
                // there is a difference in the calculation.
                float ffResult = Convert.ToSingle(a) * fB;
                decimal decResult = a * Convert.ToDecimal(b);
                long lResult = Convert.ToInt64(a) * b;
                bool isAOdd = !(a % 2 == 0);
                bool isBOdd = !(b % 2 == 0);

                // Get the binary representation of the integer A.
                string intBinaryRepresentation = Convert.ToString(a, 2);

                // Get the binary represenation of the float B.
                string floatBinary = Convert.ToString(Convert.ToInt32(fB), 2);

                // Get the binary representation of the interger A * float B.
                string binaryRepresentation = Convert.ToString(Convert.ToInt32(fResult), 2);

                lines.Add($"{a}, {intBinaryRepresentation}, {b}, {Convert.ToSingle(b)}, {floatBinary}, {fResult}, {ffResult}, {binaryRepresentation}, {dResult}, {lResult}, {decResult}, {isAOdd}, {isBOdd}");
            }
        }
    }
}