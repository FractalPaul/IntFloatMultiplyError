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

        private const float _standard_error = 0.00001f;
        public static int Main(string[] args)
        {
            Console.WriteLine("Integer multiplied to Float error calculation...");

            int maxValue = 5000;

            if (args.Length > 0)
            {
                int.TryParse(args[0], out maxValue);
            }

            Console.WriteLine($"Number to calculate to: {maxValue}");

            // Multiplication calculation setup...
            List<string> mulLines = new List<string>();

            mulLines.Add($"Max Number Calculated to: {maxValue}");
            mulLines.Add("Forumla used: result = A * B");
            mulLines.Add("A(Int),A(Int)(Binary), B(Int), B(Float), B(Float)(Binary), A(Int) * B(Float), A(Float) * B(Float), A(Int)*B(Float)(Binary), A(Int) * B(Double), A(Int) * B(Long), A(Int) * B(Decimal), A is Odd, B is Odd");
            int numHeaderLines = mulLines.Count;

            // Addition calculation setup
            List<string> addLines = new List<string>();
            addLines.Add($"Max Number Calculated to: {maxValue}");
            addLines.Add("Forumla used: result = A + B");
            addLines.Add("A(Int),A(Int)(Binary), B(Int), B(Float), B(Float)(Binary), A(Int) + B(Float), A(Float) + B(Float), A(Int)+B(Float)(Binary), A(Int) + B(Double), A(Int) + B(Long), A(Int) + B(Decimal), A is Odd, B is Odd");
            int numHeaderLinesAdd = addLines.Count;

            // Division calculation setup.
            List<string> divLines = new List<string>();
            divLines.Add($"Max number calculated to: {maxValue}");
            divLines.Add("Forumla used: result = A + B");
            divLines.Add("A(Int), B(Int), B(Float), A(Int) / B(Float), A(Float) / B(Float), A(Int) / B(Double), A(Int) / B(Long), A(Int) / B(Decimal), A is Odd, B is Odd");
            int numHeaderLinesDiv = divLines.Count;

            for (int a = 1; a <= maxValue; a++)
            {
                for (int b = 1; b <= maxValue; b++)
                {
                    Multiply(mulLines, a, b);
                    Addition(addLines, a, b);
                    Division(divLines, a, b);
                }
            }

            Console.WriteLine($"Number of lines for multiplication: {mulLines.Count - numHeaderLines}");
            Console.WriteLine($"Number of lines for addition: {addLines.Count - numHeaderLinesAdd}");
            Console.WriteLine($"Number of lines for division: {divLines.Count - numHeaderLinesDiv}");

            // Save to a file
            string fileName = "IntMultiplyFloatError.csv";
            File.WriteAllLines(fileName, mulLines);
            Console.WriteLine($"File written: {fileName}");

            string addFileName = "IntAddFloatError.csv";
            File.WriteAllLines(addFileName, addLines);
            Console.WriteLine($"File Written: {addFileName}");

            string divFileName = "IntDivFloatError.csv";
            File.WriteAllLines(divFileName, divLines);
            Console.WriteLine($"File written: {divFileName}");

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

        private static void Division(List<string> lines, int a, int b)
        {
            float fB = Convert.ToSingle(b);
            float fResult = a / fB;
            float ffResult = Convert.ToSingle(a) / fB;

            double dResult = a / Convert.ToDouble(b);

            if (Math.Abs(fResult - Convert.ToSingle(dResult)) > _standard_error || fResult - ffResult > _standard_error)
            {
                // Only perform these additional calculations if
                // there is a difference in the calculation.                
                decimal decResult = a * Convert.ToDecimal(b);
                long lResult = Convert.ToInt64(a) / b;
                bool isAOdd = !(a % 2 == 0);
                bool isBOdd = !(b % 2 == 0);

                lines.Add($"{a}, {b}, {Convert.ToSingle(b)}, {fResult}, {ffResult}, {dResult}, {lResult}, {decResult}, {isAOdd}, {isBOdd}");
            }
        }

        private static void Addition(List<string> lines, int a, int b)
        {
            float fB = Convert.ToSingle(b);
            float fResult = a + fB;
            int iiResult = a + b;

            double dResult = a + Convert.ToDouble(b);

            if (fResult - dResult != 0.0 || fResult - iiResult != 0)
            {
                // Only perform these additional calculations if
                // there is a difference in the calculation.
                float ffResult = Convert.ToSingle(a) + fB;
                decimal decResult = a + Convert.ToDecimal(b);
                long lResult = Convert.ToInt64(a) + b;
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