using System;
using System.Globalization;

namespace WorkWithMethods
{
    /// <summary>
    /// класс для ввода правильного номера месяца : числа в диапазоне от 1 до 12
    /// </summary>
    class RIN
    {
        int min = 1;   // первый месяц года
        int max = 12;  // последний месяц года
        public string info = string.Empty; // параметр для формирования диагностического сообщения
        public bool n_bool; // флаг успешного преобразования

        // Ограниченное число: поле n и свойство N
        int n;
        public int N    // свойство
        {
            get { return n; } // возвращаем значение 
            set // устанавливаем значение 
            {
                if (value < min) // проверяем на выход введённого значения за нижнюю границу
                {
                    n = min;
                    n_bool = false;
                }
                else if (value > max) // проверяем на выход введённого значения за верхнюю границу
                {
                    n = max;
                    n_bool = false;
                }
                else // возвращаем значение в указанном диапазоне
                {
                    n = value;
                    n_bool = true;
                }
            }
        }
        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="n_st"></param>
        public RIN(string n_st)
        {
            n_bool = true; // возвращаем флаг, что всё прошло успешно
            try
            {
                N = Convert.ToInt32(n_st); // попытка конвертации строки в число
                if (!n_bool) // если число вне диапазона
                {
                    // формируем диагностическое сообщение
                    info = $"Ошибка ввода параметра. Число {n_st} вне диапазона. Для изменения введите целое число от " + min.ToString() + " до " + max.ToString();
                }
            }
            catch // ошибка конвертации: пользователь ввёл НЕ число
            {
                // формируем диагностическое сообщение
                info = "Ошибка ввода параметра. Введите целое число от " + min.ToString() + " до " + max.ToString() + "."; ;
                n_bool = false; // устанавливам флаг, что все прошло НЕ успешно
            }
        }
    }   // end class RIN
    class Program
    {
        /// <summary>
        /// сформируем список времён года, с английскими названиями
        /// </summary>
        public enum Seasons
        {
            Winter = 1,
            Spring = 2,
            Summer = 3,
            Autumn = 4
        }
        /// <summary>
        /// сформируем список времён года, соответствующим английским названиям
        /// </summary>
        public enum rusSeasons
        {
            Зима = Seasons.Winter,
            Весна = Seasons.Spring,
            Лето = Seasons.Summer,
            Осень = Seasons.Autumn
        }
        /// <summary>
        /// метод форматирования аргументов в строку
        /// метод принимает аргументы - фамилия, имя, отчество
        /// возвращает ФИО в формате "Фамилия Имя Отчество"
        /// </summary>
        /// <param name="string firstName" имя></param>
        /// <param name="string lastName" отчество></param>
        /// <param name="string patronymic" фамилия></param>
        static string GetFullName(string firstName, string lastName, string patronymic)
        {
            var _fn = firstName.Replace(" ", ""); // удалим возможные пробелы в имени
            var _ln = lastName.Replace(" ", ""); // удалим возможные пробелы в отчестве
            var _p = patronymic.Replace(" ", ""); // удалим возможные пробелы в фамилии

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo; // обратим внимание на региональные различия

            return ti.ToTitleCase(_fn + " " + _ln + " " + _p); // выведем строку в которой все слова начинаются с заглавных символов
        }
        /// <summary>
        /// метод вычисления суммы чисел, переданных в строке - аргументе
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static string GetSumm(string args)
        {
            string[] _arg_str = args.Split(); // целую строку преобразуем в массив строк
            double _sum = 0.0; // локальная переменная для хранения суммы чисел
            foreach (var _a in _arg_str) // в цикле просуммируем введённые значения
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." }; // зафиксируем единый формат для преобразования чисел
                try
                {
                    _sum += double.Parse(_a, formatter); // отформатируем и преобразуем число, затем просуммируем с сохранённым значением суммы
                }
                catch
                {
                    return $"Введённое значение {_a} не является числом. Завершаю работу."; // если в момент преобразования поймём, что введённое значение не является числом,
                                                                                            // аварийно завершим метод с соответствующим сообщением пользователю
                }
            }

            return _sum.ToString(); // возвращаем сумму чисел
        }
        /// <summary>
        /// метод определяет время года
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        static Seasons GetSeason(int Month)
        {
            Seasons _m = Seasons.Winter; // определим возвращаемое по умолчанию значение 
            switch (Month) // определим принадлежность введённого номера месяца к времени года
            {
                case 3:
                case 4:
                case 5: 
                    _m = Seasons.Spring; // введённое значение месяца соответствует весне
                    break;
                case 6:
                case 7:
                case 8: 
                    _m = Seasons.Summer; // введённое значение месяца соответствует лету
                    break;
                case 9:
                case 10:
                case 11: 
                    _m = Seasons.Autumn; // введённое значение месяца соответствует осени
                    break;
            }
            return _m; // возвращаем полученное значение времени года
        }

        /// <summary>
        /// метод возвращает название времени года в соответствии с введённым пользователем номером месяца года
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static rusSeasons GetRusSeason(int Month)
        {
            Seasons _s = GetSeason(Month);
            return (rusSeasons)_s;
        }
        /// <summary>
        /// основной метод
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Выполняю первое задание:");

            Console.WriteLine(GetFullName("Иванов", "семен", "Михайлович"));
            Console.WriteLine(GetFullName("Григ орьева", "Галина", " петровна"));
            Console.WriteLine(GetFullName("Рабинович", "Сам уил", "Иосифович "));

            Console.WriteLine("Готово!\n");
            Console.WriteLine("Нажмите любую кнопку...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Выполняю второе задание:\n");
            Console.WriteLine("Введите любое количество чисел. Числа должны быть разделены пробелами");
            Console.WriteLine(GetSumm(Console.ReadLine()));
            Console.WriteLine("Готово!\n");
            Console.WriteLine("Нажмите любую кнопку...");
            Console.ReadKey();
            Console.Clear();
            
            Console.WriteLine("Выполняю третье задание:");

            // определим начальные значения
            var _month = 0; 
            var n_bool = false;
            do
            {
                Console.WriteLine("Введите номер месяца года:");
                RIN k = new RIN(Console.ReadLine()); // определим, соответствует ли введённая пользователем строка правилам: число в диапазоне от 1 до 12
                _month = k.N; // сохраним введённое число для дальнейего использования
                n_bool = k.n_bool; // сохраним флаг успешности ввода
                if (!n_bool) // если введённая строка не соблюдает правила...
                {
                    Console.WriteLine(k.info); // ... выведем соответствующее диагностическое сообщение
                }
            }
            while (!n_bool); // будем повторять ввод, пока не будет введёно число от 1 до 12
            // определим время года и выведем на экран
            Console.WriteLine("Введёный номер месяца года " + _month.ToString() + " соответствует времени года: " + GetRusSeason(_month));

            Console.WriteLine("\nГотово!");
        }
    }
}
