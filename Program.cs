using System;
using System.Collections.Generic;

namespace BaseType
{
    public class Program
    {
        /// <summary>
        /// сформируем список месяцев
        /// </summary>
        public enum Months
        { 
            Январь = 1,
            Февраль = 2,
            Март = 3,
            Апрель = 4,
            Май = 5,
            Июнь = 6,
            Июль = 7,
            Август = 8,
            Сентябрь = 9,
            Октябрь = 10,
            Ноябрь = 11,
            Декабрь = 12
        }

        /// <summary>
        /// определим рабочие дни недели
        /// </summary>
        [Flags]
        public enum WorkSchedule
        {
            Monday      = 0b_0000001,
            Tuesday     = 0b_0000010,
            Wednesday   = 0b_0000100,
            Thursday    = 0b_0001000,
            Friday      = 0b_0010000,
            Saturday    = 0b_0100000,
            Sunday      = 0b_1000000,
        }
        
        static void Main(string[] args)
        {
            // запросим данные у пользователя и обработаем их в соответствии с заданием
            WotkWithUser();

            // покажем информацию о чеке
            Check();

            // покажем расписание рабочих дней для фирмы Альфа
            Schedule("Альфа", "0011001");

            // покажем расписание рабочих дней для фирмы Омега
            Schedule("Омега", "1111001");
        }

        /// <summary>
        /// поработаем с пользователем 
        /// </summary>
        private static void WotkWithUser()
        {
            // string[] зимниеМесяцы = { "Январь", "Февраль", "Декабрь"};
            List<int> WinterMonth = new List<int>(3) { 1, 2, 12 };

            // переменная для хранения минимальной температуры
            double minTemp;
            // переменная для хранения максимальной температуры
            double maxTemp;
            // пользователь вводит минимальную температуру
            Console.Write("Введите минимальную температуру: ");
            string Temp = Console.ReadLine();
            minTemp = Double.Parse(Temp);

            // пользователь вводит максимальную температуру
            Console.Write("Введите максимальную температуру: ");
            Temp = Console.ReadLine();
            maxTemp = Double.Parse(Temp);

            // вычислим среднюю температуру
            Double average = (minTemp + maxTemp) / 2;
            Console.WriteLine("Средняя температура = " + string.Format("{0:N2}", average));

            // попросим пользователя ввести номер месяца
            Console.Write("Введите порядковый номер месяца (1 - 12): ");
            Temp = Console.ReadLine();
            int Month = Int32.Parse(Temp);
            Console.WriteLine("Месяц = " + (Months)Month);

            // какое число введено пользователем - четное или не четное?
            string prefix = string.Empty;
            if ((Int32.Parse(Temp) & 1) != 0)
                prefix = "не";
            Console.WriteLine($"Вы ввели {prefix}четное число");


            if ((WinterMonth.Contains(Month)) & (average > 0))
                Console.WriteLine("Дождливая зима");
        }

        /// <summary>
        /// определем рабочее расписание на неделю для фирмы
        /// аргумент Расписание - строка из 7 чимволов. "1" указываем рабочие дни недели, "0" указываем не рабочие дни недели.Начинается неделя с понедельника
        /// аргумент Фирма - название фирмы
        /// </summary>
        private static void Schedule(string Firm, string schedule)
        {
            Console.WriteLine();
            Console.WriteLine($"Покажем рабочее расписание фирмы {Firm}");
            Console.WriteLine();

            char[] days = schedule.ToCharArray();
            // приведём к единому виду
            WorkSchedule _schedule = 0b_0000000;
            // сформатируем расписание
            for (int i=0; i < 7; i++)
            {
                switch (i)
                {
                    case 0:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Monday;
                        break;
                    case 1:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Tuesday;
                        break;
                    case 2:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Wednesday;
                        break;
                    case 3:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Thursday;
                        break;
                    case 4:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Friday;
                        break;
                    case 5:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Saturday;
                        break;
                    case 6:
                        if (days[i] == '1')
                            _schedule = _schedule | WorkSchedule.Sunday;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine($"Расписание рабочих дней фирмы \"{Firm}\": " + _schedule.ToString()); /**/
        }

        /// <summary>
        /// выводим информацию по чеку
        /// </summary>
        private static void Check()
        {
            string shop = "Игрушки";
            int numpurchase = 5;
            double sumpurchase = 9549.12;

            Console.WriteLine();
            Console.WriteLine("Покажем чек из магазина");
            Console.WriteLine();

            Console.WriteLine("{0,-28}{1,10}", "Магазин: ", shop);
            Console.WriteLine("{0,-28}{1,10:d}", "Дата покупок: ", DateTime.Now);
            Console.WriteLine("{0,-28}{1,10}", "Количество покупок:", numpurchase);
            Console.WriteLine("{0,-28}{1,10}", "Общая сумма покупок, руб.: ", sumpurchase);

        }
    }
}
