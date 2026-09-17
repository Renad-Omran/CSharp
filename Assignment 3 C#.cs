using System;
using System.Linq;

namespace Session_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1
            Console.Write("Enter a number: ");
            if (int.TryParse(Console.ReadLine(), out int q1Num))
            {
                if (q1Num % 3 == 0 && q1Num % 4 == 0)
                    Console.WriteLine("Yes");
                else
                    Console.WriteLine("No");
            }

            // 2
            Console.Write("Enter an integer: ");
            if (int.TryParse(Console.ReadLine(), out int q2Num))
            {
                if (q2Num < 0)
                    Console.WriteLine("negative");
                else
                    Console.WriteLine("positive");
            }

            // 3
            Console.Write("Enter three integers (separated by space or comma): ");
            string[] q3Inputs = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (q3Inputs.Length >= 3)
            {
                int a = int.Parse(q3Inputs[0]);
                int b = int.Parse(q3Inputs[1]);
                int c = int.Parse(q3Inputs[2]);

                int max = Math.Max(a, Math.Max(b, c));
                int min = Math.Min(a, Math.Min(b, c));

                Console.WriteLine($"max element = {max}");
                Console.WriteLine($"min element = {min}");
            }

            // 4
            Console.Write("Enter an integer: ");
            if (int.TryParse(Console.ReadLine(), out int q4Num))
            {
                if (q4Num % 2 == 0)
                    Console.WriteLine("Even");
                else
                    Console.WriteLine("Odd");
            }

            // 5
            Console.Write("Enter a character: ");
            if (char.TryParse(Console.ReadLine(), out char q5Char))
            {
                char lowerChar = char.ToLower(q5Char);
                if ("aeiou".Contains(lowerChar))
                    Console.WriteLine("vowel");
                else
                    Console.WriteLine("Consonant");
            }

            // 6
            Console.Write("Enter an integer: ");
            if (int.TryParse(Console.ReadLine(), out int q6Num))
            {
                for (int i = 1; i <= q6Num; i++)
                {
                    Console.Write(i + (i == q6Num ? "" : ", "));
                }
                Console.WriteLine();
            }

            // 7
            Console.Write("Enter an integer: ");
            if (int.TryParse(Console.ReadLine(), out int q7Num))
            {
                for (int i = 1; i <= 12; i++)
                {
                    Console.Write((q7Num * i) + " ");
                }
                Console.WriteLine();
            }

            // 8
            Console.Write("Enter a number: ");
            if (int.TryParse(Console.ReadLine(), out int q8Num))
            {
                for (int i = 2; i <= q8Num; i += 2)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }

            // 9
            Console.Write("Enter base and exponent (separated by space): ");
            string[] q9Inputs = Console.ReadLine().Split(' ');
            if (q9Inputs.Length >= 2)
            {
                int baseNum = int.Parse(q9Inputs[0]);
                int expNum = int.Parse(q9Inputs[1]);
                long result = 1;

                for (int i = 0; i < expNum; i++)
                {
                    result *= baseNum;
                }
                Console.WriteLine(result);
            }

            // 10
            Console.Write("Enter Marks of five subjects: ");
            string[] q10Inputs = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (q10Inputs.Length >= 5)
            {
                int total = 0;
                for (int i = 0; i < 5; i++)
                {
                    total += int.Parse(q10Inputs[i]);
                }
                double average = total / 5.0;
                double percentage = (total / 500.0) * 100;

                Console.WriteLine($"Total marks = {total}");
                Console.WriteLine($"Average Marks = {average}");
                Console.WriteLine($"Percentage = {percentage}%");
            }

            // 11
            Console.Write("Month Number: ");
            if (int.TryParse(Console.ReadLine(), out int month))
            {
                switch (month)
                {
                    case 1: case 3: case 5: case 7: case 8: case 10: case 12:
                        Console.WriteLine("Days in Month: 31");
                        break;
                    case 4: case 6: case 9: case 11:
                        Console.WriteLine("Days in Month: 30");
                        break;
                    case 2:
                        Console.WriteLine("Days in Month: 28 or 29");
                        break;
                    default:
                        Console.WriteLine("Invalid Month Number");
                        break;
                }
            }

            // 12
            Console.Write("Enter First Number: ");
            double num1 = double.Parse(Console.ReadLine());
            Console.Write("Enter Operator (+, -, *, /): ");
            char op = char.Parse(Console.ReadLine());
            Console.Write("Enter Second Number: ");
            double num2 = double.Parse(Console.ReadLine());

            switch (op)
            {
                case '+': Console.WriteLine($"Result: {num1 + num2}"); break;
                case '-': Console.WriteLine($"Result: {num1 - num2}"); break;
                case '*': Console.WriteLine($"Result: {num1 * num2}"); break;
                case '/':
                    if (num2 != 0)
                        Console.WriteLine($"Result: {num1 / num2}");
                    else
                        Console.WriteLine("Cannot divide by zero.");
                    break;
                default: Console.WriteLine("Invalid operator."); break;
            }

            // 13
            Console.Write("Enter a string: ");
            string inputStr = Console.ReadLine();
            char[] charArray = inputStr.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine(new string(charArray));

            // 14
            Console.Write("Enter an integer: ");
            if (int.TryParse(Console.ReadLine(), out int q14Num))
            {
                int reversed = 0;
                while (q14Num != 0)
                {
                    int remainder = q14Num % 10;
                    reversed = reversed * 10 + remainder;
                    q14Num /= 10;
                }
                Console.WriteLine($"Reversed integer: {reversed}");
            }

            // 15
            Console.Write("Input starting number of range: ");
            int start = int.Parse(Console.ReadLine());
            Console.Write("Input ending number of range: ");
            int end = int.Parse(Console.ReadLine());

            Console.WriteLine($"The prime numbers between {start} and {end} are:");
            for (int i = Math.Max(2, start); i <= end; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) Console.Write(i + " ");
            }
            Console.WriteLine();

            // 16
            Console.Write("Enter a number to convert: ");
            if (int.TryParse(Console.ReadLine(), out int decNum))
            {
                int original = decNum;
                string binary = "";
                while (decNum > 0)
                {
                    int remainder = decNum % 2;
                    binary = remainder + binary;
                    decNum /= 2;
                }
                Console.WriteLine($"The Binary of {original} is {binary}.");
            }

            // 17
            Console.Write("Enter x1 y1: ");
            string[] p1 = Console.ReadLine().Split(' ');
            Console.Write("Enter x2 y2: ");
            string[] p2 = Console.ReadLine().Split(' ');
            Console.Write("Enter x3 y3: ");
            string[] p3 = Console.ReadLine().Split(' ');

            double x1 = double.Parse(p1[0]), y1 = double.Parse(p1[1]);
            double x2 = double.Parse(p2[0]), y2 = double.Parse(p2[1]);
            double x3 = double.Parse(p3[0]), y3 = double.Parse(p3[1]);

            if ((y2 - y1) * (x3 - x2) == (y3 - y2) * (x2 - x1))
                Console.WriteLine("The points lie on a single straight line.");
            else
                Console.WriteLine("The points do not lie on a single straight line.");

            // 18
            Console.Write("Enter hours taken to complete task: ");
            if (double.TryParse(Console.ReadLine(), out double hours))
            {
                if (hours >= 2 && hours < 3)
                    Console.WriteLine("Highly efficient worker.");
                else if (hours >= 3 && hours < 4)
                    Console.WriteLine("Instructed to increase speed.");
                else if (hours >= 4 && hours <= 5)
                    Console.WriteLine("Provided with training to enhance speed.");
                else if (hours > 5)
                    Console.WriteLine("Required to leave the company.");
                else
                    Console.WriteLine("Duration is less than 2 hours.");
            }

            // 19
            Console.Write("Enter size n: ");
            if (int.TryParse(Console.ReadLine(), out int n))
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                            Console.Write("1 ");
                        else
                            Console.Write("0 ");
                    }
                    Console.WriteLine();
                }
            }

            // 20
            Console.Write("Enter array elements separated by space: ");
            int[] arrayQ20 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int sum = 0;
            foreach (int item in arrayQ20)
            {
                sum += item;
            }
            Console.WriteLine($"Sum of all elements = {sum}");

            // 21
            Console.Write("Enter First Array Elements: ");
            int[] arr1 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            Console.Write("Enter Second Array Elements: ");
            int[] arr2 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            int[] merged = new int[arr1.Length + arr2.Length];
            arr1.CopyTo(merged, 0);
            arr2.CopyTo(merged, arr1.Length);
            Array.Sort(merged);

            Console.WriteLine("Merged and sorted array:");
            foreach (int item in merged) Console.Write(item + " ");
            Console.WriteLine();

            // 22
            Console.Write("Enter array elements separated by space: ");
            int[] arrQ22 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            bool[] visited = new bool[arrQ22.Length];

            for (int i = 0; i < arrQ22.Length; i++)
            {
                if (visited[i]) continue;
                int count = 1;
                for (int j = i + 1; j < arrQ22.Length; j++)
                {
                    if (arrQ22[i] == arrQ22[j])
                    {
                        visited[j] = true;
                        count++;
                    }
                }
                Console.WriteLine($"Element {arrQ22[i]} occurs {count} times.");
            }

            // 23
            Console.Write("Enter array elements separated by space: ");
            int[] arrQ23 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int maxEl = arrQ23[0];
            int minEl = arrQ23[0];

            for (int i = 1; i < arrQ23.Length; i++)
            {
                if (arrQ23[i] > maxEl) maxEl = arrQ23[i];
                if (arrQ23[i] < minEl) minEl = arrQ23[i];
            }
            Console.WriteLine($"Maximum element: {maxEl}");
            Console.WriteLine($"Minimum element: {minEl}");

            // 24
            Console.Write("Enter array elements separated by space: ");
            int[] arrQ24 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int firstMax = int.MinValue, secondMax = int.MinValue;

            foreach (int num in arrQ24)
            {
                if (num > firstMax)
                {
                    secondMax = firstMax;
                    firstMax = num;
                }
                else if (num > secondMax && num < firstMax)
                {
                    secondMax = num;
                }
            }
            Console.WriteLine($"Second largest element: {secondMax}");

            // 25
            Console.Write("Enter array elements separated by space: ");
            int[] arrQ25 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int maxDistance = 0;

            for (int i = 0; i < arrQ25.Length; i++)
            {
                for (int j = arrQ25.Length - 1; j > i; j--)
                {
                    if (arrQ25[i] == arrQ25[j])
                    {
                        int distance = j - i - 1;
                        if (distance > maxDistance)
                        {
                            maxDistance = distance;
                        }
                        break;
                    }
                }
            }
            Console.WriteLine($"Longest distance: {maxDistance}");

            // 26
            Console.Write("Enter words: ");
            Console.WriteLine(string.Join(" ", Console.ReadLine().Split(' ').Reverse()));

            // 27
            Console.Write("Enter rows count: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Enter columns count: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] arr12D = new int[rows, cols];
            int[,] arr22D = new int[rows, cols];

            Console.WriteLine("Enter elements for the 2D array:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Element [{i},{j}]: ");
                    arr12D[i, j] = int.Parse(Console.ReadLine());
                    arr22D[i, j] = arr12D[i, j];
                }
            }

            Console.WriteLine("Elements of Second Array:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(arr22D[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // 28
            Console.Write("Enter array elements separated by space: ");
            int[] arrQ28 = Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            Console.WriteLine("Array in Reverse Order:");
            for (int i = arrQ28.Length - 1; i >= 0; i--)
            {
                Console.Write(arrQ28[i] + " ");
            }
            Console.WriteLine();
        }
    }
}