﻿using System.Collections.Generic;
using System.IO;

using MySql.Data.MySqlClient;

namespace GOS.WorkWithDB
{
    /// <summary>
    /// Класс для работы с базой данных
    /// <br>Использует паттерн singleton</br>
    /// <para>
    /// <example>
    /// Пример работы:
    /// <code>
    ///     WorkWithDb.Instance.НужныйМетод;
    /// </code>
    /// </example>
    /// </para>
    /// </summary>
    public sealed class WorkWithDb
    {
        /// <summary>
        /// Файл настроек - указывать в первую очередь!
        /// <br>Да, прописывается прямо в классе</br>
        /// </summary>
        private const string SettingsFile = "../../WorkWithDB/HelpFiles/DatabaseSettings.dk";

        /// <summary>
        /// Для реализации паттерна singleton
        /// <br>Хранит приватный объект в единственном виде</br>
        /// </summary>
        private static WorkWithDb _instance;
        /// <summary>
        /// Для реализации паттерна singleton
        /// <br>Хранит публичный объект в единственном виде</br>
        /// </summary>
        public static WorkWithDb Instance => _instance ?? (_instance = new WorkWithDb(ReadSettingsFile()));
        /// <summary>
        /// Подключение к бд
        /// </summary>
        private readonly MySqlConnection _connection;


        /// <summary>
        /// Приватный конструктор для паттерна singleton
        /// </summary>
        /// <param name="settingsDict">Словарь с настройками, берется из метода <seealso cref="ReadSettingsFile"/></param>
        private WorkWithDb(IReadOnlyDictionary<string, string> settingsDict)
        {
            _connection = new MySqlConnection($"server={settingsDict["host"]};userid={settingsDict["username"]};password={settingsDict["password"]};database={settingsDict["databaseName"]}");
            _connection.Open();
        }

        /// <summary>
        /// Деструктор - закрывает соединение с бд
        /// </summary>
        ~WorkWithDb()
        {
            Instance._connection.Close();
        }

        /// <summary>
        /// Чтение файла настроек
        /// <para>Для использования сначала укажите файл настроек <seealso cref="SettingsFile"/> прямо в классе</para>
        /// <br><c>Внимание</c> - пропишите файл с настройками. Инструкция есть в папке <c>HelpFiles</c></br>
        /// <br>Новый файл нужно будет положить рядом с файлом-инструкцией</br>
        /// </summary>
        /// <returns>Словарь вида параметр=значение</returns>
        private static Dictionary<string, string> ReadSettingsFile()
        {
            var answer = new Dictionary<string, string>();

            var text = File.ReadAllText(SettingsFile);
            foreach (var settingsLine in text.Split('\n'))
            {
                var settingsLineSplit = settingsLine.Split('=');
                answer[settingsLineSplit[0]] = settingsLineSplit[1];
            }

            return answer;
        }

        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Словарь с RoleID и Active пользователя или null</returns>
        public Dictionary<string, long> Login(string email, string password)
        {
            var answer = new Dictionary<string, long>();

            var query = $"SELECT RoleID, Active FROM Users WHERE Email = '{email}' AND Password = '{password}'";
            var command = new MySqlCommand(query, Instance._connection);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows == false)
                    answer = null;
                else
                    for (var i = 0; i < reader.FieldCount; ++i)
                        answer[reader.GetName(i)] = reader.GetInt64(i);
            }

            return answer;
        }
    }
}
