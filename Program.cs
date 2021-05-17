using System;

namespace ArrayOperations
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Выполняем 1 задание:\n");
            Task1();
            Console.WriteLine("\nВыполняем 2 задание:\n");
            Task2();
            Console.WriteLine("\nВыполняем 3 задание:\n");
            Task3();
        }

        /// <summary>
        /// функция выполняет задание № 1
        /// </summary>
        static void Task1()
        {
            // определим массив для работы
            int[,] arrInt = new int[2, 3];

            // определим количество строк и столбцов
            int rows = arrInt.GetUpperBound(0) + 1;
            int columns = arrInt.GetUpperBound(1) + 1;
            // заполним массив случайными целыми числами от 1 до 9
            var rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    arrInt[i, j] = rand.Next(9);
                }
            }

            // выведем на экран первоначальный массив
            Console.WriteLine("Массив для работы получился такой:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{arrInt[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Выполняем задание:");
            // переменная для вывода информации на экран
            string outpuString = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    outpuString = " ";
                    if (i == j)
                        outpuString = arrInt[i, j].ToString();
                    Console.Write($"{outpuString} ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// функция выполняет задание № 2
        /// </summary>
        static void Task2()
        {
            // определим и заполним массив для работы
            string[,] arrNames = new string[5, 2] { 
                { "Иванов", "8-902-902-90-20" }, 
                { "Петров", "8-902-902-90-21" }, 
                { "Сидоров", "8-902-902-90-22" }, 
                { "Грелкин", "8-902-902-90-23" }, 
                { "Холодилкин", "8-902-902-90-24" } };

            // определим количество строк и столбцов
            int rows = arrNames.GetUpperBound(0) + 1;
            int columns = arrNames.GetUpperBound(1) + 1;

            // переменная для вывода информации на экран
            Console.WriteLine("Телефонный справочник:");
            // переменная для вывода информации на экран
            string outpuString = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("Имя контакта: {0,-12} телефон {1}", arrNames[i, 0].ToString(), arrNames[i, 1].ToString());
            }
        }
        /// <summary>
        /// функция выполняет задание № 3
        /// </summary>
        static void Task3()
        {
            // просим пользователя ввести строку
            Console.WriteLine("Введите строку:");
            string strForInvert = Console.ReadLine();

            // преобразуем строку в массив символов
            char[] symbols = strForInvert.ToCharArray();

            // переменная для вывода информации на экран
            Console.WriteLine("Выполняем задание:");
            for (int i = symbols.Length-1; i >= 0; i--)
            {
                Console.Write("{0}", symbols[i]);
            }
            Console.WriteLine();
        }
    }
}
