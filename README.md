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
![Int Float Multiply Error](https://github.com/user-attachments/assets/ff024c45-75e9-430f-83a4-ae1c0c9b1c16)
