using System;

namespace Session_07_Assignment
{
    // ==========================================
    // PART 1 & 2: Interfaces
    // ==========================================
    internal interface IMoveable
    {
        void MoveForward();
        void MoveBackward();
    }

    internal interface IFlyable
    {
        void MoveUp();
        void MoveDown();
    }

    // Q13: Interface Inheritance
    internal interface IVehicle : IMoveable, IFlyable
    {
    }

    // ==========================================
    // PART 1: Shape & Cube Classes (Static Binding)
    // ==========================================
    // Q1
    internal class Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Shape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area()
        {
            return Width * Height;
        }

        public override string ToString()
        {
            return $"(Width = {Width}, Height = {Height})";
        }
    }

    // Q2
    internal class Cube : Shape
    {
        public double Depth { get; set; }

        public Cube(double width, double height, double depth) : base(width, height)
        {
            Depth = depth;
        }

        // Hiding Area() using new keyword
        public new double Area()
        {
            return base.Area() * Depth;
        }

        public void Print()
        {
            Console.WriteLine($"(Width = {Width}, Height = {Height}, Depth = {Depth})");
        }

        public override string ToString()
        {
            return $"(Width = {Width}, Height = {Height}, Depth = {Depth})";
        }
    }

    // ==========================================
    // PART 2: Person, Doctor, Engineer Classes (Dynamic Binding)
    // ==========================================
    // Q5
    internal class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public void Greet()
        {
            Console.WriteLine("I am a Person.");
        }

        public virtual void Display()
        {
            Console.WriteLine($"Person: ID = {ID}, Name = {Name}, Age = {Age}");
        }
    }

    // Q6
    internal class Doctor : Person
    {
        public string Specialty { get; set; }

        public new void Greet()
        {
            Console.WriteLine("I am a Doctor.");
        }

        public override void Display()
        {
            Console.WriteLine($"Doctor: ID = {ID}, Name = {Name}, Age = {Age}, Specialty = {Specialty}");
        }
    }

    internal class Engineer : Person
    {
        public string Field { get; set; }
        public int YearsOfExperience { get; set; }

        public new void Greet()
        {
            Console.WriteLine("I am an Engineer.");
        }

        public override void Display()
        {
            Console.WriteLine($"Engineer: ID = {ID}, Name = {Name}, Age = {Age}, Field = {Field}, YearsOfExperience = {YearsOfExperience}");
        }
    }

    // ==========================================
    // PART 3: Vehicle Implementations
    // ==========================================
    // Q11
    internal class Car : IMoveable
    {
        public void MoveForward() => Console.WriteLine("Car is moving forward on Ground.");
        public void MoveBackward() => Console.WriteLine("Car is moving backward on Ground.");
    }

    // Q11 & Q14: Explicit Interface Implementation
    internal class Ship : IMoveable
    {
        // Explicit Interface Implementation
        void IMoveable.MoveForward()
        {
            Console.WriteLine("Ship is moving forward on Sea.");
        }

        public void MoveBackward()
        {
            Console.WriteLine("Ship is moving backward on Sea.");
        }
    }

    internal class Airplane : IMoveable, IFlyable
    {
        public void MoveForward() => Console.WriteLine("Airplane is moving forward in Air/Ground.");
        public void MoveBackward() => Console.WriteLine("Airplane is moving backward in Air/Ground.");
        public void MoveUp() => Console.WriteLine("Airplane is moving Up in Air.");
        public void MoveDown() => Console.WriteLine("Airplane is moving Down in Air.");
    }

    // Q13
    internal class Vehicle : IVehicle
    {
        public virtual void MoveForward() => Console.WriteLine("Vehicle moving forward.");
        public void MoveBackward() => Console.WriteLine("Vehicle moving backward.");
        public void MoveUp() => Console.WriteLine("Vehicle moving up.");
        public void MoveDown() => Console.WriteLine("Vehicle moving down.");
    }

    // ==========================================
    // Main Program Executable
    // ==========================================
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 1: Static Binding (Q3 - Q4)
            Console.WriteLine("=== PART 1: STATIC BINDING ===");
            Shape shape = new Shape(2, 3);
            Console.WriteLine($"Shape Area: {shape.Area()}"); // Output: 6

            Cube cube = new Cube(2, 3, 4);
            Console.WriteLine($"Cube Area: {cube.Area()}"); // Output: 24

            Shape shapeRef = new Cube(2, 3, 4);
            Console.WriteLine($"Shape Reference Cube Area: {shapeRef.Area()}"); // Output: 6

            // Q4
            object obj = new Cube(1, 2, 3);
            Console.WriteLine($"obj.ToString(): {obj.ToString()}"); // Output: (Width = 1, Height = 2, Depth = 3)
            #endregion

            #region Part 2: Dynamic Binding (Q7)
            Console.WriteLine("\n=== PART 2: DYNAMIC BINDING ===");
            Person doc = new Doctor { ID = 101, Name = "Dr. John", Age = 40, Specialty = "Cardiology" };
            Person eng = new Engineer { ID = 102, Name = "Eng. Sarah", Age = 30, Field = "Software", YearsOfExperience = 8 };

            ProcessPerson(doc);
            ProcessPerson(eng);
            #endregion

            #region Part 3: Interfaces (Q12 & Q14)
            Console.WriteLine("\n=== PART 3: INTERFACES ===");
            IMoveable carRef = new Car();
            IMoveable planeRef = new Airplane();

            carRef.MoveForward();
            planeRef.MoveForward();

            // planeRef.MoveUp(); // Compile-time Error! IMoveable does not define MoveUp().
            ((IFlyable)planeRef).MoveUp(); // Casting required to access IFlyable member

            // Q14: Explicit Implementation Call
            Ship ship = new Ship();
            // ship.MoveForward(); // Compile Error!
            ((IMoveable)ship).MoveForward(); // Must call via interface reference
            #endregion
        }

        // Q7 Method
        private static void ProcessPerson(Person person)
        {
            person.Greet();   // Static Binding: Calls Person.Greet()
            person.Display(); // Dynamic Binding: Calls overridden Display() in derived class
        }
    }
}