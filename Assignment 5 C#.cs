using System;
using System.Linq;

namespace Session_04_Assignment
{
    enum WeekDays
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }

    enum Season
    {
        Spring, Summer, Autumn, Winter
    }

    [Flags]
    enum Permissions : byte
    {
        Read = 1,
        Write = 2,
        Delete = 4,
        Execute = 8
    }

    enum Colors
    {
        Red, Green, Blue
    }

    struct Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Point p)
        {
            return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));
        }
    }

    internal class Program
    {
        static void PassByValue(int x)
        {
            x = 100;
        }

        static void PassByRef(ref int x)
        {
            x = 100;
        }

        static void PassRefTypeByValue(int[] arr)
        {
            arr[0] = 99;
            arr = new int[] { 100, 200, 300 };
        }

        static void PassRefTypeByRef(ref int[] arr)
        {
            arr = new int[] { 100, 200, 300 };
        }

        static void SumAndSubtract(int a, int b, out int sum, out int diff)
        {
            sum = a + b;
            diff = a - b;
        }

        static int SumOfDigits(int number)
        {
            int sum = 0;
            number = Math.Abs(number);
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static void MinMaxArray(int[] array, ref int min, ref int max)
        {
            if (array == null || array.Length == 0) return;
            min = array[0];
            max = array[0];
            foreach (int num in array)
            {
                if (num < min) min = num;
                if (num > max) max = num;
            }
        }

        static long Factorial(int n)
        {
            if (n < 0) return -1;
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        static string ChangeChar(string input, int position, char newChar)
        {
            if (position < 0 || position >= input.Length)
                return input;

            char[] chars = input.ToCharArray();
            chars[position] = newChar;
            return new string(chars);
        }

        static void Main(string[] args)
        {
            // Part 1: Functions - Q1
            int val1 = 10;
            PassByValue(val1);
            Console.WriteLine($"PassByValue result: {val1}");

            int val2 = 10;
            PassByRef(ref val2);
            Console.WriteLine($"PassByRef result: {val2}");

            // Part 1: Functions - Q2
            int[] arrVal = { 1, 2, 3 };
            PassRefTypeByValue(arrVal);
            Console.WriteLine($"PassRefTypeByValue result: {arrVal[0]}, Length: {arrVal.Length}");

            int[] arrRef = { 1, 2, 3 };
            PassRefTypeByRef(ref arrRef);
            Console.WriteLine($"PassRefTypeByRef result: {arrRef[0]}, Length: {arrRef.Length}");

            // Part 1: Functions - Q3
            SumAndSubtract(15, 5, out int sumRes, out int diffRes);
            Console.WriteLine($"Sum: {sumRes}, Diff: {diffRes}");

            // Part 1: Functions - Q4
            Console.Write("Enter a number: ");
            int inputNum = int.Parse(Console.ReadLine());
            Console.WriteLine($"The sum of the digits of the number {inputNum} is: {SumOfDigits(inputNum)}");

            // Part 1: Functions - Q5
            Console.Write("Enter number for Prime check: ");
            int primeInput = int.Parse(Console.ReadLine());
            Console.WriteLine($"IsPrime: {IsPrime(primeInput)}");

            // Part 1: Functions - Q6
            int[] testArray = { 12, 5, 45, 2, 67, 23 };
            int minVal = 0, maxVal = 0;
            MinMaxArray(testArray, ref minVal, ref maxVal);
            Console.WriteLine($"Min: {minVal}, Max: {maxVal}");

            // Part 1: Functions - Q7
            Console.Write("Enter number for Factorial: ");
            int factInput = int.Parse(Console.ReadLine());
            Console.WriteLine($"Factorial: {Factorial(factInput)}");

            // Part 1: Functions - Q8
            Console.WriteLine(ChangeChar("Hello", 1, 'a'));

            // Part 2: Enum and Struct - Q1
            foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
            {
                Console.WriteLine(day);
            }

            // Part 2: Enum and Struct - Q2
            Person[] peopleList = new Person[3]
            {
                new Person("Ahmed", 25),
                new Person("Sara", 30),
                new Person("Omar", 22)
            };
            foreach (Person p in peopleList)
            {
                Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
            }

            // Part 2: Enum and Struct - Q3
            Console.Write("Enter season (Spring, Summer, Autumn, Winter): ");
            string seasonStr = Console.ReadLine();
            if (Enum.TryParse(seasonStr, true, out Season seasonVal))
            {
                switch (seasonVal)
                {
                    case Season.Spring: Console.WriteLine("March to May"); break;
                    case Season.Summer: Console.WriteLine("June to August"); break;
                    case Season.Autumn: Console.WriteLine("September to November"); break;
                    case Season.Winter: Console.WriteLine("December to February"); break;
                }
            }

            // Part 2: Enum and Struct - Q4
            Permissions userPerm = Permissions.Read | Permissions.Write;
            userPerm |= Permissions.Execute;
            userPerm &= ~Permissions.Write;
            bool hasRead = (userPerm & Permissions.Read) == Permissions.Read;
            Console.WriteLine($"Permissions: {userPerm}, Has Read: {hasRead}");

            // Part 2: Enum and Struct - Q5
            Console.Write("Enter color: ");
            string colorStr = Console.ReadLine();
            if (Enum.TryParse(colorStr, true, out Colors colorVal) && Enum.IsDefined(typeof(Colors), colorVal))
            {
                Console.WriteLine($"{colorVal} is a primary color.");
            }
            else
            {
                Console.WriteLine($"{colorStr} is not a primary color.");
            }

            // Part 2: Enum and Struct - Q6
            Console.Write("Enter Point 1 (X Y): ");
            string[] pt1Inputs = Console.ReadLine().Split(' ');
            Point point1 = new Point(double.Parse(pt1Inputs[0]), double.Parse(pt1Inputs[1]));

            Console.Write("Enter Point 2 (X Y): ");
            string[] pt2Inputs = Console.ReadLine().Split(' ');
            Point point2 = new Point(double.Parse(pt2Inputs[0]), double.Parse(pt2Inputs[1]));

            Console.WriteLine($"Distance: {point1.DistanceTo(point2):F2}");

            // Part 2: Enum and Struct - Q7
            Person[] threePersons = new Person[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Enter Name for Person {i + 1}: ");
                string pName = Console.ReadLine();
                Console.Write($"Enter Age for Person {i + 1}: ");
                int pAge = int.Parse(Console.ReadLine());
                threePersons[i] = new Person(pName, pAge);
            }

            Person oldestPerson = threePersons[0];
            for (int i = 1; i < threePersons.Length; i++)
            {
                if (threePersons[i].Age > oldestPerson.Age)
                {
                    oldestPerson = threePersons[i];
                }
            }
            Console.WriteLine($"Oldest Person: {oldestPerson.Name}, Age: {oldestPerson.Age}");
        }
    }
}