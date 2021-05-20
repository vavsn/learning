using System;

namespace InputUserName
{
    class Program
    {
        static void Main(string[] args)
        {
            // переменная для хранения имени пользователя
            string UserName = string.Empty;
            Console.Write("Введите свое имя: ");
            UserName = Console.ReadLine();
            if (UserName == string.Empty)
                UserName = Environment.UserName;
            Console.Write($"Привет, {UserName}, сегодня {DateTime.Now.ToString("dd MMMM yyyy")} г.");
        }
}
}
