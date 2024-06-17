using System;

namespace lab
{
    public class Program
    {
        // Это тестовый метод. Его можно удалить из проекта.
        /// <summary>
        /// Возводит число в квадрат.
        /// </summary>
        /// <param name="number">Число для возведения в квадрат</param>
        /// <returns>Квадрат числа</returns>
        public static int GetSquare(int number)
        {
            return number * number;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            int number = 4;
            int squaredNumber = GetSquare(number);
            Console.WriteLine(squaredNumber);
        }
    }
}
