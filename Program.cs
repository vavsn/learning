using System;
using System.IO;
using System.Collections.Generic;

namespace Lesson5
{
    class Program
    {

        static void Main(string[] args)
        {
            HomeWork();
            TaskStar1();

        }

        // основное домашнее задание
        static void HomeWork()
        {
            // имя файла для работы
            string _filename = "startup.txt";
            // выводим приглашающую информацию на экран
            Console.WriteLine("Введите текст:");
            // получаем введённый пользователем текст
            var str = Console.ReadLine();
            // сохраним введённый текст в файл
            File.WriteAllText(_filename, Environment.NewLine + str);
            // выводим информационное сообщение на экран
            Console.WriteLine($"Введённая Вами строка записана в файл {_filename}. Дополняю данными о текущем времени.");
            // вносим дополнительные данные о времени в файл
            File.AppendAllLines(_filename, new[] { Environment.NewLine, "Время записи информации в файл ", DateTime.Now.ToString("HH:mm:ss") });
            // пропустим строку на экране
            Console.WriteLine();
            // выводим информационное сообщение на экран
            Console.WriteLine($"Вывожу содержимое файла {_filename}:");
            // прочитаем данные из файла
            string[] fileLines = File.ReadAllLines(_filename);
            // в цикле выводим содержимое файла на экран
            foreach (var rl in fileLines)
                Console.WriteLine(rl); // str2

            Console.WriteLine();
            // выводим приглашающую информацию на экран
            Console.WriteLine("Введите произвольное количество чисел разделённых пробелами:");

            // получаем введённый пользователем текст
            str = Console.ReadLine();
            // разделим строку на подстроки, которые запишем в массив
            string[] num = str.Split(" ");
            // создадим массив байт
            byte[] arr_num = new byte[num.Length];
            // преобразуем строки в байты и сохраним их в массив
            int i = 0;
            foreach (var n in num)
                arr_num[i++] = byte.Parse(n);
            // зададим название файла
            _filename = "startup.bin";
            // выведём информационное сообщение
            Console.WriteLine($"Сохраняю введённую информацию в файл {_filename}");
            // запишем массив байт в файл
            File.WriteAllBytes(_filename, arr_num);
            // выведём информационное сообщение
            Console.WriteLine($"Вывожу содержимое файла {_filename}");
            // считаем данные из файла
            byte[] fromFile = File.ReadAllBytes(_filename);
            // выведем содержимое файла в окно
            foreach (var ff in fromFile)
                Console.Write(ff.ToString() + " ");

            Console.WriteLine();

            Console.WriteLine("Надеюсь, информация совпадает :)");

        }

        // решение задачи 4*
        static void TaskStar1()
        {
            // получим информацию о текущей папке, в которой запускается программа
            string _curpath = Directory.GetCurrentDirectory();
            // задаем имя файла для вывода информации с помощью рекурсивного метода
            string _filename = _curpath + @"\directories_recursion.info";
            // зададим название папки для работы программы
            string testPath = @"D:\Обучение c#";
            // выведём информационное сообщение
            Console.WriteLine($"Получение информации о содержимом папки \"{testPath}\"");
            Console.WriteLine($"Применяем рекурсивный метод.");
            // получим информацию о директории в специализированном виде
            DirectoryInfo _testpath = new System.IO.DirectoryInfo(testPath);
            // проверим наличие директории поиска
            bool exists = Directory.Exists(testPath);
            // подготовим информационное сообщение в файл для случая, когда директория найдена, и для случая, когда директория отсутствует
            string notes = exists ? $"Директория \"{testPath}\" существует. Записываю данные:" : $"Директория \"{testPath}\" НЕ существует. Записывать нечего.";
            // запишем информационное сообщение в файл
            File.WriteAllText(_filename, notes + Environment.NewLine);
            // если директория отсутствует - завершим работу программы, выведем соответствующее сообщение на экран
            if (!exists)
            {
                Console.WriteLine(notes);
                return;
            }

            // вызовём рекурсиную функцию
            SearchFileInfo(_testpath, _filename);

            // выведём информационное сообщение
            Console.WriteLine($"Завершено. Полученная информация записана в файл \"{_filename}\"");
            Console.WriteLine($"Применяем метод прямого перебора.");

            // задаем имя файла для вывода информации без помощи рекурсивного метода
            _filename = _curpath + @"\directories_direct.info";

            // запишем информационное сообщение в файл
            File.WriteAllText(_filename, notes + Environment.NewLine);
            // вызовем функцию для формирования списка файлов / директорий в директивном порядке
            WalkingDirectoryDirect(_testpath.FullName, _filename);

            // выведём информационное сообщение
            Console.WriteLine($"Завершено. Полученная информация записана в файл \"{_filename}\"");

        }

        static void SearchFileInfo(DirectoryInfo path, string _filename)
        {
            // переменная для хранения информации о файлах
            FileInfo[] files = null;
            // переменная для хранения информации о директориях
            DirectoryInfo[] subDirs = null;

            // защищаемся от возможных исключительных ситуаций
            try
            {
                // получим информацию о файлах в директории / поддиректории
                files = path.GetFiles("*.*");
            }
            // обработка исключений
            catch (UnauthorizedAccessException e)
            {
                // запишем информационное сообщение в файл
                File.AppendAllLines(_filename, new[] { e.Message });
            }

            // в цикле запишем в файл информацию о каждом найденном файле 
            foreach (FileInfo fi in files)
            {
                File.AppendAllLines(_filename, new[] { fi.FullName });
            }

            // защищаемся от возможных исключительных ситуаций
            try
            {
                // получим информацию по поддиректориям.
                subDirs = path.GetDirectories();
            }
            // обработка исключений
            catch (UnauthorizedAccessException e)
            {
                // запишем информационное сообщение в файл
                File.AppendAllLines(_filename, new[] { e.Message });
            }

            foreach (DirectoryInfo dirInfo in subDirs)
            {
                // в цикле запишем в файл информацию о каждой найденной поддиректории
                File.AppendAllLines(_filename, new[] { dirInfo.FullName });
                // вызываем рекурсивно для каждой поддиректории
                SearchFileInfo(dirInfo, _filename);
            }
        }


        public static void WalkingDirectoryDirect(string path, string _filename)
        {
            // локальная переменная для формирования списка поддиректорий
            var curDirFile = path;

            // стэк для хранения информации о поддиректориях
            Stack<string> folders = new Stack<string>(20);

            // получаем информацию о поддиректориях основной директории 
            folders.Push(path);

            // работаем с каждой поддиректорией
            while (folders.Count > 0)
            {
                // получаем наименование верхней запии в стэке
                string currentDir = folders.Pop();
                // создаем пустой массив для хранения информации о поддиректориях
                string[] subDirs;
                // защищаемся от возможных исключительных ситуаций
                try
                {
                    // получаем информацию о поддиректориях текущей рабочей директории
                    subDirs = Directory.GetDirectories(currentDir);
                }
                // обработка исключения отсутствия доступа к поддиректории
                catch (UnauthorizedAccessException e)
                {
                    // запишем информационное сообщение в файл
                    File.AppendAllLines(_filename, new[] { e.Message });
                    // переходим к следующему элементу массива
                    continue;
                }
                // обработка исключения об отсутствии поддиректории
                catch (DirectoryNotFoundException e)
                {
                    // запишем информационное сообщение в файл
                    File.AppendAllLines(_filename, new[] { e.Message });
                    // переходим к следующему элементу массива
                    continue;
                }
                // создаем массив для хранения информации о файлах в поддиректории
                string[] files = null;
                // защищаемся от возможных исключительных ситуаций
                try
                {
                    // получаем информацию о файлах в текущей поддиректории
                    files = Directory.GetFiles(currentDir);
                }

                // обработка исключения отсутствия доступа к файлу
                catch (UnauthorizedAccessException e)
                {
                    // запишем информационное сообщение в файл
                    File.AppendAllLines(_filename, new[] { e.Message });
                    // переходим к следующему элементу массива
                    continue;
                }
                // обработка исключения об отсутствии файла
                catch (DirectoryNotFoundException e)
                {
                    // запишем информационное сообщение в файл
                    File.AppendAllLines(_filename, new[] { e.Message });
                    // переходим к следующему элементу массива
                    continue;
                }
                // получаем информацию о файлах
                foreach (string file in files)
                {
                    // защищаемся от возможных исключительных ситуаций
                    try
                    {
                        // создаем переменную, в которую система складывает информацию о файле
                        FileInfo fi = new FileInfo(file);
                        // сравниваем сохраненное имя поддиректории с поддиректорией, в которой хранится файл
                        if (curDirFile != fi.Directory.FullName)
                        {
                            // запишем в файл информацию о поддиректории, в которой хранится файл
                            File.AppendAllLines(_filename, new[] { fi.Directory.FullName });
                            // если имена не совпадают, сохраняем новое имя поддиректории
                            curDirFile = fi.Directory.FullName;
                        }
                        // запишем информационное сообщение в файл
                        File.AppendAllLines(_filename, new[] { fi.FullName });
                    }
                    // обработка исключения об отсутствии файла
                    catch (FileNotFoundException e)
                    {
                        // запишем информационное сообщение в файл
                        File.AppendAllLines(_filename, new[] { e.Message });
                        // переходим к следующему элементу массива
                        continue;
                    }
                }

                // сбрасываем информацию о поддиректориях на диск
                foreach (string str in subDirs)
                    folders.Push(str);
            }
        }
    }
}
