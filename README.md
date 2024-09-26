# IntFloatMultiplyError
C# Int multiplied by float error in calculation...

This program multiplies an integer by a float (of a integer) to see if there is an error in the calculation.
In C# there is an error with certain values.  This occurs when mulitplying some integer values times another integer value that are only odd for each.

Such as:
3357(int) * 4999(float) = 16,781,644 instead of 16,781,643.

There are numerous other values that are produced.  Interestingly there are only odd numbers for both that produce the calculation error.

Linux:
```terminal
$ dotnet run
```
On Linux there is quite a list of values that create discrepancies.

Odd ball calulations...
![IntFloat Odd Ball C#](https://github.com/user-attachments/assets/d42202e7-e2ac-4555-83d1-d73005231e0c)

The following shows the calculated values on Windows 10:
![Int Float Multiply Error](https://github.com/user-attachments/assets/ff024c45-75e9-430f-83a4-ae1c0c9b1c16)

A very short list of the numbers with the error calculations is below:
![Int Float Multiply Error List](https://github.com/user-attachments/assets/f09a2b87-5c58-43a0-ad11-220d766cb419)

A very short list of the numbers with the error calculations:
![Int Float Multiply Error short List](https://github.com/user-attachments/assets/316f96a6-a3b1-4eeb-a7d3-e03b0756bb26)
