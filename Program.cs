using System;

namespace Lesson7
{
    class Program
    {
        /// <summary>
        /// выполняем задание урока 7
        /// </summary>
        static void Main(string[] args)
        {
            // просим пользователя ввести строку
            Console.WriteLine("Введите строку:");

            // преобразуем строку в массив символов
            char[] symbols = Console.ReadLine().ToCharArray();

            // переменная для вывода информации на экран
            Console.WriteLine("Выполняем задание:");
            for (int i = symbols.Length - 1; i >= 0; i--)
            {
                Console.Write("{0}", symbols[i]);
            }
            Console.WriteLine();
        }
    }
}
