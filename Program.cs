using NM_Person; // Класс для работы с данными пользователя
using System;
using System.Configuration;

namespace AppScope
{
    class Program
    {
        /// <summary>
        /// метод запрашивает у пользователя все данные и возвращает объект класса Person
        /// </summary>
        /// <returns></returns>
        static Person AskFullData()
        {
            var person = new Person();
            Console.WriteLine("Введите свои данные: ");
            Console.Write("Фамилию: ");
            person.surname = Console.ReadLine();
            Console.Write("Имя: ");
            person.name = Console.ReadLine();
            Console.Write("Отчество: ");
            person.patronymic = Console.ReadLine();
            Console.Write("Возраст: ");
            person.age = Console.ReadLine();
            Console.Write("Профессию: ");
            person.profession = Console.ReadLine();
            return person;
        }

        /// <summary>
        /// метод считывает данные из файла конфигурации и возвращает объект класса Person
        /// </summary>
        /// <returns></returns>
        static Person GetFullData()
        {
            var person = new Person();
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                person.surname = appSettings["surname"] ?? "Not Found";
                person.name = appSettings["name"] ?? "Not Found";
                person.patronymic = appSettings["patronymic"] ?? "Not Found";
                person.age = appSettings["age"] ?? "Not Found";
                person.profession = appSettings["profession"] ?? "Not Found";
                return person;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }

        /// <summary>
        /// метод сохраняет данные пользователя в файле настроек
        /// </summary>
        /// <param name="_person"></param>
        static void SetFullData(Person _person)
        {
            AddUpdateAppSettings("surname", _person.surname);
            AddUpdateAppSettings("name", _person.name);
            AddUpdateAppSettings("patronymic", _person.patronymic);
            AddUpdateAppSettings("age", _person.age);
            AddUpdateAppSettings("profession", _person.profession);
        }

        /// <summary>
        /// основной метод
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(ReadSetting("ReleaseNotes"));
            Person person = GetFullData();
            Console.WriteLine("ФИО пользователя из файла настроек: " + person.FullName());
            Console.WriteLine("Полные данные пользователя из файла настроек: " + person.FullData());

            person = AskFullData();
            SetFullData(person);
            Console.WriteLine();
            Console.WriteLine("Введены ФИО: " + person.FullName());
            Console.WriteLine("Введены полные данные: " + person.FullData());
        }
        /// <summary>
        /// метод получает данные из файла настроек по имени параметра
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                return "Error reading app settings";
            }
        }
        /// <summary>
        /// метод сохраняет данные параметра в файл настроек
        /// </summary>
        /// <param name="key"> название параметра </param>
        /// <param name="value"> значение параметра </param>
        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    } 
}
