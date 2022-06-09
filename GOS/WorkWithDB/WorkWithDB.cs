using System.Collections.Generic;
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

            var query = $"SELECT RoleID, Active FROM users WHERE Email = '{email}' AND Password = '{password}'";
            var command = new MySqlCommand(query, Instance._connection);

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows == false)
                    return null;

                reader.Read();
                for (var i = 0; i < reader.FieldCount; ++i)
                    answer[reader.GetName(i)] = reader.GetInt64(i);
            }

            return answer;
        }

        /// <summary>
        /// Создает необходимы для администратора список пользователей
        /// </summary>
        /// <param name="officeName">Офис, в котором нужно найти пользователей</param>
        /// <returns>Список с пользователями или null</returns>
        public List<Dictionary<string, string>> GetOfficeUsersForAdmin(string officeName)
        {
            var answer = new List<Dictionary<string, string>>();

            var query = "SELECT " +
                        "users.FirstName AS Name, users.LastName AS LastName, " +
                        "year(current_timestamp) - year(users.Birthdate) - (right(current_timestamp, 5) < right(users.Birthdate, 5)) AS Age, " +
                        "roles.Title AS UserRole, users.Email AS EmailAddress, offices.Title AS Office, users.Active AS Active " +
                        "FROM users " +
                        "LEFT JOIN roles ON users.RoleID = roles.ID " +
                        "LEFT JOIN offices ON users.OfficeID = offices.ID";
            if (officeName != "All offices")
                query += $" WHERE offices.Title = '{officeName}'";
            var command = new MySqlCommand(query, Instance._connection);

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows == false)
                    return null;

                while (reader.Read())
                {
                    answer.Add(new Dictionary<string, string>());
                    for (var i = 0; i < reader.FieldCount; ++i)
                        answer[answer.Count - 1][reader.GetName(i)] = reader.GetString(i);
                }
            }

            return answer;
        }

        /// <summary>
        /// Создает список всех офисов для администратора
        /// </summary>
        /// <returns>Список с названиями всех офисов</returns>
        public List<string> GetAllOfficesForAdmin()
        {
            var answer = new List<string>();

            const string query = "SELECT Title FROM offices";
            var command = new MySqlCommand(query, Instance._connection);

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows == false)
                    return null;

                while (reader.Read())
                    answer.Add(reader.GetString(0));
            }

            return answer;
        }

        /// <summary>
        /// Обновляет информацию о том, активирован ли пользователь или нет
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        public void UpdateActiveForUser(string email)
        {
            var query = $"UPDATE users SET Active = IF(Active, false, true) WHERE Email = '{email}'";
            var command = new MySqlCommand(query, Instance._connection);
            command.ExecuteNonQuery();
        }

        public void InsertUserIntoDb(string email, string firstName, string lastName, string officeName,
            string birthdate, string password)
        {
            var query = "INSERT INTO users (RoleID, Email, Password, FirstName, LastName, OfficeID, Birthdate, Active) " +
                        $"SELECT 1, '{email}', '{password}', '{firstName}', '{lastName}', offices.ID, '{birthdate}', 1 " +
                        $"FROM offices WHERE offices.Title = '{officeName}'";
            var command = new MySqlCommand(query, Instance._connection);
            command.ExecuteNonQuery();
        }
    }
}
