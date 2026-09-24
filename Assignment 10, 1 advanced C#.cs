using System;
using System.Collections;
using System.Collections.Generic;

namespace Session_10_Assignment
{
    // ============================================================
    // Q1 - Optimized Bubble Sort
    // ============================================================

    internal class Helper<T> where T : IComparable<T>
    {
        public static void Swap(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        public static void OptimizedBubbleSort(T[] arr)
        {
            if (arr is null)
                return;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                bool swapped = false;

                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }
    }

    // ============================================================
    // Q2 - Generic Range<T>
    // ============================================================

    internal class Range<T> where T : IComparable<T>
    {
        public T Minimum { get; set; }
        public T Maximum { get; set; }

        public Range(T minimum, T maximum)
        {
            if (minimum.CompareTo(maximum) > 0)
                throw new ArgumentException(
                    "Minimum value cannot be greater than Maximum value."
                );

            Minimum = minimum;
            Maximum = maximum;
        }

        public bool IsInRange(T value)
        {
            return value.CompareTo(Minimum) >= 0
                && value.CompareTo(Maximum) <= 0;
        }

        public dynamic Length()
        {
            dynamic max = Maximum;
            dynamic min = Minimum;

            return max - min;
        }
    }

    // ============================================================
    // Q5 - FixedSizeList<T>
    // ============================================================

    internal class FixedSizeList<T>
    {
        private T[] items;

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return items.Length;
            }
        }

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException(
                    "Capacity must be greater than zero."
                );

            items = new T[capacity];
            Count = 0;
        }

        public void Add(T item)
        {
            if (Count == Capacity)
            {
                throw new InvalidOperationException(
                    "The FixedSizeList is full. You cannot add more elements."
                );
            }

            items[Count] = item;
            Count++;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(
                    "Invalid index."
                );
            }

            return items[index];
        }
    }

    internal class Program
    {
        // ========================================================
        // Q3 - Reverse ArrayList In-Place
        // ========================================================

        private static void ReverseArrayList(ArrayList arrayList)
        {
            if (arrayList is null)
                return;

            int left = 0;
            int right = arrayList.Count - 1;

            while (left < right)
            {
                object temp = arrayList[left];

                arrayList[left] = arrayList[right];
                arrayList[right] = temp;

                left++;
                right--;
            }
        }

        // ========================================================
        // Q4 - Get Even Numbers
        // ========================================================

        private static List<int> GetEvenNumbers(List<int> numbers)
        {
            List<int> evenNumbers = new List<int>();

            if (numbers is null)
                return evenNumbers;

            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                {
                    evenNumbers.Add(number);
                }
            }

            return evenNumbers;
        }

        // ========================================================
        // Q6 - First Non-Repeated Character
        // ========================================================

        private static int FirstNonRepeatedCharacter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return -1;

            Dictionary<char, int> frequency =
                new Dictionary<char, int>();

            foreach (char character in str)
            {
                if (frequency.ContainsKey(character))
                {
                    frequency[character]++;
                }
                else
                {
                    frequency.Add(character, 1);
                }
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (frequency[str[i]] == 1)
                {
                    return i;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            #region Q1 - Optimized Bubble Sort

            int[] arr =
            {
                10,
                2,
                3,
                -1,
                5,
                0,
                1,
                8,
                -2
            };

            Helper<int>.OptimizedBubbleSort(arr);

            Console.WriteLine("------ Sorted Array ------");

            foreach (int number in arr)
            {
                Console.WriteLine(number);
            }

            #endregion

            #region Q2 - Range

            Range<int> range = new Range<int>(10, 50);

            Console.WriteLine("\n------ Range ------");

            Console.WriteLine($"Minimum = {range.Minimum}");
            Console.WriteLine($"Maximum = {range.Maximum}");

            Console.WriteLine(
                $"Is 20 inside range? {range.IsInRange(20)}"
            );

            Console.WriteLine(
                $"Is 100 inside range? {range.IsInRange(100)}"
            );

            Console.WriteLine(
                $"Range Length = {range.Length()}"
            );

            #endregion

            #region Q3 - Reverse ArrayList

            ArrayList arrayList = new ArrayList();

            arrayList.Add(1);
            arrayList.Add(2);
            arrayList.Add(3);
            arrayList.Add(4);
            arrayList.Add(5);

            Console.WriteLine(
                "\n------ ArrayList Before Reverse ------"
            );

            foreach (var element in arrayList)
            {
                Console.WriteLine(element);
            }

            ReverseArrayList(arrayList);

            Console.WriteLine(
                "\n------ ArrayList After Reverse ------"
            );

            foreach (var element in arrayList)
            {
                Console.WriteLine(element);
            }

            #endregion

            #region Q4 - Even Numbers

            List<int> numbers = new List<int>()
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            };

            List<int> evenNumbers =
                GetEvenNumbers(numbers);

            Console.WriteLine(
                "\n------ Even Numbers ------"
            );

            foreach (int number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            #endregion

            #region Q5 - FixedSizeList

            FixedSizeList<int> fixedList =
                new FixedSizeList<int>(3);

            fixedList.Add(10);
            fixedList.Add(20);
            fixedList.Add(30);

            Console.WriteLine(
                "\n------ Fixed Size List ------"
            );

            Console.WriteLine(fixedList.Get(0));
            Console.WriteLine(fixedList.Get(1));
            Console.WriteLine(fixedList.Get(2));

            #endregion

            #region Q6 - First Non-Repeated Character

            string text = "swiss";

            int index =
                FirstNonRepeatedCharacter(text);

            Console.WriteLine(
                "\n------ First Non-Repeated Character ------"
            );

            Console.WriteLine(
                $"Index = {index}"
            );

            if (index != -1)
            {
                Console.WriteLine(
                    $"Character = {text[index]}"
                );
            }

            #endregion
        }
    }
}
