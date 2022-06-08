using System.Collections.Generic;
using System.IO;

using MySql.Data.MySqlClient;

namespace GOS.WorkWithDB
{
    /// <summary>
    /// Класс работы с базой данных.
    /// Использует паттерн singleton.
    /// </summary>
    public sealed class WorkWithDb
    {
        /// <summary>
        /// Файл настроек - указывать в первую очередь!
        /// </summary>
        private const string SettingsFile = "../../WorkWithDB/HelpFiles/DatabaseSettings.dk";


        /// <summary>
        /// Для реализации паттерна singleton
        /// </summary>
        private static WorkWithDb _instance;

        /// <summary>
        /// Для реализации паттерна singleton
        /// </summary>
        public static WorkWithDb Instance => _instance ?? (_instance = new WorkWithDb(ReadSettingsFile()));

        /// <summary>
        /// Подключение к бд
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Приватный конструктор для паттерна singleton.
        /// Для использования сначала укажите файл настроек.
        /// </summary>
        /// <param name="settingsDict">Словарь с настройками, берется из метода <c>ReadSettingsFile</c></param>
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
        /// Чтение файла настроек.
        /// Обязательно укажите настройки в нужном поле!
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

        public Dictionary<string, long> Login(string email, string password)
        {
            var answer = new Dictionary<string, long>();

            var query = $"SELECT RoleID FROM Users WHERE Email = '{email}' AND Password = '{password}'";
            var command = new MySqlCommand(query, Instance._connection);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                answer[reader.GetName(0)] = reader.GetInt64(0);
            }

            return answer;
        }
    }
}
