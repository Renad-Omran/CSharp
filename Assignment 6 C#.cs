using System;
using System.Globalization;

namespace Assignment_OOP
{
    public enum Gender
    {
        M,
        F
    }

    [Flags]
    enum SecurityLevel : byte
    {
        Guest = 1,      // 0001
        Developer = 2,  // 0010
        Secretary = 4,  // 0100
        DBA = 8,        // 1000
        SecurityOfficer = Guest | Developer | Secretary | DBA
    }

    public class HiringDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public HiringDate() : this(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) { }

        public HiringDate(int day, int month, int year)
        {
            if (year < 1900 || year > DateTime.Now.Year)
                year = DateTime.Now.Year;

            if (month < 1 || month > 12)
                month = 1;

            int maxDays = DateTime.DaysInMonth(year, month);
            if (day < 1 || day > maxDays)
                day = 1;

            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
    }

    public class Employee
    {
        private decimal _salary;

        public int ID { get; set; }
        public string Name { get; set; }
        public SecurityLevel SecurityLevel { get; set; }
        public Gender Gender { get; set; }
        public HiringDate HireDate { get; set; }

        public decimal Salary
        {
            get => _salary;
            set => _salary = value >= 0 ? value : 0;
        }

        public Employee()
        {
            Name = "Unknown";
            HireDate = new HiringDate();
        }

        public Employee(int id, string name, SecurityLevel securityLevel, decimal salary, HiringDate hireDate, Gender gender)
        {
            ID = id;
            Name = name;
            SecurityLevel = securityLevel;
            Salary = salary;
            HireDate = hireDate ?? new HiringDate();
            Gender = gender;
        }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "ID: {0} | Name: {1} | Security Level: {2} | Gender: {3} | Salary: {4:C} | Hire Date: {5}",
                ID, Name, SecurityLevel, Gender, Salary, HireDate
            );
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Employee[] EmpArr = new Employee[3];

            for (int i = 0; i < EmpArr.Length; i++)
            {
                Console.WriteLine($"=== Enter Data for Employee {i + 1} ===");

                int id = ReadInt("Enter ID: ");

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Invalid Name! Enter again: ");
                    name = Console.ReadLine();
                }

                SecurityLevel secLevel = ReadSecurityLevel();
                decimal salary = ReadDecimal("Enter Salary: ");
                HiringDate hireDate = ReadHiringDate();
                Gender gender = ReadGender();

                EmpArr[i] = new Employee(id, name, secLevel, salary, hireDate, gender);
                Console.WriteLine();
            }

            Console.WriteLine("\n================ Employee Details ================");
            foreach (Employee emp in EmpArr)
            {
                Console.WriteLine(emp);
            }
        }

        static int ReadInt(string message)
        {
            int result;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                Console.Write("Invalid input! Please enter a valid positive integer: ");
            }
            return result;
        }

        static decimal ReadDecimal(string message)
        {
            decimal result;
            Console.Write(message);
            while (!decimal.TryParse(Console.ReadLine(), out result) || result < 0)
            {
                Console.Write("Invalid input! Please enter a valid non-negative number: ");
            }
            return result;
        }

        static SecurityLevel ReadSecurityLevel()
        {
            Console.Write("Enter Security Level (Guest, Developer, Secretary, DBA, SecurityOfficer): ");
            SecurityLevel secLevel;
            while (!Enum.TryParse(Console.ReadLine(), true, out secLevel) || !Enum.IsDefined(typeof(SecurityLevel), secLevel))
            {
                Console.Write("Invalid Security Level! Enter again (Guest, Developer, Secretary, DBA, SecurityOfficer): ");
            }
            return secLevel;
        }

        static Gender ReadGender()
        {
            Console.Write("Enter Gender (M/F): ");
            Gender gender;
            while (!Enum.TryParse(Console.ReadLine(), true, out gender) || !Enum.IsDefined(typeof(Gender), gender))
            {
                Console.Write("Invalid Gender! Enter 'M' for Male or 'F' for Female: ");
            }
            return gender;
        }

        static HiringDate ReadHiringDate()
        {
            Console.WriteLine("--- Enter Hire Date ---");
            int year = ReadInt("Enter Year: ");
            while (year < 1900 || year > DateTime.Now.Year)
            {
                Console.Write($"Invalid Year! Enter a year between 1900 and {DateTime.Now.Year}: ");
                int.TryParse(Console.ReadLine(), out year);
            }

            int month = ReadInt("Enter Month (1-12): ");
            while (month < 1 || month > 12)
            {
                Console.Write("Invalid Month! Enter a value between 1 and 12: ");
                int.TryParse(Console.ReadLine(), out month);
            }

            int maxDays = DateTime.DaysInMonth(year, month);
            int day = ReadInt($"Enter Day (1-{maxDays}): ");
            while (day < 1 || day > maxDays)
            {
                Console.Write($"Invalid Day! Enter a value between 1 and {maxDays}: ");
                int.TryParse(Console.ReadLine(), out day);
            }

            return new HiringDate(day, month, year);
        }
    }
}