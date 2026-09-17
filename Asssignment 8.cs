using System;

namespace Session_08_Assignment
{
    // =========================================================================
    // FIRST PROJECT: 3D Point Class
    // =========================================================================

    public class Point3D : IComparable, ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        // 1. Constructors with Chaining
        public Point3D() : this(0, 0, 0) { }

        public Point3D(int x) : this(x, 0, 0) { }

        public Point3D(int x, int y) : this(x, y, 0) { }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // 2. Override ToString Function
        public override string ToString()
        {
            return $"Point Coordinates: ({X}, {Y}, {Z})";
        }

        // Overriding Equals and GetHashCode for logical comparison
        public override bool Equals(object obj)
        {
            if (obj is Point3D other)
            {
                return X == other.X && Y == other.Y && Z == other.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        // 4. Overloading == and != Operators
        public static bool operator ==(Point3D p1, Point3D p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 is null || p2 is null) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Point3D p1, Point3D p2)
        {
            return !(p1 == p2);
        }

        // 5. Implement IComparable to sort array based on X, then Y
        public int CompareTo(object obj)
        {
            if (obj is Point3D other)
            {
                if (X != other.X)
                    return X.CompareTo(other.X);
                return Y.CompareTo(other.Y);
            }
            throw new ArgumentException("Object is not a Point3D");
        }

        // 6. Implement ICloneable interface
        public object Clone()
        {
            return new Point3D(X, Y, Z);
        }
    }

    // =========================================================================
    // SECOND PROJECT: Maths Class
    // =========================================================================

    public static class Maths
    {
        public static double Add(double a, double b) => a + b;

        public static double Subtract(double a, double b) => a - b;

        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    // =========================================================================
    // THIRD PROJECT: Duration Class
    // =========================================================================

    public class Duration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        // 3. Constructors to normalize input seconds into H:M:S
        public Duration(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Normalize();
        }

        public Duration(int totalSeconds)
        {
            Hours = totalSeconds / 3600;
            totalSeconds %= 3600;
            Minutes = totalSeconds / 60;
            Seconds = totalSeconds % 60;
        }

        public Duration() : this(0, 0, 0) { }

        private void Normalize()
        {
            if (Seconds >= 60)
            {
                Minutes += Seconds / 60;
                Seconds %= 60;
            }
            if (Minutes >= 60)
            {
                Hours += Minutes / 60;
                Minutes %= 60;
            }
        }

        private int ToTotalSeconds() => (Hours * 3600) + (Minutes * 60) + Seconds;

        // 2. Override System.Object Members
        public override string ToString()
        {
            if (Hours > 0)
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
            if (Minutes > 0)
                return $"Minutes :{Minutes}, Seconds :{Seconds}";
            return $"Seconds :{Seconds}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Duration other)
            {
                return ToTotalSeconds() == other.ToTotalSeconds();
            }
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Hours, Minutes, Seconds);

        // Operator Overloading Implementations

        public static Duration operator +(Duration d1, Duration d2)
        {
            return new Duration(d1.ToTotalSeconds() + d2.ToTotalSeconds());
        }

        public static Duration operator +(Duration d1, int seconds)
        {
            return new Duration(d1.ToTotalSeconds() + seconds);
        }

        public static Duration operator +(int seconds, Duration d1)
        {
            return d1 + seconds;
        }

        public static Duration operator -(Duration d1, Duration d2)
        {
            int diff = d1.ToTotalSeconds() - d2.ToTotalSeconds();
            return new Duration(diff > 0 ? diff : 0);
        }

        // Increase / Decrease by 1 minute
        public static Duration operator ++(Duration d)
        {
            return new Duration(d.ToTotalSeconds() + 60);
        }

        public static Duration operator --(Duration d)
        {
            int newTotal = d.ToTotalSeconds() - 60;
            return new Duration(newTotal > 0 ? newTotal : 0);
        }

        // Relational Operators
        public static bool operator >(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() > d2.ToTotalSeconds();
        }

        public static bool operator <(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() < d2.ToTotalSeconds();
        }

        public static bool operator >=(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() >= d2.ToTotalSeconds();
        }

        public static bool operator <=(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() <= d2.ToTotalSeconds();
        }

        // True / False Evaluation (Truthy check if duration > 0)
        public static bool operator true(Duration d)
        {
            return d.ToTotalSeconds() > 0;
        }

        public static bool operator false(Duration d)
        {
            return d.ToTotalSeconds() == 0;
        }

        // Explicit Cast to DateTime
        public static explicit operator DateTime(Duration d)
        {
            DateTime baseDate = DateTime.MinValue;
            return baseDate.AddHours(d.Hours).AddMinutes(d.Minutes).AddSeconds(d.Seconds);
        }
    }

    // =========================================================================
    // MAIN PROGRAM EXECUTION
    // =========================================================================

    internal class Program
    {
        static void Main(string[] args)
        {
            #region First Project Demo

            Console.WriteLine("=== FIRST PROJECT: 3D Point ===");

            Point3D p = new Point3D(10, 10, 10);
            Console.WriteLine(p.ToString());

            // Reading 2 Points from User safely
            Point3D P1 = ReadPointFromConsole("P1");
            Point3D P2 = ReadPointFromConsole("P2");

            // Q4: Does == work properly?
            // Before overloading '==', it checks Reference Equality (returns false for 2 separate instances with identical values).
            // After overloading '==', it evaluates Value Equality based on X, Y, and Z.
            if (P1 == P2)
                Console.WriteLine("P1 and P2 are equal.");
            else
                Console.WriteLine("P1 and P2 are NOT equal.");

            // Sorting Array of Points
            Point3D[] points = new Point3D[]
            {
                new Point3D(5, 10, 2),
                new Point3D(2, 20, 1),
                new Point3D(2, 5, 0),
                new Point3D(10, 1, 1)
            };

            Array.Sort(points);

            Console.WriteLine("\nSorted Points Array (Sorted by X then Y):");
            foreach (var pt in points)
            {
                Console.WriteLine(pt);
            }

            // Cloning
            Point3D clonedPoint = (Point3D)P1.Clone();
            Console.WriteLine($"\nCloned Point: {clonedPoint}");

            #endregion

            #region Second Project Demo

            Console.WriteLine("\n=== SECOND PROJECT: Maths Class ===");
            // Calling static methods directly without instantiating Maths
            Console.WriteLine($"Add: {Maths.Add(10, 5)}");
            Console.WriteLine($"Subtract: {Maths.Subtract(10, 5)}");
            Console.WriteLine($"Multiply: {Maths.Multiply(10, 5)}");
            Console.WriteLine($"Divide: {Maths.Divide(10, 5)}");

            #endregion

            #region Third Project Demo

            Console.WriteLine("\n=== THIRD PROJECT: Duration Class ===");

            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine(D1.ToString()); // Output: Hours: 1, Minutes :10, Seconds :15

            Duration D2 = new Duration(3600);
            Console.WriteLine(D2.ToString()); // Output: Hours: 1, Minutes :0, Seconds :0

            Duration D3 = new Duration(7800);
            Console.WriteLine(D3.ToString()); // Output: Hours: 2, Minutes :10, Seconds :0

            Duration D4 = new Duration(666);
            Console.WriteLine(D4.ToString()); // Output: Minutes :11, Seconds :6

            Console.WriteLine("\n--- Testing Operator Overloading ---");
            D3 = D1 + D2;
            Console.WriteLine($"D3 = D1 + D2 -> {D3}");

            D3 = D1 + 7800;
            Console.WriteLine($"D3 = D1 + 7800 -> {D3}");

            D3 = 666 + D3;
            Console.WriteLine($"D3 = 666 + D3 -> {D3}");

            D3 = ++D1;
            Console.WriteLine($"D3 = ++D1 -> {D3}");

            D3 = --D2;
            Console.WriteLine($"D3 = --D2 -> {D3}");

            D1 = D1 - D2;
            Console.WriteLine($"D1 = D1 - D2 -> {D1}");

            if (D1 > D2)
                Console.WriteLine("D1 is greater than D2");

            if (D1 <= D2)
                Console.WriteLine("D1 is less than or equal to D2");

            if (D1)
                Console.WriteLine("D1 evaluates to True (Has duration)");

            DateTime obj = (DateTime)D1;
            Console.WriteLine($"Converted to DateTime: {obj:HH:mm:ss}");

            #endregion
        }

        private static Point3D ReadPointFromConsole(string pointName)
        {
            Console.WriteLine($"\nEnter coordinates for {pointName}:");
            int x = ReadInt("X: ");
            int y = ReadInt("Y: ");
            int z = ReadInt("Z: ");
            return new Point3D(x, y, z);
        }

        private static int ReadInt(string prompt)
        {
            int result;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Invalid input. Please enter a valid integer: ");
            }
            return result;
        }
    }
}