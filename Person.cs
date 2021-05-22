using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NM_Person
{
    public class Person
    {
        public string name = string.Empty; // храним имя
        public string patronymic = string.Empty; // храним отчество
        public string surname = string.Empty; // храним фамилию
        public string age = string.Empty; // храним возраст
        public string profession = string.Empty; // храним род деятельности

        /// <summary>
        /// метод возвращает ФИО
        /// </summary>
        /// <returns></returns>
        public string FullName()
        {
            var _n = name.Replace(" ", ""); // удалим возможные пробелы в имени
            var _p = patronymic.Replace(" ", ""); // удалим возможные пробелы в отчестве
            var _s = surname.Replace(" ", ""); // удалим возможные пробелы в фамилии

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo; // обратим внимание на региональные различия

            return ti.ToTitleCase(_n + " " + _p + " " + _s); // выведем строку в которой все слова начинаются с заглавных символов
        }
        /// <summary>
        /// метод возвращает все данные о пользователе в одной строке
        /// </summary>
        public string FullData()
        {
            var _a = age.Replace(" ", ""); // удалим возможные пробелы в возрасте
            var _p = profession;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo; // обратим внимание на региональные различия

            return ti.ToTitleCase("ФИО " + FullName() + " возраст: " + _a + " профессия: ") + _p; // выведем строку в которой все слова начинаются с заглавных символов
        }
    }
}
