﻿using System;
using System.Diagnostics;

namespace TaskManager
{
    /// <summary>
    /// класс для хранения и вывода на экран информации по процессам
    /// </summary>
    class TaskMan: Process
    {
        public string name = string.Empty; // имя процесса для завершения
        public string info = string.Empty; // информационная строка для ввода на экран
        public bool kill = false; // признак того, что переданный процесс необходимо завершить
        public int ID = -1; // номер процесса для завершения
        Process[] lAll; // список всех процессов в системе
        public Process[] localAll // список всех процессов в системе
        {
            get { return lAll; } // возвращаем сохранённый список всех процессов в системе
            set { lAll = value; } // сохраняем в приватном параметре список всех процессов в системе
        }
        /// <summary>
        /// конструктор для случая, когда ни одного параметра не задано
        /// получаем список всех процессов в системе
        /// </summary>
        public TaskMan()
        {
            try
            {
                localAll = Process.GetProcesses(); // получаем и сохраняем список всех процессов в системе
                info = "Вывожу список всех процессов в системе"; // формируем информационное сообщение
            }
            catch (Exception ex) // обработка исключений
            {
                Console.WriteLine(ex.ToString()); // выводим на экран системное сообщение
            }
        }

        /// <summary>
        /// конструктор для случая, когда задан 1 параметр: либо ID процесса, либо имя процесса
        /// </summary>
        /// <param name="_name"></param>
        public TaskMan(string _name)
        {
            if (int.TryParse(_name, out ID)) // проверка: переданный параметр - число? если да, значит надо найти информацию по процессу с переданным кодом
            {
                try // на случай, если информация по процессу не будет найдена
                {
                    Process _p = Process.GetProcessById(ID); // получаем информацию по процессу с определённым кодом
                    info = "Вывожу информацию о процессе с ID = " + ID; // формируем информационное сообщение
                    name = Process.GetProcessById(ID).ProcessName; // получаем название этого процесса
                    if (localAll != null) // проверяем, есть или нет информация в локальном списке процессов? ...
                    {
                        Array.Clear(localAll, 0, localAll.Length); // ...если есть, обнуляем список
                        localAll[0] = Process.GetProcessById(ID); // добавляем информацию по найденному процессу
                    }
                    else
                    {
                        localAll = new Process[] { _p }; // создаём список и добавляем информацию по найденному процессу
                    }
                }
                catch (Exception ex) // обработка исключений
                {
                    Console.WriteLine(ex.ToString()); // выводим на экран системное сообщение
                }
            }
            else
            {
                name = _name; // сохраняем наименование процесса
                info = "Вывожу информацию о всех процессах с именем = " + name; // формируем информационное сообщение
                ID = -1; // кода процесса нет, т.к. процессов с указанным названием может быть много
                try
                {
                    localAll = Process.GetProcessesByName(name); // получаем список процессов с указанным названием 
                }
                catch (Exception ex) // обработка исключений
                {
                    Console.WriteLine(ex.ToString()); // выводим на экран системное сообщение
                }
            }
        }
        /// <summary>
        /// метод вывода на экран информации о процессах
        /// </summary>
        private void WriteConsole()
        {
            Console.WriteLine(info); // вывод на экран сформированного информационного сообщения

            Console.WriteLine("ID:    Name of Process"); // выводим на экран заголок
            int i = 1;

            foreach (var la in localAll) // выводим информацию в цикле
            {
                Console.WriteLine(string.Format("{0,6} {1}", la.Id, la.ProcessName)); // форматируем информацию для вывода на экран кода и названия процесса / процессов
                if (i % 23 == 0) // если список большой, логично показывать его кусками. 25 - количество строк в строковом представлении экрана компьютера,
                                 // поэтому выводим 23 строки информации + 2 строки информационного характера
                {
                    Console.WriteLine("----- ПРОДОЛЖИТЬ - НАЖАТЬ ЛЮБУЮ КЛАВИШУ -----"); // выводим на экран информационное сообщение
                    Console.WriteLine("----- ЗАКОНЧИТЬ - НАЖАТЬ КЛАВИШУ \"Q\" -----"); // выводим на экран информационное сообщение
                    ConsoleKeyInfo _c = Console.ReadKey(true);
                    if (_c.Key == ConsoleKey.Q)
                        break;
                }
                i++;
            }
        }
        /// <summary>
        /// метод выполнения каких-то действий с объектом
        /// </summary>
        public void DoIt()
        {
            if (kill) // если пользователь дал команду на завершение процесса...
            {
                if (localAll == null) // проверка, что список не пустой
                    return;
                string _name = name; // ... сохраним название процесса, для итогового информационного сообщения
                int _id = ID; // ... сохраним код процесса, для итогового информационного сообщения
                foreach (var lP in localAll) // удаление проводим в цикле. в случае, когда пользователь указывает название процесса, самих процессов может быть много. их надо завершить все.
                {
                    try
                    {
                        lP.Kill(); // пробуем удалить процесс в "жестком" режиме.
                    }
                    catch (Exception ex) // обработка исключений
                    {
                        Console.WriteLine(ex.ToString()); // выводим на экран системное сообщение
                        break; // прерываем выполнение текущего шага цикла
                    }
                }
                if (_id == -1) // признак того, что в программу передано название процесса
                    info = "Удалены процессы с именем = " + _name; // формируем информационное сообщение 
                else // признак того, что в программу передан код процесса.
                    info = "Удален процесс с ID = " + _id; // формируем информационное сообщение
                return;
            }

            WriteConsole(); // вызываем метод вывода информации на экран
        }
    }

    class Program
    {
        /// <summary>
        /// основной метод
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            Console.WriteLine("Приступаю к выполнению задания"); // выводим на экран информационное сообщение

            TaskMan TM = null; // создаем пустой объект нужного типа, чтобы не зависеть от областей видимости переменных

            switch (args.Length) // анализируем количество переданных аргументов
            { 
                case 0: // пользователь не указал аргументы
                    TM = new TaskMan(); // заполняем объект информацией о всех процессах в системе
                    TM.kill = false; // признак того, что пользователь не даёт команду на завершение процесса
                    TM.DoIt(); // выполняем действия с объектом
                    break;
                case 1: // пользователь указал 1 аргумент: либо код процесса, либо название процесса
                    TM = new TaskMan(args[0]); // заполняем объект информацией в зависимости от значения аргумента
                    TM.kill = false; // признак того, что пользователь не даёт команду на завершение процесса
                    TM.DoIt(); // выполняем действия с объектом
                    break;
                case 2:
                    if (args[0] == "KILL" | args[0] == "K") // пользователь даёт команду на завершение процесса, 2 аргумент - код или название процесса
                    {
                        TM = new TaskMan(args[1]); // заполняем объект информацией в зависимости от значения 2 аргумента
                        TM.kill = true; // признак того, что пользователь даёт команду на завершение процесса
                        TM.DoIt(); // выполняем действия с объектом
                        break;
                    }
                    if (args[0] == "START" | args[0] == "S") // пользователь даёт команду на старт процесса, 2 аргумент - название программы
                    {
                        try
                        {
                            Process notepad = Process.Start(args[1]); // запускаем процес
                            Console.WriteLine($"Запущен процесс {notepad.ProcessName}. Его ID {notepad.Id}."); // выводим на экран информационное сообщение
                        }
                        catch (Exception ex) // обработка исключений
                        {
                            Console.WriteLine(ex.ToString()); // выводим на экран системное сообщение
                        }
                        break;
                    }
                    Console.WriteLine($"Введен неверный первый аргумент {args[1]}. Ознакомьтесь с файлом READ.ME"); // выводим на экран информационное сообщение
                    break;
                default:
                    Console.WriteLine("Превышено количество аргументов."); // выводим на экран информационное сообщение
                    break;
            }

            Console.WriteLine("\nВыполнение задания завершено"); // выводим на экран информационное сообщение
            return 0; // возвращаем в систему код успешного завершения программы
        }
    }
}
