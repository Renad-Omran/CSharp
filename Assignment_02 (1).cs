using System;

namespace Assignment_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Question 1 - Enter a number and print it
            Console.Write("Enter a number: ");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("You entered: " + number);


            // Question 2 - Convert a non-numeric string to integer
            string invalidStr = "12abc";
            try
            {
                int parsed = int.Parse(invalidStr);
                Console.WriteLine(parsed);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException: " + ex.Message);
                // Output: Input string was not in a correct format.
            }


            // Question 3 - Arithmetic with floating-point numbers
            double a = 0.1;
            double b = 0.2;
            double sum = a + b;
            Console.WriteLine($"0.1 + 0.2 = {sum}");        // 0.30000000000000004
            Console.WriteLine($"Equals 0.3? {sum == 0.3}"); // False


            // Question 4 - Extract a substring from a given string
            string original = "Hello, World!";
            // Substring(startIndex, length) — index 7 is 'W', length 5 gives "World"
            string extracted = original.Substring(7, 5);
            Console.WriteLine("Original  : " + original);  // Hello, World!
            Console.WriteLine("Substring : " + extracted); // World


            // Question 5 - Value type: assign one variable to another and modify
            /*
             * What Happens:
             * Value types (int, double, bool, struct...) store data directly in memory.
             * Assigning y = x copies the VALUE of x into y.
             * They are completely independent after the assignment.
             * Modifying y has NO effect on x.
             */
            int x = 10;
            int y = x;  // y receives a COPY of x's value
            y = 20;     // only y changes
            Console.WriteLine("x = " + x); // 10 — unchanged
            Console.WriteLine("y = " + y); // 20


            // Question 6 - Reference type: assign one variable to another and modify
            /*
             * What Happens:
             * Reference types store a reference (memory address) to the object on the heap.
             * Assigning P2 = P1 copies the REFERENCE — both variables point to the same object.
             * Modifying the object through P2 is visible through P1 as well.
             */
            Point P1 = new Point(1, 2);
            Point P2 = P1;   // P2 holds the SAME reference as P1
            P2.X = 99;       // Modifying via P2 also changes what P1 sees
            Console.WriteLine("P1.X = " + P1.X); // 99 — affected by P2's change
            Console.WriteLine("P2.X = " + P2.X); // 99


            // Question 7 - Concatenate two string variables into one
            string str1 = "Hello";
            string str2 = "World";
            string combined = str1 + ", " + str2 + "!";
            Console.WriteLine(combined); // Hello, World!

            // Alternative using string interpolation
            Console.WriteLine($"{str1}, {str2}!");


            // Question 8
            // Answer: 2. A value 1 will be assigned to d

            // Question 9
            // Answer: 4. 6 1

            // Question 10
            // Answer: 4. 7 7
        }
    }

    // ---------------------------------------------------------------
    //  Point class — used in Question 6 (reference type example)
    // ---------------------------------------------------------------
    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point() { }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
