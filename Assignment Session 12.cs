using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static Session_12.ListGenerators;

namespace Session_12
{
    // LINQ Assignment 01 - Complete Solution
    // Required: ListGenerators.cs (provided alongside this file).
    // Place Customers.xml next to the project or the executable to use the full customer data.
    // Place dictionary_english.txt there as well to run Aggregate questions 5-8.
    // Replace your existing Program.cs with this file (do not keep two Main methods).
    internal class Program
    {
        private static readonly string[] Digits =
        {
            "zero", "one", "two", "three", "four",
            "five", "six", "seven", "eight", "nine"
        };

        private static readonly int[] Numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        private static readonly string[] MixedCaseWords =
        {
            "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"
        };

        private static void Main(string[] args)
        {
            List<CustomerRecord> customers = LoadCustomers();
            string[] dictionaryWords = LoadDictionaryWords();

            RestrictionOperators();
            ElementOperators();
            AggregateOperators(customers, dictionaryWords);
            OrderingOperators();
            TransformationOperators(customers);
        }

        #region LINQ - Restriction Operators (Where)
        private static void RestrictionOperators()
        {
            Section("LINQ - Restriction Operators");

            // 1. Find all products that are out of stock.
            Question("1. Products that are out of stock");
            var result1 = ProductList.Where(p => p.UnitsInStock == 0);
            PrintItems(result1);

            // 2. Find all products in stock and costing more than 3.00 per unit.
            Question("2. In-stock products with UnitPrice > 3.00");
            var result2 = ProductList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00M);
            PrintItems(result2);

            // 3. Return digit names shorter than their numeric value (the index).
            Question("3. Digit names shorter than the digit's value");
            var result3 = Digits.Where((name, value) => name.Length < value);
            PrintItems(result3);
        }
        #endregion

        #region LINQ - Element Operators
        private static void ElementOperators()
        {
            Section("LINQ - Element Operators");

            // 1. Get the first product that is out of stock.
            Question("1. First product out of stock");
            var result1 = ProductList.First(p => p.UnitsInStock == 0);
            Console.WriteLine(result1);

            // 2. Get the first product priced over 1000; return null if none exists.
            Question("2. First product with UnitPrice > 1000 (or null)");
            var result2 = ProductList.FirstOrDefault(p => p.UnitPrice > 1000M);
            Console.WriteLine(result2 == null ? "null" : result2.ToString());

            // 3. Retrieve the second number greater than 5.
            Question("3. Second number greater than 5");
            var result3 = Numbers.Where(n => n > 5).ElementAt(1);
            Console.WriteLine(result3);
        }
        #endregion

        #region LINQ - Aggregate Operators
        private static void AggregateOperators(List<CustomerRecord> customers, string[] words)
        {
            Section("LINQ - Aggregate Operators");

            // 1. Count the odd numbers in the array.
            Question("1. Count odd numbers");
            var result1 = Numbers.Count(n => n % 2 != 0);
            Console.WriteLine(result1);

            // 2. List customers and how many orders each has.
            Question("2. Customers and their order counts");
            var result2 = customers.Select(c => new
            {
                Customer = c.Name,
                OrderCount = c.Orders.Count
            });
            PrintItems(result2);

            // 3. List categories and the number of products in each.
            Question("3. Product count by category (GroupBy)");
            var result3 = ProductList
                .GroupBy(p => p.Category)
                .Select(group => new
                {
                    Category = group.Key,
                    ProductCount = group.Count()
                });
            PrintItems(result3);

            // 4. Get the sum of the numbers in the array.
            Question("4. Sum of all numbers");
            var result4 = Numbers.Sum();
            Console.WriteLine(result4);

            // Questions 5-8: Read dictionary_english.txt into an array of strings first.
            // The dictionary file was not included with the assignment attachments.
            if (words.Length == 0)
            {
                Question("5-8. Dictionary word length statistics");
                Console.WriteLine("Skipped: dictionary_english.txt was not found (or is empty).");
                Console.WriteLine("Add dictionary_english.txt to the project folder and run again.");
                return;
            }

            // 5. Total number of characters of all words in the dictionary.
            Question("5. Total number of characters in dictionary words");
            var result5 = words.Sum(word => (long)word.Length);
            Console.WriteLine(result5);

            // 6. Length of the shortest dictionary word.
            Question("6. Shortest dictionary word length");
            var result6 = words.Min(word => word.Length);
            Console.WriteLine(result6);

            // 7. Length of the longest dictionary word.
            Question("7. Longest dictionary word length");
            var result7 = words.Max(word => word.Length);
            Console.WriteLine(result7);

            // 8. Average word length in the dictionary.
            Question("8. Average dictionary word length");
            var result8 = words.Average(word => word.Length);
            Console.WriteLine(result8.ToString("F2", CultureInfo.InvariantCulture));
        }
        #endregion

        #region LINQ - Ordering Operators
        private static void OrderingOperators()
        {
            Section("LINQ - Ordering Operators");
            var caseInsensitiveComparer = new CaseInsensitiveWordComparer();

            // 1. Sort products by product name.
            Question("1. Products ordered by name");
            var result1 = ProductList.OrderBy(p => p.ProductName);
            PrintItems(result1);

            // 2. Case-insensitive word sorting with a custom comparer.
            Question("2. Case-insensitive alphabetical sort (custom comparer)");
            var result2 = MixedCaseWords.OrderBy(word => word, caseInsensitiveComparer);
            PrintItems(result2);

            // 3. Sort products by units in stock, highest first.
            Question("3. Products by UnitsInStock (descending)");
            var result3 = ProductList.OrderByDescending(p => p.UnitsInStock);
            PrintItems(result3);

            // 4. Sort digit names by length, then alphabetically.
            Question("4. Digit names by length, then alphabetical order");
            var result4 = Digits.OrderBy(word => word.Length).ThenBy(word => word);
            PrintItems(result4);

            // 5. Sort words by length, then case-insensitively.
            Question("5. Words by length, then case-insensitive alphabetical order");
            var result5 = MixedCaseWords
                .OrderBy(word => word.Length)
                .ThenBy(word => word, caseInsensitiveComparer);
            PrintItems(result5);

            // 6. Sort products by category, then by unit price descending.
            Question("6. Products by category, then UnitPrice (descending)");
            var result6 = ProductList
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);
            PrintItems(result6);

            // 7. Sort words by length, then case-insensitively in descending order.
            Question("7. Words by length, then case-insensitive descending order");
            var result7 = MixedCaseWords
                .OrderBy(word => word.Length)
                .ThenByDescending(word => word, caseInsensitiveComparer);
            PrintItems(result7);

            // 8. Keep digits whose SECOND letter is 'i', then reverse their order.
            Question("8. Digit names whose second letter is 'i', in reverse order");
            var result8 = Digits
                .Where(word => word.Length >= 2 && word[1] == 'i')
                .Reverse();
            PrintItems(result8);
        }
        #endregion

        #region LINQ - Transformation Operators (Select / SelectMany)
        private static void TransformationOperators(List<CustomerRecord> customers)
        {
            Section("LINQ - Transformation Operators");

            // 1. Return only the names of the products.
            Question("1. Product names");
            var result1 = ProductList.Select(p => p.ProductName);
            PrintItems(result1);

            // 2. Return uppercase and lowercase versions (anonymous types).
            Question("2. Uppercase and lowercase versions of words");
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var result2 = words.Select(word => new
            {
                Upper = word.ToUpperInvariant(),
                Lower = word.ToLowerInvariant()
            });
            PrintItems(result2);

            // 3. Return selected product properties; rename UnitPrice to Price.
            Question("3. Selected product properties (UnitPrice renamed Price)");
            var result3 = ProductList.Select(p => new
            {
                p.ProductID,
                p.ProductName,
                p.Category,
                Price = p.UnitPrice
            });
            PrintItems(result3);

            // 4. Check whether each number equals its index in the array.
            Question("4. Is each number in its matching array position?");
            var result4 = Numbers.Select((number, index) => new
            {
                Number = number,
                InPlace = number == index
            });
            foreach (var item in result4)
                Console.WriteLine($"{item.Number}: {item.InPlace}");

            // 5. Produce all pairs (a, b) for which a < b.
            Question("5. Pairs of numbers where a < b (SelectMany)");
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var result5 = numbersA
                .SelectMany(a => numbersB, (a, b) => new { A = a, B = b })
                .Where(pair => pair.A < pair.B);
            foreach (var pair in result5)
                Console.WriteLine($"{pair.A} is less than {pair.B}");

            // 6. Select all orders with a total below 500.00.
            Question("6. Orders with Total < 500.00");
            var result6 = customers
                .SelectMany(c => c.Orders)
                .Where(order => order.Total < 500.00);
            PrintItems(result6);

            // 7. Select all orders placed in 1998 or later.
            Question("7. Orders placed in 1998 or later");
            var result7 = customers
                .SelectMany(c => c.Orders)
                .Where(order => order.OrderDate.Year >= 1998);
            PrintItems(result7);
        }
        #endregion

        #region Helpers and Data Loading
        private static void Section(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 70));
        }

        private static void Question(string title)
        {
            Console.WriteLine();
            Console.WriteLine("--- " + title + " ---");
        }

        private static void PrintItems<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
                Console.WriteLine(item);
        }

        // Use the complete Customers.xml, rather than only the 3 demo customers.
        // Customer IDs in the XML are a mixture of numbers and strings, so we
        // keep them as strings instead of forcing them into Customer.Id (int).
        private static List<CustomerRecord> LoadCustomers()
        {
            string xmlPath = FindFile("Customers.xml");

            if (xmlPath == null)
            {
                Console.WriteLine("Customers.xml not found; using demo CustomerList instead.");
                return CustomerList.Select(c => new CustomerRecord
                {
                    Id = c.Id.ToString(CultureInfo.InvariantCulture),
                    Name = c.Name,
                    Orders = c.Orders == null ? new List<Order>() : c.Orders.ToList()
                }).ToList();
            }

            XDocument xml = XDocument.Load(xmlPath);
            return xml.Descendants("customer")
                .Select(customer => new CustomerRecord
                {
                    Id = (string)customer.Element("id") ?? "",
                    Name = (string)customer.Element("name") ?? "",
                    Orders = customer.Descendants("order")
                        .Select(order => new Order
                        {
                            Id = int.Parse((string)order.Element("id") ?? "0", CultureInfo.InvariantCulture),
                            OrderDate = DateTime.Parse((string)order.Element("orderdate") ?? "", CultureInfo.InvariantCulture),
                            Total = double.Parse((string)order.Element("total") ?? "0", CultureInfo.InvariantCulture)
                        })
                        .ToList()
                })
                .ToList();
        }

        private static string[] LoadDictionaryWords()
        {
            string dictionaryPath = FindFile("dictionary_english.txt");
            if (dictionaryPath == null)
                return Array.Empty<string>();

            // Read all lines into an array, as required by the assignment.
            return File.ReadAllLines(dictionaryPath)
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .ToArray();
        }

        // Finds files in the current directory or up the executable folder tree.
        // This works with typical Visual Studio and dotnet run folder layouts.
        private static string FindFile(string fileName)
        {
            string currentDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (File.Exists(currentDirectoryPath))
                return currentDirectoryPath;

            DirectoryInfo folder = new DirectoryInfo(AppContext.BaseDirectory);
            while (folder != null)
            {
                string candidate = Path.Combine(folder.FullName, fileName);
                if (File.Exists(candidate))
                    return candidate;
                folder = folder.Parent;
            }

            return null;
        }

        private sealed class CustomerRecord
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public List<Order> Orders { get; set; } = new List<Order>();
        }

        private sealed class CaseInsensitiveWordComparer : IComparer<string>
        {
            public int Compare(string left, string right)
            {
                return StringComparer.OrdinalIgnoreCase.Compare(left, right);
            }
        }
        #endregion
    }
}
