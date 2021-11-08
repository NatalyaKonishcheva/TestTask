using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (GetTaskNum())
            {
                case 1:
                    Console.WriteLine("Введите строку для проверки на палиндром");
                    if(Task1(Console.ReadLine()))
                        Console.WriteLine("Строка является палиндромом");
                    else
                        Console.WriteLine("Строка не является палиндромом");

                    break;
                case 2:
                    Console.WriteLine($"Ближайшая пятница: {Task2().Day}.{Task2().Month}.{Task2().Year}");


                    break;
                case 3:
                    Console.WriteLine("Введите массив с элементами через пробел");
                    string valueAfterReading = Console.ReadLine();
                    string[] array = valueAfterReading.Split(' ');
                    if (array.Length < 2)
                    {
                        Console.WriteLine("Недостаточно элементов в массиве, попробуйте снова");
                        return;
                    }
                    
                    Console.WriteLine("Введите индекс первого элемента");
                    valueAfterReading = Console.ReadLine();
                    int firstIndex;
                    if (!Int32.TryParse(valueAfterReading, out firstIndex))
                    {
                        Console.WriteLine("Некорректное значение, пропробуйте снова");
                        return;
                    }
                        
                    Console.WriteLine("Введите индекс второго элемента");
                    valueAfterReading = Console.ReadLine();
                    int secondIndex;
                    if (!Int32.TryParse(valueAfterReading, out secondIndex))
                    {
                        Console.WriteLine("Некорректное значение, пропробуйте снова");
                        return;
                    }   

                    Console.WriteLine($"Получившийся массив: ");
                    string[] resultArray = Task3(array, firstIndex, secondIndex);
                    for (int i = 0; i < resultArray.Length; i++)
                    {
                        Console.Write($"{resultArray[i]} ");
                    }
                    Console.WriteLine("");
                    break;
                case 4:
                    Console.WriteLine("Введите массив с элементами через пробел");
                    string AfterReading = Console.ReadLine();
                    string[] stringArray = AfterReading.Split(' ');
                    double[] doubleArray = new double[stringArray.Length];

                    for(int i = 0; i < stringArray.Length; i++)
                    {
                        if(!Double.TryParse(stringArray[i], out doubleArray[i]))
                        {
                            Console.WriteLine("Некорректное значение, пропробуйте снова");
                            return;
                        }    
                    }
                    Array.Sort(doubleArray);

                    Console.WriteLine("Введите значение, индекс которого нужно найти");
                    string stringValue = Console.ReadLine();
                    double doubleValue;
                    if(!Double.TryParse(stringValue, out doubleValue))
                    {
                        Console.WriteLine("Некорректное значение, пропробуйте снова");
                        return;
                    }

                    if (Task4(doubleArray, doubleValue) == null)
                        Console.WriteLine("Такого значения нет в массиве");
                    else
                        Console.WriteLine($"Значение {doubleValue} находится по индексу {Task4(doubleArray, doubleValue)} в отсортированном массиве");
                    break;
                default:
                    Console.WriteLine("Ошибка!");
                    break;
            }
        }

        public static int GetTaskNum()
        {
            Console.WriteLine("Введите номер задания для запуска одной из функций");
            int TaskNum;
            while (!Int32.TryParse(Console.ReadLine(), out TaskNum) || TaskNum < 1 && TaskNum > 4)
            {
                Console.WriteLine("Некорректный ввод или несуществующий номер задания, попробуйте, снова");
            }
            return TaskNum;
        } 

        public static bool Task1(string TestString)
        {
            for(int i = 0; i < TestString.Length; i++)
            {
                if (!Char.IsLetter(TestString, i))
                {
                    TestString = TestString.Remove(i, 1);
                    i--;
                }
            }

            TestString = TestString.ToLower();
            for(int i = 0; i < TestString.Length / 2; i++)
            {
                if (TestString[i].Equals(TestString[TestString.Length - 1 - i]))
                    continue;
                else
                    return false;
            }
            return true;
        }

        public static DateTime Task2()
        {
            DateTime futureFriday = DateTime.Now;

            while (!futureFriday.DayOfWeek.Equals(DayOfWeek.Friday))
            {
                futureFriday = futureFriday.AddDays(1);
            }

            DateTime pastFriday = DateTime.Now;

            while (!pastFriday.DayOfWeek.Equals(DayOfWeek.Friday))
            {
                pastFriday = pastFriday.AddDays(-1);
            }
            if ((DateTime.Now - pastFriday) < (futureFriday - DateTime.Now))
                return pastFriday;
            else return futureFriday;;
        }

        public static string[] Task3(string[] array, int firstElemIndex, int secondElemIndex)
        {
            string a = array[firstElemIndex];
            array[firstElemIndex] = array[secondElemIndex];
            array[secondElemIndex] = a;
            return array;
        }

        public static int? Task4(double[] array, double value)
        {
            return BinarySearch(array, value, 0, array.Length);
        }
        private static int? BinarySearch(double[] array, double value, int leftIndex, int rightIndex)
        {
            int middleIndex = (rightIndex - leftIndex) / 2;

            if (value == array[middleIndex])
                return middleIndex;
            else if (leftIndex == rightIndex)
                return null;
            else if (array[middleIndex] > value)
                return BinarySearch(array, value, leftIndex, middleIndex);
            else
                return BinarySearch(array, value, middleIndex, rightIndex);
        }
    }
}
