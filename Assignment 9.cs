using System;
using System.Text;
using Xunit;

namespace Session09_Assignment
{
    // =========================
    // Part 1 - Patient
    // =========================

    public class Patient(int id, string fullName, string phoneNumber, string medicalHistory)
    {
        public int Id { get; set; } = id;
        public string FullName { get; set; } = fullName;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string MedicalHistory { get; set; } = medicalHistory;

        public override string ToString()
        {
            return $"Id: {Id} :: FullName: {FullName} :: PhoneNumber: {PhoneNumber} :: MedicalHistory: {MedicalHistory}";
        }
    }

    public record PatientDto(int Id, string FullName, string PhoneNumber);

    public static class PatientMapper
    {
        public static PatientDto MapFromModelToDto(Patient patient)
        {
            return new PatientDto(patient.Id, patient.FullName, patient.PhoneNumber);
        }
    }


    // =========================
    // Part 2 - Singleton
    // =========================

    public class AppLogger
    {
        private static AppLogger myLogger = null;

        private AppLogger()
        {
        }

        public static AppLogger GetLogger()
        {
            if (myLogger is null)
                myLogger = new AppLogger();

            return myLogger;
        }
    }


    // =========================
    // Part 5 - Extension Methods
    // =========================

    public static class TextHelper
    {
        // Normal static method before converting it to extension method
        // public static bool IsShorterThan(string value, int length)
        // {
        //     return value.Length < length;
        // }

        public static bool IsShorterThan(this string value, int length)
        {
            if (value.Length < length)
                return true;

            return false;
        }

        public static string Repeat(this string value, int times)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < times; i++)
            {
                result.Append(value);
            }

            return result.ToString();
        }
    }


    // =========================
    // Part 6 - Fine Calculator
    // =========================

    public class FineCalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException();

            return a / b;
        }
    }


    // =========================
    // Main
    // =========================

    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 1 - Primary Constructor & Records

            // Q1 + Q2
            Patient patient01 = new Patient(1, "Ahmed Ali", "01012345678", "Diabetes");
            Patient patient02 = new Patient(1, "Ahmed Ali", "01012345678", "Diabetes");

            Console.WriteLine("----- Patient Class -----");
            Console.WriteLine(patient01);
            Console.WriteLine(patient02);

            Console.WriteLine($"patient01 HashCode: {patient01.GetHashCode()}");
            Console.WriteLine($"patient02 HashCode: {patient02.GetHashCode()}");
            Console.WriteLine($"Equals before assignment: {patient01.Equals(patient02)}");

            // Answer:
            // They have the same data, but Patient is a normal class.
            // Normal classes compare references by default.
            // So two different objects are not equal even if their values are the same.
            // Hash codes may be different because they are different objects.

            patient01 = patient02;

            Console.WriteLine($"Equals after patient01 = patient02: {patient01.Equals(patient02)}");

            // Answer:
            // Now both variables point to the same object,
            // so Equals returns true.

            Console.WriteLine();


            // Q3
            PatientDto patientDto01 = new PatientDto(1, "Ahmed Ali", "01012345678");
            PatientDto patientDto02 = new PatientDto(1, "Ahmed Ali", "01012345678");

            Console.WriteLine("----- PatientDto Record -----");
            Console.WriteLine(patientDto01);
            Console.WriteLine(patientDto02);

            Console.WriteLine($"DTO 1 HashCode: {patientDto01.GetHashCode()}");
            Console.WriteLine($"DTO 2 HashCode: {patientDto02.GetHashCode()}");
            Console.WriteLine($"DTO Equals: {patientDto01.Equals(patientDto02)}");

            // Answer:
            // Record compares values, not only references.
            // That is why two records with the same values are equal.
            // A normal class usually compares references unless Equals is overridden.

            Console.WriteLine();


            // Q4
            Patient patient03 = new Patient(2, "Sara Mohamed", "01112345678", "Blood Pressure");
            PatientDto patientDto03 = PatientMapper.MapFromModelToDto(patient03);

            Console.WriteLine("----- Mapper -----");
            Console.WriteLine(patientDto03);

            // MedicalHistory is not inside PatientDto because it is sensitive data.

            #endregion


            #region Part 2 - Singleton

            Console.WriteLine();
            Console.WriteLine("----- Singleton -----");

            AppLogger logger01 = AppLogger.GetLogger();
            AppLogger logger02 = AppLogger.GetLogger();
            AppLogger logger03 = AppLogger.GetLogger();
            AppLogger logger04 = AppLogger.GetLogger();

            Console.WriteLine(logger01.GetHashCode());
            Console.WriteLine(logger02.GetHashCode());
            Console.WriteLine(logger03.GetHashCode());
            Console.WriteLine(logger04.GetHashCode());

            // Answer:
            // All calls return the same AppLogger object.
            // The constructor is private and GetLogger creates the object only once.
            // After that it returns the same instance every time.

            #endregion


            #region Part 3 - var & dynamic

            Console.WriteLine();
            Console.WriteLine("----- var & dynamic -----");

            // Target-typed new
            Patient p = new(3, "Omar Hassan", "01234567890", "No Medical History");
            Console.WriteLine(p);

            // var
            var patient04 = new Patient(4, "Ali Mohamed", "01512345678", "Asthma");
            Console.WriteLine(patient04);

            // dynamic
            dynamic patient05;
            patient05 = new Patient(5, "Mona Ahmed", "01098765432", "No Medical History");
            Console.WriteLine(patient05);

            // Answer:
            // var type is decided at compile time.
            // dynamic type is checked at run time.
            // var cannot change to another type after the compiler knows its type.
            // dynamic is more flexible, but errors can appear while the program is running.

            #endregion


            #region Part 4 - Anonymous Types

            Console.WriteLine();
            Console.WriteLine("----- Anonymous Types -----");

            var doctor01 = new
            {
                Name = "Sara",
                Specialty = "Cardiology",
                ExperienceYears = 8,
                Salary = 25_000
            };

            var doctor02 = new
            {
                Name = "Sara",
                Specialty = "Cardiology",
                ExperienceYears = 8,
                Salary = 25_000
            };

            Console.WriteLine(doctor01.Name);
            Console.WriteLine(doctor01.Specialty);

            Console.WriteLine($"doctor01 HashCode: {doctor01.GetHashCode()}");
            Console.WriteLine($"doctor02 HashCode: {doctor02.GetHashCode()}");

            Console.WriteLine($"Type: {doctor01.GetType()}");
            Console.WriteLine($"Equals: {doctor01.Equals(doctor02)}");
            Console.WriteLine($"ToString: {doctor01}");

            // Answer:
            // Anonymous types compare their property values.
            // Here doctor01 and doctor02 have the same properties and same values,
            // so Equals returns true.
            // This is closer to record equality than normal class equality.

            #endregion


            #region Part 5 - Extension Methods

            Console.WriteLine();
            Console.WriteLine("----- Extension Methods -----");

            bool shorterResult = "Stethoscope".IsShorterThan(5);
            string repeatResult = "Ab".Repeat(4);

            Console.WriteLine(shorterResult);
            Console.WriteLine(repeatResult);

            // "Stethoscope" has more than 5 characters, so the result is false.
            // "Ab".Repeat(4) => AbAbAbAb

            #endregion
        }
    }


    // =========================================================
    // xUnit Tests
    // This class should be inside the xUnit test project when running tests.
    // =========================================================

    public class FineCalculatorTests
    {
        private readonly FineCalculator _calculator;

        public FineCalculatorTests()
        {
            _calculator = new FineCalculator();
        }


        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            // Arrange
            int a = 10;
            int b = 5;
            int expected = 15;

            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Add_ShouldReturnNotEqual()
        {
            // Arrange
            int a = 10;
            int b = 5;
            int notExpected = 20;

            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.NotEqual(notExpected, result);
        }


        [Fact]
        public void Subtract_ShouldReturnCorrectDifference()
        {
            // Arrange
            int a = 10;
            int b = 4;
            int expected = 6;

            // Act
            int result = _calculator.Subtract(a, b);

            // Assert
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(5, 2, 10)]
        [InlineData(4, 3, 12)]
        [InlineData(-5, 2, -10)]
        public void Multiply_ShouldReturnCorrectResult(int a, int b, int expected)
        {
            // Arrange is already coming from InlineData

            // Act
            int result = _calculator.Multiply(a, b);

            // Assert
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(5, 2, 20)]
        [InlineData(4, 3, 15)]
        [InlineData(-5, 2, 10)]
        public void Multiply_ShouldReturnNotEqual(int a, int b, int notExpected)
        {
            // Arrange is already coming from InlineData

            // Act
            int result = _calculator.Multiply(a, b);

            // Assert
            Assert.NotEqual(notExpected, result);
        }


        [Fact]
        public void Divide_ByZero_ShouldThrowException()
        {
            // Arrange
            int a = 10;
            int b = 0;

            // Act + Assert
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
        }
    }
}
